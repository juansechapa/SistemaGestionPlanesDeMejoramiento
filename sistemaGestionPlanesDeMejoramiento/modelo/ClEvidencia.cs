using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sistema_de_gestion_de_olanes_de_mejoramiento.Modelo
{
    public class ClEvidencia
    {
        public int idEvidencia { get; set; }
        public String nombreArchivo { get; set; }
        public String rutaArchivo { get; set; }
        public String tipoArchivo { get; set; }
        public DateTime fechaSubida { get; set; }
        public String observaciones { get; set; }
        public int idPlanMejoramiento { get; set; }

    }
}