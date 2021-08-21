using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KONTAKTOR.DA.Models;
using KONTAKTOR.DA.Mongo.Repository;
using KONTAKTOR.Service.Models.Chat;
using Microsoft.AspNetCore.Mvc;

namespace KONTAKTOR.Service.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class ChatController : ControllerBase
    {
        private ChatRepository _chats;
        private MessageRepository _messages;
        private BinaryContentRepository _binaries;

        public ChatController(ChatRepository chats, MessageRepository messages, BinaryContentRepository content)
        {
            _chats = chats;
            _messages = messages;
            _binaries = content;
        }

        [HttpPost]
        public async Task<IActionResult> Create(ChatStartModel chat)
        {
            var initId = chat.InitiatorId + string.Concat(chat.TargetsIds) + chat.Subject;
            var chatEntity = await _chats.GetByExtIdAsync(initId) ??
                       await _chats.CreateAsync(new Chat()
                       {
                           ExternalId = chat.InitiatorId + string.Concat(chat.TargetsIds.OrderBy(s=>s)) + chat.Subject,
                           Subject = chat.Subject,
                           Users = chat.TargetsIds.Concat(new []{chat.InitiatorId}).Distinct().ToArray()
                       });
            if (chat.FirstMessage != null)
            {
                chat.FirstMessage.ChatId = chatEntity.Id;
                await AddMessage(chat.FirstMessage);
            }

            return Ok(chatEntity);
        }

        /// <summary>
        /// Получаем все чаты, в которых участвует пользователь с userid
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        [HttpGet("{userid}")]
        public async Task<IActionResult> GetChatsForUser(string userid)
        {
            var chats = await _chats.GetForUser(userid);
            return Ok(chats);
        }

        /// <summary>
        /// Получаем все чаты в системе
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var chats = await _chats.GetAllAsync();
            return Ok(chats);
        }

        private async Task<Message> AddMessage(MessageModel messageModel)
        {
            string binaryId = null;
            if (messageModel.Attachment != null)
            {
                var binary = await _binaries.CreateAsync(new BinaryContent()
                {
                    Content = messageModel.Attachment,
                    ContentType = messageModel.AttachmentType ?? "application/octet-stream",
                    Filename = messageModel.AttachmentName
                });

                binaryId = binary.Id;
            }

            var result = await _messages.CreateAsync(new Message()
            {
                AttachmentId = binaryId,
                ChatId = messageModel.ChatId,
                SendDate = DateTime.Now,
                Text = messageModel.Text
            });

            return result;
        }

        [HttpPost]
        [Route("messages/create")]
        public async Task<IActionResult> CreateMessage(MessageModel model)
        {
            var result = await AddMessage(model);
            return Ok(result);
        }

        [HttpGet("messages/{chatId}")]
        public async Task<IActionResult> GetMessages(string chatId)
        {
            var result = await _messages.GetForChatAsync(chatId);
            return Ok(result.OrderBy(m=>m.SendDate));
        }
    }

  
}
