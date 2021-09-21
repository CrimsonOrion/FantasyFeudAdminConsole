using Dapper;

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Threading.Tasks;

namespace FantasyFeudAdminConsole.Core.DataAccess
{
    public class SQLiteDataAccess : IDataAccess
    {
        private static readonly string _connectionString = SQLiteConnectionString.ConnectionString;

        public async Task<int> DeleteDataAsync(string queryOrStoredProcedure, bool isStoredProcedure, int? commandTimeout) => await PostDataAsync(queryOrStoredProcedure, isStoredProcedure, commandTimeout);

        public async Task<int> DeleteDataAsync<T>(string queryOrStoredProcedure, T parameters, bool isStoredProcedure, int? commandTimeout) => await PostDataAsync(queryOrStoredProcedure, parameters, isStoredProcedure, commandTimeout);

        public async Task<IEnumerable<T>> GetDataAsync<T>(string queryOrStoredProcedure, bool isStoredProcedure, int? commandTimeout)
        {
            using IDbConnection conn = new SQLiteConnection(_connectionString);
            IEnumerable<T> rows = isStoredProcedure ?
                await conn.QueryAsync<T>(queryOrStoredProcedure, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout) :
                await conn.QueryAsync<T>(queryOrStoredProcedure, commandTimeout: commandTimeout);

            return rows;
        }

        public async Task<IEnumerable<T>> GetDataAsync<T, U>(string queryOrStoredProcedure, U parameters, bool isStoredProcedure, int? commandTimeout)
        {
            using IDbConnection conn = new SQLiteConnection(_connectionString);
            IEnumerable<T> rows = isStoredProcedure ?
                await conn.QueryAsync<T>(queryOrStoredProcedure, GetParameters(parameters), commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout) :
                await conn.QueryAsync<T>(queryOrStoredProcedure, commandTimeout: commandTimeout);

            return rows;
        }
        public async Task<int> PostDataAsync(string queryOrStoredProcedure, bool isStoredProcedure, int? commandTimeout)
        {
            using IDbConnection conn = new SQLiteConnection(_connectionString);
            return isStoredProcedure ?
                await conn.ExecuteAsync(queryOrStoredProcedure, commandType: CommandType.StoredProcedure) :
                await conn.ExecuteAsync(queryOrStoredProcedure, commandTimeout: commandTimeout);
        }

        public async Task<int> PostDataAsync<T>(string queryOrStoredProcedure, T data, bool isStoredProcedure, int? commandTimeout)
        {
            using IDbConnection conn = new SQLiteConnection(_connectionString);
            return isStoredProcedure ?
                await conn.ExecuteAsync(queryOrStoredProcedure, data, commandType: CommandType.StoredProcedure) :
                await conn.ExecuteAsync(queryOrStoredProcedure, data, commandTimeout: commandTimeout);
        }

        public async Task<int> PostDataTransactionAsync<T>(string queryOrStoredProcedure, IEnumerable<T> data, bool isStoredProcedure, int? commandTimeout)
        {
            using IDbConnection conn = new SQLiteConnection(_connectionString);
            conn.Open();
            IDbTransaction trans = conn.BeginTransaction();
            var affectedRecords = 0;
            try
            {
                affectedRecords += isStoredProcedure ?
                    await conn.ExecuteAsync(queryOrStoredProcedure, data, transaction: trans, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout) :
                    await conn.ExecuteAsync(queryOrStoredProcedure, data, transaction: trans, commandTimeout: commandTimeout);
            }
            catch (SQLiteException)
            {
                // Report the error in post
            }
            trans.Commit();
            return affectedRecords;
        }

        public async Task<int> PostDataTransactionForEachAsync<T>(string queryOrStoredProcedure, IEnumerable<T> data, bool isStoredProcedure, int? commandTimeout)
        {
            using IDbConnection conn = new SQLiteConnection(_connectionString);
            conn.Open();
            IDbTransaction trans = conn.BeginTransaction();
            var affectedRecords = 0;
            foreach (T item in data)
            {
                try
                {
                    affectedRecords += isStoredProcedure ?
                        await conn.ExecuteAsync(queryOrStoredProcedure, item, transaction: trans, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout) :
                        await conn.ExecuteAsync(queryOrStoredProcedure, item, transaction: trans, commandTimeout: commandTimeout);
                }
                catch (SQLiteException)
                {
                    // Report the error in post
                }
            }
            trans.Commit();
            return affectedRecords;
        }

        public async Task<int> PutDataAsync(string queryOrStoredProcedure, bool isStoredProcedure, int? commandTimeout) => await PostDataAsync(queryOrStoredProcedure, isStoredProcedure, commandTimeout);

        public async Task<int> PutDataAsync<T>(string queryOrStoredProcedure, T parameters, bool isStoredProcedure, int? commandTimeout) => await PostDataAsync(queryOrStoredProcedure, parameters, isStoredProcedure, commandTimeout);

        public bool TestConnection()
        {
            using IDbConnection conn = new SQLiteConnection(_connectionString);
            conn.Open();
            return conn.State == ConnectionState.Open;
        }

        private static DynamicParameters GetParameters<T>(T parameters)
        {
            if (parameters != null)
            {
                DynamicParameters p = new();

                if (parameters is IDictionary<string, dynamic> dictionary)
                {
                    foreach (KeyValuePair<string, dynamic> para in dictionary)
                    {
                        p.Add(para.Key, para.Value);
                    }
                }
                else if (parameters is DynamicParameters)
                {
                    p = new DynamicParameters(parameters);
                }

                return p;
            }
            else
            {
                return null;
            }
        }
    }
}