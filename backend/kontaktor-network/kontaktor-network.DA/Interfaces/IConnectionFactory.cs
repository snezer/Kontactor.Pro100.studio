using System.Data;

namespace KONTAKTOR.Notifications.DA.Interfaces
{
    public interface IConnectionFactory
    {
        /// <summary>
        /// Get default connection
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns></returns>
        IDbConnection GetConnection(IDbTransaction transaction = null);

        /// <summary>
        /// Get specific connection
        /// </summary>
        /// <param name="name"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        IDbConnection GetConnection(string name, IDbTransaction transaction = null);
    }

    // ??
    public class NonDisposableConnectionProxy : IDbConnection
    {
        private IDbConnection _connection;

        public NonDisposableConnectionProxy(IDbConnection connection)
        {
            _connection = connection;
        }

        public void Dispose()
        {
            _connection = null;
        }

        public IDbTransaction BeginTransaction()
        {
            return _connection.BeginTransaction();
        }

        public IDbTransaction BeginTransaction(IsolationLevel il)
        {
            return _connection.BeginTransaction(il);
        }

        public void Close()
        {
            _connection.Close();
        }

        public void ChangeDatabase(string databaseName)
        {
            _connection.ChangeDatabase(databaseName);
        }

        public IDbCommand CreateCommand()
        {
            return _connection.CreateCommand();
        }

        public void Open()
        {
            _connection.Open();
        }

        public string ConnectionString
        {
            get { return _connection.ConnectionString;}
            set => _connection.ConnectionString = value;
        }

        public int ConnectionTimeout => _connection.ConnectionTimeout;
        public string Database => _connection.Database;
        public ConnectionState State => _connection.State;
    }
}
