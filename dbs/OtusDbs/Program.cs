using Microsoft.Extensions.Configuration;
using OtusDbs.Db;
using OtusDbs.Interaction;

IConfiguration config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddEnvironmentVariables()
    .Build();

var schemaName = config.GetSection("SchemaName").Value!;
var connectionString = config.GetSection("ConnectionString").Value!;

var reader = new DbReader(schemaName, connectionString);
var writer = new DbWriter(schemaName, connectionString);

var interceptor = new ConsoleUserInteractor(reader);
var printer = new ConsolePrettyPrinter();

interceptor.AnyKeyPressed += () => { printer.PrintInfo(); };

interceptor.TKeyPressed += async () =>
{
    var tables = await reader.GetTablesAsync();

    printer.PrintTableList(tables);
};

interceptor.DKeyPressed += async () =>
{
    var tablesWithData = await reader.GetTablesDataAsync();

    printer.PrintTablesWithData(tablesWithData);
};

interceptor.TableRowAdded += async (tableName, rowData) =>
{
    await writer.AddRowAsync(tableName, rowData);
    printer.PrintRowAddedSuccessfully(tableName);
};

await interceptor.ShowMainMenuAsync();