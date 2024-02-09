#pragma warning disable CA1506

using Itmo.Dev.Platform.Common.Extensions;
using Itmo.Dev.Platform.Logging.Extensions;
using OrganizingEventsService.Application.Extensions;
using OrganizingEventsService.Infrastructure.Persistence.Extensions;

// using EventMaster.Presentation.Http.Extensions;

// using Microsoft.Extensions.Options;
// using Newtonsoft.Json;
// WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// builder.Configuration.AddUserSecrets<Program>();
//
// builder.Services.AddOptions<JsonSerializerSettings>();
// builder.Services.AddSingleton(sp => sp.GetRequiredService<IOptions<JsonSerializerSettings>>().Value);
//
// builder.Services.AddApplication();
// builder.Services.AddInfrastructurePersistence();
// builder.Services
//     .AddControllers()
//     .AddNewtonsoftJson()
//     .AddPresentationHttp();
//
// builder.Services.AddSwaggerGen().AddEndpointsApiExplorer();
//
// builder.Host.AddPlatformSerilog(builder.Configuration);
// builder.Services.AddUtcDateTimeProvider();
//
// WebApplication app = builder.Build();
//
// app.UseRouting();
// app.UseSwagger();
// app.UseSwaggerUI();
//
// app.MapControllers();
//
// await app.RunAsync();
WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Services.AddApplication();
builder.Services.AddInfrastructurePersistence(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Host.AddPlatformSerilog(builder.Configuration);
builder.Services.AddUtcDateTimeProvider();

WebApplication app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseRouting();
app.MapControllers();