using sistemaGestionPlanesDeMejoramiento.Datos;
using sistemaGestionPlanesDeMejoramiento.Modelo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace sistemaGestionPlanesDeMejoramiento.datos
{
    public class ClUsuarioD
    {
        ClConexion cn = new ClConexion();

        public ClUsuario Login (string username, string password)
        {
            ClUsuario objUsuario = null;

            SqlCommand cmd = new SqlCommand("SELECT idUsuario, username, idRol FROM usuarios WHERE username = @username AND password = @password ", cn.MtAbrirConexion());

            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@password", password);

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                objUsuario = new ClUsuario()
                {
                    idUsuario = dr.GetInt32(0),
                    username = dr.GetString(1),
                    idRol = dr.GetInt32(2),
                };
            }
            dr.Close();
            cn.MtCerrarConexion();
            return objUsuario;
        }
    }
}