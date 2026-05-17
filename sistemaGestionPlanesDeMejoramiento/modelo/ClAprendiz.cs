using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Sistema_de_gestion_de_olanes_de_mejoramiento.Modelo
{
    public class ClAprendiz
    {
        public int IdAprendiz { get; set; }
        public string nombres { get; set; }
        public string apellidos { get; set; }
        public string tipoDocumento { get; set; }
        public string direccion { get; set; }
        public string numeroDocumento { get; set; }
        public string telefono { get; set; }
        public DateTime fechaNacimiento { get; set; }
        public int IdFicha { get; set; }
        public int IdUsuario { get; set; }
    }
}