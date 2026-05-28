using sistemaGestionPlanesDeMejoramiento.Datos;
using sistemaGestionPlanesDeMejoramiento.Modelo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace sistemaGestionPlanesDeMejoramiento.datos
{
    public class ClEvidenciaD
    {
        ClConexion cn = new ClConexion();

        public bool InsertarEvidencia(ClEvidencia evidencia)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(
                    "INSERT INTO evidencias (nombreArchivo, rutaArchivo, tipoArchivo, fechaSubida, observaciones, idPlanMejoramiento) " +
                    "VALUES (@nombre, @ruta, @tipo, @fecha, @obs, @idPlan)",
                    cn.MtAbrirConexion());
                cmd.Parameters.AddWithValue("@nombre", evidencia.nombreArchivo);
                cmd.Parameters.AddWithValue("@ruta", evidencia.rutaArchivo);
                cmd.Parameters.AddWithValue("@tipo", evidencia.tipoArchivo);
                cmd.Parameters.AddWithValue("@fecha", evidencia.fechaSubida);
                cmd.Parameters.AddWithValue("@obs", evidencia.observaciones ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@idPlan", evidencia.idPlanMejoramiento);
                return cmd.ExecuteNonQuery() > 0;
            }
            finally { cn.MtCerrarConexion(); }
        }

        public List<ClEvidencia> ListarEvidenciasPorPlan(int idPlan)
        {
            List<ClEvidencia> lista = new List<ClEvidencia>();
            try
            {
                SqlCommand cmd = new SqlCommand(
                    "SELECT idEvidencia, nombreArchivo, rutaArchivo, tipoArchivo, fechaSubida, observaciones, idPlanMejoramiento " +
                    "FROM evidencias WHERE idPlanMejoramiento = @idPlan ORDER BY fechaSubida DESC",
                    cn.MtAbrirConexion());
                cmd.Parameters.AddWithValue("@idPlan", idPlan);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lista.Add(new ClEvidencia
                    {
                        idEvidencia = Convert.ToInt32(dr["idEvidencia"]),
                        nombreArchivo = dr["nombreArchivo"].ToString(),
                        rutaArchivo = dr["rutaArchivo"].ToString(),
                        tipoArchivo = dr["tipoArchivo"].ToString(),
                        fechaSubida = Convert.ToDateTime(dr["fechaSubida"]),
                        observaciones = dr["observaciones"] != DBNull.Value ? dr["observaciones"].ToString() : null,
                        idPlanMejoramiento = Convert.ToInt32(dr["idPlanMejoramiento"])
                    });
                }
                dr.Close();
            }
            finally { cn.MtCerrarConexion(); }
            return lista;
        }

        public ClEvidencia ObtenerEvidenciaPorId(int idEvidencia)
        {
            ClEvidencia evidencia = null;
            try
            {
                SqlCommand cmd = new SqlCommand(
                    "SELECT idEvidencia, nombreArchivo, rutaArchivo, tipoArchivo, fechaSubida, observaciones, idPlanMejoramiento " +
                    "FROM evidencias WHERE idEvidencia = @id",
                    cn.MtAbrirConexion());
                cmd.Parameters.AddWithValue("@id", idEvidencia);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    evidencia = new ClEvidencia
                    {
                        idEvidencia = Convert.ToInt32(dr["idEvidencia"]),
                        nombreArchivo = dr["nombreArchivo"].ToString(),
                        rutaArchivo = dr["rutaArchivo"].ToString(),
                        tipoArchivo = dr["tipoArchivo"].ToString(),
                        fechaSubida = Convert.ToDateTime(dr["fechaSubida"]),
                        observaciones = dr["observaciones"] != DBNull.Value ? dr["observaciones"].ToString() : null,
                        idPlanMejoramiento = Convert.ToInt32(dr["idPlanMejoramiento"])
                    };
                }
                dr.Close();
            }
            finally { cn.MtCerrarConexion(); }
            return evidencia;
        }
    }
}