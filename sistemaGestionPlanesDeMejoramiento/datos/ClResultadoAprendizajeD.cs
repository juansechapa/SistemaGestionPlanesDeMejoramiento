using sistemaGestionPlanesDeMejoramiento.Datos;
using sistemaGestionPlanesDeMejoramiento.Modelo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace sistemaGestionPlanesDeMejoramiento.datos
{
    public class ClResultadoAprendizajeD
    {
        ClConexion cn = new ClConexion();

        public List<ClResultado> ListarResultadosPendientes(int idAprendiz)
        {
            List<ClResultado> lista = new List<ClResultado>();
            try
            {
                SqlCommand cmd = new SqlCommand(
                    @"SELECT ra.idResultado, ra.codigo, ra.descripcion
                      FROM resultadosAprendizaje ra
                      INNER JOIN competencias c ON ra.idCompetencias = c.idCompetencias
                      INNER JOIN programa p ON c.idPrograma = p.idPrograma
                      INNER JOIN centroPrograma cp ON p.idPrograma = cp.idPrograma
                      INNER JOIN ficha f ON cp.idCentroPrograma = f.idCentroPrograma
                      INNER JOIN aprendiz a ON f.idFicha = a.idFicha
                      WHERE a.idAprendiz = @idAprendiz
                        AND ra.idResultado NOT IN (
                            SELECT pr.idResultado
                            FROM planMejoramiento pm
                            INNER JOIN planResultado pr ON pm.idPlanMejoramiento = pr.idPlanMejoramiento
                            WHERE pm.idAprendiz = @idAprendiz AND pm.estadoPlan = 'Aprobado'
                        )",
                    cn.MtAbrirConexion());
                cmd.Parameters.AddWithValue("@idAprendiz", idAprendiz);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lista.Add(new ClResultado
                    {
                        idResultado = Convert.ToInt32(dr["idResultado"]),
                        codigo = dr["codigo"].ToString(),
                        descripcion = dr["descripcion"].ToString()
                    });
                }
                dr.Close();
            }
            finally { cn.MtCerrarConexion(); }
            return lista;
        }

        public List<ClResultado> ListarPorCompetencia(int idCompetencias)
        {
            List<ClResultado> lista = new List<ClResultado>();
            try
            {
                SqlCommand cmd = new SqlCommand(
                    "SELECT idResultado, codigo, descripcion, estado, idCompetencias FROM resultadosAprendizaje WHERE idCompetencias = @idCompetencias ORDER BY codigo",
                    cn.MtAbrirConexion());
                cmd.Parameters.AddWithValue("@idCompetencias", idCompetencias);

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lista.Add(new ClResultado
                    {
                        idResultado = Convert.ToInt32(dr["idResultado"]),
                        codigo = dr["codigo"].ToString(),
                        descripcion = dr["descripcion"].ToString(),
                        estado = dr["estado"] != DBNull.Value ? dr["estado"].ToString() : null,
                        idCompetencias = Convert.ToInt32(dr["idCompetencias"])
                    });
                }
                dr.Close();
            }
            finally { cn.MtCerrarConexion(); }
            return lista;
        }

        public bool Insertar(ClResultado resultado)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(
                    "INSERT INTO resultadosAprendizaje (codigo, descripcion, estado, idCompetencias) VALUES (@codigo, @descripcion, @estado, @idCompetencias)",
                    cn.MtAbrirConexion());
                cmd.Parameters.AddWithValue("@codigo", resultado.codigo);
                cmd.Parameters.AddWithValue("@descripcion", resultado.descripcion);
                cmd.Parameters.AddWithValue("@estado", string.IsNullOrWhiteSpace(resultado.estado) ? (object)DBNull.Value : resultado.estado);
                cmd.Parameters.AddWithValue("@idCompetencias", resultado.idCompetencias);
                return cmd.ExecuteNonQuery() > 0;
            }
            finally { cn.MtCerrarConexion(); }
        }

        public bool Eliminar(int idResultado)
        {
            try
            {
                SqlConnection conexion = cn.MtAbrirConexion();

                SqlCommand cmdPlanes = new SqlCommand("SELECT COUNT(1) FROM planResultado WHERE idResultado = @idResultado", conexion);
                cmdPlanes.Parameters.AddWithValue("@idResultado", idResultado);
                if (Convert.ToInt32(cmdPlanes.ExecuteScalar()) > 0)
                    throw new InvalidOperationException("No se puede eliminar el resultado porque esta asociado a uno o mas planes.");

                SqlCommand cmd = new SqlCommand("DELETE FROM resultadosAprendizaje WHERE idResultado = @idResultado", conexion);
                cmd.Parameters.AddWithValue("@idResultado", idResultado);
                return cmd.ExecuteNonQuery() > 0;
            }
            finally { cn.MtCerrarConexion(); }
        }
    }
}
