using sistemaGestionPlanesDeMejoramiento.datos;
using sistemaGestionPlanesDeMejoramiento.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sistemaGestionPlanesDeMejoramiento.logica
{
    public class ClEvidenciaL
    {
        ClEvidenciaD evidenciaD = new ClEvidenciaD();

        public bool InsertarEvidencia(ClEvidencia evidencia)
        {
            if (evidencia == null) throw new ArgumentException("Evidencia inválida.");
            if (evidencia.idPlanMejoramiento <= 0) throw new ArgumentException("Plan inválido.");
            if (string.IsNullOrWhiteSpace(evidencia.nombreArchivo)) throw new ArgumentException("Nombre de archivo obligatorio.");
            if (string.IsNullOrWhiteSpace(evidencia.rutaArchivo)) throw new ArgumentException("Ruta de archivo obligatoria.");
            if (string.IsNullOrWhiteSpace(evidencia.tipoArchivo)) throw new ArgumentException("Tipo de archivo obligatorio.");
            return evidenciaD.InsertarEvidencia(evidencia);
        }

        public List<ClEvidencia> ListarEvidenciasPorPlan(int idPlan)
        {
            if (idPlan <= 0) throw new ArgumentException("Plan inválido.");
            return evidenciaD.ListarEvidenciasPorPlan(idPlan);
        }

        public ClEvidencia ObtenerEvidenciaPorId(int idEvidencia)
        {
            if (idEvidencia <= 0) return null;
            return evidenciaD.ObtenerEvidenciaPorId(idEvidencia);
        }
    }
}
