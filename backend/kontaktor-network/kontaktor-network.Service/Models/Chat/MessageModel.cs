using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KONTAKTOR.Service.Models.Chat
{
    public class MessageModel
    {
        public string ChatId { get; set; }
        public string Text { get; set; }
        public byte[] Attachment { get; set; }
        public string AttachmentName { get; set; }
        public string AttachmentType { get; set; }
    }
}
