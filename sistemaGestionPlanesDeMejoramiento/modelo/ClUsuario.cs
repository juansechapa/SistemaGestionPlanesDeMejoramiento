using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sistema_de_gestion_de_olanes_de_mejoramiento.Modelo
{
    public class ClUsuario
    {
        public int idUsuario { get; set; }
        public string nombreUsuario { get; set; }
        public string contrasena { get; set; }
        public string rol { get; set; }
    }
}