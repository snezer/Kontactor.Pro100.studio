using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using KONTAKTOR.DA.Models;

namespace KONTAKTOR.Service.Models.Chat
{
    public class ChatStartModel
    {
        public string Subject { get; set; }
        public string InitiatorId { get; set; }
        public string[] TargetsIds { get; set; }

        public MessageModel FirstMessage { get; set; }
    }
}
