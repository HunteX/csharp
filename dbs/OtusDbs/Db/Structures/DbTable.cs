namespace OtusDbs.Db.Structures;

public class DbTable
{
    public string Name { get; init; }

    public TableHeader Header { get; init; }

    public List<TableRow<object>> Rows { get; init; }
}