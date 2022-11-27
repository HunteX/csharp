using Dapper;
using Npgsql;
using WebApi.Models;

namespace WebApi.Database;

public class DbManager
{
    private readonly string _schemaName;
    private readonly string _connectionString;

    public DbManager(IConfiguration configuration)
    {
        _schemaName = configuration.GetSection("SchemaName").Value!;
        _connectionString = configuration.GetSection("ConnectionString").Value!;
    }

    public async Task<Customer?> GetCustomerByIdAsync(long id)
    {
        await using var sqlConnection = new NpgsqlConnection(_connectionString);

        return await sqlConnection.QueryFirstOrDefaultAsync<Customer?>(
            $"SELECT id, firstname, lastname FROM \"{_schemaName}\".\"Customers\" WHERE id = @Id",
            new { Id = id }
        );
    }

    public async Task<Customer> CreateCustomerAsync(long id, string firstname, string lastname)
    {
        await using var sqlConnection = new NpgsqlConnection(_connectionString);

        var query =
            $"INSERT INTO \"{_schemaName}\".\"Customers\" (id, firstname, lastname) VALUES (@Id, @Firstname, @Lastname)";

        await sqlConnection.ExecuteScalarAsync<Customer>(
            query,
            new { Id = id, Firstname = firstname, Lastname = lastname }
        );

        return GetCustomerByIdAsync(id).Result!;
    }
}