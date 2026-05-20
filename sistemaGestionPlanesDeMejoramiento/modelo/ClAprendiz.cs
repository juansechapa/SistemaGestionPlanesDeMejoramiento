using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace sistemaGestionPlanesDeMejoramiento.Modelo
{
    public class ClAprendiz
    {
        public int idAprendiz { get; set; }
        public string nombres { get; set; }
        public string apellidos { get; set; }
        public string tipoDocumento { get; set; }
        public string numeroDocumento { get; set; }
        public string correo { get; set; }
        public string telefono { get; set; }
        public DateTime fechaNacimiento { get; set; }
        public int IdFicha { get; set; }
        public int IdUsuario { get; set; }
    }
}