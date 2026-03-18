using CoreWCF;
using CoreWCF.Configuration;
using CoreWCF.Description;
using HelloWorldWcf;

var builder = WebApplication.CreateBuilder(args);

// Add WCF services to the DI container
builder.Services.AddServiceModelServices();
builder.Services.AddServiceModelMetadata();
builder.Services.AddSingleton<IServiceBehavior, UseRequestHeadersForMetadataAddressBehavior>();

var app = builder.Build();

app.UseServiceModel(b =>
{
    b.AddService<HelloWorldService>(serviceOptions => { })
     .AddServiceEndpoint<HelloWorldService, IHelloWorldService>(new BasicHttpBinding(), "/HelloWorldService/basichttp")
     .AddServiceEndpoint<HelloWorldService, IHelloWorldService>(new WSHttpBinding(SecurityMode.None), "/HelloWorldService/wshttp");
});

// Enable WSDL
var serviceMetadataBehavior = app.Services.GetRequiredService<ServiceMetadataBehavior>();
serviceMetadataBehavior.HttpGetEnabled = true;

app.MapGet("/", () => "WCF Hello World Service is running!");

app.Run();
