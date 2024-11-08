namespace GoogleTranslate.Interfaces;

public interface ITranslation
{
    public Task<string> GetServicesInfoAsync(string foreignServiceInfo = "");

    public Task<IEnumerable<string>> TranslateAsync(string sourceLang, string targetLang, IEnumerable<string> texts);
}
