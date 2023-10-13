using Payment.Sdk.Common.Enum;
using Payment.Sdk.DI;
using Payment.Sdk.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigurePayHubClient(cnf =>
{
    // Set payment microservice domain
    cnf.ConnectionConfiguration.Address = "http://localhost:9090";
    cnf.ConnectionConfiguration.Timeout = 100;
    // your webhook address
    cnf.ConnectionConfiguration.WebHook = "http://localhost:7107/hook";

    // Security method between company applications and payment hub
    cnf.SecurityConfiguration.Method = SecurityMethod.HMAC;

    // Your application api secret
    cnf.SecurityConfiguration.ApiSecret = "e8db9d0fe90b10489c1e739ab818311000817316dacb37cbc861be2ed02f7a24";

    // Your application api key
    cnf.SecurityConfiguration.ApiKey = "NL_584216efed319bc206d28231485c5bd152f94d21e4bf7dbcbe151b61f871295c";
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UsePayHubCallBackAuthentication();
app.MapControllers();

app.Run();