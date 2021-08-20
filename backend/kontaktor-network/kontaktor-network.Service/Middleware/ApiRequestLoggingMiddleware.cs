using System;
using System.IO;
using System.Threading.Tasks;
using KONTAKTOR.Notifications.DA.Data;
using netcoreservice.Service.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace netcoreservice.Service.Middleware
{
    public class ApiRequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IMediator _mediator;
        private readonly Microsoft.IO.RecyclableMemoryStreamManager _streamManager;

        public ApiRequestLoggingMiddleware(RequestDelegate next, IMediator mediator)
        {
            _next = next;
            _mediator = mediator;
            _streamManager = new Microsoft.IO.RecyclableMemoryStreamManager();
        }

        private async Task<ServiceLog> LogEntryCapture(HttpContext context)
        {
            var request = context.Request;
            context.Request.EnableBuffering();

            await using var requestStream = _streamManager.GetStream();
            await context.Request.Body.CopyToAsync(requestStream);

            var result = new ServiceLog
            {
                Route = $"{request.Method} {request.Path}",
                RequestBody = ReadStreamInChunks(requestStream),
                ClientIP = $"{request.HttpContext.Connection.RemoteIpAddress}:{request.HttpContext.Connection.RemotePort}",
                ClientID = null,
                StartDate = DateTime.Now
            };

            context.Request.Body.Position = 0;
            return result;
        }

        private static string ReadStreamInChunks(Stream stream)
        {
            const int readSize = 4096;
            stream.Seek(0, SeekOrigin.Begin);

            using var textWriter = new StringWriter();
            using var reader = new StreamReader(stream);

            var readChunk = new char[readSize];
            int readChunkLength;
            do
            {
                readChunkLength = reader.ReadBlock(readChunk, 0, readSize);
                textWriter.Write(readChunk, 0, readChunkLength);

            } while (readChunkLength > 0);

            return textWriter.ToString();
        }        

        public async Task Invoke(HttpContext context)
        {
            // Получаем исходный запрос
            var serviceLog = await LogEntryCapture(context);

            // Продолжаем действие pipeline, которое вновь продолжится здесь, когда придет ответ
            await this._next(context);

            // Сохраняем в БД полученный лог
            var command = new SaveLogCommand
            {
                Route = serviceLog.Route,
                RequestBody = serviceLog.RequestBody,
                ClientIP = serviceLog.ClientIP,
                ClientID = serviceLog.ClientID,
                StartDate = serviceLog.StartDate,
                EndDate = DateTime.Now
            };

            await _mediator.Send(command);
        }
    }
}
