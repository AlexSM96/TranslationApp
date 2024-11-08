using GoogleTranslate.Interfaces;
using Grpc.Core;
using ProtoObjects;

namespace GrpcTranslation.Services
{
    public class GrpcTranslationService(ITranslation translationService) : Translation.TranslationBase
    {
        private readonly ITranslation _translationService = translationService;

        public override async Task<TranslateReply> Translate(TranslateRequest request, ServerCallContext context)
        {
            var translationReply = new TranslateReply();
            var translationResult = await _translationService
                .TranslateAsync(request.SourceLang, request.TargetLang, request.Texts);
            translationReply.TranslatedTexts.AddRange(translationResult);

            return await Task.FromResult(translationReply);
        }

        public override async Task<ServiceInfoReply> GetServiceInfo(ServiceInfoRequest request, ServerCallContext context)
        {
            var serviceInfo = await _translationService
               .GetServicesInfoAsync(request.ForeignServiceInfo);

            var infoReply = new ServiceInfoReply()
            {
                ServiceInfo = serviceInfo
            };

            return await Task.FromResult(infoReply);
        }
    }
}
