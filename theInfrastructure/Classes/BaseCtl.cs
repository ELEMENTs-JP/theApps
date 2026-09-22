using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using theInfrastructure;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace theInfrastructure
{
    public class BaseCtl : ComponentBase, IAsyncDisposable
    {
        [Inject]
        public IMessagingBusService msg { get; set; } = default!;

        [Inject]
        public ISqlDatabaseService sql { get; set; } = default!;

        [Inject]
        public ISecurityService sec { get; set; } = default!;

        [Inject]
        public ISearchService searchService { get; set; } = default!;

        [Inject]
        public IAppService appService { get; set; } = default!;

        [Inject]
        public IJSRuntime JS { get; set; } = default!;

        // Events 
        protected override async Task OnInitializedAsync()
        {
            msg.OnMessage += OnMessageIncome;
        }

        // Callback 
        private async void OnMessageIncome(AppMessage message)
        {
            if (message.Action == BusAction.Refresh)
            {
               
            }
        }


        public async ValueTask DisposeAsync()
        {
            try
            {
                msg.OnMessage -= OnMessageIncome;

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("FAIL : DISPOSE : BoardComp : " + ex.Message);
            }
        }
    }
}