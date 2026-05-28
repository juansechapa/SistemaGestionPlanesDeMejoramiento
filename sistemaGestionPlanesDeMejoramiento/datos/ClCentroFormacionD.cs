using sistemaGestionPlanesDeMejoramiento.Datos;
using sistemaGestionPlanesDeMejoramiento.Modelo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace sistemaGestionPlanesDeMejoramiento.datos
{
    public class ClCentroFormacionD
    {
        ClConexion cn = new ClConexion();

        private object SqlValue(string valor)
        {
            return string.IsNullOrWhiteSpace(valor) ? (object)DBNull.Value : valor;
        }

        public bool InsertarCentro(ClCentroFormacion centro)
        {
            bool respuesta = false;
            try
            {
                SqlCommand cmd = new SqlCommand(
                    "INSERT INTO centroFormacion (nombre, direccion, telefono, estado) " +
                    "VALUES (@nombre, @direccion, @telefono, @estado)",
                    cn.MtAbrirConexion());

                cmd.Parameters.AddWithValue("@nombre", centro.nombre);
                cmd.Parameters.AddWithValue("@direccion", SqlValue(centro.direccion));
                cmd.Parameters.AddWithValue("@telefono", SqlValue(centro.telefono));
                cmd.Parameters.AddWithValue("@estado", centro.estado);

                respuesta = cmd.ExecuteNonQuery() > 0;
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627)
                {
                    if (ex.Message.Contains("UQ_nombreCentro"))
                        throw new Exception("Ya existe un centro con ese nombre.");
                    else if (ex.Message.Contains("UQ_direccion_Centro"))  
                        throw new Exception("Ya existe un centro con esa dirección.");
                    else
                        throw new Exception("Error de duplicado en la base de datos.");
                }
                throw;
            }
            finally
            {
                cn.MtCerrarConexion();
            }
            return respuesta;
        }

        public List<ClCentroFormacion> ListarCentros()
        {
            List<ClCentroFormacion> lista = new List<ClCentroFormacion>();
            try
            {
                SqlCommand cmd = new SqlCommand(
                    "SELECT idCentro, nombre, direccion, telefono, estado FROM centroFormacion",
                    cn.MtAbrirConexion());
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lista.Add(new ClCentroFormacion
                    {
                        idCentro = Convert.ToInt32(dr["idCentro"]),
                        nombre = dr["nombre"].ToString(),
                        direccion = dr["direccion"] != DBNull.Value ? dr["direccion"].ToString() : null,
                        telefono = dr["telefono"] != DBNull.Value ? dr["telefono"].ToString() : null,
                        estado = Convert.ToBoolean(dr["estado"])
                    });
                }
                dr.Close();
            }
            finally
            {
                cn.MtCerrarConexion();
            }
            return lista;
        }

        public bool ActualizarCentro(ClCentroFormacion centro)
        {
            bool respuesta = false;
            try
            {
                SqlCommand cmd = new SqlCommand(
                    "UPDATE centroFormacion SET nombre = @nombre, direccion = @direccion, " +
                    "telefono = @telefono, estado = @estado WHERE idCentro = @idCentro",
                    cn.MtAbrirConexion());

                cmd.Parameters.AddWithValue("@idCentro", centro.idCentro);
                cmd.Parameters.AddWithValue("@nombre", centro.nombre);
                cmd.Parameters.AddWithValue("@direccion", SqlValue(centro.direccion));
                cmd.Parameters.AddWithValue("@telefono", SqlValue(centro.telefono));
                cmd.Parameters.AddWithValue("@estado", centro.estado);

                respuesta = cmd.ExecuteNonQuery() > 0;
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627)
                {
                    if (ex.Message.Contains("UQ_nombreCentro"))
                        throw new Exception("Ya existe otro centro con ese nombre.");
                    else if (ex.Message.Contains("UQ_direccionCentro"))
                        throw new Exception("Ya existe otro centro con esa dirección.");
                    else
                        throw new Exception("Error de duplicado.");
                }
                throw;
            }
            finally
            {
                cn.MtCerrarConexion();
            }
            return respuesta;
        }

        public bool EliminarCentro(int idCentro)
        {
            bool respuesta = false;
            try
            {
                SqlConnection conexion = cn.MtAbrirConexion();
                SqlCommand cmdAsignaciones = new SqlCommand("SELECT COUNT(1) FROM centroPrograma WHERE idCentro = @idCentro", conexion);
                cmdAsignaciones.Parameters.AddWithValue("@idCentro", idCentro);
                if (Convert.ToInt32(cmdAsignaciones.ExecuteScalar()) > 0)
                    throw new InvalidOperationException("No se puede eliminar el centro porque tiene programas asignados.");

                SqlCommand cmd = new SqlCommand("DELETE FROM centroFormacion WHERE idCentro = @idCentro", conexion);
                cmd.Parameters.AddWithValue("@idCentro", idCentro);
                respuesta = cmd.ExecuteNonQuery() > 0;
            }
            finally
            {
                cn.MtCerrarConexion();
            }
            return respuesta;
        }
    }
}
