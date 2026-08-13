using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
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

        [Parameter]
        public IQueryContext Context { get; set; }

        [Parameter]
        public IDTO Item { get; set; }

        [Parameter]
        public bool IsRelated { get; set; } = false;

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
