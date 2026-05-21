using sistemaGestionPlanesDeMejoramiento.datos;
using sistemaGestionPlanesDeMejoramiento.Modelo;
using System;
using System.Collections.Generic;

namespace sistemaGestionPlanesDeMejoramiento.logica
{
    public class ClCentroFormacionL
    {
        private ClCentroFormacionD centroD = new ClCentroFormacionD();

        public bool InsertarCentro(ClCentroFormacion centro)
        {
            // Validaciones de negocio
            if (centro == null)
                throw new ArgumentNullException(nameof(centro), "El centro no puede ser nulo.");

            if (string.IsNullOrWhiteSpace(centro.nombre))
                throw new ArgumentException("El nombre del centro es obligatorio.");

            return centroD.InsertarCentro(centro);
        }

        public List<ClCentroFormacion> ListarCentros()
        {
            // Metodo para filtrar mas adelante por centro
            return centroD.ListarCentros();
        }

        public bool ActualizarCentro(ClCentroFormacion centro)
        {
            if (centro == null)
                throw new ArgumentNullException(nameof(centro));
            if (centro.idCentro <= 0)
                throw new ArgumentException("ID de centro inválido.");
            if (string.IsNullOrWhiteSpace(centro.nombre))
                throw new ArgumentException("El nombre es obligatorio.");

            return centroD.ActualizarCentro(centro);
        }

        public bool EliminarCentro(int idCentro)
        {
            if (idCentro <= 0)
                throw new ArgumentException("ID de centro inválido.");

            // Añdir logica para lanzar alerta si el centro tiene fichas o programas asignados para devolver una alerta

            return centroD.EliminarCentro(idCentro);
        }

        public ClCentroFormacion ObtenerCentroPorId(int idCentro)
        {
            return centroD.ListarCentros().Find(c => c.idCentro == idCentro);
        }
    }
}