using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace KONTAKTOR.Service.Services.DocxTemplating
{
    public static class CurrentPathHelper
    {
        public static string GetPathToReports()
        {
            var codebase = Assembly.GetEntryAssembly().CodeBase;
            var url = new UriBuilder(codebase);
            var path = Uri.UnescapeDataString(url.Path);
            return Path.GetDirectoryName(path);
        }
    }
}
