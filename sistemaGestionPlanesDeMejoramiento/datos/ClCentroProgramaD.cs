using sistemaGestionPlanesDeMejoramiento.Datos;
using sistemaGestionPlanesDeMejoramiento.Modelo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace sistemaGestionPlanesDeMejoramiento.datos
{
    public class ClCentroProgramaD
    {
        ClConexion cn = new ClConexion();

        public List<ClPrograma> ListarProgramasPorCentro(int idCentro)
        {
            List<ClPrograma> lista = new List<ClPrograma>();
            try
            {
                SqlCommand cmd = new SqlCommand(
                    "SELECT p.idPrograma, p.codigoPrograma, p.nombre, p.version, p.nivel, p.duracionHoras, p.estado " +
                    "FROM programa p " +
                    "INNER JOIN centroPrograma cp ON p.idPrograma = cp.idPrograma " +
                    "WHERE cp.idCentro = @idCentro", cn.MtAbrirConexion());
                cmd.Parameters.AddWithValue("@idCentro", idCentro);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lista.Add(new ClPrograma
                    {
                        idPrograma = Convert.ToInt32(dr["idPrograma"]),
                        codigoPrograma = dr["codigoPrograma"].ToString(),
                        nombre = dr["nombre"].ToString(),
                        version = dr["version"] != DBNull.Value ? dr["version"].ToString() : null,
                        nivel = dr["nivel"] != DBNull.Value ? dr["nivel"].ToString() : null,
                        duracionHoras = dr["duracionHoras"] != DBNull.Value ? Convert.ToInt32(dr["duracionHoras"]) : (int?)null,
                        estado = dr["estado"] != DBNull.Value ? Convert.ToBoolean(dr["estado"]) : false
                    });
                }
                dr.Close();
            }
            finally { cn.MtCerrarConexion(); }
            return lista;
        }

        public List<ClPrograma> ListarProgramasNoAsignados(int idCentro)
        {
            List<ClPrograma> lista = new List<ClPrograma>();
            try
            {
                SqlCommand cmd = new SqlCommand(
                    "SELECT p.idPrograma, p.codigoPrograma, p.nombre, p.version, p.nivel, p.duracionHoras, p.estado " +
                    "FROM programa p " +
                    "WHERE p.idPrograma NOT IN (SELECT idPrograma FROM centroPrograma WHERE idCentro = @idCentro)", cn.MtAbrirConexion());
                cmd.Parameters.AddWithValue("@idCentro", idCentro);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lista.Add(new ClPrograma
                    {
                        idPrograma = Convert.ToInt32(dr["idPrograma"]),
                        codigoPrograma = dr["codigoPrograma"].ToString(),
                        nombre = dr["nombre"].ToString(),
                        version = dr["version"] != DBNull.Value ? dr["version"].ToString() : null,
                        nivel = dr["nivel"] != DBNull.Value ? dr["nivel"].ToString() : null,
                        duracionHoras = dr["duracionHoras"] != DBNull.Value ? Convert.ToInt32(dr["duracionHoras"]) : (int?)null,
                        estado = dr["estado"] != DBNull.Value ? Convert.ToBoolean(dr["estado"]) : false
                    });
                }
                dr.Close();
            }
            finally { cn.MtCerrarConexion(); }
            return lista;
        }

        public bool AsignarPrograma(int idCentro, int idPrograma)
        {
            bool respuesta = false;
            try
            {
                SqlCommand cmd = new SqlCommand(
                    "INSERT INTO centroPrograma (idCentro, idPrograma) VALUES (@idCentro, @idPrograma)", cn.MtAbrirConexion());
                cmd.Parameters.AddWithValue("@idCentro", idCentro);
                cmd.Parameters.AddWithValue("@idPrograma", idPrograma);
                respuesta = cmd.ExecuteNonQuery() > 0;
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627) throw new Exception("El programa ya está asignado a este centro.");
                throw;
            }
            finally { cn.MtCerrarConexion(); }
            return respuesta;
        }

        public bool DesasignarPrograma(int idCentro, int idPrograma)
        {
            bool respuesta = false;
            try
            {
                SqlCommand cmd = new SqlCommand(
                    "DELETE FROM centroPrograma WHERE idCentro = @idCentro AND idPrograma = @idPrograma", cn.MtAbrirConexion());
                cmd.Parameters.AddWithValue("@idCentro", idCentro);
                cmd.Parameters.AddWithValue("@idPrograma", idPrograma);
                respuesta = cmd.ExecuteNonQuery() > 0;
            }
            finally { cn.MtCerrarConexion(); }
            return respuesta;
        }

        public List<ClCentroProgramaInfo> ListarCentroProgramaInfo()
        {
            List<ClCentroProgramaInfo> lista = new List<ClCentroProgramaInfo>();
            try
            {
                SqlCommand cmd = new SqlCommand(
                    @"SELECT cp.idCentroPrograma, c.nombre AS Centro, p.nombre AS Programa
                      FROM centroPrograma cp
                      INNER JOIN centroFormacion c ON cp.idCentro = c.idCentro
                      INNER JOIN programa p ON cp.idPrograma = p.idPrograma",
                    cn.MtAbrirConexion());
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lista.Add(new ClCentroProgramaInfo
                    {
                        idCentroPrograma = Convert.ToInt32(dr["idCentroPrograma"]),
                        Descripcion = $"{dr["Centro"]} - {dr["Programa"]}"
                    });
                }
                dr.Close();
            }
            finally { cn.MtCerrarConexion(); }
            return lista;
        }


    }
}