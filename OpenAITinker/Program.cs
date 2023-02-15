using BlazorAceEditor.Extensions;
using ImageEditor;
using ImageEditor.Models;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using OpenAIDotNet.Extensions;
using OpenAITinker;
using OpenAITinker.Services;
using System.Text;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddImageInterop();
builder.Services.AddBlazorAceEditor();
builder.Services.AddScoped<ImageService>();
builder.Services.AddSingleton<AppState>();
var key = "c2steUNrb3ZaYmQyY2VSVnNsWHV4ZmZUM0JsYmtGSmdnTUdIaVRLNW9jb0NkMmhkMldo";
var bytes = Convert.FromBase64String(key);
var alt = Encoding.ASCII.GetString(bytes);
var objApiKey = builder.Configuration["OpenAIDotNetServiceOptions:ApiKey"];
objApiKey = string.IsNullOrEmpty(objApiKey) ? alt : objApiKey;
//Get your API key at https://beta.openai.com/
builder.Services.AddOpenAIDotNet(o =>
{
    o.ApiKey = objApiKey;
    o.Organization = "org-vzjblyRugVShXOXHAgmIRTuQ";
});
await builder.Build().RunAsync();
