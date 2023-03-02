using System.Text;
using ImageEditor.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;
using Newtonsoft.Json;

namespace ImageEditor
{
    // This class provides an example of how JavaScript functionality can be wrapped
    // in a .NET class for easy consumption. The associated JavaScript module is
    // loaded on demand when first needed.
    //
    // This class can be registered as scoped DI service and then injected into Blazor
    // components for use.

    public class ImageCanvasInterop : IAsyncDisposable
    {
        private readonly Lazy<Task<IJSObjectReference>> _moduleTask;
        private DotNetObjectReference<ImageCanvasInterop> ThisObjectReference => DotNetObjectReference.Create(this);
        public ImageCanvasInterop(IJSRuntime jsRuntime)
        {
            _moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
                "import", "./_content/ImageEditor/imageCanvas.js").AsTask());
        }

        public async ValueTask DrawImageToCanvas(ElementReference element)
        {
            await (await _moduleTask.Value).InvokeVoidAsync("drawImage", element);
        }

        public async ValueTask<DrawnRectangle[]> GrabImageFromCanvas()
        {
            var drawn1 = await (await _moduleTask.Value).InvokeAsync<DrawnRectangle[]>("grabImage");
            Console.WriteLine(JsonConvert.SerializeObject(drawn1));
            //var drawn = drawn1.Select(x => new DrawnRectangleI((int)x.X, (int)x.Y, (int)x.Width, (int)x.Height)).ToArray();
            //Console.WriteLine(drawn.ToString());
            return drawn1;
        }

        public async ValueTask RemoveLast()
        {
            await (await _moduleTask.Value).InvokeVoidAsync("removeLast");
        }
       
        public async ValueTask ClearSelection()
        {
            await (await _moduleTask.Value).InvokeVoidAsync("clear");
        }
        public async ValueTask DisposeAsync()
        {
            if (_moduleTask.IsValueCreated)
            {
                var module = await _moduleTask.Value;
                await module.DisposeAsync();
            }
        }
    }

    
    public static class ServiceCollectionExt
    {
        public static IServiceCollection AddImageInterop(this IServiceCollection services)
        {
            return services.AddScoped<ImageCanvasInterop>().AddScoped<ImageService>();
        }
    }
}