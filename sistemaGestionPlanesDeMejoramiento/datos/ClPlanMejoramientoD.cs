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

        public bool InsertarPlan(ClPlanMejoramiento plan)
        {
            bool respuesta = false;

            try
            {
                SqlCommand cmd = new SqlCommand(
                    "INSERT INTO planMejoramiento (idAprendiz, idInstructor, TipoPlan, actividadesPropuestas, observaciones, fechaAsignacion, fechaEntrega, estadoPlan) " +
                    "VALUES (@idAprendiz, @idInstructor, @TipoPlan, @actividadesPropuestas, @observaciones, @fechaAsignacion, @fechaEntrega, @estadoPlan)",
                    cn.MtAbrirConexion());

                cmd.Parameters.AddWithValue("@idAprendiz", plan.idAprendiz);
                cmd.Parameters.AddWithValue("@idInstructor", plan.idInstructor);
                cmd.Parameters.AddWithValue("@TipoPlan", plan.TipoPlan ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@actividadesPropuestas", plan.actividadesPropuestas ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@observaciones", plan.observaciones ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@fechaAsignacion", plan.fechaAsignacion);
                cmd.Parameters.AddWithValue("@fechaEntrega", plan.fechaEntrega.HasValue ? (object)plan.fechaEntrega.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@estadoPlan", plan.estadoPlan);

                respuesta = cmd.ExecuteNonQuery() > 0;
            }
            finally
            {
                cn.MtCerrarConexion();
            }
            return respuesta;
        }

        public List<ClPlanMejoramiento> ListarTodosLosPlanes()
        {
            List<ClPlanMejoramiento> lista = new List<ClPlanMejoramiento>();

            try
            {
                SqlCommand cmd = new SqlCommand(
                    "SELECT idPlanMejoramiento, idAprendiz, idInstructor, TipoPlan, actividadesPropuestas, observaciones, fechaAsignacion, fechaEntrega, estadoPlan " +
                    "FROM planMejoramiento ORDER BY fechaAsignacion DESC",
                    cn.MtAbrirConexion());

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    ClPlanMejoramiento plan = new ClPlanMejoramiento()
                    {
                        idPlanMejoramiento = Convert.ToInt32(dr["idPlanMejoramiento"]),
                        idAprendiz = Convert.ToInt32(dr["idAprendiz"]),
                        idInstructor = Convert.ToInt32(dr["idInstructor"]),
                        TipoPlan = dr["TipoPlan"] != DBNull.Value ? Convert.ToString(dr["TipoPlan"]) : null,
                        actividadesPropuestas = dr["actividadesPropuestas"] != DBNull.Value ? Convert.ToString(dr["actividadesPropuestas"]) : null,
                        observaciones = dr["observaciones"] != DBNull.Value ? Convert.ToString(dr["observaciones"]) : null,
                        fechaAsignacion = Convert.ToDateTime(dr["fechaAsignacion"]),
                        fechaEntrega = dr["fechaEntrega"] != DBNull.Value ? Convert.ToDateTime(dr["fechaEntrega"]) : (DateTime?)null,
                        estadoPlan = Convert.ToString(dr["estadoPlan"])
                    };
                    lista.Add(plan);
                }
                dr.Close();
            }
            finally
            {
                cn.MtCerrarConexion();
            }
            return lista;
        }

        public List<ClPlanMejoramiento> ListarPlanesPorInstructor(int idInstructor)
        {
            List<ClPlanMejoramiento> lista = new List<ClPlanMejoramiento>();

            try
            {
                SqlCommand cmd = new SqlCommand(
                    "SELECT idPlanMejoramiento, idAprendiz, idInstructor, TipoPlan, actividadesPropuestas, observaciones, fechaAsignacion, fechaEntrega, estadoPlan " +
                    "FROM planMejoramiento WHERE idInstructor = @idInstructor ORDER BY fechaAsignacion DESC",
                    cn.MtAbrirConexion());

                cmd.Parameters.AddWithValue("@idInstructor", idInstructor);

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    ClPlanMejoramiento plan = new ClPlanMejoramiento()
                    {
                        idPlanMejoramiento = Convert.ToInt32(dr["idPlanMejoramiento"]),
                        idAprendiz = Convert.ToInt32(dr["idAprendiz"]),
                        idInstructor = Convert.ToInt32(dr["idInstructor"]),
                        TipoPlan = dr["TipoPlan"] != DBNull.Value ? Convert.ToString(dr["TipoPlan"]) : null,
                        actividadesPropuestas = dr["actividadesPropuestas"] != DBNull.Value ? Convert.ToString(dr["actividadesPropuestas"]) : null,
                        observaciones = dr["observaciones"] != DBNull.Value ? Convert.ToString(dr["observaciones"]) : null,
                        fechaAsignacion = dr["fechaAsignacion"] != DBNull.Value ? Convert.ToDateTime(dr["fechaAsignacion"]) : (DateTime?)null,
                        fechaEntrega = dr["fechaEntrega"] != DBNull.Value ? Convert.ToDateTime(dr["fechaEntrega"]) : (DateTime?)null,
                        estadoPlan = Convert.ToString(dr["estadoPlan"])
                    };
                    lista.Add(plan);
                }
                dr.Close();
            }
            finally
            {
                cn.MtCerrarConexion();
            }
            return lista;
        }

        public List<ClPlanMejoramiento> ListarPlanesPorAprendiz(int idAprendiz)
        {
            List<ClPlanMejoramiento> lista = new List<ClPlanMejoramiento>();

            try
            {
                SqlCommand cmd = new SqlCommand(
                    "SELECT idPlanMejoramiento, idAprendiz, idInstructor, TipoPlan, actividadesPropuestas, observaciones, fechaAsignacion, fechaEntrega, estadoPlan " +
                    "FROM planMejoramiento WHERE idAprendiz = @idAprendiz ORDER BY fechaAsignacion DESC",
                    cn.MtAbrirConexion());

                cmd.Parameters.AddWithValue("@idAprendiz", idAprendiz);

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    ClPlanMejoramiento plan = new ClPlanMejoramiento()
                    {
                        idPlanMejoramiento = Convert.ToInt32(dr["idPlanMejoramiento"]),
                        idAprendiz = Convert.ToInt32(dr["idAprendiz"]),
                        idInstructor = Convert.ToInt32(dr["idInstructor"]),
                        TipoPlan = dr["TipoPlan"] != DBNull.Value ? Convert.ToString(dr["TipoPlan"]) : null,
                        actividadesPropuestas = dr["actividadesPropuestas"] != DBNull.Value ? Convert.ToString(dr["actividadesPropuestas"]) : null,
                        observaciones = dr["observaciones"] != DBNull.Value ? Convert.ToString(dr["observaciones"]) : null,
                        fechaAsignacion = Convert.ToDateTime(dr["fechaAsignacion"]),
                        fechaEntrega = dr["fechaEntrega"] != DBNull.Value ? Convert.ToDateTime(dr["fechaEntrega"]) : (DateTime?)null,
                        estadoPlan = Convert.ToString(dr["estadoPlan"])
                    };
                    lista.Add(plan);
                }
                dr.Close();
            }
            finally
            {
                cn.MtCerrarConexion();
            }
            return lista;
        }

        public ClPlanMejoramiento ObtenerPlanPorId(int idPlanMejoramiento)
        {
            ClPlanMejoramiento plan = null;

            try
            {
                SqlCommand cmd = new SqlCommand(
                    "SELECT idPlanMejoramiento, idAprendiz, idInstructor, TipoPlan, actividadesPropuestas, observaciones, fechaAsignacion, fechaEntrega, estadoPlan " +
                    "FROM planMejoramiento WHERE idPlanMejoramiento = @idPlanMejoramiento",
                    cn.MtAbrirConexion());

                cmd.Parameters.AddWithValue("@idPlanMejoramiento", idPlanMejoramiento);

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    plan = new ClPlanMejoramiento()
                    {
                        idPlanMejoramiento = Convert.ToInt32(dr["idPlanMejoramiento"]),
                        idAprendiz = Convert.ToInt32(dr["idAprendiz"]),
                        idInstructor = Convert.ToInt32(dr["idInstructor"]),
                        TipoPlan = dr["TipoPlan"] != DBNull.Value ? Convert.ToString(dr["TipoPlan"]) : null,
                        actividadesPropuestas = dr["actividadesPropuestas"] != DBNull.Value ? Convert.ToString(dr["actividadesPropuestas"]) : null,
                        observaciones = dr["observaciones"] != DBNull.Value ? Convert.ToString(dr["observaciones"]) : null,
                        fechaAsignacion = Convert.ToDateTime(dr["fechaAsignacion"]),
                        fechaEntrega = dr["fechaEntrega"] != DBNull.Value ? Convert.ToDateTime(dr["fechaEntrega"]) : (DateTime?)null,
                        estadoPlan = Convert.ToString(dr["estadoPlan"])
                    };
                }
                dr.Close();
            }
            finally
            {
                cn.MtCerrarConexion();
            }
            return plan;
        }

        public bool ActualizarPlan(ClPlanMejoramiento plan)
        {
            bool respuesta = false;

            try
            {
                SqlCommand cmd = new SqlCommand(
                    "UPDATE planMejoramiento SET " +
                    "idAprendiz = @idAprendiz, " +
                    "idInstructor = @idInstructor, " +
                    "TipoPlan = @TipoPlan, " +
                    "actividadesPropuestas = @actividadesPropuestas, " +
                    "observaciones = @observaciones, " +
                    "fechaAsignacion = @fechaAsignacion, " +
                    "fechaEntrega = @fechaEntrega, " +
                    "estadoPlan = @estadoPlan " +
                    "WHERE idPlanMejoramiento = @idPlanMejoramiento",
                    cn.MtAbrirConexion());

                cmd.Parameters.AddWithValue("@idPlanMejoramiento", plan.idPlanMejoramiento);
                cmd.Parameters.AddWithValue("@idAprendiz", plan.idAprendiz);
                cmd.Parameters.AddWithValue("@idInstructor", plan.idInstructor);
                cmd.Parameters.AddWithValue("@TipoPlan", plan.TipoPlan ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@actividadesPropuestas", plan.actividadesPropuestas ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@observaciones", plan.observaciones ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@fechaAsignacion", plan.fechaAsignacion);
                cmd.Parameters.AddWithValue("@fechaEntrega", plan.fechaEntrega.HasValue ? (object)plan.fechaEntrega.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@estadoPlan", plan.estadoPlan);

                respuesta = cmd.ExecuteNonQuery() > 0;
            }
            finally
            {
                cn.MtCerrarConexion();
            }
            return respuesta;
        }

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