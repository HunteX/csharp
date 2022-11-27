using WebClient;

var webApiClient = new HttpWebApiClient();
var interactor = new ConsoleInteractor(webApiClient);

await interactor.ShowMainMenuAsync();