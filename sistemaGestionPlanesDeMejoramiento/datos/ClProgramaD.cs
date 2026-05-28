using sistemaGestionPlanesDeMejoramiento.Datos;
using sistemaGestionPlanesDeMejoramiento.Modelo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace sistemaGestionPlanesDeMejoramiento.datos
{
    public class ClProgramaD
    {
        ClConexion cn = new ClConexion();

        public bool InsertarPrograma(ClPrograma programa)
        {
            bool respuesta = false;
            try
            {
                SqlCommand cmd = new SqlCommand(
                    "INSERT INTO programa (codigoPrograma, nombre, descripcion, version, nivel, duracionHoras, estado) " +
                    "VALUES (@codigoPrograma, @nombre, @descripcion, @version, @nivel, @duracionHoras, @estado)",
                    cn.MtAbrirConexion());

                cmd.Parameters.AddWithValue("@codigoPrograma", programa.codigoPrograma);
                cmd.Parameters.AddWithValue("@nombre", programa.nombre);
                cmd.Parameters.AddWithValue("@descripcion", programa.descripcion ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@version", programa.version ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@nivel", programa.nivel ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@duracionHoras", programa.duracionHoras.HasValue ? (object)programa.duracionHoras.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@estado", programa.estado);

                respuesta = cmd.ExecuteNonQuery() > 0;
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627 && ex.Message.Contains("UQ_codigoPrograma"))
                    throw new Exception("Ya existe un programa con ese código.");
                throw;
            }
            finally
            {
                cn.MtCerrarConexion();
            }
            return respuesta;
        }

        public List<ClPrograma> ListarProgramas()
        {
            List<ClPrograma> lista = new List<ClPrograma>();
            try
            {
                SqlCommand cmd = new SqlCommand(
                    "SELECT idPrograma, codigoPrograma, nombre, descripcion, version, nivel, duracionHoras, estado " +
                    "FROM programa", cn.MtAbrirConexion());

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lista.Add(new ClPrograma
                    {
                        idPrograma = Convert.ToInt32(dr["idPrograma"]),
                        codigoPrograma = dr["codigoPrograma"].ToString(),
                        nombre = dr["nombre"].ToString(),
                        descripcion = dr["descripcion"] != DBNull.Value ? dr["descripcion"].ToString() : null,
                        version = dr["version"] != DBNull.Value ? dr["version"].ToString() : null,
                        nivel = dr["nivel"] != DBNull.Value ? dr["nivel"].ToString() : null,
                        duracionHoras = dr["duracionHoras"] != DBNull.Value ? Convert.ToInt32(dr["duracionHoras"]) : (int?)null,
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

        public bool ActualizarPrograma(ClPrograma programa)
        {
            bool respuesta = false;
            try
            {
                SqlCommand cmd = new SqlCommand(
                    "UPDATE programa SET codigoPrograma=@codigoPrograma, nombre=@nombre, descripcion=@descripcion, " +
                    "version=@version, nivel=@nivel, duracionHoras=@duracionHoras, estado=@estado " +
                    "WHERE idPrograma=@idPrograma", cn.MtAbrirConexion());

                cmd.Parameters.AddWithValue("@idPrograma", programa.idPrograma);
                cmd.Parameters.AddWithValue("@codigoPrograma", programa.codigoPrograma);
                cmd.Parameters.AddWithValue("@nombre", programa.nombre);
                cmd.Parameters.AddWithValue("@descripcion", programa.descripcion ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@version", programa.version ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@nivel", programa.nivel ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@duracionHoras", programa.duracionHoras.HasValue ? (object)programa.duracionHoras.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@estado", programa.estado);

                respuesta = cmd.ExecuteNonQuery() > 0;
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627 && ex.Message.Contains("UQ_codigoPrograma"))
                    throw new Exception("Ya existe otro programa con ese código.");
                throw;
            }
            finally
            {
                cn.MtCerrarConexion();
            }
            return respuesta;
        }

        public bool EliminarPrograma(int idPrograma)
        {
            bool respuesta = false;
            try
            {
                SqlConnection conexion = cn.MtAbrirConexion();

                SqlCommand cmdCentros = new SqlCommand("SELECT COUNT(1) FROM centroPrograma WHERE idPrograma = @idPrograma", conexion);
                cmdCentros.Parameters.AddWithValue("@idPrograma", idPrograma);
                if (Convert.ToInt32(cmdCentros.ExecuteScalar()) > 0)
                    throw new InvalidOperationException("No se puede eliminar el programa porque esta asignado a uno o mas centros.");

                SqlCommand cmdCompetencias = new SqlCommand("SELECT COUNT(1) FROM competencias WHERE idPrograma = @idPrograma", conexion);
                cmdCompetencias.Parameters.AddWithValue("@idPrograma", idPrograma);
                if (Convert.ToInt32(cmdCompetencias.ExecuteScalar()) > 0)
                    throw new InvalidOperationException("No se puede eliminar el programa porque tiene competencias asociadas.");

                SqlCommand cmd = new SqlCommand("DELETE FROM programa WHERE idPrograma = @idPrograma", conexion);
                cmd.Parameters.AddWithValue("@idPrograma", idPrograma);
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
