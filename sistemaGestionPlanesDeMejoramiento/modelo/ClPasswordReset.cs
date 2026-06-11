using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sistemaGestionPlanesDeMejoramiento.Modelo
{
    public class ClPasswordReset
    {
        public int idCodigo {  get; set; }
        public String codigo { get; set; }
        public DateTime expiracionDate { get; set; }
        public bool usado {  get; set; }
        public int idUsuario { get; set; }

    }
}
