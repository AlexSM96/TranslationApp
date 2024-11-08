var services = new ServiceConfiguration();
var translationServices = services?.ServiceProvider.GetServices<ITranslation>().ToList();
// Прямой доступ через GoogleTranslateService
var translationService = translationServices?.Find(x => x is GrpcTranslationClient) 
    ?? throw new ArgumentNullException("Сервис не найден");

while (true)
{
    var serviceInfo = await translationService.GetServicesInfoAsync();
    Console.WriteLine(serviceInfo);
    Console.WriteLine();
    Console.WriteLine("Ведите язык 1:");
    string? sourceLang = Console.ReadLine() ?? "en";
    Console.WriteLine("Ведите язык 2:");
    string? targetLang = Console.ReadLine() ?? "ru";
    Console.WriteLine("Ведите текст: ");
    string? text = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(text)) continue;
    var translateResult = await translationService.TranslateAsync(sourceLang, targetLang, [text]);
    Console.WriteLine("Перевод^: ");
    Console.WriteLine(string.Join("\n", translateResult));
    Console.WriteLine(new string('-', Console.BufferWidth));
}