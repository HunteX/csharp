using System.Reflection;

namespace Game.UI;

public sealed class GameSelector
{
    private readonly IServiceProvider _serviceProvider;

    public GameSelector(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public void ShowGameListMenu()
    {
        var gameTypes = Assembly.GetExecutingAssembly().GetTypes()
            .Where(type => type.IsSubclassOf(typeof(Games.Base.Game)))
            .OrderBy(type => type.Name)
            .ToList();

        PrintGameList(gameTypes);

        var selectedGameNumber = SelectGameNumber(gameTypes);

        if (selectedGameNumber == 0)
        {
            return;
        }

        var gameType = gameTypes[selectedGameNumber - 1];
        var game = (Games.Base.Game)_serviceProvider.GetService(gameType)!;

        Console.Clear();

        game.Load();

        Console.WriteLine("Игра завершена! Нажмите любую клавишу для продолжения ...");
        Console.ReadKey();

        ShowGameListMenu();
    }

    private void PrintGameList(List<Type> games)
    {
        Console.Clear();
        Console.WriteLine("Доступные игры:");
        Console.WriteLine("---------------");

        for (var index = 0; index < games.Count; index++)
        {
            Console.WriteLine($"{index + 1}. {games[index].Name}");
        }

        Console.WriteLine("0. Выход");
    }

    private int SelectGameNumber(List<Type> games)
    {
        Console.WriteLine();
        Console.WriteLine("Выберите игру:");

        var strValue = Console.ReadLine();
        int selectedGameNumber;

        while (!
               (int.TryParse(strValue, out selectedGameNumber) &&
                selectedGameNumber >= 0 &&
                selectedGameNumber <= games.Count))
        {
            Console.WriteLine("Укажите корректный номер меню!");
            
            strValue = Console.ReadLine();
        }

        return selectedGameNumber;
    }
}