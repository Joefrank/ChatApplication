
using System.Collections.Generic;
using System.Data.SqlClient;

namespace PlainQueries.Interfaces
{
    public interface INonQueryFunctions
    {
        int InsertUpdateStoredProcedure(string storedProcedureName, IEnumerable<SqlParameter> parameters);
    }
}
