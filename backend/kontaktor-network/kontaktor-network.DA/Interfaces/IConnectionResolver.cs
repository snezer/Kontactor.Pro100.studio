using System.Data;

namespace KONTAKTOR.Notifications.DA.Interfaces
{
    public interface IConnectionResolver
    {
        IDbConnection Resolve(string name);
    }
}
