using Appstract.Front.Client;
using Appstract.Front.SharedUI;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

WebAssemblyHostBuilder builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

AppstractConfiguration config = new(builder);
builder.Services.AddAppstractServices(config);

WebAssemblyHost app = builder.Build();
await app.RunAsync();