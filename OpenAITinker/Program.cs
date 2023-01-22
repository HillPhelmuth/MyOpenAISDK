using ImageEditor;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using OpenAIDotNet.Extensions;
using OpenAITinker;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddImageInterop();
//From User Secrets
//var config = builder.Configuration.GetSection("OpenAIDotNetServiceOptions");
//Get your API key at https://beta.openai.com/
builder.Services.AddOpenAIDotNet(o =>
{
    o.ApiKey = "<Your API Key>";
    o.Organization = "<Your Organization>";
});
await builder.Build().RunAsync();
