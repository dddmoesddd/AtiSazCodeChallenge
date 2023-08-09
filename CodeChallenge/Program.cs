using CodeChallenge.Utility.Middlewars;
using CodeChallenge.Utility.ServiceRegisteration;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();
builder.Services.AddApplicationServiceServiceServices();
builder.Services.AddInfrastructureServiceServices(builder.Configuration);
builder.Services.AddLogServicees(builder.Configuration);
builder.Services.AddCacheServices(builder.Configuration);
builder.Services.AddHealthCheckServices(builder.Configuration);
builder.Services.AddRateLimitServices();
builder.Services.AddSecurityHeader();




var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();
app.UseRateLimiter();
app.UseMiddleware<CustomSecurityHeaderMiddelware>();
app.UseMiddleware<RequestResponseLoggerMiddleware>();
app.AddHealthCheckAppMiddleWare();


app.Run();
