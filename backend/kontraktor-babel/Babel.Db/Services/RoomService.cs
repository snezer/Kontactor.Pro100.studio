using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Babel.Db.Models.Rooms;
using Babel.Db.Settings;
using MongoDB.Driver;

namespace Babel.Db.Services
{
    public class RoomService: BaseMongoService<BaseRoom>
    {
        public RoomService(IBaseDatabaseSettings settings) : base(settings)
        {
        }

        public async Task<List<BaseRoom>> GetRoomsByLevel(string levelId)
        {
            var rooms = (await _collection.FindAsync(x => x.Level == levelId)).ToList();
            return rooms;
        }

        public async Task RemoveRoomsByLevel(string levelId)
        {
            await _collection.DeleteManyAsync(x => x.Level == levelId);
        }

        public async Task<BaseRoom> GetRoomByName(string roomName)
        {
            var room = (await _collection.FindAsync(x => x.Name.ToLower() == roomName.ToLower())).FirstOrDefault();
            return room;
        }
    }
}
