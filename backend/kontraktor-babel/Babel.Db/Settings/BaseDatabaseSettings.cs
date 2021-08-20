﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Babel.Db.Settings
{
    public class BaseDatabaseSettings : IBaseDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
