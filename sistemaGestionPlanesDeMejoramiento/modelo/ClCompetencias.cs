using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sistemaGestionPlanesDeMejoramiento.Modelo
{
    public class ClCompetencias
    {
        public int idCompetencias { get; set; }
        public String nombre { get; set; }
        public String descripcion { get; set; }
        public int idPrograma { get; set; }
    }
}