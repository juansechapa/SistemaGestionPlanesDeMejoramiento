using sistemaGestionPlanesDeMejoramiento.Datos;
using sistemaGestionPlanesDeMejoramiento.Modelo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace sistemaGestionPlanesDeMejoramiento.datos
{
    public class ClInstructorFichaD
    {
        ClConexion cn = new ClConexion();

        public List<ClFicha> ListarFichasActivas()
        {
            List<ClFicha> lista = new List<ClFicha>();
            try
            {
                SqlCommand cmd = new SqlCommand(
                    "SELECT idFicha, codigoFicha FROM ficha WHERE LTRIM(RTRIM(estado)) = 'Activa'",
                    cn.MtAbrirConexion());
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lista.Add(new ClFicha
                    {
                        idFicha = Convert.ToInt32(dr["idFicha"]),
                        codigoFicha = dr["codigoFicha"].ToString()
                    });
                }
                dr.Close();
            }
            finally { cn.MtCerrarConexion(); }
            return lista;
        }

        public List<int> ListarFichasPorInstructor(int idInstructor)
        {
            List<int> ids = new List<int>();
            try
            {
                SqlCommand cmd = new SqlCommand(
                    "SELECT idFicha FROM instructorFicha WHERE idInstructor = @idInstructor",
                    cn.MtAbrirConexion());
                cmd.Parameters.AddWithValue("@idInstructor", idInstructor);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                    ids.Add(Convert.ToInt32(dr["idFicha"]));
                dr.Close();
            }
            finally { cn.MtCerrarConexion(); }
            return ids;
        }

        public bool ActualizarFichasPorInstructor(int idInstructor, List<int> fichasIds)
        {
            bool resultado = false;
            SqlTransaction trans = null;
            SqlConnection conexion = null;
            try
            {
                conexion = cn.MtAbrirConexion();
                trans = conexion.BeginTransaction();

                // Eliminar actuales
                SqlCommand cmdDel = new SqlCommand("DELETE FROM instructorFicha WHERE idInstructor = @idInstructor", conexion, trans);
                cmdDel.Parameters.AddWithValue("@idInstructor", idInstructor);
                cmdDel.ExecuteNonQuery();

                // Insertar nuevas
                foreach (int idFicha in fichasIds)
                {
                    SqlCommand cmdIns = new SqlCommand("INSERT INTO instructorFicha (idInstructor, idFicha) VALUES (@idInstructor, @idFicha)", conexion, trans);
                    cmdIns.Parameters.AddWithValue("@idInstructor", idInstructor);
                    cmdIns.Parameters.AddWithValue("@idFicha", idFicha);
                    cmdIns.ExecuteNonQuery();
                }

                trans.Commit();
                resultado = true;
            }
            catch
            {
                trans?.Rollback();
                throw;
            }
            finally { cn.MtCerrarConexion(); }
            return resultado;
        }
    }
}
