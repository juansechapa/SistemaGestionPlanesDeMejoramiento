using sistemaGestionPlanesDeMejoramiento.datos;
using sistemaGestionPlanesDeMejoramiento.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sistemaGestionPlanesDeMejoramiento.logica
{
    public class ClUsuarioL
    {
        ClUsuarioD usuarioD = new ClUsuarioD();
        public ClUsuario validarLogin(String username, String password)
        {
            return usuarioD.Login(username, password);
        }

        public int ObtenerIdInstructorPorIdUsuario(int idUsuario)
        {
            return usuarioD.ObtenerIdInstructorPorIdUsuario(idUsuario);
        }

        public ClUsuario ObtenerUsuarioPorId(int idUsuario)
        {
            if (idUsuario <= 0) throw new ArgumentException("Usuario inválido.");
            return usuarioD.ObtenerUsuarioPorId(idUsuario);
        }

        public bool ActualizarCredenciales(int idUsuario, string username, string password)
        {
            if (idUsuario <= 0) throw new ArgumentException("Usuario inválido.");
            if (string.IsNullOrWhiteSpace(username)) throw new ArgumentException("El nombre de usuario es obligatorio.");
            return usuarioD.ActualizarCredenciales(idUsuario, username.Trim(), password);
        }

        public bool AprendizEstaCanceladoPorIdUsuario(int idUsuario)
        {
            return usuarioD.AprendizEstaCanceladoPorIdUsuario(idUsuario);
        }
    }
}
