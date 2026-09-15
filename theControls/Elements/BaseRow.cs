using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Text;
using theInfrastructure;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace theControls.Elements
{
    public class BaseRow : ComponentBase, IAsyncDisposable
    {
        [Inject]
        public NavigationManager nm { get; set; } = default;

        [Inject] 
        public IMessagingBusService msg { get; set; } = default;

        [Inject]
        public ISqlDatabaseService sql { get; set; } = default;

        [Inject]
        public ISecurityService sec { get; set; } = default;

        [Parameter]
        public IItemType ItemType { get; set; }

        [Parameter]
        public IDTO Item { get; set; }

        [Parameter]
        public IDTO RelatedItem { get; set; }

        [Parameter]
        public bool IsRelated { get; set; } = false;

        IQueryContext Context { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            if (Context == null && ItemType != null)
            {
                Context = new theDatabase.QueryContext(sql, sec, ItemType);
            }
        }
        protected override async Task OnParametersSetAsync()
        {
            await base.OnParametersSetAsync();

            if (Context != null && RelatedItem != null)
            {
                Context.RelatedItem = RelatedItem;
            }
        }

        [Parameter]
        public EventCallback<IDTO> OnUpdate { get; set; }
        
        [Parameter]
        public EventCallback<IDTO> OnDisconnect { get; set; }

        [Parameter]
        public EventCallback<IDTO> OnConnect { get; set; }

        [Parameter]
        public EventCallback<IDTO> OnDelete { get; set; }

        public async Task Delete()
        {
            if (Context == null)
            {
                return;
            }

            await Context.Delete(Item);

            // Message
            AppMessage am = new AppMessage("ID", BusAction.Refresh, "Datensatz verbunden");
            msg.Publish(am);

            if (OnDelete.HasDelegate)
            {
                await OnDelete.InvokeAsync(Item);
            }
        }
        public async Task Update()
        {
            await Context.Update(Item);

            // Message
            AppMessage am = new AppMessage("ID", BusAction.Refresh, "Datensatz aktualisiert");
            msg.Publish(am);

            if (OnUpdate.HasDelegate)
            {
                await OnUpdate.InvokeAsync(Item);
            }
        }
        public async Task Disconnect()
        {
            if (Context == null)
            {
                return;
            }

            await Context.Remove(Item);

            // Message
            AppMessage am = new AppMessage("ID", BusAction.Refresh, "Datensatz verbunden");
            msg.Publish(am);

            if (OnDisconnect.HasDelegate)
            {
                await OnDisconnect.InvokeAsync(Item);
            }
        }


        public async ValueTask DisposeAsync()
        {
            try
            {
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
