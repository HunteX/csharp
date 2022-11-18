using System.Text;
using OtusDbs.Db.Structures;

namespace OtusDbs.Interaction;

public class ConsolePrettyPrinter
{
    private const char VerticalBorder = '|';
    private const char HorizontalBorder = '-';

    /// <summary>
    /// Выводит в консоль справку
    /// </summary>
    public void PrintInfo()
    {
        Console.WriteLine("Главное меню");
        Console.WriteLine("-----------------------------");
        Console.WriteLine("A: Добавить новую строку в таблицу");
        Console.WriteLine("D: Вывести данные всех таблиц");
        Console.WriteLine("T: Вывести список таблиц");
        Console.WriteLine("Q: Выход");
        Console.WriteLine();
    }

    public void PrintRowAddedSuccessfully(string tableName)
    {
        Console.WriteLine($"Строка успешно добавлена в таблицу [{tableName}]");
        Console.WriteLine("Нажмите любую клавишу для продолжения ...");
    }

    /// <summary>
    /// Выводит в консоль список таблиц 
    /// </summary>
    /// <param name="tables"></param>
    public void PrintTableList(List<string> tables)
    {
        Console.WriteLine("Таблицы");
        Console.WriteLine("-----------------------------");
        tables.ForEach(Console.WriteLine);
        Console.WriteLine();
    }

    /// <summary>
    /// Выводит в консоль список таблиц в удобном виде
    /// </summary>
    /// <param name="tablesWithData"></param>
    public void PrintTablesWithData(List<DbTable> tablesWithData)
    {
        Console.WriteLine("Таблицы и данные");
        Console.WriteLine("-----------------------------");

        foreach (var table in tablesWithData)
        {
            var linesToPrint = FormatAsTable(table.Name, table.Header, table.Rows);

            foreach (var lineToPrint in linesToPrint)
            {
                Console.WriteLine(lineToPrint);
            }

            Console.WriteLine();
        }
    }

    /// <summary>
    /// Формирует представление таблицы в удобном виде
    /// </summary>
    /// <param name="tableName"></param>
    /// <param name="tableHeader"></param>
    /// <param name="tableRows"></param>
    /// <returns></returns>
    private List<string> FormatAsTable(string tableName, TableHeader tableHeader, List<TableRow<object>> tableRows)
    {
        var result = new List<string>();
        var maxColumnSizes = CalculateMaxColumnSizes(tableHeader, tableRows);

        result.Add(BuildTableHeader(tableName, tableHeader, maxColumnSizes));

        foreach (var row in tableRows)
        {
            result.Add(BuildTableRow(row, maxColumnSizes));
        }

        return result;
    }

    /// <summary>
    /// Подсчитывает максимальную длину колонки таблицы на основании всех данных колонки (включая заголовок)
    /// </summary>
    /// <param name="tableHeader"></param>
    /// <param name="tableRows"></param>
    /// <returns></returns>
    private Dictionary<int, int> CalculateMaxColumnSizes(TableHeader tableHeader, List<TableRow<object>> tableRows)
    {
        var maxColumnSizes = new Dictionary<int, int>();
        var index = 0;

        tableHeader.Values.ForEach(tableColumnName =>
        {
            var maxColumnLength = tableColumnName.Length;

            tableRows.ForEach(tableRow =>
            {
                var value = tableRow.Values[index].ToString() ?? "null";

                maxColumnLength = Math.Max(maxColumnLength, value.Length);
            });

            maxColumnSizes.Add(index, maxColumnLength);

            index++;
        });

        return maxColumnSizes;
    }

    /// <summary>
    /// Формирует представление заголовка таблицы
    /// </summary>
    /// <param name="tableName"></param>
    /// <param name="tableHeader"></param>
    /// <param name="maxColumnSizes"></param>
    /// <returns></returns>
    private string BuildTableHeader(string tableName, TableHeader tableHeader, Dictionary<int, int> maxColumnSizes)
    {
        var result = new StringBuilder();
        var headerLine = BuildTableRow(tableHeader, maxColumnSizes);
        var spacesPlusVerticalBordersLength = 4; // "| column |"
        var padLeftLength = (headerLine.Length - tableName.Length - spacesPlusVerticalBordersLength) / 2;
        var padRightLength = headerLine.Length - padLeftLength - tableName.Length - spacesPlusVerticalBordersLength;

        result.AppendLine(
            $"{VerticalBorder}{"".PadLeft(padLeftLength, HorizontalBorder) + " " + tableName + " " + "".PadRight(padRightLength, HorizontalBorder)}{VerticalBorder}"
        );
        result.AppendLine(headerLine);
        result.Append("".PadRight(headerLine.Length, HorizontalBorder));

        return result.ToString();
    }

    /// <summary>
    /// Формирует представление строки таблицы
    /// </summary>
    /// <param name="row"></param>
    /// <param name="maxColumnSizes"></param>
    /// <returns></returns>
    private string BuildTableRow<T>(TableRow<T> row, Dictionary<int, int> maxColumnSizes)
    {
        var result = new StringBuilder();
        var index = 0;

        result.Append(VerticalBorder);

        foreach (var column in row.Values)
        {
            var maxColumnSize = maxColumnSizes[index];
            var columnValue = column?.ToString() ?? "null";

            result.Append($" {columnValue.PadRight(maxColumnSize)} ");
            result.Append(VerticalBorder);

            index++;
        }

        return result.ToString();
    }
}