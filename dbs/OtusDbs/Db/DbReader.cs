using Dapper;
using Npgsql;
using OtusDbs.Db.Structures;

namespace OtusDbs.Db;

public class DbReader
{
    private readonly string _schemaName;
    private readonly string _connectionString;

    public DbReader(string schemaName, string connectionString)
    {
        _schemaName = schemaName;
        _connectionString = connectionString;
    }

    /// <summary>
    /// Возвращает из БД таблицы с данными
    /// </summary>
    /// <returns></returns>
    public async Task<List<DbTable>> GetTablesData()
    {
        var result = new List<DbTable>();
        var tables = await GetTables();

        await using var sqlConnection = new NpgsqlConnection(_connectionString);

        foreach (var table in tables)
        {
            var query = $"SELECT * FROM \"{_schemaName}\".\"{table}\"";
            var dapperRows = await sqlConnection.QueryAsync(query, new { TableName = table });
            var dbTable = new DbTable
            {
                Name = table,
                Header = new TableHeader
                {
                    Values = ((IDictionary<string, object>)dapperRows.ToList().First()).Keys.ToList()
                },
                Rows = new()
            };

            foreach (var dapperRow in dapperRows.ToList())
            {
                var row = (IDictionary<string, object>)dapperRow;
                var dbRow = new TableRow<object>
                {
                    Values = new()
                };

                foreach (var column in row.Keys)
                {
                    dbRow.Values.Add(row[column]);
                }

                dbTable.Rows.Add(dbRow);
            }

            result.Add(dbTable);
        }

        return result;
    }

    /// <summary>
    /// Возвращает из БД имена таблиц
    /// </summary>
    /// <returns></returns>
    public async Task<List<string>> GetTables()
    {
        await using var sqlConnection = new NpgsqlConnection(_connectionString);

        var query = @"SELECT table_name FROM information_schema.tables WHERE table_schema = @SchemaName";
        var rows = await sqlConnection.QueryAsync<string>(query, new { SchemaName = _schemaName });

        return rows.ToList();
    }
}