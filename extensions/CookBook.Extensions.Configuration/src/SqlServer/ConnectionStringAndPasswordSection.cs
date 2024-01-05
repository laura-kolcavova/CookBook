namespace CookBook.Extensions.Configuration.SqlServer;

public class ConnectionStringAndPasswordSection
{
    public required string ConnectionString { get; set; }

    public required string Password { get; set; }
}
