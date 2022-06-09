// Copyright (c) xxx, 2022. All rights reserved.

using System.Data;
using Microsoft.Data.SqlClient;

namespace Platform.Services
{
    public static class SqlConnectionExtensions
    {
        public static IServiceCollection AddDbConnection(this IServiceCollection services, string dbConnectionString)
        {
            return services.AddScoped<IDbConnection>(t => new SqlConnection(dbConnectionString));
        }
    }
}
