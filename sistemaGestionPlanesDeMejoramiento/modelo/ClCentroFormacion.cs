using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sistema_de_gestion_de_olanes_de_mejoramiento.Modelo
{
    public class ClCentroFormacion
    {
        public int idCentro {  get; set; }
        public String nombre { get; set; }
        public string direccion { get; set; }
        public string telefono { get; set; }
        public Boolean estado { get; set; }

    }
}