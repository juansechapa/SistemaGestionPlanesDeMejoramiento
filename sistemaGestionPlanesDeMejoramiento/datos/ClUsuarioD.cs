using sistemaGestionPlanesDeMejoramiento.Datos;
using sistemaGestionPlanesDeMejoramiento.Modelo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;

namespace sistemaGestionPlanesDeMejoramiento.datos
{
    public class ClUsuarioD
    {
        ClConexion cn = new ClConexion();

        public static string HashPassword(string password)
        {
            if (password == null) password = "";

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return "SHA256:" + Convert.ToBase64String(bytes);
            }
        }

        private static bool EsHashPassword(string password)
        {
            return !string.IsNullOrWhiteSpace(password) && password.StartsWith("SHA256:", StringComparison.Ordinal);
        }

        public ClUsuario Login (string username, string password)
        {
            ClUsuario objUsuario = null;
            string passwordGuardada = null;
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT idUsuario, username, password, idRol FROM usuarios WHERE username = @username", cn.MtAbrirConexion());

                cmd.Parameters.AddWithValue("@username", username);

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    passwordGuardada = dr["password"].ToString();
                    string passwordHash = HashPassword(password);
                    bool passwordCorrecta = passwordGuardada == passwordHash || passwordGuardada == password;

                    if (passwordCorrecta)
                    {
                        objUsuario = new ClUsuario()
                        {
                            idUsuario = Convert.ToInt32(dr["idUsuario"]),
                            username = dr["username"].ToString(),
                            idRol = Convert.ToInt32(dr["idRol"]),
                        };
                    }
                }
                dr.Close();
            }
            finally
            {
                cn.MtCerrarConexion();
            }

            if (objUsuario != null && !EsHashPassword(passwordGuardada))
                ActualizarCredenciales(objUsuario.idUsuario, objUsuario.username, password);

            return objUsuario;
        }

        public ClUsuario ObtenerUsuarioPorId(int idUsuario)
        {
            ClUsuario usuario = null;
            try
            {
                SqlCommand cmd = new SqlCommand(
                    "SELECT idUsuario, username, idRol FROM usuarios WHERE idUsuario = @idUsuario",
                    cn.MtAbrirConexion());
                cmd.Parameters.AddWithValue("@idUsuario", idUsuario);

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

        public bool ActualizarCredenciales(int idUsuario, string username, string password)
        {
            try
            {
                string sql = string.IsNullOrWhiteSpace(password)
                    ? "UPDATE usuarios SET username = @username WHERE idUsuario = @idUsuario"
                    : "UPDATE usuarios SET username = @username, password = @password WHERE idUsuario = @idUsuario";

                SqlCommand cmd = new SqlCommand(sql, cn.MtAbrirConexion());
                cmd.Parameters.AddWithValue("@idUsuario", idUsuario);
                cmd.Parameters.AddWithValue("@username", username);
                if (!string.IsNullOrWhiteSpace(password))
                    cmd.Parameters.AddWithValue("@password", HashPassword(password));

                return cmd.ExecuteNonQuery() > 0;
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627 || ex.Number == 2601)
                    throw new Exception("El nombre de usuario ya existe. Elija otro.");
                throw;
            }
            finally
            {
                cn.MtCerrarConexion();
            }
        }

        public int ObtenerIdInstructorPorIdUsuario(int idUsuario)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT idInstructor FROM instructor WHERE idUsuario = @idUsuario", cn.MtAbrirConexion());
                cmd.Parameters.AddWithValue("@idUsuario", idUsuario);
                object resultado = cmd.ExecuteScalar();
                return resultado != null && resultado != DBNull.Value ? Convert.ToInt32(resultado) : 0;
            }
            finally
            {
                cn.MtCerrarConexion();
            }
        }

        public bool AprendizEstaCanceladoPorIdUsuario(int idUsuario)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(
                    @"SELECT COUNT(1)
                      FROM aprendiz
                      WHERE idUsuario = @idUsuario
                        AND LOWER(LTRIM(RTRIM(ISNULL(estado, '')))) IN ('cancelado', 'canselado', 'cancelada', 'canselada')",
                    cn.MtAbrirConexion());
                cmd.Parameters.AddWithValue("@idUsuario", idUsuario);
                return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
            }
            finally
            {
                cn.MtCerrarConexion();
            }
        }
    }
}
