using Game.Configuration;

namespace Game.Games.GuessingGame;

public sealed class GuessingGame : Base.Game
{
    public GuessingGame(GameConfigLoader gameConfigLoader) : base(gameConfigLoader)
    {
    }

    protected override void Run()
    {
        var config = (GameConfig as GuessingGameConfig)!;

        Console.WriteLine("****************");
        Console.WriteLine("* Угадай число *");
        Console.WriteLine("****************");

        var min = config.RandomRange.Min;
        var max = config.RandomRange.Max;
        var randomNumber = Random.Shared.Next(min, max + 1);

        Console.WriteLine();
        Console.WriteLine($"Угадайте число от {config.RandomRange.Min} до {config.RandomRange.Max}");
        Console.WriteLine($"Количество попыток: {config.NumberOfAttempts}");

        TryGuess(randomNumber, min, max, config.NumberOfAttempts, new List<int>());
    }

    private void TryGuess(int randomNumber, int min, int max, int numberOfAttempts, List<int> answers)
    {
        var strValue = Console.ReadLine();
        int selectedNumber;

        while (
            !int.TryParse(strValue, out selectedNumber) ||
            selectedNumber < min || selectedNumber > max ||
            answers.Contains(selectedNumber)
        )
        {
            if (!answers.Contains(selectedNumber))
            {
                Console.WriteLine("Укажите корректное число!");
            }
            else
            {
                Console.WriteLine("Такое число уже было! Введите другое");
            }

            strValue = Console.ReadLine();
        }

        answers.Add(selectedNumber);

        numberOfAttempts--;

        if (selectedNumber == randomNumber)
        {
            Console.WriteLine($"Поздравляю ʕ ᵔᴥᵔ ʔ Вы угадали! Загаданное число: {randomNumber}");

            return;
        }

        if (numberOfAttempts == 0)
        {
            Console.WriteLine($"Вы проиграли ¯\\_(ツ)_/¯ Загаданное число: {randomNumber}");
        }
        else
        {
            var message = selectedNumber < randomNumber
                ? $"Загаданное число больше {selectedNumber}"
                : $"Загаданное число меньше {selectedNumber}";

            Console.WriteLine($"{message}. Осталось попыток: {numberOfAttempts}");

            TryGuess(randomNumber, min, max, numberOfAttempts, answers);
        }
    }
}