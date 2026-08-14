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
    public class LocalizationService : ILocalizationService, INotifyPropertyChanged, IDisposable
    {
        // Fields 
        IWebHostEnvironment Environment;
        ISqlDatabaseService SqlService;

        public SystemConfiguration Configuration { get; set; } = null;

        // CTR 
        public LocalizationService()
        {
            Init();

            this.PropertyChanged += AppService_PropertyChanged;
        }
        public LocalizationService(IWebHostEnvironment env, ISqlDatabaseService sql)
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
                Configuration = SystemConfiguration.Load();
            }
        }

        public string GetLabel(string de, string en, string es)
        {
            if (Configuration.Sprache == "de")
                return de;

            if (Configuration.Sprache == "en")
                return en;

            if (Configuration.Sprache == "es")
                return es;

            return de;
        }

        // Property Changed 
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