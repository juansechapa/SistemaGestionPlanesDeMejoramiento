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
            SqlTransaction trans = null;
            SqlConnection conexion = null;

            try
            {
                conexion = cn.MtAbrirConexion();
                trans = conexion.BeginTransaction();

                string[] dependencias =
                {
                    "DELETE FROM evaluacionPlan WHERE idPlanMejoramiento = @idPlanMejoramiento",
                    "DELETE FROM planResultado WHERE idPlanMejoramiento = @idPlanMejoramiento",
                    "DELETE FROM evidencias WHERE idPlanMejoramiento = @idPlanMejoramiento"
                };

                foreach (string sqlDependencia in dependencias)
                {
                    SqlCommand cmdDependencia = new SqlCommand(sqlDependencia, conexion, trans);
                    cmdDependencia.Parameters.AddWithValue("@idPlanMejoramiento", idPlanMejoramiento);
                    cmdDependencia.ExecuteNonQuery();
                }

                SqlCommand cmd = new SqlCommand(
                    "DELETE FROM planMejoramiento WHERE idPlanMejoramiento = @idPlanMejoramiento",
                    conexion,
                    trans);

                cmd.Parameters.AddWithValue("@idPlanMejoramiento", idPlanMejoramiento);

                respuesta = cmd.ExecuteNonQuery() > 0;
                trans.Commit();
            }
            catch
            {
                trans?.Rollback();
                throw;
            }
            finally
            {
                cn.MtCerrarConexion();
            }
            return respuesta;
        }

        public int InsertarPlanConResultados(ClPlanMejoramiento plan, List<int> resultadosIds)
        {
            int nuevoId = 0;
            SqlTransaction trans = null;
            SqlConnection conexion = null;
            try
            {
                conexion = cn.MtAbrirConexion();
                trans = conexion.BeginTransaction();

                SqlCommand cmdPlan = new SqlCommand(
                    @"INSERT INTO planMejoramiento (idAprendiz, idInstructor, TipoPlan, actividadesPropuestas, observaciones, fechaAsignacion, fechaEntrega, estadoPlan)
              VALUES (@idAprendiz, @idInstructor, @TipoPlan, @actividadesPropuestas, @observaciones, @fechaAsignacion, @fechaEntrega, @estadoPlan);
              SELECT SCOPE_IDENTITY();",
                    conexion, trans);
                cmdPlan.Parameters.AddWithValue("@idAprendiz", plan.idAprendiz);
                cmdPlan.Parameters.AddWithValue("@idInstructor", plan.idInstructor);
                cmdPlan.Parameters.AddWithValue("@TipoPlan", plan.TipoPlan ?? (object)DBNull.Value);
                cmdPlan.Parameters.AddWithValue("@actividadesPropuestas", plan.actividadesPropuestas ?? (object)DBNull.Value);
                cmdPlan.Parameters.AddWithValue("@observaciones", plan.observaciones ?? (object)DBNull.Value);
                cmdPlan.Parameters.AddWithValue("@fechaAsignacion", plan.fechaAsignacion);
                cmdPlan.Parameters.AddWithValue("@fechaEntrega", plan.fechaEntrega.HasValue ? (object)plan.fechaEntrega.Value : DBNull.Value);
                cmdPlan.Parameters.AddWithValue("@estadoPlan", "Pendiente");

                nuevoId = Convert.ToInt32(cmdPlan.ExecuteScalar());

                // Insertar resultados asociados
                foreach (int idRes in resultadosIds)
                {
                    SqlCommand cmdRes = new SqlCommand(
                        "INSERT INTO planResultado (idPlanMejoramiento, idResultado) VALUES (@idPlan, @idRes)",
                        conexion, trans);
                    cmdRes.Parameters.AddWithValue("@idPlan", nuevoId);
                    cmdRes.Parameters.AddWithValue("@idRes", idRes);
                    cmdRes.ExecuteNonQuery();
                }

                trans.Commit();
            }
            catch
            {
                trans?.Rollback();
                throw;
            }
            finally
            {
                cn.MtCerrarConexion();
            }
            return nuevoId;
        }

        public bool GuardarEvaluacion(int idPlanMejoramiento, string producto, string conocimiento, string desempeno, string observaciones)
        {
            bool respuesta = false;
            SqlTransaction trans = null;
            SqlConnection conexion = null;
            try
            {
                conexion = cn.MtAbrirConexion();
                trans = conexion.BeginTransaction();

                int idProducto = ObtenerOCrearCriterio(conexion, trans, "Producto");
                int idConocimiento = ObtenerOCrearCriterio(conexion, trans, "Conocimiento");
                int idDesempeno = ObtenerOCrearCriterio(conexion, trans, "Desempeño");

                SqlCommand cmdEliminarEvaluacionAnterior = new SqlCommand(
                    "DELETE FROM evaluacionPlan WHERE idPlanMejoramiento = @idPlan",
                    conexion,
                    trans);
                cmdEliminarEvaluacionAnterior.Parameters.AddWithValue("@idPlan", idPlanMejoramiento);
                cmdEliminarEvaluacionAnterior.ExecuteNonQuery();

                string sql = @"INSERT INTO evaluacionPlan (idPlanMejoramiento, idCriterio, valoracion, observaciones)
                       VALUES
                         (@idPlan, @idProducto, @producto, @obs),
                         (@idPlan, @idConocimiento, @conocimiento, @obs),
                         (@idPlan, @idDesempeno, @desempeno, @obs)";
                SqlCommand cmd = new SqlCommand(sql, conexion, trans);
                cmd.Parameters.AddWithValue("@idPlan", idPlanMejoramiento);
                cmd.Parameters.AddWithValue("@idProducto", idProducto);
                cmd.Parameters.AddWithValue("@idConocimiento", idConocimiento);
                cmd.Parameters.AddWithValue("@idDesempeno", idDesempeno);
                cmd.Parameters.AddWithValue("@producto", producto);
                cmd.Parameters.AddWithValue("@conocimiento", conocimiento);
                cmd.Parameters.AddWithValue("@desempeno", desempeno);
                cmd.Parameters.AddWithValue("@obs", observaciones ?? (object)DBNull.Value);
                cmd.ExecuteNonQuery();

                // Determinar si aprueba (todos deben ser "Aprueba")
                bool aprobado = (producto == "Aprueba" && conocimiento == "Aprueba" && desempeno == "Aprueba");
                string nuevoEstado = aprobado ? "Aprobado" : "No Aprobado";

                // Actualizar estado del plan
                SqlCommand cmdUpd = new SqlCommand("UPDATE planMejoramiento SET estadoPlan = @estado WHERE idPlanMejoramiento = @id", conexion, trans);
                cmdUpd.Parameters.AddWithValue("@estado", nuevoEstado);
                cmdUpd.Parameters.AddWithValue("@id", idPlanMejoramiento);
                cmdUpd.ExecuteNonQuery();

                if (!aprobado)
                {
                    GenerarPlanComiteSiAplica(conexion, trans, idPlanMejoramiento);
                }

                trans.Commit();
                respuesta = true;
            }
            catch
            {
                trans?.Rollback();
                throw;
            }
            finally
            {
                cn.MtCerrarConexion();
            }
            return respuesta;
        }

        private int ObtenerOCrearCriterio(SqlConnection conexion, SqlTransaction trans, string nombre)
        {
            SqlCommand cmdBuscar = new SqlCommand(
                "SELECT idCriterio FROM criteriosEvaluacion WHERE nombre = @nombre",
                conexion,
                trans);
            cmdBuscar.Parameters.AddWithValue("@nombre", nombre);

            object existente = cmdBuscar.ExecuteScalar();
            if (existente != null && existente != DBNull.Value)
                return Convert.ToInt32(existente);

            SqlCommand cmdInsertar = new SqlCommand(
                "INSERT INTO criteriosEvaluacion (nombre, descripcion, estado) VALUES (@nombre, @descripcion, 1); SELECT SCOPE_IDENTITY();",
                conexion,
                trans);
            cmdInsertar.Parameters.AddWithValue("@nombre", nombre);
            cmdInsertar.Parameters.AddWithValue("@descripcion", "Criterio " + nombre);

            return Convert.ToInt32(cmdInsertar.ExecuteScalar());
        }

        private int GenerarPlanComiteSiAplica(SqlConnection conexion, SqlTransaction trans, int idPlanInterno)
        {
            SqlCommand cmdPlanOriginal = new SqlCommand(
                @"SELECT idPlanMejoramiento, idAprendiz, idInstructor, TipoPlan, actividadesPropuestas, observaciones
                  FROM planMejoramiento
                  WHERE idPlanMejoramiento = @idPlan",
                conexion,
                trans);
            cmdPlanOriginal.Parameters.AddWithValue("@idPlan", idPlanInterno);

            int idAprendiz;
            int idInstructor;
            string tipoPlan;
            string actividades;
            string observaciones;

            using (SqlDataReader dr = cmdPlanOriginal.ExecuteReader())
            {
                if (!dr.Read())
                    throw new Exception("No se encontró el plan evaluado.");

                idAprendiz = Convert.ToInt32(dr["idAprendiz"]);
                idInstructor = Convert.ToInt32(dr["idInstructor"]);
                tipoPlan = dr["TipoPlan"] != DBNull.Value ? dr["TipoPlan"].ToString() : "";
                actividades = dr["actividadesPropuestas"] != DBNull.Value ? dr["actividadesPropuestas"].ToString() : null;
                observaciones = dr["observaciones"] != DBNull.Value ? dr["observaciones"].ToString() : null;
            }

            if (!string.Equals(tipoPlan, "Interno", StringComparison.OrdinalIgnoreCase))
                return 0;

            SqlCommand cmdExiste = new SqlCommand(
                @"SELECT TOP 1 idPlanMejoramiento
                  FROM planMejoramiento
                  WHERE idAprendiz = @idAprendiz
                    AND idInstructor = @idInstructor
                    AND TipoPlan = @tipoPlan
                    AND estadoPlan = 'Pendiente'
                  ORDER BY idPlanMejoramiento DESC",
                conexion,
                trans);
            cmdExiste.Parameters.AddWithValue("@idAprendiz", idAprendiz);
            cmdExiste.Parameters.AddWithValue("@idInstructor", idInstructor);
            cmdExiste.Parameters.AddWithValue("@tipoPlan", "Comité");

            object idExistente = cmdExiste.ExecuteScalar();
            if (idExistente != null && idExistente != DBNull.Value)
                return Convert.ToInt32(idExistente);

            SqlCommand cmdCrearComite = new SqlCommand(
                @"INSERT INTO planMejoramiento (idAprendiz, idInstructor, TipoPlan, actividadesPropuestas, observaciones, fechaAsignacion, fechaEntrega, estadoPlan)
                  VALUES (@idAprendiz, @idInstructor, @tipoPlan, @actividades, @observaciones, @fechaAsignacion, @fechaEntrega, 'Pendiente');
                  SELECT SCOPE_IDENTITY();",
                conexion,
                trans);
            cmdCrearComite.Parameters.AddWithValue("@idAprendiz", idAprendiz);
            cmdCrearComite.Parameters.AddWithValue("@idInstructor", idInstructor);
            cmdCrearComite.Parameters.AddWithValue("@tipoPlan", "Comité");
            cmdCrearComite.Parameters.AddWithValue("@actividades", actividades ?? (object)DBNull.Value);
            cmdCrearComite.Parameters.AddWithValue("@observaciones", observaciones ?? (object)DBNull.Value);
            cmdCrearComite.Parameters.AddWithValue("@fechaAsignacion", DateTime.Now);
            cmdCrearComite.Parameters.AddWithValue("@fechaEntrega", DateTime.Now.AddDays(15));

            int nuevoId = Convert.ToInt32(cmdCrearComite.ExecuteScalar());

            SqlCommand cmdCopiarResultados = new SqlCommand(
                @"INSERT INTO planResultado (idPlanMejoramiento, idResultado)
                  SELECT @nuevoId, idResultado
                  FROM planResultado
                  WHERE idPlanMejoramiento = @idOriginal",
                conexion,
                trans);
            cmdCopiarResultados.Parameters.AddWithValue("@nuevoId", nuevoId);
            cmdCopiarResultados.Parameters.AddWithValue("@idOriginal", idPlanInterno);
            cmdCopiarResultados.ExecuteNonQuery();

            return nuevoId;
        }

        public int GenerarPlanComite(int idPlanInterno)
        {
            int nuevoId = 0;
            SqlTransaction trans = null;
            SqlConnection conexion = null;
            try
            {
                conexion = cn.MtAbrirConexion();
                trans = conexion.BeginTransaction();
                nuevoId = GenerarPlanComiteSiAplica(conexion, trans, idPlanInterno);
                trans.Commit();
            }
            catch
            {
                trans?.Rollback();
                throw;
            }
            finally
            {
                cn.MtCerrarConexion();
            }
            return nuevoId;
        }
    }
}
