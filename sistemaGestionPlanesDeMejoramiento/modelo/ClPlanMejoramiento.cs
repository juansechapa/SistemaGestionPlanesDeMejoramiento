using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sistemaGestionPlanesDeMejoramiento.Modelo
{
    public class ClPlanMejoramiento
    {
        public int idPlanMejoramiento {  get; set; }
        public int idAprendiz { get; set; }

        public int idInstructor { get; set; }
        public String TipoPlan {  get; set; }
        public String actividadesPropuestas { get; set; }
        public String observaciones {  get; set; }
        public DateTime fechaAsignacion {  get; set; }
        public DateTime fechaEntrega { get; set; }
        public String estadoPlan {  get; set; }
    }
}