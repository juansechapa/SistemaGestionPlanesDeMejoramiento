using sistemaGestionPlanesDeMejoramiento.Datos;
using sistemaGestionPlanesDeMejoramiento.Modelo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace sistemaGestionPlanesDeMejoramiento.datos
{
    public class ClAdministradorD
    {
        ClConexion cn = new ClConexion();

        public bool InsertarAdministradorConUsuario(ClAdministrador administrador, string username, string password)
        {
            SqlConnection conexion = null;
            SqlTransaction trans = null;
            try
            {
                conexion = cn.MtAbrirConexion();
                trans = conexion.BeginTransaction();

                SqlCommand cmdUsuario = new SqlCommand(
                    "INSERT INTO usuarios (username, password, idRol) VALUES (@username, @password, 1); SELECT SCOPE_IDENTITY();",
                    conexion,
                    trans);
                cmdUsuario.Parameters.AddWithValue("@username", username);
                cmdUsuario.Parameters.AddWithValue("@password", ClUsuarioD.HashPassword(password));
                int idUsuario = Convert.ToInt32(cmdUsuario.ExecuteScalar());

                SqlCommand cmdAdmin = new SqlCommand(
                    @"INSERT INTO administrador (nombres, apellidos, tipoDocumento, numeroDocumento, telefono, correo, idUsuario)
                      VALUES (@nombres, @apellidos, @tipoDocumento, @numeroDocumento, @telefono, @correo, @idUsuario)",
                    conexion,
                    trans);
                AgregarParametrosAdministrador(cmdAdmin, administrador);
                cmdAdmin.Parameters.AddWithValue("@idUsuario", idUsuario);
                cmdAdmin.ExecuteNonQuery();

                trans.Commit();
                return true;
            }
            catch (SqlException ex)
            {
                trans?.Rollback();
                if (ex.Number == 2627 || ex.Number == 2601)
                    throw new Exception("Ya existe un administrador con esos datos o ese usuario.");
                throw;
            }
            catch
            {
                trans?.Rollback();
                throw;
            }
            finally
            {
                cn.MtCerrarConexion();
            }
        }

        public List<ClAdministrador> ListarAdministradores()
        {
            List<ClAdministrador> lista = new List<ClAdministrador>();
            try
            {
                SqlConnection conexion = cn.MtAbrirConexion();
                string columnaId = ObtenerColumnaId(conexion);
                SqlCommand cmd = new SqlCommand(
                    $@"SELECT {columnaId} AS idAmin, nombres, apellidos, tipoDocumento, numeroDocumento, telefono, correo, idUsuario
                       FROM administrador
                       ORDER BY apellidos, nombres",
                    conexion);

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lista.Add(MapearAdministrador(dr));
                }
                dr.Close();
            }
            finally
            {
                cn.MtCerrarConexion();
            }
            return lista;
        }

        public ClAdministrador ObtenerAdministradorPorId(int idAdministrador)
        {
            ClAdministrador administrador = null;
            try
            {
                SqlConnection conexion = cn.MtAbrirConexion();
                string columnaId = ObtenerColumnaId(conexion);
                SqlCommand cmd = new SqlCommand(
                    $@"SELECT {columnaId} AS idAmin, nombres, apellidos, tipoDocumento, numeroDocumento, telefono, correo, idUsuario
                       FROM administrador
                       WHERE {columnaId} = @idAdministrador",
                    conexion);
                cmd.Parameters.AddWithValue("@idAdministrador", idAdministrador);

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                    administrador = MapearAdministrador(dr);
                dr.Close();
            }
            finally
            {
                cn.MtCerrarConexion();
            }
            return administrador;
        }

        public bool ActualizarAdministrador(ClAdministrador administrador)
        {
            try
            {
                SqlConnection conexion = cn.MtAbrirConexion();
                string columnaId = ObtenerColumnaId(conexion);
                SqlCommand cmd = new SqlCommand(
                    $@"UPDATE administrador
                       SET nombres = @nombres,
                           apellidos = @apellidos,
                           tipoDocumento = @tipoDocumento,
                           numeroDocumento = @numeroDocumento,
                           telefono = @telefono,
                           correo = @correo
                       WHERE {columnaId} = @idAdministrador",
                    conexion);
                cmd.Parameters.AddWithValue("@idAdministrador", administrador.idAmin);
                AgregarParametrosAdministrador(cmd, administrador);
                return cmd.ExecuteNonQuery() > 0;
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627 || ex.Number == 2601)
                    throw new Exception("Ya existe otro administrador con esos datos.");
                throw;
            }
            finally
            {
                cn.MtCerrarConexion();
            }
        }

        public bool EliminarAdministrador(int idAdministrador)
        {
            SqlConnection conexion = null;
            SqlTransaction trans = null;
            try
            {
                conexion = cn.MtAbrirConexion();
                trans = conexion.BeginTransaction();
                string columnaId = ObtenerColumnaId(conexion, trans);

                SqlCommand cmdBuscar = new SqlCommand(
                    $"SELECT idUsuario FROM administrador WHERE {columnaId} = @idAdministrador",
                    conexion,
                    trans);
                cmdBuscar.Parameters.AddWithValue("@idAdministrador", idAdministrador);
                object idUsuarioObj = cmdBuscar.ExecuteScalar();

                SqlCommand cmdAdmin = new SqlCommand(
                    $"DELETE FROM administrador WHERE {columnaId} = @idAdministrador",
                    conexion,
                    trans);
                cmdAdmin.Parameters.AddWithValue("@idAdministrador", idAdministrador);
                bool eliminado = cmdAdmin.ExecuteNonQuery() > 0;

                if (eliminado && idUsuarioObj != null && idUsuarioObj != DBNull.Value)
                {
                    SqlCommand cmdUsuario = new SqlCommand(
                        "DELETE FROM usuarios WHERE idUsuario = @idUsuario AND idRol = 1",
                        conexion,
                        trans);
                    cmdUsuario.Parameters.AddWithValue("@idUsuario", Convert.ToInt32(idUsuarioObj));
                    cmdUsuario.ExecuteNonQuery();
                }

                trans.Commit();
                return eliminado;
            }
            catch
            {
                trans?.Rollback();
                throw;
            }
            finally
            {
                cn.MtCerrarConexion();
            }
        }

        private void AgregarParametrosAdministrador(SqlCommand cmd, ClAdministrador administrador)
        {
            cmd.Parameters.AddWithValue("@nombres", administrador.nombres);
            cmd.Parameters.AddWithValue("@apellidos", administrador.apellidos);
            cmd.Parameters.AddWithValue("@tipoDocumento", administrador.tipoDocumento);
            cmd.Parameters.AddWithValue("@numeroDocumento", administrador.numeroDocumento);
            cmd.Parameters.AddWithValue("@telefono", string.IsNullOrWhiteSpace(administrador.telefono) ? (object)DBNull.Value : administrador.telefono);
            cmd.Parameters.AddWithValue("@correo", administrador.correo);
        }

        private ClAdministrador MapearAdministrador(SqlDataReader dr)
        {
            return new ClAdministrador
            {
                idAmin = Convert.ToInt32(dr["idAmin"]),
                nombres = dr["nombres"].ToString(),
                apellidos = dr["apellidos"].ToString(),
                tipoDocumento = dr["tipoDocumento"].ToString(),
                numeroDocumento = dr["numeroDocumento"].ToString(),
                telefono = dr["telefono"] != DBNull.Value ? dr["telefono"].ToString() : null,
                correo = dr["correo"].ToString(),
                idUsuario = Convert.ToInt32(dr["idUsuario"])
            };
        }

        private string ObtenerColumnaId(SqlConnection conexion, SqlTransaction trans = null)
        {
            SqlCommand cmd = new SqlCommand(
                @"SELECT TOP 1 COLUMN_NAME
                  FROM INFORMATION_SCHEMA.COLUMNS
                  WHERE TABLE_NAME = 'administrador'
                    AND COLUMN_NAME IN ('idAmin', 'idAdmin', 'idAdministrador')
                  ORDER BY CASE COLUMN_NAME
                      WHEN 'idAmin' THEN 1
                      WHEN 'idAdmin' THEN 2
                      ELSE 3
                  END",
                conexion,
                trans);
            object columna = cmd.ExecuteScalar();
            if (columna == null || columna == DBNull.Value)
                throw new Exception("No se encontró la columna ID de la tabla administrador.");
            return columna.ToString();
        }
    }
}
