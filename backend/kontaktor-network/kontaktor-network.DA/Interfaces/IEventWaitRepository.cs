using System.Data;
using System.Threading.Tasks;

namespace KONTAKTOR.Notifications.DA.Interfaces
{
    public interface IEventWaitRepository
    {
        Task<long[]> SignalAsync(string ticket, string originName, object parameters = null, IDbTransaction transaction = null);
        Task<long[]> SignalWithJsonAsync(string ticket, string originName, string json, string type, IDbTransaction transaction = null);
        Task SignalAsync(long id, string originName, IDbTransaction transaction = null);
        Task<bool> CheckAllChildrenSignaledAsync(long id, string originName, IDbTransaction transaction = null);
        Task CheckParentAsync(long id, string originName, IDbTransaction transaction = null);
        Task<long?> GetParentIdAsync(long id, string originName, IDbTransaction transaction = null);
        Task<bool> IsSignaledAsync(long id, string originName, IDbTransaction transaction = null);
    }
}
