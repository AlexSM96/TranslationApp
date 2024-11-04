var services = new ServiceConfiguration();

while (true)
{
    var serviceInfo = await services.Client.GetServiceInfoAsync(new SInfoRequest { ForeignServiceInfo = "GrpcService" });
    Console.WriteLine(serviceInfo.ServiceInfo);

    Console.WriteLine("Ведите язык 1:");
    string? sourceLang = Console.ReadLine() ?? "en";
    Console.WriteLine("Ведите язык 2:");
    string? tragetLang = Console.ReadLine() ?? "ru";
    Console.WriteLine("Ведите текст: ");
    string? text = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(text)) continue;

    var translateReauest = new TR();
    translateReauest.Texts.AddRange([text]);
    var translateResult = await services.Client.TranslateAsync(translateReauest);

    Console.WriteLine("Перевод^: ");
    Console.WriteLine(string.Join("\n", translateResult.TranslatedTexts));
    Console.WriteLine(new string('-', Console.BufferWidth));
}
