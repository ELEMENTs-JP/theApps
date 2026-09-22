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
        public List<IDTO> AllPages { get; set; } = new();
        public List<IDTO> AllBoards { get; set; } = new();
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

        private IDTO _board = null;
        public IDTO Board
        {
            get { return _board; }
            set { _board = value; OnPropertyChanged(); }
        }


        private IDTO _item = null;
        public IDTO Item
        {
            get { return _item; }
            set { _item = value; OnPropertyChanged(); }
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

        public async void Init()
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
            AllApps.Add(new App_Search());
            AllApps.Add(new App_Security());
            AllApps.Add(new App_RUNer());

            // Individuall Apps 
            IQueryParameter qp = new QueryParameter();
            qp.MasterGUID = SqlService.MasterGUID;

            // Apps 
            qp.ItemType = "App";
            IQueryResult result = await SqlService.GetItems(qp);
            List<IDTO> apps = result.Items;
            foreach (IDTO app in apps)
            {
                IApp template = new App_Template(app, SqlService);
                AllApps.Add(template);
            }

            // Pages 
            qp.ItemType = "Page";
            result = await SqlService.GetItems(qp);
            AllPages = result.Items;

            // Boards 
            qp.ItemType = "Board";
            result = await SqlService.GetItems(qp);
            AllBoards = result.Items;
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

            this.Item = null;
            this.ItemType = null;
            this.Page = null;
            this.Board = null;
            await Task.CompletedTask;
        }
        public async Task SetItemType(IItemType it)
        {
            this.Page = null;
            this.Board = null;

            // ItemType 
            this.ItemType = it;

            await AppByItemType(it);
        }
        public async Task SetPage(IDTO page)
        {
            this.ItemType = null;
            this.Board = null;

            // Page 
            this.Page = page;

            await AppByPage(page);
        }
        public async Task SetBoard(IDTO board)
        {
            this.Page = null;
            this.ItemType = null;

            // Board 
            this.Board = board;
            await AppByBoard(board);
        }
        public async Task SetPage(Guid GUID)
        {
            this.Page = AllPages.Find(se => se.GUID == GUID);
            this.ItemType = null;
            await Task.CompletedTask;
        }
        public async Task<IApp?> AppByPage(IDTO? page)
        {
            if (page == null || AllApps == null || !AllApps.Any())
                return null;

            // 1.) App by ItemType 
            foreach (IApp _app in AllApps)
            {
                List<IDTO> _pages = await _app.GetPages();
                foreach (IDTO _page in _pages)
                {
                    if (page.GUID == _page.GUID)
                    {
                        App = _app;
                        return _app;
                    }
                }
            }

            return null;
        }
        public async Task<IApp?> AppByBoard(IDTO? board)
        {
            if (board == null || AllApps == null || !AllApps.Any())
                return null;

            // 1.) App by ItemType 
            foreach (IApp _app in AllApps)
            {
                List<IDTO> _boards = await _app.GetBoards();
                foreach (IDTO _board in _boards)
                {
                    if (board.GUID == _board.GUID)
                    {
                        App = _app;
                        return _app;
                    }
                }
            }

            return null;
        }
        public async Task<IApp?> AppByItemType(IItemType? it)
        {
            if (it == null || AllApps == null || !AllApps.Any())
                return null;

            // 1.) App by ItemType 
            foreach (IApp _app in AllApps)
            {
                List<IItemType> _its = await _app.GetItemTypes(AssociationTyp.Children);
                foreach (IItemType _it in _its)
                {
                    if (it.Name == _it.Name)
                    {
                        App = _app;
                        return _app;
                    }
                }
            }

            return null;
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