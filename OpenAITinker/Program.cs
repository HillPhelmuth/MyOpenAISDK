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

var objApiKey = builder.Configuration["OpenAIDotNetServiceOptions:ApiKey"];
//Get your API key at https://beta.openai.com/
builder.Services.AddOpenAIDotNet(o =>
{
    o.ApiKey = objApiKey;
    o.Organization = "org-vzjblyRugVShXOXHAgmIRTuQ";
});
await builder.Build().RunAsync();
