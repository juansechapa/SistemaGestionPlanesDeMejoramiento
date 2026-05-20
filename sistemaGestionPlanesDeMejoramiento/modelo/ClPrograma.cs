using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sistemaGestionPlanesDeMejoramiento.Modelo
{
    public class ClPrograma
    {
        public int idPrograma { get; set; }
        public String codigoPrograma { get; set; }
        public String nombre {  get; set; }
        public String apellidos { get; set; }
        public String descripcion { get; set; }
        public String version { get; set; }
        public String nivel { get; set; }
        public int duracionHoras { get; set; }
        public Boolean estado {  get; set; }
    }
}