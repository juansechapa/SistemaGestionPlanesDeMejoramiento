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


        public SqlTransaction IniciarTransaccion()
        {
            _transaccion = oConex.BeginTransaction();
            return _transaccion;
        }

        public void ConfirmarTransaccion()
        {
            _transaccion?.Commit();
            _transaccion = null;
        }

        public void RevertirTransaccion()
        {
            _transaccion?.Rollback();
            _transaccion = null;
        }
    }
}