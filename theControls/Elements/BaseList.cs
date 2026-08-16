using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Text;
using theDatabase;
using theInfrastructure;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace theControls.Elements
{
    public class BaseList : ComponentBase, IAsyncDisposable
    {
        [Inject] 
        public IMessagingBusService bus { get; set; } = default!;

        [Inject] 
        public ISqlDatabaseService sql { get; set; } = default!;

        [Parameter]
        public IItemType ItemType { get; set; }

        [Parameter]
        public IDTO RelatedItem { get; set; }

        public IQueryContext Context { get; set; }

        protected override async Task OnInitializedAsync()
        {
            bus.OnMessage += OnMessageIncome;
        }

        protected override async Task OnParametersSetAsync()
        {
            await base.OnParametersSetAsync();

            // Context
            Context = new QueryContext(sql);
            Context.ItemType = ItemType;

            if (RelatedItem != null)
            {
                // Related Items
                Context.RelatedItem = RelatedItem;
                await Context.RelatedItems(ItemType.Name);
            }
            else
            {
                await Context.Search();
            }

        }

        private async void OnMessageIncome(AppMessage msg)
        {
            if (msg.Action == BusAction.Refresh)
            {
                if (RelatedItem != null)
                {
                    // Related Items
                    Context.RelatedItem = RelatedItem;
                    await Context.RelatedItems(ItemType.Name);
                }
                else
                {
                    await Context.Search();
                }

                await InvokeAsync(this.StateHasChanged);

                // Logik: Prüfen ob Wechsel erlaubt...
                bool canChange = true;

                if (canChange)
                {
                    msg.Reply?.Invoke(new AppMessage(msg.Id, BusAction.Refresh));
                }
            }
        }

        public async ValueTask DisposeAsync()
        {
            try
            {
                bus.OnMessage -= OnMessageIncome;

                ItemType = null;
                RelatedItem = null;

                if (Context != null)
                {
                    ((IDisposable)Context).Dispose();
                    Context = null;
                }

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("FAIL : DISPOSE : BoardComp : " + ex.Message);
            }
        }
    }
}