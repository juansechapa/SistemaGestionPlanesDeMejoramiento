using sistemaGestionPlanesDeMejoramiento.Datos;
using sistemaGestionPlanesDeMejoramiento.Modelo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace sistemaGestionPlanesDeMejoramiento.datos
{
    public class ClPasswordResetD
    {
        ClConexion cn = new ClConexion();

        //Busca al usuario por su correo
        public ClUsuario obtenerUsuarioPorCorreo(string correo)
        {
            ClUsuario usuario = null;

            try
            {
                SqlCommand cmd = new SqlCommand(
                    @"SELECT TOP 1 idUsuario, username, idRol
                      FROM (
                          SELECT u.idUsuario, u.username, u.idRol
                          FROM usuarios u
                          INNER JOIN administrador a ON a.idUsuario = u.idUsuario
                          WHERE a.correo = @correo

                          UNION ALL

                          SELECT u.idUsuario, u.username, u.idRol
                          FROM usuarios u
                          INNER JOIN instructor i ON i.idUsuario = u.idUsuario
                          WHERE i.correo = @correo

                          UNION ALL

                          SELECT u.idUsuario, u.username, u.idRol
                          FROM usuarios u
                          INNER JOIN aprendiz ap ON ap.idUsuario = u.idUsuario
                          WHERE ap.correo = @correo
                      ) usuariosCorreo",
                    cn.MtAbrirConexion());
                cmd.Parameters.AddWithValue("@correo", correo);

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    usuario = new ClUsuario
                    {
                        idUsuario = Convert.ToInt32(dr["idUsuario"]),
                        username = dr["username"].ToString(),
                        idRol = Convert.ToInt32(dr["idRol"])
                    };
                }
                dr.Close();
            }
            finally
            {
                cn.MtCerrarConexion();
            }
            return usuario;
        }

        //Guarda codigo de verificacion
        public bool guardarCodigo(int idUsuario, string codigo, DateTime expiracion)
        {
            try
            {
                SqlConnection conexion = cn.MtAbrirConexion();

                SqlCommand cmdInvalidar = new SqlCommand(
                    "UPDATE passwordReset SET usado = 1 WHERE idUsuario = @idUsuario AND usado = 0",
                    conexion);
                cmdInvalidar.Parameters.AddWithValue("@idUsuario", idUsuario);
                cmdInvalidar.ExecuteNonQuery();

                SqlCommand cmd = new SqlCommand(
                    "INSERT INTO passwordReset (codigo, expiracionDate, usado, idUsuario) VALUES (@codigo, @expiracion, 0, @idUsuario)",
                    conexion);
                cmd.Parameters.Add("@codigo", SqlDbType.VarBinary).Value = CodigoABytes(codigo);
                cmd.Parameters.AddWithValue("@expiracion", expiracion);
                cmd.Parameters.AddWithValue("@idUsuario", idUsuario);
                return cmd.ExecuteNonQuery() > 0;
            }
            finally
            {
                cn.MtCerrarConexion();
            }
        }

        public bool validarCodigo(string codigo, out int idUsuario)
        {
            idUsuario = 0;
            try
            {
                SqlCommand cmd = new SqlCommand(
                    @"SELECT TOP 1 idUsuario
                      FROM passwordReset
                      WHERE codigo = @codigo
                        AND expiracionDate > GETDATE()
                        AND usado = 0
                      ORDER BY expiracionDate DESC",
                    cn.MtAbrirConexion());
                cmd.Parameters.Add("@codigo", SqlDbType.VarBinary).Value = CodigoABytes(codigo);
                object resultado = cmd.ExecuteScalar();
                if (resultado != null)
                {
                    idUsuario = Convert.ToInt32(resultado);
                    return true;
                }
                return false;
            }
            finally
            {
                cn.MtCerrarConexion();
            }
        }

        public bool MarcarCodigoComoUsado(string codigo)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(
                    @"UPDATE passwordReset SET usado = 1 WHERE codigo = @codigo",
                    cn.MtAbrirConexion());
                cmd.Parameters.Add("@codigo", SqlDbType.VarBinary).Value = CodigoABytes(codigo);
                return cmd.ExecuteNonQuery() > 0;
            }
            finally { cn.MtCerrarConexion(); }
        }

        public bool ActualizarPassword(int idUsuario, string nuevoPasswordHash)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(
                    @"UPDATE usuarios SET password = @pwd WHERE idUsuario = @id",
                    cn.MtAbrirConexion());
                cmd.Parameters.AddWithValue("@pwd", nuevoPasswordHash);
                cmd.Parameters.AddWithValue("@id", idUsuario);
                return cmd.ExecuteNonQuery() > 0;
            }
            finally { cn.MtCerrarConexion(); }
        }

        private byte[] CodigoABytes(string codigo)
        {
            return Encoding.UTF8.GetBytes((codigo ?? string.Empty).Trim().ToUpperInvariant());
        }

    }
}
