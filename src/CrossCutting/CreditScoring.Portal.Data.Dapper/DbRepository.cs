using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace CreditScoring.Portal.Core.Data.Dapper
{
    public class DbRepository : IDbRepository
    {
        private readonly string _connectionString;

        public DbRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        private SqlConnection GetDbConnection()
        {
            return new SqlConnection(_connectionString);
        }
        public async Task<IEnumerable<T>> QueryAsync<T>(string query, CommandType commandType)
        {
            using (var connection = GetDbConnection())
            {
                connection.Open();
                var result = await connection.QueryAsync<T>(query, commandType);
                return result;
            }
                
        }
        public async Task<IEnumerable<T>> QueryAsync<T>(string query, object param, CommandType commandType)
        {
            using (var connection = GetDbConnection())
            {
                connection.Open();
                var result = await connection.QueryAsync<T>(query, param,null, null, commandType);
                return result;
            }

        }
        public async Task<int> ExecuteAsync(string query, object param,CommandType commandType)
        {
            using (var connection = GetDbConnection())
            {
                connection.Open();
                var result = await connection.ExecuteAsync(query, param, null,null, commandType);
                return result;
            }

        }
    }
}
