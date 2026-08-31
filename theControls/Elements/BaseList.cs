using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
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
        public AssociationTyp Association { get; set; } = AssociationTyp.NULL;

        [Parameter]
        public IItemType ItemType { get; set; }

        [Parameter]
        public IDTO RelatedItem { get; set; }

        public IQueryContext Context { get; set; }

        // Events 
        protected override async Task OnInitializedAsync()
        {
            bus.OnMessage += OnMessageIncome;
        }
        protected override async Task OnParametersSetAsync()
        {
            await base.OnParametersSetAsync();

            if (ItemType != null)
            {
                // Context
                Context = new QueryContext(sql);
                Context.ItemType = ItemType;

                if (RelatedItem != null)
                {
                    // Related Items
                    Context.RelatedItem = RelatedItem;
                    Context.Items = await Context.RelatedItems(ItemType.Name, Association);
                }
                else
                {
                    await Context.Search();
                }
            }

        }

        [Parameter]
        public EventCallback<string> OnSearch { get; set; }

        private async void OnMessageIncome(AppMessage msg)
        {
            if (msg.Action == BusAction.Refresh)
            {
                if (ItemType != null)
                {
                    if (RelatedItem != null)
                    {
                        // Related Items
                        Context.RelatedItem = RelatedItem;
                        Context.Items = await Context.RelatedItems(ItemType.Name, Association);
                    }
                    else
                    {
                        await Context.Search();
                    }
                }

                await InvokeAsync(this.StateHasChanged);

                // Logik: Prüfen ob Wechsel erlaubt...
                bool canChange = true;

                if (canChange)
                {
                    msg.Reply?.Invoke(new AppMessage(msg.Id, BusAction.Refresh));
                }
            }


            if (msg.Id == "LocalSearch" && msg.Action == BusAction.Search)
            {
                //List<IDTO> items = new();
                //string matchcode = msg.Payload.ToSecureString();
                //if (string.IsNullOrEmpty(matchcode))
                //{
                //    await Context.Search();
                //}
                //else
                //{ 
                //    await Context.Filter(matchcode);
                //}
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