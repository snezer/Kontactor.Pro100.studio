using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Babel.Api.Base;
using Babel.Api.Graph;
using Babel.Api.Services;
using Babel.Db.Base;
using Babel.Db.Models;
using Babel.Db.Models.Entities;
using Babel.Db.Models.Rooms;
using Babel.Db.Services;
using Microsoft.AspNetCore.Mvc;

namespace Babel.Api.Controllers
{
    [ApiController]
    [Route("path")]
    public class PathController: ControllerBase
    {
        private readonly RoomService _roomService;
        private readonly EntityService _entityService;
        private readonly NgonbLibraryService _ngonbLibraryService;

        public PathController(RoomService roomService, EntityService entityService,
            NgonbLibraryService ngonbLibraryService)
        {
            _roomService = roomService;
            _entityService = entityService;
            _ngonbLibraryService = ngonbLibraryService;
        }

        /// <summary>
        /// Получить название библиотечного фонда по идентификатору книги
        /// </summary>
        [HttpGet]
        [Route("findbook/{bookId}")]
        public async Task<IActionResult> GetFundForBook(string bookId)
        {
            try
            {
                var result = await _ngonbLibraryService.SearchById(bookId);
                return JsonResponse.New(result);
            }
            catch (Exception e)
            {
                return BadRequest("Что-то пошло не так при попытке запросить расположение книги");
            }
        }

        /// <summary>
        /// Проложить маршрут до книги
        /// </summary>
        /// <param name="sourceRoomName"></param>
        /// <param name="bookId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("pathtobook")]
        public async Task<IActionResult> GetPathToBook(string sourceRoomName, string bookId)
        {
            var fundName = await _ngonbLibraryService.SearchById(bookId);
            if (string.IsNullOrEmpty(fundName))
                return NotFound("Не удалось найти зал для книги " + bookId);

            var sourceRoom = await _roomService.Get(sourceRoomName);
            var targetRoom = await _roomService.GetRoomByName(fundName);

            if (sourceRoom == null)
                return NotFound("Исходная комната не найдена");
            if (targetRoom == null)
                return NotFound("Целевая комната не найдена");

            return await PathToRoom(sourceRoom, targetRoom);
        }

        /// <summary>
        /// Получить путь из комнаты в комнату
        /// </summary>
        /// <param name="sourceRoomName"></param>
        /// <param name="targetRoomName"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetPathToRoom(string sourceRoomName, string targetRoomName)
        {
            if (string.IsNullOrEmpty(sourceRoomName) || string.IsNullOrEmpty(targetRoomName))
                return NotFound("Идентификаторы исходной и целевой комнаты не указаны");

            var sourceRoom = await _roomService.GetRoomByName(sourceRoomName);
            var targetRoom = await _roomService.GetRoomByName(targetRoomName);

            if (sourceRoom == null)
                sourceRoom = await _roomService.Get(sourceRoomName);
            if (targetRoom == null)
                targetRoom = await _roomService.Get(targetRoomName);

            if (sourceRoom == null)
                return NotFound("Исходная комната не найдена");
            if (targetRoom == null)
                return NotFound("Целевая комната не найдена");

            return await PathToRoom(sourceRoom, targetRoom);
        }

        private async Task<IActionResult> PathToRoom(BaseRoom sourceRoom, BaseRoom targetRoom)
        {
            var target = (BasePathable) targetRoom;

            var rooms = (await _roomService.Get()).Where(x => x.Type == "room" && x.Level == sourceRoom.Level);
            var nonPassable = (await _roomService.Get()).Where(x => x.Type != "room" && x.Level == sourceRoom.Level).ToList();
            var doors = (await _entityService.GetEntitiesByType("door")).Where(x => x.LevelId == sourceRoom.Level).ToList();
            var stairs = (await _entityService.GetEntitiesByType("stair")).Where(x => x.LevelId == sourceRoom.Level).ToList();
            var elevators = (await _entityService.GetEntitiesByType("elevator")).Where(x => x.LevelId == sourceRoom.Level).ToList();

            bool isAnotherLevel = false;
            if (targetRoom.Level != sourceRoom.Level)
            {
                isAnotherLevel = true;
                if (stairs.Any())
                {}
                    //target = stairs.First();
                else if (elevators.Any())
                {}
                   // target = elevators.First();
                else
                    return NotFound(
                        "Невозможно проложить путь: комнаты находятся на разных этажах, но нет ни лестниц ни лифта");
            }

            // строим граф соединений всех комнат
            var graph = new Graph<BasePathable>();
            foreach (var baseRoom in rooms) // для начала добавим все комнаты
            {
                graph.AddVertex(baseRoom);
            }

            foreach (var door in doors) // потом добавим все двери
            {
                graph.AddVertex(door);
                foreach (var room in rooms) // и посмотрим, если дверь пересекается с комнатой, то добавим грань
                {
                    bool doesIntersects = DoesIntersects(door, room);
                    if (doesIntersects)
                    {
                        graph.AddEdge(new Tuple<BasePathable, BasePathable>(door, room));
                        graph.AddEdge(new Tuple<BasePathable, BasePathable>(room, door));
                    }
                }
            }

            if (isAnotherLevel)
            {
                foreach (var door in stairs) 
                {
                    foreach (var room in rooms)
                    {
                        bool doesIntersects = DoesIntersects(door, room);
                        if (doesIntersects)
                        {
                            target = room;
                        }
                    }
                }

                foreach (var door in elevators) 
                {
                    foreach (var room in rooms)
                    {
                        bool doesIntersects = DoesIntersects(door, room);
                        if (doesIntersects)
                        {
                            target = room;
                        }
                    }
                }
            }

            var bfsAlgo = new GraphBfsAlgorithm();
            var shortestPathFunc = bfsAlgo.ShortestPathFunction(graph, rooms.First(x => x.Id == sourceRoom.Id));
            try
            {
                // сначала графами строим путь
                var shortestPath = shortestPathFunc(rooms.First(x => x.Id == target.Id)).ToList();

               /* string result = "";

                result = result.Trim();*/

                 /*var result = string.Join(" ",
                     shortestPath.Select(x => Math.Floor(x.Position.X + (x.Size == null ? 0 : x.Size.Width / 2)) + ","
                         + Math.Floor(x.Position.Y + (x.Size == null ? 0 : x.Size.Height / 2))));*/

                string result = "";
                var previous = shortestPath[0].Position + shortestPath[0].Size / 2;
                previous.X = Math.Floor(previous.X);
                previous.Y = Math.Floor(previous.Y);
                result += previous.X + "," + previous.Y + " ";

                for (int i = 1; i < shortestPath.Count() - 1; i++)
                {
                    var current = shortestPath[i];
                    if (current.GetType() == typeof(Entity))
                    {
                        result += current.Position.X + "," + current.Position.Y + " ";
                        continue;
                    }
                    continue;
                }

                previous = shortestPath.Last().Position + shortestPath.Last().Size / 2;
                previous.X = Math.Floor(previous.X);
                previous.Y = Math.Floor(previous.Y);
                result += previous.X + "," + previous.Y;


                return JsonResponse.New(result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return BadRequest("Не удалось построить маршрут");
            }

            return BadRequest("Не удалось построить маршрут");
        }

        private async Task<List<Vector>> PathThroughRoom(BasePathable room, Vector from, Vector to, List<BaseRoom> notPassableList)
        {
            var grid = new List<List<Node>>();
            for (int i = 0; i < room.Size.Height; i++)
            {
                var row = new List<Node>();
                grid.Add(row);
                for (int j = 0; j < room.Size.Width; j++)
                {
                    bool isNotPassable = notPassableList.Any(x => x.Position.X < room.Position.X + j
                    && x.Position.X + x.Size.Width > room.Position.X + j
                    && x.Position.Y < room.Position.Y + i
                    && x.Position.Y + x.Size.Height > room.Position.Y + i);
                    var node = new Node(new Vector(j, i), !isNotPassable );
                    row.Add(node);
                }
            }

            var astar = new Astar(grid);
            var path = astar.FindPath(from - room.Position, to - room.Position);
            return path.Select(x => new Vector(x.Position.X, x.Position.Y)).ToList();
        }

        private bool DoesIntersects(Entity door, BaseRoom room)
        {
            int doorRadius = 10;
            var distX = Math.Abs(door.Position.X - room.Position.X - room.Size.Width / 2);
            var distY = Math.Abs(door.Position.Y - room.Position.Y - room.Size.Height / 2);

            if (distX > (room.Size.Width / 2 + doorRadius)) { return false; }
            if (distY > (room.Size.Height / 2 + doorRadius)) { return false; }

            if (distX <= (room.Size.Width / 2)) { return true; }
            if (distY <= (room.Size.Height / 2)) { return true; }

            var dx = distX - room.Size.Width / 2;
            var dy = distY - room.Size.Height / 2;
            return (dx * dx + dy * dy <= (doorRadius * doorRadius));
        }
    }
}
