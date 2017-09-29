using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Canalex.AWS.Data
{
    public interface ISQLConnection
    {
        void OpenConnection();
    }

    public class SQLConnection : ISQLConnection,IDisposable
    {
        public SqlConnection conn = null;
        public const string connetionString = "Data Source=supernova.cevuurdyogk9.us-east-1.rds.amazonaws.com;Initial Catalog=DTCN;User ID=supernova;Password=supernova";


        public void Dispose()
        {
            conn.Dispose();
        }

        //Open Connection 
        public void OpenConnection()
        {
            conn = new SqlConnection(connetionString);
            try
            {
                if (conn.State == System.Data.ConnectionState.Closed)
                {
                    conn.Open();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
