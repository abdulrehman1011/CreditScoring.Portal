using CreditScoring.Portal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CreditScoring.Portal.Services
{
    public interface IHttpClientHelper
    {
        Task<T> GetSingleItemRequest<T>(string apiUrl, CancellationToken token = default(CancellationToken));
        Task<Tuple<string, ApiModel>> GetSingleItemRequest(string apiUrl, CancellationToken token = default(CancellationToken));
        Task<T[]> GetMultipleItemsRequest<T>(string apiUrl, CancellationToken token = default(CancellationToken));
        Task<T> PostRequest<T>(string apiUrl, T postObject, CancellationToken token = default(CancellationToken));
        Task PutRequest<T>(string apiUrl, T putObject, CancellationToken token = default(CancellationToken));
        Task DeleteRequest<T>(string apiUrl, CancellationToken token = default(CancellationToken));
    }
}
