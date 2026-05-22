using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sistemaGestionPlanesDeMejoramiento.Modelo
{
    public class ClFicha
    {
        public int idFicha {  get; set; }
        public int idCentroPrograma { get; set; }
        public String codigoFicha { get; set; }
        public DateTime fechaInicio { get; set; }
        public DateTime fechaFinalizacion {  get; set; }
        public String jornada {  get; set; }
        public String nivel { get; set; }
        public String duracion { get; set; }
        public String estado { get; set; }

    }
}