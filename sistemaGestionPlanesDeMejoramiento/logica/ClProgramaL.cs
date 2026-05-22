using sistemaGestionPlanesDeMejoramiento.datos;
using sistemaGestionPlanesDeMejoramiento.Modelo;
using System;
using System.Collections.Generic;

namespace sistemaGestionPlanesDeMejoramiento.logica
{
    public class ClProgramaL
    {
        ClProgramaD programaD = new ClProgramaD();

        public bool InsertarPrograma(ClPrograma programa)
        {
            ValidarPrograma(programa);
            return programaD.InsertarPrograma(programa);
        }

        public List<ClPrograma> ListarProgramas()
        {
            return programaD.ListarProgramas();
        }

        public bool ActualizarPrograma(ClPrograma programa)
        {
            if (programa.idPrograma <= 0) throw new ArgumentException("ID de programa inválido.");
            ValidarPrograma(programa);
            return programaD.ActualizarPrograma(programa);
        }

        public bool EliminarPrograma(int idPrograma)
        {
            if (idPrograma <= 0) throw new ArgumentException("ID de programa inválido.");
            return programaD.EliminarPrograma(idPrograma);
        }

        private void ValidarPrograma(ClPrograma p)
        {
            if (string.IsNullOrWhiteSpace(p.codigoPrograma))
                throw new ArgumentException("El código del programa es obligatorio.");
            if (string.IsNullOrWhiteSpace(p.nombre))
                throw new ArgumentException("El nombre del programa es obligatorio.");
        }
    }
}