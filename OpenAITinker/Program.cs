using BlazorAceEditor.Extensions;
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
builder.Services.AddBlazorAceEditor();
//From User Secrets
//var config = builder.Configuration.GetSection("OpenAIDotNetServiceOptions");
//Get your API key at https://beta.openai.com/
builder.Services.AddOpenAIDotNet(o =>
{
    o.ApiKey = "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";
    o.Organization = "org-vzjblyRugVShXOXHAgmIRTuQ";
});
await builder.Build().RunAsync();
