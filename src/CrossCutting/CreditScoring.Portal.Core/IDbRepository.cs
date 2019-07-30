using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace CreditScoring.Portal.Core
{
    public interface IDbRepository
    {
        Task<IEnumerable<T>> QueryAsync<T>(string query, CommandType commandType);
        Task<IEnumerable<T>> QueryAsync<T>(string query, object param, CommandType commandType);
        Task<int> ExecuteAsync(string query, object param, CommandType commandType);
    }
}
