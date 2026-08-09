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
        ISqlDatabaseService SqlService;

        // Properties 
        public List<IApp> AllApps { get; set; } = new();
        
        // CTR 
        public AppService()
        {
            InitApps();
        }
        public AppService(IWebHostEnvironment env, ISqlDatabaseService sql)
        {
            Environment = env;
            SqlService = sql;

            InitApps();
        }

        private async void InitApps()
        {
            AppBuilder builder = new AppBuilder();

            AllApps.Clear();

            AllApps.Add(new AppFarm());
            AllApps.Add(new AppTask());

            IQueryParameter qp = new QueryParameter();
            qp.ItemType = "App";
            qp.MasterGUID = SqlService.MasterGUID;

            IQueryResult result = await SqlService.GetItems(qp);
            List<IDTO> apps = result.Items;

            foreach (IDTO app in apps)
            {
                IApp template = new App_Template(app.Title);
                AllApps.Add(template);
            }

        }
    }
}