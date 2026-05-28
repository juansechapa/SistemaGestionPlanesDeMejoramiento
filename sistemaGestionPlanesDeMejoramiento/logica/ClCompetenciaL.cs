using sistemaGestionPlanesDeMejoramiento.datos;
using sistemaGestionPlanesDeMejoramiento.Modelo;
using System;
using System.Collections.Generic;

namespace sistemaGestionPlanesDeMejoramiento.logica
{
    public class ClCompetenciaL
    {
        ClCompetenciaD competenciaD = new ClCompetenciaD();

        public List<ClCompetencias> ListarPorPrograma(int idPrograma)
        {
            if (idPrograma <= 0) throw new ArgumentException("Programa inválido.");
            return competenciaD.ListarPorPrograma(idPrograma);
        }

        public bool Insertar(ClCompetencias competencia)
        {
            if (competencia == null) throw new ArgumentException("Competencia inválida.");
            if (competencia.idPrograma <= 0) throw new ArgumentException("Programa inválido.");
            if (string.IsNullOrWhiteSpace(competencia.nombre)) throw new ArgumentException("El nombre de la competencia es obligatorio.");
            return competenciaD.Insertar(competencia);
        }

        public bool Eliminar(int idCompetencias)
        {
            if (idCompetencias <= 0) throw new ArgumentException("Competencia inválida.");
            return competenciaD.Eliminar(idCompetencias);
        }
    }
}
