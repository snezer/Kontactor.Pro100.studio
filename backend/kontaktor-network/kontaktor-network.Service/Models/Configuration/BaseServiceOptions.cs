using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KONTAKTOR.DA.Interfaces;

namespace KONTAKTOR.Network.Service.Models.Configuration
{
    public class BaseServiceOptions : IBaseServiceOptions
    {
        public string MainDBConnectionName { get; set; } = "mainDB";
        public bool IsConnectionsEncoded { get; set; } = false;
    }
}
