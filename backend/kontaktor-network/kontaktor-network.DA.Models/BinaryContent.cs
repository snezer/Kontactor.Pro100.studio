using System;
using System.Collections.Generic;
using System.Text;
using Dapper.Contrib.Extensions;
using KONTAKTOR.DA.Models;

namespace KONTAKTOR.DA.Models
{
    public class BinaryContent : BaseModel
    {
        public byte[] Content { get; set; }
        public string Filename { get; set;}
        public string ContentType { get; set; }
            
    }
}
