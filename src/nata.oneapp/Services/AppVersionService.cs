using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace nata.Services
{
    public interface IAppVersionService
    {
        string Version { get; }
    }

    public class AppVersionService : IAppVersionService
    {
        public string Version =>
            Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion;
    }

}
