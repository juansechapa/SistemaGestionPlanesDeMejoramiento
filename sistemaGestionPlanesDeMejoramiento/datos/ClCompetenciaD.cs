using sistemaGestionPlanesDeMejoramiento.Datos;
using sistemaGestionPlanesDeMejoramiento.Modelo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace sistemaGestionPlanesDeMejoramiento.datos
{
    public class ClCompetenciaD
    {
        ClConexion cn = new ClConexion();

        public List<ClCompetencias> ListarPorPrograma(int idPrograma)
        {
            List<ClCompetencias> lista = new List<ClCompetencias>();
            try
            {
                SqlCommand cmd = new SqlCommand(
                    "SELECT idCompetencias, nombre, descripcion, idPrograma FROM competencias WHERE idPrograma = @idPrograma ORDER BY nombre",
                    cn.MtAbrirConexion());
                cmd.Parameters.AddWithValue("@idPrograma", idPrograma);

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lista.Add(new ClCompetencias
                    {
                        idCompetencias = Convert.ToInt32(dr["idCompetencias"]),
                        nombre = dr["nombre"].ToString(),
                        descripcion = dr["descripcion"] != DBNull.Value ? dr["descripcion"].ToString() : null,
                        idPrograma = Convert.ToInt32(dr["idPrograma"])
                    });
                }
                dr.Close();
            }
            finally { cn.MtCerrarConexion(); }
            return lista;
        }

        public bool Insertar(ClCompetencias competencia)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(
                    "INSERT INTO competencias (nombre, descripcion, idPrograma) VALUES (@nombre, @descripcion, @idPrograma)",
                    cn.MtAbrirConexion());
                cmd.Parameters.AddWithValue("@nombre", competencia.nombre);
                cmd.Parameters.AddWithValue("@descripcion", string.IsNullOrWhiteSpace(competencia.descripcion) ? (object)DBNull.Value : competencia.descripcion);
                cmd.Parameters.AddWithValue("@idPrograma", competencia.idPrograma);
                return cmd.ExecuteNonQuery() > 0;
            }
            finally { cn.MtCerrarConexion(); }
        }

        public bool Eliminar(int idCompetencias)
        {
            SqlConnection conexion = null;
            SqlTransaction trans = null;
            try
            {
                conexion = cn.MtAbrirConexion();
                trans = conexion.BeginTransaction();

                SqlCommand cmdResultados = new SqlCommand("DELETE FROM resultadosAprendizaje WHERE idCompetencias = @id", conexion, trans);
                cmdResultados.Parameters.AddWithValue("@id", idCompetencias);
                cmdResultados.ExecuteNonQuery();

                SqlCommand cmdCompetencia = new SqlCommand("DELETE FROM competencias WHERE idCompetencias = @id", conexion, trans);
                cmdCompetencia.Parameters.AddWithValue("@id", idCompetencias);
                bool eliminado = cmdCompetencia.ExecuteNonQuery() > 0;

                trans.Commit();
                return eliminado;
            }
            catch
            {
                trans?.Rollback();
                throw;
            }
            finally { cn.MtCerrarConexion(); }
        }
    }
}
