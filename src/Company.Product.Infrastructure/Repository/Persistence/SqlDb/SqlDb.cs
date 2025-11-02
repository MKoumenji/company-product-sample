using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Company.Product.Infrastructure.Repository.Persistence.SqlDb
{

    public class SqlDb(string? getConnectionString) : ISqlDb
    {
        private string? ConnectionString { get; set; } = getConnectionString;

        void IDisposable.Dispose()
           => GC.SuppressFinalize(this);
        
        public T? Update<T>(string sp, DynamicParameters? @params, CommandType commandType = CommandType.Text)
            => Execute<T>(sp, @params, commandType);
        
        public T? UpdateInTransaction<T>(string sp, DynamicParameters parms, IDbConnection connection, IDbTransaction transaction, CommandType commandType = CommandType.StoredProcedure)
            => ExecuteInTransaction<T>(sp, parms, connection, transaction, commandType);
        
        public T? Delete<T>(string sp, DynamicParameters? parms, CommandType commandType = CommandType.StoredProcedure)
            => Execute<T>(sp, parms, commandType);
        
        public T? DeleteInTransaction<T>(string sp, DynamicParameters parms, IDbConnection connection, IDbTransaction transaction, CommandType commandType = CommandType.StoredProcedure)
            => ExecuteInTransaction<T>(sp, parms, connection, transaction, commandType);
        
        public T? Insert<T>(string sp, DynamicParameters? parms, CommandType commandType = CommandType.StoredProcedure)
            =>  Execute<T>(sp, parms, commandType);
        
        public T? InsertInTransaction<T>(string sp, DynamicParameters parms, IDbConnection connection,
            IDbTransaction transaction, CommandType commandType = CommandType.StoredProcedure)
            => ExecuteInTransaction<T>(sp, parms, connection,transaction, commandType);
        private T? Execute<T>(string sp, DynamicParameters? parms, CommandType commandType = CommandType.StoredProcedure)
        {
            T? ret;
            using IDbConnection connection = new SqlConnection(ConnectionString);
            try
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                using var tran = connection.BeginTransaction();
                try
                {
                    ret = connection.Query<T>(sp, parms, commandType: commandType, transaction: tran).FirstOrDefault();
                    tran.Commit();
                }
                catch (Exception)
                {
                    tran.Rollback();
                    throw;
                }
                finally
                {
                    if (connection.State != ConnectionState.Closed)
                        connection.Close();
                }
            }
            finally
            {
                if (connection.State != ConnectionState.Closed)
                    connection.Close();
            }

            return ret;
        }
        public T? ExecuteInTransaction<T>(string sp, DynamicParameters parms, IDbConnection connection,
            IDbTransaction transaction, CommandType commandType = CommandType.StoredProcedure)
        {
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
                return connection.Query<T>(sp, parms, commandType: commandType, transaction: transaction).FirstOrDefault();
            }
            else
            {
                return connection.Query<T>(sp, parms, commandType: commandType, transaction: transaction).FirstOrDefault();
            }
        }
        
        public  void PerformUnitOfWorkTransaction (Action<IDbConnection, IDbTransaction> work, Action? onSuccess = null, Action<Exception>? onError = null)
        {
            using var db = new SqlConnection(ConnectionString);
            try
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();
                using var dbTransaction = db.BeginTransaction();
                try
                {
                    work(db, dbTransaction);
                    dbTransaction.Commit();
                    onSuccess?.Invoke();
                }
                catch (Exception ex)
                {
                    dbTransaction.Rollback();
                    onError?.Invoke(ex);
                }
            }
            catch (Exception ex)
            {
                onError?.Invoke(ex);
            }
            finally
            {
                if (db.State != ConnectionState.Closed)
                    db.Close();
            }
        }
        
        public List<T> GetAll<T>(string sp, DynamicParameters? @params, CommandType commandType = CommandType.StoredProcedure)  
        {
            List<T> retList;
            using IDbConnection db = new SqlConnection(ConnectionString);
            try
            {
                retList = db.Query<T>(sp, @params, commandType: commandType).ToList();
            }
            finally
            {
                if (db.State == ConnectionState.Open)
                    db.Close();
            }

            return retList; 
        }

    }  
}
