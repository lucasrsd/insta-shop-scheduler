using System;
using System.IO;
using System.Threading.Tasks;
using System.Security.Authentication;
using System.Data.SqlClient;
using Dapper;
using System.Collections.Generic;

namespace InstaShop.DbContext.Base
{
    public class BaseDbContext
    {
        public IEnumerable<T> ObterListagem<T>(string query)
        {
            return ObterListagem<T>(query, new { });
        }

        public IEnumerable<T> ObterListagem<T>(string query, object values)
        {
            using (var sqlInstance = new SqlConnection(Environment.GetEnvironmentVariable("DbInstaRobo")))
            {
                var result = sqlInstance.Query<T>(query, values, commandTimeout: 180);
                return result;
            }
        }

        public T ObterObjeto<T>(string query, object values)
        {
            using (var sqlInstance = new SqlConnection(Environment.GetEnvironmentVariable("DbInstaRobo")))
            {
                var result = sqlInstance.QueryFirstOrDefault<T>(query, values, commandTimeout: 180);
                return result;
            }
        }

        public void Executar(string query, object values)
        {
            using (var sqlInstance = new SqlConnection(Environment.GetEnvironmentVariable("DbInstaRobo")))
            {
                sqlInstance.Query(query, values, commandTimeout: 180);
            }
        }
    }
}
