using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace theInfrastructure
{
    public class AppService : IAppService, INotifyPropertyChanged, IDisposable
    {
        // Fields 
        IWebHostEnvironment Environment;
        ISqlDatabaseService SqlService;
     

        // Properties 
        private IApp _app = null;
        public IApp App 
        { 
            get { return _app; } 
            set { _app = value; OnPropertyChanged(); } 
        } 
        public List<IApp> AllApps { get; set; } = new();
        
        // CTR 
        public AppService()
        {
            InitApps();

            this.PropertyChanged += AppService_PropertyChanged;
        }
        public AppService(IWebHostEnvironment env, ISqlDatabaseService sql)
        {
            Environment = env;
            SqlService = sql;

            InitApps();

            this.PropertyChanged += AppService_PropertyChanged;
        }

        private async void InitApps()
        {
            Factory builder = new Factory(SqlService);

            AllApps.Clear();

            // Default Apps 
            AllApps.Add(new App_Farm());
            AllApps.Add(new App_System());
            AllApps.Add(new App_Security());
            AllApps.Add(new App_Task());

            // Individuall Apps 
            IQueryParameter qp = new QueryParameter();
            qp.ItemType = "App";
            qp.MasterGUID = SqlService.MasterGUID;

            IQueryResult result = await SqlService.GetItems(qp);
            List<IDTO> apps = result.Items;

            foreach (IDTO app in apps)
            {
                IApp template = new App_Template(app, SqlService);
                AllApps.Add(template);
            }

        }
        public async Task SetApp(IApp app)
        {
            App = AllApps.Where(se => se.Name == app.Name).FirstOrDefault();
            await Task.CompletedTask;
        }

        // Property Changed 
        private void AppService_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "App")
            { 
            
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            try
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            }
            catch (Exception ex)
            {
                Debug.WriteLine("FAIL: Property Changed: " + ex.Message);
            }
        }

        // Dispose
        public void Dispose()
        {
            try
            {
                this.PropertyChanged -= AppService_PropertyChanged;
                GC.Collect();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("FAIL: Property Changed: " + ex.Message);
            }

        }
    }
}