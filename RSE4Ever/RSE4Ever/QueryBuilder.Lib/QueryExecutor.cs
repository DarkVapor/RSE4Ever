using DataTablesMapping.Lib.Mapping;
using RSE4Ever.DataTablesMapping.Lib;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Configuration;
using RSE4Ever.QueryBuilder.Lib.QueryBuilderExpressions.Exceptions;

namespace RSE4Ever.QueryBuilder.Lib
{
    public class QueryExecutor<T> where T : IBaseEntity, new()
    {
        private static volatile QueryExecutor<T> instance;
        private static object syncRoot = new object();
        Mapper<T> mapper;
        T entity;
        StringBuilder sb;
        public static QueryExecutor<T> Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new QueryExecutor<T>();
                        }
                    }
                }
                return instance;
            }
        }
        private QueryExecutor() { }

        public List<T> Execute(StringBuilder QueryString)
        {
            List<T> EntityList = new List<T>();
            string cs = System.Configuration.ConfigurationManager.ConnectionStrings["RSEConnectionString"].ConnectionString;

            DataTable dt = null;
            KeyValuePair<int, DataTable> kvp = new KeyValuePair<int, DataTable>(0, dt);

            using (SqlConnection connection = new SqlConnection(cs))
            using (SqlCommand command = connection.CreateCommand())
            {
                string s = QueryString.ToString().Substring(0, 1).ToLower();
                command.CommandText = QueryString.ToString();

                if (s == "i")
                {
                    var id = 0;
                    try
                    {
                        connection.Open();
                        id =(int) command.ExecuteScalar();
                        connection.Close();
                    }
                    catch (System.Data.SqlClient.SqlException ex)
                    {
                        string str;
                        str = "Source:" + ex.Source;
                        str += "\n" + "Message:" + ex.Message;
                        throw new QueryExecutorException(str);
                    }

                    kvp = new KeyValuePair<int, DataTable>(Int32.Parse(id.ToString()), dt);
                }
                else if (s == "u" || s == "d")
                {
                    try
                    {
                        connection.Open();
                        command.ExecuteScalar();
                        connection.Close();
                    }
                    catch (System.Data.SqlClient.SqlException ex)
                    {
                        string str;
                        str = "Source:" + ex.Source;
                        str += "\n" + "Message:" + ex.Message;
                        throw new QueryExecutorException(str);
                    }
                    int id = 0;
                    kvp = new KeyValuePair<int, DataTable>(id, dt);
                }
                else
                {
                    dt = new DataTable();
                    using (SqlDataAdapter sda = new SqlDataAdapter(command))
                    {
                        sda.Fill(dt);
                    }
                }
                if (dt != null)
                {
                    mapper = new Mapper<T>();
                    EntityList = mapper.Map(dt);
                }
            }
            return EntityList;
        }

        public DataTable GetDataTable(StringBuilder QueryString)
        {

            string cs = System.Configuration.ConfigurationManager.ConnectionStrings["RSEConnectionString"].ConnectionString;

            DataTable dt = null;
            KeyValuePair<int, DataTable> kvp = new KeyValuePair<int, DataTable>(0, dt);

            using (SqlConnection connection = new SqlConnection(cs))
            using (SqlCommand command = connection.CreateCommand())
            {
                string s = QueryString.ToString().Substring(0, 1).ToLower();
                command.CommandText = QueryString.ToString();

                if (s == "i" || s == "u" || s == "d")
                {
                    var id = 0;
                    try
                    {
                        connection.Open();
                        id = (int)command.ExecuteScalar();
                        connection.Close();
                    }
                    catch (System.Data.SqlClient.SqlException ex)
                    {
                        string str;
                        str = "Source:" + ex.Source;
                        str += "\n" + "Message:" + ex.Message;
                        throw new QueryExecutorException(str);
                    }
                    kvp = new KeyValuePair<int, DataTable>(id, dt);
                }
                else
                {
                    dt = new DataTable();
                    using (SqlDataAdapter sda = new SqlDataAdapter(command))
                    {
                        sda.Fill(dt);
                    }
                }
            }
            return dt;
        }
    }
}