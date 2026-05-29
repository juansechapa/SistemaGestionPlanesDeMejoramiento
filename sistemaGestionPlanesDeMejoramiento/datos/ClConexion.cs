using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace sistemaGestionPlanesDeMejoramiento.Datos
{
    public class ClConexion
    {
        private SqlTransaction _transaccion;

        SqlConnection oConex = new SqlConnection("Data Source=PlanMejora.mssql.somee.com;Initial Catalog=PlanMejora;User ID=EPSILON_SQLLogin_1;Password=aqynstjxvu;TrustServerCertificate=True;");

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