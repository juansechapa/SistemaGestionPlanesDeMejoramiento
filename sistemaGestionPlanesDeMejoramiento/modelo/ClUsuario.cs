using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sistemaGestionPlanesDeMejoramiento.Modelo
{
    public class ClUsuario
    {
        public int idUsuario { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public int idRol { get; set; }
    }
}