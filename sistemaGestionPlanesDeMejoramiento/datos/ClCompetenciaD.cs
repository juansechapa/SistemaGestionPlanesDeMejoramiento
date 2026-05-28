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
            try
            {
                SqlConnection conexion = cn.MtAbrirConexion();

                SqlCommand cmdResultados = new SqlCommand("SELECT COUNT(1) FROM resultadosAprendizaje WHERE idCompetencias = @id", conexion);
                cmdResultados.Parameters.AddWithValue("@id", idCompetencias);
                if (Convert.ToInt32(cmdResultados.ExecuteScalar()) > 0)
                    throw new InvalidOperationException("No se puede eliminar la competencia porque tiene resultados de aprendizaje asociados.");

                SqlCommand cmdCompetencia = new SqlCommand("DELETE FROM competencias WHERE idCompetencias = @id", conexion);
                cmdCompetencia.Parameters.AddWithValue("@id", idCompetencias);
                return cmdCompetencia.ExecuteNonQuery() > 0;
            }
            finally { cn.MtCerrarConexion(); }
        }
    }
}
