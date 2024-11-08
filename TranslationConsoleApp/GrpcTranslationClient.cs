using Grpc.Net.Client;

class GrpcTranslationClient : ITranslation
{
    private readonly string _serviceUri = "https://localhost:7107";
    private readonly TranslationClient _client;

    public GrpcTranslationClient()
    {
        var channel = GrpcChannel.ForAddress(_serviceUri);
        _client = new TranslationClient(channel);
    }

    public async Task<string> GetServicesInfoAsync(string foreignServiceInfo = "")
    {
        if (string.IsNullOrWhiteSpace(foreignServiceInfo))
        {
            foreignServiceInfo = nameof(GrpcTranslationClient);
        }

        var request = new SInfoRequest() { ForeignServiceInfo = foreignServiceInfo };
        var response = await _client.GetServiceInfoAsync(request: request);
        if(response is null)
        {
            return await Task.FromResult(string.Empty);
        }

        return await Task.FromResult(response.ServiceInfo);
    }

    public async Task<IEnumerable<string>> TranslateAsync(string sourceLang, string targetLang, IEnumerable<string> texts)
    {
        var requset = new TR();
        requset.SourceLang = sourceLang;
        requset.TargetLang = targetLang;
        requset.Texts.AddRange(texts);
        var response = await _client.TranslateAsync(requset);
        if(response is null)
        {
            return await Task.FromResult(Enumerable.Empty<string>());
        }

        return await Task.FromResult(response.TranslatedTexts);
    }
}