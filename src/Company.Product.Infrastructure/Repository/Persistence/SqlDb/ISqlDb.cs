using System.Data;
using Dapper;

namespace Company.Product.Infrastructure.Repository.Persistence.SqlDb
{
    /// <summary>
    /// This Interface defines the methods for accessing the data base
    /// </summary>
    public interface ISqlDb : IDisposable
    {

        /// <summary>
        /// This Method update data 
        /// </summary>
        /// <param name="sp"> a stored procedure or an SQL command</param>
        /// <param name="params">parameters as DynamicParameters</param>
        /// <param name="commandType">Type of the command</param>
        /// <returns>Generic Value</returns>
       public  T? Update<T>(string sp, DynamicParameters? @params, CommandType commandType = CommandType.StoredProcedure);

        /// <summary>
        /// This Method update data in transaction 
        /// </summary>
        /// <param name="sp"> a stored procedure or an SQL command</param>
        /// <param name="parms">parameters as DynamicParameters</param>
        /// <param name="connection">Database connection </param>
        /// <param name="transaction">Database Transaction</param>
        /// <param name="commandType">Type of the command</param>
        /// <returns>Generic Value</returns>
        public T? UpdateInTransaction<T>(string sp, DynamicParameters parms, IDbConnection connection,
            IDbTransaction transaction, CommandType commandType = CommandType.StoredProcedure);


        /// <summary>
        /// This Method delete data 
        /// </summary>
        /// <param name="sp"> a stored procedure or an SQL command</param>
        /// <param name="parms">parameters as DynamicParameters</param>
        /// <param name="commandType">Type of the command</param>
        /// <returns>Generic Value</returns>
        public T? Delete<T>(string sp, DynamicParameters? parms, CommandType commandType = CommandType.StoredProcedure);

        /// <summary>
        /// TThis Method delete data  in transaction 
        /// </summary>
        /// <param name="sp"> a stored procedure or an SQL command</param>
        /// <param name="parms">parameters as DynamicParameters</param>
        /// <param name="connection">Database connection </param>
        /// <param name="transaction">Database Transaction</param>
        /// <param name="commandType">Type of the command</param>
        /// <returns>Generic Value</returns>
        public T? DeleteInTransaction<T>(string sp, DynamicParameters parms, IDbConnection connection,
            IDbTransaction transaction, CommandType commandType = CommandType.StoredProcedure);


        /// <summary>
        /// This Method inserts data 
        /// </summary>
        /// <param name="sp"> a stored procedure or an SQL command</param>
        /// <param name="parms">parameters as DynamicParameters</param>
        /// <param name="commandType">Type of the command</param>
        /// <returns>Generic Value</returns>
        public T? Insert<T>(string sp, DynamicParameters? parms, CommandType commandType = CommandType.StoredProcedure);


        /// <summary>
        /// This Method inserts data  (in transaction)
        /// </summary>
        /// <param name="sp"> a stored procedure or an SQL command</param>
        /// <param name="parms">parameters as DynamicParameters</param>
        /// <param name="connection">Database connection </param>
        /// <param name="transaction">Database Transaction</param>
        /// <param name="commandType">Type of the command</param>
        public T? InsertInTransaction<T>(string sp, DynamicParameters parms, IDbConnection connection,
            IDbTransaction transaction, CommandType commandType = CommandType.StoredProcedure);


        /// <summary>
        /// This method execute a passed action (in transaction)
        /// </summary>
        /// <param name="work"> action(s) to be executed in a transaction </param>
        /// <param name="onSuccess">action(s) to execute on success</param>
        /// <param name="onError">action(s) to execute on error </param>
        public void PerformUnitOfWorkTransaction(Action<IDbConnection, IDbTransaction> work, Action? onSuccess = null, Action<Exception> onError = null!);

   
        /// <summary>
        /// This Method gets a list of data from the data base
        /// </summary>
        /// <param name="sp"> a stored procedure or an SQL command</param>
        /// <param name="params">parameters as DynamicParameters</param>
        /// <param name="commandType">Type of the command</param>
        /// <returns>Generic List of generic Value</returns>
        List<T> GetAll<T>(string sp, DynamicParameters? @params, CommandType commandType = CommandType.StoredProcedure);
    }
}
