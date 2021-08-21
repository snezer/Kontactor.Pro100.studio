using System;
using System.Collections.Generic;
using System.Text;
using KONTAKTOR.DA.Models;

namespace KONTAKTOR.DA.Models
{
    /// <summary>
    /// Помещение
    /// </summary>
    public class Compartment : BaseModel
    {
        /// <summary>
        /// Название помещения
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Код помещения
        /// </summary>
        public string ShortNameOrCode { get; set; }
        /// <summary>
        /// Арендуется ли в принципе или это служебное помещение/общего пользования
        /// </summary>
        public bool IsForRent { get; set; }
        /// <summary>
        /// Разрешена долговременная аренда
        /// </summary>
        public bool IsForLongTermRent { get; set; }
        /// <summary>
        /// Площадь
        /// </summary>
        public decimal Area { get; set; }
        /// <summary>
        /// Ссылка на карту
        /// </summary>
        public string MapRoomId { get; set; }
        /// <summary>
        /// Максимальное количество размещаемых людей
        /// </summary>
        public int MaxPeopleNumber { get; set; }
        /// <summary>
        /// Этаж помещения
        /// </summary>
        public int Floor { get; set; }
    }
}
