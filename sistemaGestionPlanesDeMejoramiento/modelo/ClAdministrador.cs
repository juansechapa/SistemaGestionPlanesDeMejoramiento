using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sistemaGestionPlanesDeMejoramiento.Modelo
{
    public class ClAdministrador
    {
        public int idAmin {  get; set; }
        public string nombres { get; set; }
        public string apellidos { get; set; }
        public string tipoDocumento { get; set; }
        public string numeroDocumento { get; set; }
        public string telefono { get; set; }
        public string correo { get; set; }
        public int idUsuario { get; set; }

    }
}
