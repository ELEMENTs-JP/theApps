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
    public class SecurityService : ISecurityService, INotifyPropertyChanged, IDisposable
    {
        // Fields 
        IWebHostEnvironment Environment;
        ISqlDatabaseService SqlService;

        public IDTO Principal { get; set; }

        // Properties 
        private IDTO _user = null;
        public IDTO User
        { 
            get { return _user; } 
            set { _user = value; OnPropertyChanged(); } 
        }

        public List<IDTO> AllUser { get; set; } = new();
        
        // CTR 
        public SecurityService()
        {
            Init();

            this.PropertyChanged += AppService_PropertyChanged;
        }
        public SecurityService(IWebHostEnvironment env, ISqlDatabaseService sql)
        {
            Environment = env;
            SqlService = sql;

            Init();

            this.PropertyChanged += AppService_PropertyChanged;
        }

        private async void Init()
        {
            // User 
            IQueryParameter qp = new QueryParameter();
            qp.MasterGUID = SqlService.MasterGUID;
            qp.ItemType = "User";
            IQueryResult result = await SqlService.GetItems(qp);

            AllUser.Clear();
            AllUser.AddRange(result.Items);
        }
        public async Task SetUser(Guid GUID)
        {
            User = AllUser.FirstOrDefault(x => x.GUID == GUID);

            if (User != null)
            {
                IQueryResult result = await SqlService.GetRelatedItems(User, "Principal");
                if (result.Items.Count() == 1)
                {
                    // Set Principal 
                    Principal = result.Items[0];
                }
            }

            await Task.CompletedTask;
        }
        public async Task RefreshUser()
        {
            Guid userGUID = User.GUID;

            Init();

            await SetUser(userGUID);
        }

        // Events 

        private void AppService_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "User")
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