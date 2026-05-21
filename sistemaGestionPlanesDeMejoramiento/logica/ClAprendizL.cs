using sistemaGestionPlanesDeMejoramiento.datos;
using sistemaGestionPlanesDeMejoramiento.Modelo;
using System;
using System.Collections.Generic;

namespace sistemaGestionPlanesDeMejoramiento.logica
{
    public class ClAprendizL
    {
        ClAprendizD aprendizD = new ClAprendizD();

        public bool InsertarAprendiz(ClAprendiz aprendiz)
        {
            ValidarAprendiz(aprendiz);
            return aprendizD.InsertarAprendiz(aprendiz);
        }

        public List<ClAprendiz> ListarAprendices()
        {
            return aprendizD.ListarAprendices();
        }

        public bool ActualizarAprendiz(ClAprendiz aprendiz)
        {
            if (aprendiz.idAprendiz <= 0) throw new ArgumentException("ID inválido");
            ValidarAprendiz(aprendiz, false);
            return aprendizD.ActualizarAprendiz(aprendiz);
        }

        public bool EliminarAprendiz(int idAprendiz)
        {
            if (idAprendiz <= 0) throw new ArgumentException("ID inválido");
            return aprendizD.EliminarAprendiz(idAprendiz);
        }

        private void ValidarAprendiz(ClAprendiz a, bool esNuevo = true)
        {
            if (string.IsNullOrWhiteSpace(a.nombres)) throw new ArgumentException("Los nombres son obligatorios.");
            if (string.IsNullOrWhiteSpace(a.apellidos)) throw new ArgumentException("Los apellidos son obligatorios.");
            if (string.IsNullOrWhiteSpace(a.numeroDocumento)) throw new ArgumentException("El número de documento es obligatorio.");
            if (string.IsNullOrWhiteSpace(a.correo)) throw new ArgumentException("El correo es obligatorio.");
            if (a.fechaNacimiento == DateTime.MinValue) throw new ArgumentException("La fecha de nacimiento es obligatoria.");
            if (a.idFicha <= 0) throw new ArgumentException("Debe seleccionar una ficha.");
            if (a.idUsuario <= 0) throw new ArgumentException("Debe seleccionar un usuario.");
        }
    }
}