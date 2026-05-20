using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace sistemaGestionPlanesDeMejoramiento.Datos
{
    public class ClConexion
    {
            SqlConnection oConex = new SqlConnection("Data Source=DESKTOP-9VCLAG1\\SQLEXPRESS;Initial Catalog=SistemaGestionPlanes;Integrated Security=True;");
           
            public SqlConnection MtAbrirConexion()
            {
                oConex.Open();
                return oConex;
            }
            public void MtCerrarConexion()
            {
                oConex.Close();
            }
        
    }
}