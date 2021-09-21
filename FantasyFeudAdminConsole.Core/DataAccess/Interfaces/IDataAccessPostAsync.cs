using System.Collections.Generic;
using System.Threading.Tasks;

namespace FantasyFeudAdminConsole.Core.DataAccess
{
    public interface IDataAccessPostAsync
    {
        Task<int> PostDataAsync(string queryOrStoredProcedure, bool isStoredProcedure, int? commandTimeout);
        async Task<int> PostDataAsync(string query) => await PostDataAsync(query, false, null);
        async Task<int> PostDataAsync(string queryOrStoredProcedure, bool isStoredProcedure) => await PostDataAsync(queryOrStoredProcedure, isStoredProcedure, null);
        async Task<int> PostDataAsync(string query, int? commandTimeout) => await PostDataAsync(query, false, commandTimeout);

        Task<int> PostDataAsync<T>(string queryOrStoredProcedure, T parameters, bool isStoredProcedure, int? commandTimeout);
        async Task<int> PostDataAsync<T>(string query, T parameters) => await PostDataAsync(query, parameters, false, null);
        async Task<int> PostDataAsync<T>(string queryOrStoredProcedure, T parameters, bool isStoredProcedure) => await PostDataAsync(queryOrStoredProcedure, parameters, isStoredProcedure, null);
        async Task<int> PostDataAsync<T>(string query, T parameters, int? commandTimeout) => await PostDataAsync(query, parameters, false, commandTimeout);

        Task<int> PostDataTransactionAsync<T>(string queryOrStoredProcedure, IEnumerable<T> data, bool isStoredProcedure, int? commmandTimeout);
        async Task<int> PostDataTransactionAsync<T>(string queryOrStoredProcedure, IEnumerable<T> data, bool isStoredProcedure) => await PostDataTransactionAsync(queryOrStoredProcedure, data, isStoredProcedure, null);
        async Task<int> PostDataTransactionAsync<T>(string queryOrStoredProcedure, IEnumerable<T> data, int? commandTimeout) => await PostDataTransactionAsync(queryOrStoredProcedure, data, false, commandTimeout);
        Task<int> PostDataTransactionForEachAsync<T>(string queryOrStoredProcedure, IEnumerable<T> data, bool isStoredProcedure, int? commmandTimeout);
        async Task<int> PostDataTransactionForEachAsync<T>(string queryOrStoredProcedure, IEnumerable<T> data, bool isStoredProcedure) => await PostDataTransactionForEachAsync(queryOrStoredProcedure, data, isStoredProcedure, null);
        async Task<int> PostDataTransactionForEachAsync<T>(string queryOrStoredProcedure, IEnumerable<T> data, int? commandTimeout) => await PostDataTransactionForEachAsync(queryOrStoredProcedure, data, false, commandTimeout);
    }
}