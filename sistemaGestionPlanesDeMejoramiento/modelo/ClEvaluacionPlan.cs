using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sistemaGestionPlanesDeMejoramiento.Modelo
{
    public class ClEvaluacionPlan
    {
        public int idEvaluacionPlan {  get; set; }
        public int idPlanMejoramiento { get; set; }
        public int idCriterio { get; set; }
        public String valoracion {  get; set; }
        public String observaciones { get; set; }
    }
}