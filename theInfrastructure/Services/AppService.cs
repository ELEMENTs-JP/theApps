using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using theInfrastructure;

namespace theInfrastructure
{
    public class AppService : IAppService
    {
        // Fields 
        IWebHostEnvironment Environment;

        // Properties 
        public List<IApp> AllApps { get; set; } = new();
        



        // CTR 
        public AppService()
        {
            InitApps();
        }
        public AppService(IWebHostEnvironment env)
        {
            Environment = env;
            InitApps();
        }

        private void InitApps()
        {
            AppBuilder builder = new AppBuilder();

            AllApps.Clear();

            AllApps.Add(builder.BuildApp("Task"));

            
        }
    }
}