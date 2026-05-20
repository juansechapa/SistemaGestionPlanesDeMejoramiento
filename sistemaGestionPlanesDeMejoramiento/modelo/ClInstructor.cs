using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sistemaGestionPlanesDeMejoramiento.Modelo
{
    public class ClInstructor
    {
        public int idInstructor { get; set; }
        public String nombres { get; set; }

        public String apellidos { get; set; }

        public String correo { get; set; }

        public String telefono { get; set; }

        public String especialidad { get; set; }

        public int idUsuario { get; set; }

    }
}