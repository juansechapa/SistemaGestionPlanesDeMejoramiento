using sistemaGestionPlanesDeMejoramiento.datos;
using sistemaGestionPlanesDeMejoramiento.Modelo;
using System;
using System.Collections.Generic;

namespace sistemaGestionPlanesDeMejoramiento.logica
{
    public class ClInstructorFichaL
    {
        ClInstructorFichaD ifD = new ClInstructorFichaD();

        public List<ClFicha> ListarFichasActivas()
        {
            return ifD.ListarFichasActivas();
        }

        public List<int> ListarFichasPorInstructor(int idInstructor)
        {
            return ifD.ListarFichasPorInstructor(idInstructor);
        }

        public bool ActualizarFichasPorInstructor(int idInstructor, List<int> fichasIds)
        {
            if (idInstructor <= 0) throw new ArgumentException("Instructor inválido");
            return ifD.ActualizarFichasPorInstructor(idInstructor, fichasIds);
        }
    }
}