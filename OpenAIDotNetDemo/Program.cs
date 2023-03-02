using BlazorAceEditor.Extensions;
using ImageEditor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using OpenAIDotNet.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddImageInterop();
builder.Services.AddBlazorAceEditor();

var objApiKey = builder.Configuration["OpenAIDotNetServiceOptions:ApiKey"];
//Get your API key at https://beta.openai.com/
builder.Services.AddOpenAIDotNet(o =>
{
    o.ApiKey = objApiKey;
    o.Organization = "org-vzjblyRugVShXOXHAgmIRTuQ";
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
