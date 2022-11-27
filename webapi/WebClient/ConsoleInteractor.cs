namespace WebClient;

public class ConsoleInteractor
{
    private readonly HttpWebApiClient _httpWebApiClient;

    public ConsoleInteractor(HttpWebApiClient httpWebApiClient)
    {
        _httpWebApiClient = httpWebApiClient;
    }

    public async Task ShowMainMenuAsync()
    {
        PrintUsage();

        while (true)
        {
            var result = Console.ReadKey(true);

            switch (result.Key)
            {
                case ConsoleKey.A:
                    await ShowAddCustomerMenu();
                    break;
                case ConsoleKey.S:
                    await ShowGetCustomerMenu();
                    break;
                case ConsoleKey.Q:
                    return;
                default:
                    PrintUsage();
                    break;
            }
        }
    }

    public void PrintUsage()
    {
        Console.WriteLine("-----------------------------");
        Console.WriteLine("Главное меню");
        Console.WriteLine("-----------------------------");
        Console.WriteLine("A: Добавить запись");
        Console.WriteLine("S: Получить запись");
        Console.WriteLine("Q: Выход");
        Console.WriteLine();
    }

    private async Task ShowGetCustomerMenu()
    {
        Console.WriteLine("Введите идентификатор записи:");

        var id = Convert.ToInt64(Console.ReadLine());

        if (id != 0)
        {
            var result = await _httpWebApiClient.Get(id);

            Console.WriteLine("Запрашиваемая запись:");
            Console.WriteLine(result ?? "Не найдена");
            Console.WriteLine("Нажмите любую клавишу для продолжения ...");

            return;
        }

        await ShowGetCustomerMenu();
    }

    private async Task ShowAddCustomerMenu()
    {
        Console.WriteLine("Введите идентификатор записи:");

        var id = Convert.ToInt64(Console.ReadLine());

        if (id != 0)
        {
            var rnd = new Random();
            var rndValue = rnd.Next();

            var randomFirstName = $"Firstname{rndValue}";
            var randomLastName = $"Lastname{rndValue}";

            var result = await _httpWebApiClient.Add(id, randomFirstName, randomLastName);

            if (result != null)
            {
                Console.WriteLine("Запись успешно добавлена");
            }
            else
            {
                Console.WriteLine("Запись уже существует!");
            }

            Console.WriteLine("Нажмите любую клавишу для продолжения ...");

            return;
        }

        await ShowAddCustomerMenu();
    }
}