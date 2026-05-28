using sistemaGestionPlanesDeMejoramiento.datos;
using sistemaGestionPlanesDeMejoramiento.Modelo;
using System;
using System.Collections.Generic;

namespace sistemaGestionPlanesDeMejoramiento.logica
{
    public class ClPlanMejoramientoL
    {
        ClPlanMejoramientoD planD = new ClPlanMejoramientoD();
        ClAprendizD aprendizD = new ClAprendizD(); // temporal para resultados

        public int CrearPlanInterno(ClPlanMejoramiento plan, List<int> resultadosIds)
        {
            if (aprendizD.AprendizEstaCancelado(plan.idAprendiz))
                throw new Exception("No se pueden crear planes para un aprendiz cancelado.");
            if (plan.fechaEntrega <= DateTime.Now)
                throw new Exception("La fecha de entrega debe ser futura.");
            plan.TipoPlan = "Interno";
            plan.estadoPlan = "Pendiente";
            plan.fechaAsignacion = DateTime.Now;
            return planD.InsertarPlanConResultados(plan, resultadosIds);
        }

        public bool InsertarPlan(ClPlanMejoramiento plan)
        {
            ValidarPlan(plan);
            if (aprendizD.AprendizEstaCancelado(plan.idAprendiz))
                throw new Exception("No se pueden crear planes para un aprendiz cancelado.");
            if (!plan.fechaAsignacion.HasValue)
                plan.fechaAsignacion = DateTime.Now;
            if (string.IsNullOrWhiteSpace(plan.estadoPlan))
                plan.estadoPlan = "Pendiente";
            return planD.InsertarPlan(plan);
        }

        public bool ActualizarPlan(ClPlanMejoramiento plan)
        {
            if (plan.idPlanMejoramiento <= 0) throw new ArgumentException("Plan inválido.");
            ValidarPlan(plan);
            return planD.ActualizarPlan(plan);
        }

        public bool EliminarPlan(int idPlan)
        {
            if (idPlan <= 0) throw new ArgumentException("Plan inválido.");
            return planD.EliminarPlan(idPlan);
        }

        public List<ClPlanMejoramiento> ListarTodosLosPlanes()
        {
            return planD.ListarTodosLosPlanes();
        }

        public List<ClResultado> ObtenerResultadosPendientes(int idAprendiz)
        {
            return aprendizD.ObtenerResultadosPendientes(idAprendiz);
        }

        public bool EvaluarPlan(int idPlan, string producto, string conocimiento, string desempeno, string observaciones)
        {
            return planD.GuardarEvaluacion(idPlan, producto, conocimiento, desempeno, observaciones);
        }

        public List<ClPlanMejoramiento> ListarPlanesPorInstructor(int idInstructor)
        {
            return planD.ListarPlanesPorInstructor(idInstructor);
        }

        public List<ClPlanMejoramiento> ListarPlanesPorAprendiz(int idAprendiz)
        {
            if (idAprendiz <= 0) throw new ArgumentException("Aprendiz inválido.");
            return planD.ListarPlanesPorAprendiz(idAprendiz);
        }

        public bool PlanPerteneceAAprendiz(int idPlan, int idAprendiz)
        {
            if (idPlan <= 0 || idAprendiz <= 0) return false;
            ClPlanMejoramiento plan = planD.ObtenerPlanPorId(idPlan);
            return plan != null && plan.idAprendiz == idAprendiz;
        }

        public ClPlanMejoramiento ObtenerPlanPorId(int idPlan)
        {
            return planD.ObtenerPlanPorId(idPlan);
        }

        private void ValidarPlan(ClPlanMejoramiento plan)
        {
            if (plan == null) throw new ArgumentException("Plan inválido.");
            if (plan.idAprendiz <= 0) throw new ArgumentException("Debe seleccionar un aprendiz.");
            if (plan.idInstructor <= 0) throw new ArgumentException("Debe seleccionar un instructor.");
            if (string.IsNullOrWhiteSpace(plan.TipoPlan)) throw new ArgumentException("Debe seleccionar el tipo de plan.");
            if (string.IsNullOrWhiteSpace(plan.actividadesPropuestas)) throw new ArgumentException("Las actividades propuestas son obligatorias.");
            if (!plan.fechaEntrega.HasValue) throw new ArgumentException("La fecha de entrega es obligatoria.");
        }
    }
}
