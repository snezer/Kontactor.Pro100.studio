using System;
using System.Collections.Generic;
using System.Text;

namespace Babel.Db.Settings
{
    public interface IBaseDatabaseSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
