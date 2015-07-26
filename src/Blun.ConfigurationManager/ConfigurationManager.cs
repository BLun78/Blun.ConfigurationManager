using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Conf = System.Configuration.ConfigurationManager;

namespace Blun.ConfigurationManager
{
    public class ConfigurationManager
    {
        public Configuration Configuration { get; private set; }

        public AppSettingsSection AppSettings => Configuration.AppSettings;

        public ConnectionStringsSection ConnectionStrings => Configuration.ConnectionStrings;

        public ConfigurationManager([Optional]string exePath)
        {
            Configuration = exePath == null ? Conf.OpenMachineConfiguration() : Conf.OpenExeConfiguration(exePath);
        }

        public static string GetAssemblyPath(Type assemblyType)
        {
            return assemblyType.Assembly.Location;
        }
    }
}
