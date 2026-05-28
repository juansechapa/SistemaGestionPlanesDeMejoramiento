using sistemaGestionPlanesDeMejoramiento.Datos;
using sistemaGestionPlanesDeMejoramiento.Modelo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace sistemaGestionPlanesDeMejoramiento.datos
{
    public class ClFichaD
    {
        ClConexion cn = new ClConexion();

        public bool InsertarFicha(ClFicha ficha)
        {
            bool respuesta = false;
            try
            {
                SqlCommand cmd = new SqlCommand(
                    "INSERT INTO ficha (idCentroPrograma, codigoFicha, fechaInicio, fechaFinalizacion, jornada, nivel, duracion, estado) " +
                    "VALUES (@idCentroPrograma, @codigoFicha, @fechaInicio, @fechaFinalizacion, @jornada, @nivel, @duracion, @estado)",
                    cn.MtAbrirConexion());

                cmd.Parameters.AddWithValue("@idCentroPrograma", ficha.idCentroPrograma);
                cmd.Parameters.AddWithValue("@codigoFicha", ficha.codigoFicha);
                cmd.Parameters.AddWithValue("@fechaInicio", ficha.fechaInicio);
                cmd.Parameters.AddWithValue("@fechaFinalizacion", ficha.fechaFinalizacion);
                cmd.Parameters.AddWithValue("@jornada", ficha.jornada ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@nivel", ficha.nivel ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@duracion", ficha.duracion ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@estado", ficha.estado);

                respuesta = cmd.ExecuteNonQuery() > 0;
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627 && ex.Message.Contains("UQ_ficha"))
                    throw new Exception("Ya existe una ficha con ese código.");
                throw;
            }
            finally { cn.MtCerrarConexion(); }
            return respuesta;
        }

        public List<ClFicha> ListarFichas()
        {
            List<ClFicha> lista = new List<ClFicha>();
            try
            {
                SqlCommand cmd = new SqlCommand(
                    "SELECT idFicha, idCentroPrograma, codigoFicha, fechaInicio, fechaFinalizacion, jornada, nivel, duracion, estado FROM ficha",
                    cn.MtAbrirConexion());
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lista.Add(new ClFicha
                    {
                        idFicha = Convert.ToInt32(dr["idFicha"]),
                        idCentroPrograma = Convert.ToInt32(dr["idCentroPrograma"]),
                        codigoFicha = dr["codigoFicha"].ToString(),
                        fechaInicio = Convert.ToDateTime(dr["fechaInicio"]),
                        fechaFinalizacion = Convert.ToDateTime(dr["fechaFinalizacion"]),
                        jornada = dr["jornada"] != DBNull.Value ? dr["jornada"].ToString() : null,
                        nivel = dr["nivel"] != DBNull.Value ? dr["nivel"].ToString() : null,
                        duracion = dr["duracion"] != DBNull.Value ? dr["duracion"].ToString() : null,
                        estado = dr["estado"].ToString()
                    });
                }
                dr.Close();
            }
            finally { cn.MtCerrarConexion(); }
            return lista;
        }

        public bool ActualizarFicha(ClFicha ficha)
        {
            bool respuesta = false;
            try
            {
                SqlCommand cmd = new SqlCommand(
                    "UPDATE ficha SET idCentroPrograma=@idCentroPrograma, codigoFicha=@codigoFicha, fechaInicio=@fechaInicio, " +
                    "fechaFinalizacion=@fechaFinalizacion, jornada=@jornada, nivel=@nivel, duracion=@duracion, estado=@estado " +
                    "WHERE idFicha=@idFicha",
                    cn.MtAbrirConexion());

                cmd.Parameters.AddWithValue("@idFicha", ficha.idFicha);
                cmd.Parameters.AddWithValue("@idCentroPrograma", ficha.idCentroPrograma);
                cmd.Parameters.AddWithValue("@codigoFicha", ficha.codigoFicha);
                cmd.Parameters.AddWithValue("@fechaInicio", ficha.fechaInicio);
                cmd.Parameters.AddWithValue("@fechaFinalizacion", ficha.fechaFinalizacion);
                cmd.Parameters.AddWithValue("@jornada", ficha.jornada ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@nivel", ficha.nivel ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@duracion", ficha.duracion ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@estado", ficha.estado);

                respuesta = cmd.ExecuteNonQuery() > 0;
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627 && ex.Message.Contains("UQ_ficha"))
                    throw new Exception("Ya existe otra ficha con ese código.");
                throw;
            }
            finally { cn.MtCerrarConexion(); }
            return respuesta;
        }

        public bool EliminarFicha(int idFicha)
        {
            bool respuesta = false;
            try
            {
                SqlConnection conexion = cn.MtAbrirConexion();

                SqlCommand cmdAprendices = new SqlCommand("SELECT COUNT(1) FROM aprendiz WHERE idFicha = @idFicha", conexion);
                cmdAprendices.Parameters.AddWithValue("@idFicha", idFicha);
                if (Convert.ToInt32(cmdAprendices.ExecuteScalar()) > 0)
                    throw new InvalidOperationException("No se puede eliminar la ficha porque tiene aprendices asignados.");

                SqlCommand cmdInstructores = new SqlCommand("SELECT COUNT(1) FROM instructorFicha WHERE idFicha = @idFicha", conexion);
                cmdInstructores.Parameters.AddWithValue("@idFicha", idFicha);
                if (Convert.ToInt32(cmdInstructores.ExecuteScalar()) > 0)
                    throw new InvalidOperationException("No se puede eliminar la ficha porque tiene instructores asignados.");

                SqlCommand cmd = new SqlCommand("DELETE FROM ficha WHERE idFicha = @idFicha", conexion);
                cmd.Parameters.AddWithValue("@idFicha", idFicha);
                respuesta = cmd.ExecuteNonQuery() > 0;
            }
            finally { cn.MtCerrarConexion(); }
            return respuesta;
        }

        public ClFicha ObtenerFichaPorId(int idFicha)
        {
            ClFicha ficha = null;
            try
            {
                SqlCommand cmd = new SqlCommand(
                    "SELECT idFicha, idCentroPrograma, codigoFicha, fechaInicio, fechaFinalizacion, jornada, nivel, duracion, estado " +
                    "FROM ficha WHERE idFicha = @idFicha",
                    cn.MtAbrirConexion());
                cmd.Parameters.AddWithValue("@idFicha", idFicha);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    ficha = new ClFicha
                    {
                        idFicha = Convert.ToInt32(dr["idFicha"]),
                        idCentroPrograma = Convert.ToInt32(dr["idCentroPrograma"]),
                        codigoFicha = dr["codigoFicha"].ToString(),
                        fechaInicio = Convert.ToDateTime(dr["fechaInicio"]),
                        fechaFinalizacion = Convert.ToDateTime(dr["fechaFinalizacion"]),
                        jornada = dr["jornada"] != DBNull.Value ? dr["jornada"].ToString() : null,
                        nivel = dr["nivel"] != DBNull.Value ? dr["nivel"].ToString() : null,
                        duracion = dr["duracion"] != DBNull.Value ? dr["duracion"].ToString() : null,
                        estado = dr["estado"].ToString()
                    };
                }
                dr.Close();
            }
            finally { cn.MtCerrarConexion(); }
            return ficha;
        }
    }
}
