using App.SDK.Common.Enum;
using App.SDK.DI;
using App.SDK.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.ConfigureKycClient(opt =>
{
    opt.ConnectionConfiguration.Address = "http://localhost:5259";
    opt.ConnectionConfiguration.Webhooks = new[] { "My_Webhook" };
    opt.SecurityConfiguration.ApiSecret = "689d118805a43b7f79eeadbee8f5eb10c558b574e3b098e711b2459a085ba367";
    opt.SecurityConfiguration.ApiKey = "NL_ca8d07236cc16dc9b6191fdeeb348644e3ee7c68242d762ce7e011eb8da2ef7d";
    opt.SecurityConfiguration.Method = SecurityMethod.HMAC;
    opt.SecurityConfiguration.BanAccountAfterSpecificNumbersOfTry = 5;
});

var app = builder.Build();

app.UseKycCallBackAuthentication();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();