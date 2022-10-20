using var httpClient = new HttpClient();

string pathOutput = "Output.txt";

var response = await httpClient.GetAsync("https://localhost:7204/api/word-count?path=C:/Users/Admin/source/repos/ConsoleApp2/bin/Debug/net6.0/Input.txt");

if (response.IsSuccessStatusCode)
{
    var wordsString = await response.Content.ReadAsStringAsync();

    var words = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, int>>(wordsString);

    using (StreamWriter sw = new StreamWriter(new FileStream(pathOutput, FileMode.Append, FileAccess.Write)))
    {
        var result = words.OrderByDescending(w => w.Value).ToList();

        foreach (var word in result)
        {
            sw.WriteLine($"{word.Value} - {word.Key}");
        }
    }
    Console.WriteLine("Словарь готов");
}

else
{
    Console.WriteLine("Ошибка");
}

Console.ReadLine();