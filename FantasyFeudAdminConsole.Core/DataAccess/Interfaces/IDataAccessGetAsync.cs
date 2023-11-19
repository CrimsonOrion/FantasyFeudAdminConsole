using System.Collections.Generic;
using System.Threading.Tasks;

namespace FantasyFeudAdminConsole.Core.DataAccess;

public interface IDataAccessGetAsync
{
    Task<IEnumerable<T>> GetDataAsync<T>(string queryOrStoredProcedure, bool isStoredProcedure, int? commandTimeout);
    async Task<IEnumerable<T>> GetDataAsync<T>(string query) => await GetDataAsync<T>(query, false, null);
    async Task<IEnumerable<T>> GetDataAsync<T>(string queryOrStoredProcedure, bool isStoredProcedure) => await GetDataAsync<T>(queryOrStoredProcedure, isStoredProcedure, null);
    async Task<IEnumerable<T>> GetDataAsync<T>(string query, int? commandTimeout) => await GetDataAsync<T>(query, false, commandTimeout);

    Task<IEnumerable<T>> GetDataAsync<T, U>(string queryOrStoredProcedure, U parameters, bool isStoredProcedure, int? commandTimeout);
    async Task<IEnumerable<T>> GetDataAsync<T, U>(string query, U parameters) => await GetDataAsync<T, U>(query, parameters, false, null);
    async Task<IEnumerable<T>> GetDataAsync<T, U>(string queryOrStoredProcedure, U parameters, bool isStoredProcedure) => await GetDataAsync<T, U>(queryOrStoredProcedure, parameters, isStoredProcedure, null);
    async Task<IEnumerable<T>> GetDataAsync<T, U>(string query, U parameters, int? commandTimeout) => await GetDataAsync<T, U>(query, parameters, false, commandTimeout);
}