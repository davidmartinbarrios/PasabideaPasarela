using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pasabidea.Core.Entities.POCOs
{
    public class SP_Response
    {
        public DataTable Data { get; set; }
        public int SQLCode { get; set; }
        public string SQLMessage { get; set; }

    }
}
