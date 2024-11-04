namespace TranslationConsoleApp;

internal class ServiceConfiguration
{
    private readonly string _serviceUri = "https://localhost:7107";

    public ServiceConfiguration()
    {
        var serviceProvider = GetServices().BuildServiceProvider();
        TranslationService = serviceProvider.GetRequiredService<ITranslation>();
        Client = serviceProvider.GetRequiredService<TranslationClient>();
    }

    public ITranslation TranslationService { get; }

    public TranslationClient Client { get; }

    private IServiceCollection GetServices()
    {
        var clientBuilder = new ServiceCollection()
            .AddGrpcClient<Translation.TranslationClient>(opt =>
            {
                opt.Address = new Uri(_serviceUri);
            });

        return clientBuilder.Services
            .AddSingleton<ITranslation, GoogleTranslateService>();
    }
}
