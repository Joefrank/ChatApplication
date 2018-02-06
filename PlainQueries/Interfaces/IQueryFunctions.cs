
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


namespace PlainQueries.Interfaces
{
    public interface IQueryFunctions
    {
        DataSet DataSetFromStoredProcedure(string storedProcedureName, IEnumerable<SqlParameter> parameters);

        DataTable DataTableFromStoredProcedure(string storedProcedureName, IEnumerable<SqlParameter> parameters);
        DataTable DataTableFromStoredProcedure(string storedProcedureName, SqlParameter param);
        DataTable DataTableFromStoredProcedure(string storedProcedureName, string paramName, string paramValue);
        DataTable DataTableFromStoredProcedure(string storedProcedureName);

        DataRow DataRowFromStoredProcedure(string storedProcedureName, IEnumerable<SqlParameter> parameters);
        DataRow DataRowFromStoredProcedure(string storedProcedureName, string paramName, string paramValue);
        DataRow DataRowFromStoredProcedure(string storedProcedureName);

        int ExecuteNonQueryStoredProc(string storedProcedureName, IEnumerable<SqlParameter> parameters);
        int ExecuteNonQueryStoredProc(string storedProcedureName, SqlParameter parameter);
        int ExecuteNonQueryStoredProc(string storedProcedureName);
    }
}
