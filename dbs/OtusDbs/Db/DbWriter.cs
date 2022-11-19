using System.Text;
using Dapper;
using Npgsql;

namespace OtusDbs.Db;

public class DbWriter
{
    private readonly string _schemaName;
    private readonly string _connectionString;

    public DbWriter(string schemaName, string connectionString)
    {
        _schemaName = schemaName;
        _connectionString = connectionString;
    }

    /// <summary>
    /// Добавляет запись в таблицу
    /// </summary>
    /// <param name="tableName"></param>
    /// <param name="row"></param>
    public async Task AddRowAsync(string tableName, Dictionary<string, object> row)
    {
        PrepareKeysValuesAndParams(row, out string keys, out string values, out Dictionary<string, object> queryParams);

        var query = $"INSERT INTO \"{_schemaName}\".\"{tableName}\" ({keys}) VALUES ({values})";

        await using var sqlConnection = new NpgsqlConnection(_connectionString);
        await sqlConnection.ExecuteAsync(query, queryParams);
    }

    /// <summary>
    /// Формирует колонки, их значения, а также параметры для Dapper
    /// </summary>
    /// <param name="row"></param>
    /// <param name="keys"></param>
    /// <param name="values"></param>
    /// <param name="queryParams"></param>
    private void PrepareKeysValuesAndParams(
        Dictionary<string, object> row,
        out string keys,
        out string values,
        out Dictionary<string, object> queryParams
    )
    {
        queryParams = new Dictionary<string, object>();
        var keysSb = new StringBuilder();
        var valuesSb = new StringBuilder();
        var index = 0;

        foreach (var rowKey in row.Keys)
        {
            keysSb.Append($"\"{rowKey}\",");
            valuesSb.Append($"@Param{index},");
            queryParams.Add($"Param{index}", row[rowKey]);
            index++;
        }

        keys = keysSb.ToString().TrimEnd(',');
        values = valuesSb.ToString().TrimEnd(',');
    }
}