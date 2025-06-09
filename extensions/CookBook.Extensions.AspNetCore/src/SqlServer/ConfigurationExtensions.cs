using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace CookBook.Extensions.AspNetCore.SqlServer;

public static class ConfigurationExtensions
{
    public static string GetSqlConnectionString(this IConfiguration configuration, string sectionName, int? connectTimeout = 1)
    {
        ArgumentNullException.ThrowIfNull(configuration);

        if (string.IsNullOrWhiteSpace(sectionName))
        {
            throw new ArgumentNullException(nameof(sectionName));
        }

        var connectionStringAndPasswordSection = configuration
            .GetRequiredSection(sectionName)
            .Get<ConnectionStringAndPasswordSection>()!;

        var sqlConnectionStringBuilder = new SqlConnectionStringBuilder(connectionStringAndPasswordSection.ConnectionString);

        if (connectTimeout.HasValue && sqlConnectionStringBuilder.ConnectTimeout == 15)
        {
            sqlConnectionStringBuilder.ConnectTimeout = connectTimeout.Value;
        }

        if (!string.IsNullOrWhiteSpace(connectionStringAndPasswordSection.Password))
        {
            sqlConnectionStringBuilder.Password = connectionStringAndPasswordSection.Password;
        }

        if (sqlConnectionStringBuilder.Encrypt.Equals(SqlConnectionEncryptOption.Mandatory) && !(connectionStringAndPasswordSection.ConnectionString?.Contains("Encrypt=True") ?? false))
        {
            sqlConnectionStringBuilder.Encrypt = SqlConnectionEncryptOption.Optional;
        }

        return sqlConnectionStringBuilder.ConnectionString;
    }
}

