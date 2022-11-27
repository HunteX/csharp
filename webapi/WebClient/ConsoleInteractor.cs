namespace WebClient;

public class ConsoleInteractor
{
    public delegate Task GetCustomer(long id);

    public delegate Task AddCustomer(long id);

    public event GetCustomer GetCustomerEmitted;
    public event AddCustomer AddCustomerEmitted;

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

    public void ShowGetCustomerResult(string? result)
    {
        Console.WriteLine("Запрашиваемая запись:");
        Console.WriteLine(result ?? "Не найдена");
        Console.WriteLine("Нажмите любую клавишу для продолжения ...");
    }

    public void ShowAddCustomerResult(bool result)
    {
        Console.WriteLine(result ? "Запись успешно добавлена" : "Запись уже существует!");
        Console.WriteLine("Нажмите любую клавишу для продолжения ...");
    }

    private async Task ShowGetCustomerMenu()
    {
        Console.WriteLine("Введите идентификатор записи:");

        var id = Convert.ToInt64(Console.ReadLine());

        if (id != 0)
        {
            GetCustomerEmitted(id);
        }
        else
        {
            await ShowGetCustomerMenu();
        }
    }

    private async Task ShowAddCustomerMenu()
    {
        Console.WriteLine("Введите идентификатор записи:");

        var id = Convert.ToInt64(Console.ReadLine());

        if (id != 0)
        {
            AddCustomerEmitted(id);
        }
        else
        {
            await ShowAddCustomerMenu();
        }
    }
}