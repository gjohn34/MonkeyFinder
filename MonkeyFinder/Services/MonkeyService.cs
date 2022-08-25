using System.Net.Http.Json;

namespace MonkeyFinder.Services;

/// <summary>
/// Fetching API
/// </summary>
public class MonkeyService
{
    HttpClient httpClient;
    public MonkeyService()
    {
        httpClient = new HttpClient();
    }
    List<Monkey> monkeyList = new List<Monkey>();

    public async Task<List<Monkey>> GetMonkeys()
    {
        if (monkeyList.Count > 0)
            return monkeyList;


        HttpResponseMessage response = await httpClient.GetAsync("montemagno.com/monkeys.json");

        if (response.IsSuccessStatusCode)
        {
            // Deserializing
            monkeyList = await response.Content.ReadFromJsonAsync<List<Monkey>>();
        }

        // If having fetch issues, check docs for code to read from file.
        //using var stream = await FileSystem.OpenAppPackageFileAsync("monkeydata.json");
        //using var reader = new StreamReader(stream);
        //var contents = await reader.ReadToEndAsync();
        //monkeyList = JsonSerializer.Deserialize<List<Monkey>>(contents);
        return monkeyList;
    }
}
