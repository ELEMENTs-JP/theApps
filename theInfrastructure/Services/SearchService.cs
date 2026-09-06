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
    public class SearchService : ISearchService, INotifyPropertyChanged, IDisposable
    {
        // Fields 
        IWebHostEnvironment Environment;
        ISqlDatabaseService SqlService;

        public List<IDTO> Store { get; set; } = new();

        // CTR 
        public SearchService()
        {
            Init();

            this.PropertyChanged += AppService_PropertyChanged;
        }
        public SearchService(IWebHostEnvironment env, ISqlDatabaseService sql)
        {
            Environment = env;
            SqlService = sql;

            Init();

            this.PropertyChanged += AppService_PropertyChanged;
        }

        public async Task Init()
        {
            Store.Clear();
            
            // Individuall Apps 
            IQueryParameter qp = new QueryParameter();
            qp.MasterGUID = SqlService.MasterGUID;

            IQueryResult result = await SqlService.Search(qp);
            Store = result.Items;
        }
        public async Task<List<IDTO>> Search(string matchcode)
        {
            string searchInput = matchcode?.Trim() ?? string.Empty;

            if (string.IsNullOrEmpty(searchInput))
            {
                return new List<IDTO>();
            }

            return Store.Where(se =>
                !string.IsNullOrEmpty(se.Matchcode) &&
                (
                    se.Matchcode.Contains(searchInput, StringComparison.OrdinalIgnoreCase) ||
                    se.Matchcode.MatchesPropertySearch(searchInput)
                )
            )
            .OrderBy(se =>
                // 1. & 2. Vorschlag: StartsWith (Präfix) bevorzugen vor Contains (Infix)
                se.Matchcode.StartsWith(searchInput, StringComparison.OrdinalIgnoreCase) ? 0 : 1
            )
            .ThenBy(se =>
            // 3. Vorschlag: Position des Treffers (je kleiner der Index, desto weiter vorne)
            {
                int index = se.Matchcode.IndexOf(searchInput, StringComparison.OrdinalIgnoreCase);
                return index < 0 ? int.MaxValue : index;
            }
            )
            .ThenBy(se =>
                // 4. Vorschlag: String-Länge (kürzere Matchcodes sind relevanter/exakter)
                se.Matchcode.Length
            )
            .ThenBy(se =>
                // Reiner Alphabetischer Fallback
                se.Matchcode
            )
            .ToList();
        }
        public int CountByItemType(string itemtype)
        {
            int count = Store.Where(se => se.ItemType.ToLowerInvariant() == itemtype.ToLowerInvariant()).Count();
            return count;
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