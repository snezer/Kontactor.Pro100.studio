using System;

namespace KONTAKTOR.Notifications.DA.Data
{
    public class ServiceLog
    {
        public long Id { get; set; }
        public string Route { get; set; }
        public string RequestBody { get; set; }
        public string ClientIP { get; set; }
        public long? ClientID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
