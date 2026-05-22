using sistemaGestionPlanesDeMejoramiento.datos;
using sistemaGestionPlanesDeMejoramiento.Modelo;
using System;
using System.Collections.Generic;

namespace sistemaGestionPlanesDeMejoramiento.logica
{
    public class ClFichaL
    {
        ClFichaD fichaD = new ClFichaD();

        public bool InsertarFicha(ClFicha ficha)
        {
            ValidarFicha(ficha);
            return fichaD.InsertarFicha(ficha);
        }

        public List<ClFicha> ListarFichas()
        {
            return fichaD.ListarFichas();
        }

        public bool ActualizarFicha(ClFicha ficha)
        {
            if (ficha.idFicha <= 0) throw new ArgumentException("ID de ficha inválido.");
            ValidarFicha(ficha);
            return fichaD.ActualizarFicha(ficha);
        }

        public bool EliminarFicha(int idFicha)
        {
            if (idFicha <= 0) throw new ArgumentException("ID inválido.");
            // Agregar validacion si hay aprendices asociados
            return fichaD.EliminarFicha(idFicha);
        }

        public ClFicha ObtenerFichaPorId(int idFicha)
        {
            return fichaD.ObtenerFichaPorId(idFicha);
        }

        private void ValidarFicha(ClFicha f)
        {
            if (string.IsNullOrWhiteSpace(f.codigoFicha))
                throw new ArgumentException("El código de ficha es obligatorio.");
            if (f.idCentroPrograma <= 0)
                throw new ArgumentException("Debe seleccionar un programa/centro.");
            if (f.fechaInicio == DateTime.MinValue)
                throw new ArgumentException("La fecha de inicio es obligatoria.");
            if (f.fechaFinalizacion == DateTime.MinValue)
                throw new ArgumentException("La fecha de finalización es obligatoria.");
            if (f.fechaFinalizacion <= f.fechaInicio)
                throw new ArgumentException("La fecha de finalización debe ser posterior a la fecha de inicio.");
            if (string.IsNullOrWhiteSpace(f.estado))
                throw new ArgumentException("El estado es obligatorio.");
        }
        
    }
}