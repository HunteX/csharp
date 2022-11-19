using OtusDbs.Db;
using OtusDbs.Db.Structures;

namespace OtusDbs.Interaction;

public class ConsoleUserInteractor
{
    private DbReader _reader;

    public delegate void KeyPress();

    public delegate void TableRow(string tableName, Dictionary<string, object> rowData);

    public event KeyPress TKeyPressed = null!;
    public event KeyPress DKeyPressed = null!;
    public event KeyPress AnyKeyPressed = null!;
    public event TableRow TableRowAdded = null!;

    public ConsoleUserInteractor(DbReader reader)
    {
        _reader = reader;
    }

    /// <summary>
    /// Отображает главное меню
    /// </summary>
    public async Task ShowMainMenuAsync()
    {
        bool showInfo = true;

        while (true)
        {
            if (showInfo)
            {
                AnyKeyPressed();
                showInfo = false;
            }

            var result = Console.ReadKey(true);

            switch (result.Key)
            {
                case ConsoleKey.T:
                    TKeyPressed();
                    break;
                case ConsoleKey.D:
                    DKeyPressed();
                    break;
                case ConsoleKey.A:
                    var tables = await _reader.GetTablesDataAsync();

                    ShowCreateTableRowMenu(tables);
                    break;
                case ConsoleKey.Q:
                    return;
                case ConsoleKey.Escape:
                    break;
                default:
                    showInfo = true;
                    break;
            }
        }
    }

    /// <summary>
    /// Отображает меню добавления строки в таблицу
    /// </summary>
    /// <param name="tables"></param>
    private void ShowCreateTableRowMenu(List<DbTable> tables)
    {
        var selectedTable = ShowSelectTableMenu(tables);
        var rowData = new Dictionary<string, object>();
        var index = 0;

        selectedTable.Header.Values.ForEach(column =>
        {
            Console.WriteLine($"Задайте значение для колонки [{column}] (значение _ исключает колонку):");

            var result = Console.ReadLine();

            if (result != "_")
            {
                // пока добавил только поддержку значений типа int - всё остальное считается строками
                if (selectedTable.Rows.First().Values[index] is int && int.TryParse(result, out int intResult))
                {
                    rowData.Add(column, intResult);
                }

                if (selectedTable.Rows.First().Values[index] is not int)
                {
                    rowData.Add(column, result);
                }
            }

            index++;
        });

        TableRowAdded(selectedTable.Name, rowData);
    }

    /// <summary>
    /// Отображает меню выбора таблицы
    /// </summary>
    /// <param name="tables"></param>
    /// <returns></returns>
    private DbTable ShowSelectTableMenu(List<DbTable> tables)
    {
        var index = 0;

        Console.WriteLine("Доступные таблицы:");

        tables.ForEach(table =>
        {
            Console.WriteLine($"{index}. {table.Name}");
            index++;
        });

        Console.WriteLine("\nУкажите номер выбранной таблицы:");

        var inputLineStr = Console.ReadLine();

        if (!int.TryParse(inputLineStr, out int inputIndex) || (inputIndex < 0 || inputIndex >= tables.Count))
        {
            return ShowSelectTableMenu(tables);
        }

        return tables[inputIndex];
    }
}