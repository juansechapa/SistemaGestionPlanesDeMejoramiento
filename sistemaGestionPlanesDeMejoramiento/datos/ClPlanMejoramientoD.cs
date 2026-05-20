using sistemaGestionPlanesDeMejoramiento.Datos;
using sistemaGestionPlanesDeMejoramiento.Modelo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace sistemaGestionPlanesDeMejoramiento.datos
{
    public class ClPlanMejoramientoD
    {
        ClConexion cn = new ClConexion();
        public bool EliminarPlan(int idPlanMejoramiento)
        {
            bool respuesta = false;

            try
            {
                SqlCommand cmd = new SqlCommand(
                    "DELETE FROM planMejoramiento WHERE idPlanMejoramiento = @idPlanMejoramiento",
                    cn.MtAbrirConexion());

                cmd.Parameters.AddWithValue("@idPlanMejoramiento", idPlanMejoramiento);

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