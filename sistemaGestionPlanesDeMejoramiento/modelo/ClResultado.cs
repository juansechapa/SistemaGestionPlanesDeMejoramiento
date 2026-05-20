using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sistemaGestionPlanesDeMejoramiento.Modelo
{
    public class ClResultado
    {
        public int idResultado {  get; set; }
        public String codigo { get; set; }
        public String descripcion { get; set; }
        public String estado { get; set; }
        public int idCompetencias { get; set; }

    }
}