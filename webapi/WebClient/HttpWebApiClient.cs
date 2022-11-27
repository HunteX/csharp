using System.Net;
using System.Text;
using System.Text.Json;

namespace WebClient;

public class HttpWebApiClient
{
    private readonly HttpClient _httpClient = new();
    private readonly string _host = "http://localhost:5000";

    public async Task<string?> Get(long id)
    {
        var response = await _httpClient.GetAsync($"{_host}/Customers/{id}");

        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            return null;
        }

        return await response.Content.ReadAsStringAsync();
    }

    public async Task<long?> Add(long id, string firstname, string lastname)
    {
        var newCustomer = new { id, firstname, lastname };
        var content = new StringContent(JsonSerializer.Serialize(newCustomer), Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync($"{_host}/Customers", content);

        if (response.StatusCode == HttpStatusCode.Conflict)
        {
            return null;
        }

        return Convert.ToInt64(await response.Content.ReadAsStringAsync());
    }
}