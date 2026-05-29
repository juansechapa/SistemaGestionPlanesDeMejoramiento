using sistemaGestionPlanesDeMejoramiento.datos;
using sistemaGestionPlanesDeMejoramiento.Modelo;
using System;
using System.Collections.Generic;

namespace sistemaGestionPlanesDeMejoramiento.logica
{
    public class ClInstructorL
    {
        ClInstructorD instructorD = new ClInstructorD();

        public bool InsertarInstructorConUsuario(ClInstructor instructor, string username, string password)
        {
            ValidarInstructor(instructor);
            if (string.IsNullOrWhiteSpace(username)) throw new ArgumentException("El nombre de usuario es obligatorio.");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("La contraseña es obligatoria.");
            return instructorD.InsertarInstructorConUsuario(instructor, username, password);
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

        public ClInstructor ObtenerInstructorPorIdUsuario(int idUsuario)
        {
            if (idUsuario <= 0) throw new ArgumentException("Usuario inválido.");
            return instructorD.ObtenerInstructorPorIdUsuario(idUsuario);
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
            if (string.IsNullOrWhiteSpace(i.tipoDocumento)) throw new ArgumentException("Tipo de documento obligatorio.");
            if (string.IsNullOrWhiteSpace(i.numeroDocumento)) throw new ArgumentException("Número de documento obligatorio.");
            if (string.IsNullOrWhiteSpace(i.correo)) throw new ArgumentException("Correo obligatorio.");
        }
    }
}
