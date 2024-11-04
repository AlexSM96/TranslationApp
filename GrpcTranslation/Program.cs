using GoogleTranslate;
using GoogleTranslate.Interfaces;
using GrpcTranslation.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddSingleton<ITranslation, GoogleTranslateService>()
    .AddGrpc();

var app = builder.Build();
app.MapGrpcService<GrpcTranslationService>();
app.MapGet("/", () => "Hello");
app.Run();
