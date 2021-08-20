using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KONTAKTOR.Service.Models
{
    public class BookingValidationModel
    {
        /// <summary>
        /// Валидируемая заявка на помещение
        /// </summary>
        public string RentInfoId { get; set; }
        /// <summary>
        /// Пользователь от УК, провалидировавший заявку
        /// </summary>
        public string UserId { get; set; }
    }
}
