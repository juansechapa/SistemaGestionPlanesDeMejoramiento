using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sistema_de_gestion_de_olanes_de_mejoramiento.Modelo
{
    public class ClCriteriosEvaluacion
    {
        public int idCriterio {  get; set; }
        public String nombre { get; set; }
        public String descripcion { get; set; }
        public Boolean estado { get; set; }
    }
}