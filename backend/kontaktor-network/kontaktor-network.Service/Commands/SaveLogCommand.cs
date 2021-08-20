using System;
using KONTAKTOR.Notifications.DA.Data;
using MediatR;

namespace netcoreservice.Service.Commands
{
    public class SaveLogCommand : IRequest<ServiceLog>
    {
        public string Route { get; set; }
        public string RequestBody { get; set; }
        public string ClientIP { get; set; }
        public long? ClientID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
