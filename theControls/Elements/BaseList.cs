using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using theControls.Tables;
using theDatabase;
using theInfrastructure;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace theControls.Elements
{
    public class BaseList : ComponentBase, IAsyncDisposable
    {
        [Inject]public IMessagingBusService bus { get; set; } = default!;

        [Inject]public ISqlDatabaseService sql { get; set; } = default!;

        [Inject]public ISecurityService sec { get; set; } = default!;

        [Parameter]
        public AssociationTyp Association { get; set; } = AssociationTyp.NULL;

        [Parameter]
        public IItemType ItemType { get; set; }

        [Parameter]
        public IDTO RelatedItem { get; set; }

        [Parameter]
        public IQueryContext Context { get; set; }

        [Parameter]
        public bool HasItems { get; set; } = false;

        [Parameter]
        public EventCallback<string> OnSearch { get; set; }

        public int InternalSearchCount { get; set; } = 0;

        // Events 
        protected override async Task OnInitializedAsync()
        {
            bus.OnMessage += OnMessageIncome;

       
        }
        protected override async Task OnParametersSetAsync()
        {
            await base.OnParametersSetAsync();

            await Search();
        }

        // Methoden 
        private async Task Search()
        {
            if (Context == null && ItemType != null)
            {
                Context = new QueryContext(sql, sec, ItemType);
            }

            if (ItemType != null && Context != null)
            {
                // Matchcode aus der abgeleiteten Klasse übernehmen (falls vorhanden)
                //if (this is DataTable dataTable)
                //{
                //    if (Context.Filter == null)
                //    {
                //        IFilterParameter filter = new FilterParameter();
                //        Context.Filter = filter;
                //    }

                //    // Matchcode 
                //    Context.Filter.Matchcode = dataTable.Matchcode;
                //}

                if (RelatedItem != null)
                {
                    Context.RelatedItem = RelatedItem;
                    Context.Items = await Context.RelatedItems(ItemType.Name, Association);

                    InternalSearchCount++;

                    await ValidateProgressVisibility();
                }
                else
                {
                    // Führt Search() exakt einmal aus
                    await Context.Search();

                    HasItems = (Context.Items.Count >= 1);
                }
            }
        }

        // Callback 
        private async void OnMessageIncome(AppMessage msg)
        {
            if (msg.Action == BusAction.Refresh)
            {
                await Search();

                await InvokeAsync(this.StateHasChanged);

                // Logik: Prüfen ob Wechsel erlaubt...
                bool canChange = true;

                if (canChange)
                {
                    msg.Reply?.Invoke(new AppMessage(msg.Id, BusAction.Refresh));
                }
            }
        }

        public bool ShowProgressBar { get; set; } = false;
        public string Progress { get; set; } = "0";

        private async Task ValidateProgressVisibility()
        {
            ShowProgressBar = false;

            if (Context?.Items == null || Context.Items.Count == 0)
                return;

            string col = await Context.ItemType.GetRelevantPropertyName(RelevantPropertyType.Progress);
            if (string.IsNullOrEmpty(col))
                return;

            ShowProgressBar = true;
            int count = Context.Items.Count;
            int sum = 0;

            // Lokale Referenz oder Aufruf einmalig abrufen
            var values = Context.ValuesByColumn(col);

            // Bei Listen/Arrays ist for-Schleife performanter als foreach (vermeidet Enumerator-Allokation)
            if (values is IList<string> list)
            {
                int listCount = list.Count;
                for (int i = 0; i < listCount; i++)
                {
                    sum += list[i].ExtractNumber();
                }
            }

            // Präzise Division (Decimal) und Guard gegen Division durch Null
            decimal average = (decimal)sum / count;
            Progress = average.ToSecureInt().ToSecureString();
        }

        public async ValueTask DisposeAsync()
        {
            try
            {
                bus.OnMessage -= OnMessageIncome;

                ItemType = null;
                RelatedItem = null;

                //if (Context != null)
                //{
                //    ((IDisposable)Context).Dispose();
                //    Context = null;
                //}

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("FAIL : DISPOSE : BoardComp : " + ex.Message);
            }
        }
    }
}