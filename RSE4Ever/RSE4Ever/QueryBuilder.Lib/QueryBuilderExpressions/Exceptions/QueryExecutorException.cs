using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace RSE4Ever.QueryBuilder.Lib.QueryBuilderExpressions.Exceptions
{
    public class QueryExecutorException:Exception
    {
        public QueryExecutorException()
        {
        }

        public QueryExecutorException(string message) : base(message)
        {
        }

        public QueryExecutorException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected QueryExecutorException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}