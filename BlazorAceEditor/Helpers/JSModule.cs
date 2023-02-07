using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAceEditor.Helpers
{
    public abstract class JSModule : IAsyncDisposable
    {
        private readonly Task<IJSObjectReference> moduleTask;

        protected JSModule(IJSRuntime js, string moduleUrl)
            => moduleTask = js.InvokeAsync<IJSObjectReference>("import", moduleUrl).AsTask();

        protected async ValueTask InvokeVoidAsync(string identifier, params object[]? args)
            => await (await moduleTask).InvokeVoidAsync(identifier, args);
        protected async ValueTask<T> InvokeAsync<T>(string identifier, params object[]? args)
            => await (await moduleTask).InvokeAsync<T>(identifier, args);

        // Release the JS module
        public async ValueTask DisposeAsync()
        {
            await (await moduleTask).DisposeAsync();
            GC.SuppressFinalize(this);
        }
    }
}
