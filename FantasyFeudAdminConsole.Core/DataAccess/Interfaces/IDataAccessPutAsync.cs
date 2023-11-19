using System.Threading.Tasks;

namespace FantasyFeudAdminConsole.Core.DataAccess;

public interface IDataAccessPutAsync
{
    Task<int> PutDataAsync(string queryOrStoredProcedure, bool isStoredProcedure, int? commandTimeout);
    async Task<int> PutDataAsync(string query) => await PutDataAsync(query, false, null);
    async Task<int> PutDataAsync(string queryOrStoredProcedure, bool isStoredProcedure) => await PutDataAsync(queryOrStoredProcedure, isStoredProcedure, null);
    async Task<int> PutDataAsync(string query, int? commandTimeout) => await PutDataAsync(query, false, commandTimeout);

    Task<int> PutDataAsync<T>(string queryOrStoredProcedure, T parameters, bool isStoredProcedure, int? commandTimeout);
    async Task<int> PutDataAsync<T>(string query, T parameters) => await PutDataAsync(query, parameters, false, null);
    async Task<int> PutDataAsync<T>(string queryOrStoredProcedure, T parameters, bool isStoredProcedure) => await PutDataAsync(queryOrStoredProcedure, parameters, isStoredProcedure, null);
    async Task<int> PutDataAsync<T>(string query, T parameters, int? commandTimeout) => await PutDataAsync(query, parameters, false, commandTimeout);
}