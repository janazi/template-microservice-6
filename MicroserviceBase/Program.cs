using Jnz.RedisRepository.Extensions;
using Jnz.RequestHeaderCorrelationId;
using MasstransitCorrelationId;
using MediatR;
using MicroserviceBase;
using MicroserviceBase.Infra.Mvc.HealChecks;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Newtonsoft.Json;
using Serilog;
using Serilog.Core.Enrichers;
using Serilog.Enrichers.AspNetCore.HttpContext;
using System.IO.Compression;
using System.Net;
using Microsoft.OpenApi.Models;
using MassTransit;
using MicroserviceBase.Application.Consumers;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseSerilog();

// Add services to the container.

builder.Services.AddControllers()
    .AddControllersAsServices(); // to use on WarmUp
builder.Services.AddHealthChecks()
    .AddCheck<WarmupHealthCheck>("Warm Up");

builder.Services.PropagateCorrelationIdHeader();
builder.Services.AddMasstransitCorrelationId();

builder.Services.AddLogging();

builder.Services.AddRedisRepository(builder.Configuration);

builder.Services.AddMediatR(AppDomain.CurrentDomain.Load("MicroserviceBase.Application"));
builder.Services.AddResponseCompression(opt =>
{
    opt.Providers.Add<GzipCompressionProvider>();
});
builder.Services.Configure<GzipCompressionProviderOptions>(opt =>
{
    opt.Level = CompressionLevel.Fastest;
});

builder.WebHost.ConfigureKestrel((ctx, opt) =>
{
    opt.AddServerHeader = false;
    opt.Listen(IPAddress.Any, 5001, listenOptions =>
    {
        listenOptions.Protocols = HttpProtocols.Http1AndHttp2;
        listenOptions.UseHttps();
    });
});


builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "MicroserviceBase", Version = "v1" });
});


builder.Services.AddMassTransit(opt =>
{
    var rabbitConfig = builder.Configuration.GetSection("RabbitConfig").Get<RabbitMQConfiguration>();
    opt.AddConsumer(typeof(CustomerCreatedConsumer), typeof(CustomerCreatedConsumerDefinition));
    opt.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
    {
        cfg.Host(rabbitConfig.Host, rabbitConfig.VirtualHost, auth =>
        {
            auth.Username(rabbitConfig.UserName);
            auth.Password(rabbitConfig.Password);
        });

        cfg.ConfigureEndpoints(provider);
    }));
});
builder.Services.AddMassTransitHostedService(true);

JsonConvert.DefaultSettings = () => new JsonSerializerSettings
{
    NullValueHandling = NullValueHandling.Ignore,
    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
};

var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .Enrich.WithCorrelationIdHeader()
                .Enrich.WithMachineName()
                .Enrich.WithProperty("Environment", environment)
                .Enrich.WithProperty("AppName", ApplicationInfo.GetServiceName())
                .Enrich.WithProperty("AppVersion", ApplicationInfo.GetVersion())
                .WriteTo.Debug()
                .WriteTo.Console(outputTemplate: "{Timestamp:HH:mm:ss:ms} [{Level}] {Properties}: {NewLine}{Message}{NewLine}{Exception}{NewLine}")
                //.WriteTo.Elasticsearch(ConfigureElasticSink(context.Configuration, context.HostingEnvironment.EnvironmentName?.ToLower().Replace(".", "-")))
                .ReadFrom.Configuration(builder.Configuration)
                .CreateLogger();

var app = builder.Build();
app.UseResponseCompression();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MicroserviceBase v1"));
}

app.UseSerilogLogContext(opt =>
{
    opt.EnrichersForContextFactory = context => new[]
    {
        new PropertyEnricher("User-Agent", context.Request.Headers["User-Agent"])
    };
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.AddRequestHeaderCorrelationId();

app.Run();
