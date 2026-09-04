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

        public LayoutConfiguration Configuration { get; set; } = null;

        // Properties 
        private IApp _app = null;
        public IApp App 
        { 
            get { return _app; } 
            set { _app = value; OnPropertyChanged(); } 
        } 
        public List<IApp> AllApps { get; set; } = new();
        private IItemType _it = null;
        public IItemType ItemType
        {
            get { return _it; }
            set { _it = value; OnPropertyChanged(); }
        }

        private IDTO _page = null;
        public IDTO Page
        {
            get { return _page; }
            set { _page = value; OnPropertyChanged(); }
        }

        // CTR 
        public AppService()
        {
            Init();

            this.PropertyChanged += AppService_PropertyChanged;
        }
        public AppService(IWebHostEnvironment env, ISqlDatabaseService sql)
        {
            Environment = env;
            SqlService = sql;

            Init();

            this.PropertyChanged += AppService_PropertyChanged;
        }

        private async void Init()
        {
            if (Configuration == null)
            {
                Configuration = LayoutConfiguration.Load();
            }

            Factory builder = new Factory(SqlService);

            AllApps.Clear();

            // Default Apps 
            AllApps.Add(new App_Farm());
            AllApps.Add(new App_System());
            AllApps.Add(new App_Security());
            AllApps.Add(new App_RUNer());

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
        public async Task SetApp(IApp? _app)
        {
            if (_app == null)
            {
                App = null;
            }
            else
            { 
                App = AllApps.FirstOrDefault(se => se.Name == _app.Name);
            }

            this.ItemType = null;
            this.Page = null;
            await Task.CompletedTask;
        }
        public async Task SetItemType(IItemType it)
        {
            // ItemType 
            this.ItemType = it;

            this.Page = null;

            await AppByItemType(it);

            await Task.CompletedTask;
        }
        public async Task SetPage(IDTO page)
        {
            this.Page = page;
            this.ItemType = null;
            await Task.CompletedTask;
        }

        public async Task AppByItemType(IItemType it)
        {
            if (it == null || AllApps == null || !AllApps.Any())
                return;

            foreach (IApp _app in AllApps)
            {
                foreach (IItemType _it in _app.GetItemTypes(AssociationTyp.Children).Result)
                {
                    if (it.Name == _it.Name)
                    {
                        App = _app;
                        await Task.CompletedTask;
                        return;
                    }
                }
            }

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