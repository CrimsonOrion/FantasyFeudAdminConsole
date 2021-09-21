using System.Threading.Tasks;

namespace FantasyFeudAdminConsole.Core.DataAccess
{
    public interface IDataAccessDeleteAsync
    {
        Task<int> DeleteDataAsync(string queryOrStoredProcedure, bool isStoredProcedure, int? commandTimeout);
        async Task<int> DeleteDataAsync(string query) => await DeleteDataAsync(query, false, null);
        async Task<int> DeleteDataAsync(string queryOrStoredProcedure, bool isStoredProcedure) => await DeleteDataAsync(queryOrStoredProcedure, isStoredProcedure, null);
        async Task<int> DeleteDataAsync(string query, int? commandTimeout) => await DeleteDataAsync(query, false, commandTimeout);

        Task<int> DeleteDataAsync<T>(string queryOrStoredProcedure, T parameters, bool isStoredProcedure, int? commandTimeout);
        async Task<int> DeleteDataAsync<T>(string query, T parameters) => await DeleteDataAsync(query, parameters, false, null);
        async Task<int> DeleteDataAsync<T>(string queryOrStoredProcedure, T parameters, bool isStoredProcedure) => await DeleteDataAsync(queryOrStoredProcedure, parameters, isStoredProcedure, null);
        async Task<int> DeleteDataAsync<T>(string query, T parameters, int? commandTimeout) => await DeleteDataAsync(query, parameters, false, commandTimeout);
    }
}