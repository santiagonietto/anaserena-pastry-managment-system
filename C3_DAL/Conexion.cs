using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using C4_ENTIDAD;
using System.Data;
using System.Data.SqlClient;

namespace C3_DAL
{
    public class Conexion
    {
        public string connectionString = "data source=compu; initial catalog=anaserena_pms_db; integrated security=sspi";

        public SqlConnection ObtenerConxeion()
        {
            return new SqlConnection(connectionString);
        }


    }
}
