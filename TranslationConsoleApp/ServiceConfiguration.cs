namespace TranslationConsoleApp;

internal class ServiceConfiguration
{
    public ServiceConfiguration()
    {
        ServiceProvider = GetServices().BuildServiceProvider();
    }

    public IServiceProvider ServiceProvider { get; }

    public ITranslation TranslationService { get; }

    private IServiceCollection GetServices()
    {
        return new ServiceCollection()
            .AddSingleton<ITranslation, GrpcTranslationClient>()
            .AddSingleton<ITranslation, GoogleTranslateService>()
        ;
    }
}
