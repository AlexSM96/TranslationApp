using GoogleTranslate.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace GoogleTranslate
{
    public class GoogleTranslateService : ITranslation
    {
        private MemoryCache _cache = new(new MemoryCacheOptions()
        {
            SizeLimit = 1024
        });

        public Task<string> GetServicesInfoAsync(string foreignServiceInfo)
        {
            string info = $"""
             Текущий сервис: {nameof(GoogleTranslateService)}
             Внешний сервис: {foreignServiceInfo}
             Тип: {nameof(MemoryCache)}
             Объём кэша: {_cache.Count}
             """;

            return Task.FromResult(info);
        }

        public async Task<IEnumerable<string>> TranslateAsync(string sourceLang, string targetLang, IEnumerable<string> texts)
        {
            var cacheKey = $"{sourceLang}:{targetLang}:{string.Join(", ", texts)}";

            var translationResult = await _cache.GetOrCreateAsync<IEnumerable<string>>(cacheKey, 
                async (x) => await Task.FromResult(texts
                    .Where(t => !string.IsNullOrWhiteSpace(t))
                    .Select(t => t.ToUpper())
                    .ToList()),
                new MemoryCacheEntryOptions()
                    .SetSize(1)
                    .SetPriority(CacheItemPriority.High)
                    .SetSlidingExpiration(TimeSpan.FromSeconds(30))
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(50))
            );

            return await Task.FromResult(translationResult ?? Enumerable.Empty<string>());
        }
    }
}
