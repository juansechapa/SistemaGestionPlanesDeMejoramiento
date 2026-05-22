using sistemaGestionPlanesDeMejoramiento.datos;
using sistemaGestionPlanesDeMejoramiento.Modelo;
using System;
using System.Collections.Generic;

namespace sistemaGestionPlanesDeMejoramiento.logica
{
    public class ClCentroProgramaL
    {
        ClCentroProgramaD cpD = new ClCentroProgramaD();

        public List<ClPrograma> ListarProgramasPorCentro(int idCentro)
        {
            if (idCentro <= 0) throw new ArgumentException("Centro inválido");
            return cpD.ListarProgramasPorCentro(idCentro);
        }

        public List<ClPrograma> ListarProgramasNoAsignados(int idCentro)
        {
            if (idCentro <= 0) throw new ArgumentException("Centro inválido");
            return cpD.ListarProgramasNoAsignados(idCentro);
        }

        public bool AsignarPrograma(int idCentro, int idPrograma)
        {
            if (idCentro <= 0 || idPrograma <= 0) throw new ArgumentException("IDs inválidos");
            return cpD.AsignarPrograma(idCentro, idPrograma);
        }

        public bool DesasignarPrograma(int idCentro, int idPrograma)
        {
            if (idCentro <= 0 || idPrograma <= 0) throw new ArgumentException("IDs inválidos");
            return cpD.DesasignarPrograma(idCentro, idPrograma);
        }
        public List<ClCentroProgramaInfo> ListarCentroProgramaInfo()
        {
            return cpD.ListarCentroProgramaInfo();
        }
    }
}