namespace WebClient;

public class WebClientManager
{
    private readonly HttpWebApiClient _webApiClient = new();
    private readonly ConsoleInteractor _consoleInteractor = new();

    public WebClientManager()
    {
        _consoleInteractor.GetCustomerEmitted += OnGetCustomer;
        _consoleInteractor.AddCustomerEmitted += OnAddCustomer;
    }

    public async Task Start()
    {
        await _consoleInteractor.ShowMainMenuAsync();
    }

    private async Task OnGetCustomer(long id)
    {
        var result = await _webApiClient.Get(id);

        _consoleInteractor.ShowGetCustomerResult(result);
    }

    private async Task OnAddCustomer(long id)
    {
        var rnd = new Random();
        var rndValue = rnd.Next();

        var randomFirstName = $"Firstname{rndValue}";
        var randomLastName = $"Lastname{rndValue}";

        var result = await _webApiClient.Add(id, randomFirstName, randomLastName);

        _consoleInteractor.ShowAddCustomerResult(result != null);
    }
}