using System.Data;
using System.Threading.Tasks;
using KONTAKTOR.Notifications.DA.Data;

namespace KONTAKTOR.Notifications.DA.Interfaces
{
    public interface IServiceLogRepository
    {
        Task<long> AddLogAsync(ServiceLog log, IDbTransaction transaction = null);
    }
}
