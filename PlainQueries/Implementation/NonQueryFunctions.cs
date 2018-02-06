using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using PlainQueries.Interfaces;

namespace PlainQueries.Implementation
{
    public class NonQueryFunctions : INonQueryFunctions
    {
        private readonly string _connString;

        public NonQueryFunctions(string connString)
        {
            _connString = connString;
        }

        public int InsertUpdateStoredProcedure(string storedProcedureName, IEnumerable<SqlParameter> parameters)
        {
            try
            {
                var result = 0;

                using (var conn = new SqlConnection(_connString))
                {
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = storedProcedureName;
                        cmd.CommandType = CommandType.StoredProcedure;
                        
                        //load params
                        foreach (var parameter in parameters)
                        {
                            cmd.Parameters.Add(parameter);
                        }

                        //execute
                        conn.Open();
                        result = cmd.ExecuteNonQuery();                        
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
    }
}
