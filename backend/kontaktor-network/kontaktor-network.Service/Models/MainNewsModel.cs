using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KONTAKTOR.DA.Models;
using kontaktor_network.DA.Models;

namespace KONTAKTOR.Service.Models
{
    public class MainNewsModel : MainNews
    {
        /// <summary>
        /// Картинка к новости
        /// </summary>
        public BinaryContent Image { get; set; }
    }
}
