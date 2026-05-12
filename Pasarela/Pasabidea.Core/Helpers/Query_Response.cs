using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lantik.Pasabidea.Data
{
    public class ResponseBase<T>
    {
        public T Data { get; set; }
        public Query_Response Query_Result { get; set; }

        public ResponseBase()
        {
            Data = default(T);
            Query_Result = new Query_Response();
        }
    }
    public class Query_Response
    {
        public int RC { get; set; }
        public int SQLCode { get; set; }
        public string SQLState { get; set; }
        public string SQLMessage { get; set; }

        public Query_Response()
        {
            this.RC = 0;
            this.SQLCode = 0;
            this.SQLState = "";
            this.SQLMessage = "";
        }
        
        public bool Query_HasError()
        {
            return (this.SQLCode != 0);
        }

    }
}
