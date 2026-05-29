using sistemaGestionPlanesDeMejoramiento.datos;
using sistemaGestionPlanesDeMejoramiento.Modelo;
using System;
using System.Collections.Generic;

namespace sistemaGestionPlanesDeMejoramiento.logica
{
    public class ClAprendizL
    {
        ClAprendizD aprendizD = new ClAprendizD();

        public bool InsertarAprendizConUsuario(ClAprendiz aprendiz, string username, string password)
        {
            ValidarAprendiz(aprendiz);
            if (string.IsNullOrWhiteSpace(username)) throw new ArgumentException("Nombre de usuario obligatorio.");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Contraseña obligatoria.");
            return aprendizD.InsertarAprendizConUsuario(aprendiz, username, password);
        }

        public List<ClAprendiz> ListarAprendices()
        {
            return aprendizD.ListarAprendices();
        }

        public bool ActualizarAprendiz(ClAprendiz aprendiz)
        {
            if (aprendiz.idAprendiz <= 0) throw new ArgumentException("ID inválido");
            ValidarAprendiz(aprendiz);
            return aprendizD.ActualizarAprendiz(aprendiz);
        }

        public System.Data.DataTable ListarAprendicesPorInstructor(int idInstructor)
        {
            return aprendizD.ListarAprendicesPorInstructor(idInstructor);
        }

        public bool AprendizEstaCancelado(int idAprendiz)
        {
            if (idAprendiz <= 0) throw new ArgumentException("Aprendiz inválido.");
            return aprendizD.AprendizEstaCancelado(idAprendiz);
        }

        public ClAprendiz ObtenerAprendizPorIdUsuario(int idUsuario)
        {
            if (idUsuario <= 0) throw new ArgumentException("Usuario inválido.");
            return aprendizD.ObtenerAprendizPorIdUsuario(idUsuario);
        }

        public int ContarAprendicesPorFicha(int idFicha)
        {
            if (idFicha <= 0) throw new ArgumentException("Ficha inválida.");
            return aprendizD.ContarAprendicesPorFicha(idFicha);
        }

        public bool EliminarAprendiz(int idAprendiz)
        {
            if (idAprendiz <= 0) throw new ArgumentException("ID inválido");
            return aprendizD.EliminarAprendiz(idAprendiz);
        }

        private void ValidarAprendiz(ClAprendiz a)
        {
            if (string.IsNullOrWhiteSpace(a.nombres)) throw new ArgumentException("Nombres obligatorios.");
            if (string.IsNullOrWhiteSpace(a.apellidos)) throw new ArgumentException("Apellidos obligatorios.");
            if (string.IsNullOrWhiteSpace(a.tipoDocumento)) throw new ArgumentException("Tipo documento obligatorio.");
            if (string.IsNullOrWhiteSpace(a.numeroDocumento)) throw new ArgumentException("Número documento obligatorio.");
            if (string.IsNullOrWhiteSpace(a.correo)) throw new ArgumentException("Correo obligatorio.");
            if (a.fechaNacimiento == DateTime.MinValue) throw new ArgumentException("Fecha nacimiento obligatoria.");
            if (a.idFicha <= 0) throw new ArgumentException("Debe seleccionar una ficha.");
        }
        public List<ClResultado> ObtenerResultadosPendientes(int idAprendiz)
        {
            return aprendizD.ObtenerResultadosPendientes(idAprendiz);
        }
    }
}
