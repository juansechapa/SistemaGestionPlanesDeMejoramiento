using sistemaGestionPlanesDeMejoramiento.datos;
using sistemaGestionPlanesDeMejoramiento.Modelo;
using System;
using System.Collections.Generic;

namespace sistemaGestionPlanesDeMejoramiento.logica
{
    public class ClInstructorL
    {
        ClInstructorD instructorD = new ClInstructorD();

        public bool InsertarInstructor(ClInstructor instructor)
        {
            ValidarInstructor(instructor);
            return instructorD.InsertarInstructor(instructor);
        }

        public List<ClInstructor> ListarInstructores()
        {
            return instructorD.ListarInstructores();
        }

        public bool ActualizarInstructor(ClInstructor instructor)
        {
            if (instructor.idInstructor <= 0) throw new ArgumentException("ID inválido");
            ValidarInstructor(instructor);
            return instructorD.ActualizarInstructor(instructor);
        }

        public bool EliminarInstructor(int idInstructor)
        {
            if (idInstructor <= 0) throw new ArgumentException("ID inválido");
            return instructorD.EliminarInstructor(idInstructor);
        }

        private void ValidarInstructor(ClInstructor i)
        {
            if (string.IsNullOrWhiteSpace(i.nombres)) throw new ArgumentException("Nombres obligatorios.");
            if (string.IsNullOrWhiteSpace(i.apellidos)) throw new ArgumentException("Apellidos obligatorios.");
            if (string.IsNullOrWhiteSpace(i.correo)) throw new ArgumentException("Correo obligatorio.");
            if (i.idUsuario <= 0) throw new ArgumentException("Debe asignar un usuario.");
        }
    }
}