#pragma warning disable CA1506

using Itmo.Dev.Platform.Common.Extensions;
using Itmo.Dev.Platform.Logging.Extensions;
using OrganizingEventsService.Application.Extensions;
using OrganizingEventsService.Infrastructure.Persistence.Extensions;
using OrganizingEventsService.Presentation.Http.Extensions;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Host.AddPlatformSerilog(builder.Configuration);
builder.Services.AddUtcDateTimeProvider();

builder.Services.AddApplication();
builder.Services.AddInfrastructurePersistence(builder.Configuration);
builder.Services
    .AddControllers()
    .AddNewtonsoftJson()
    .AddPresentationHttp();

WebApplication app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseRouting();
app.MapControllers();

await app.RunAsync();