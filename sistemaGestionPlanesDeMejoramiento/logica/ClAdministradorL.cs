using sistemaGestionPlanesDeMejoramiento.datos;
using sistemaGestionPlanesDeMejoramiento.Modelo;
using System;
using System.Collections.Generic;

namespace sistemaGestionPlanesDeMejoramiento.logica
{
    public class ClAdministradorL
    {
        ClAdministradorD administradorD = new ClAdministradorD();

        public bool InsertarAdministradorConUsuario(ClAdministrador administrador, string username, string password)
        {
            ValidarAdministrador(administrador);
            if (string.IsNullOrWhiteSpace(username)) throw new ArgumentException("El usuario es obligatorio.");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("La contraseña es obligatoria.");
            return administradorD.InsertarAdministradorConUsuario(administrador, username.Trim(), password);
        }

        public List<ClAdministrador> ListarAdministradores()
        {
            return administradorD.ListarAdministradores();
        }

        public ClAdministrador ObtenerAdministradorPorId(int idAdministrador)
        {
            if (idAdministrador <= 0) throw new ArgumentException("Administrador inválido.");
            return administradorD.ObtenerAdministradorPorId(idAdministrador);
        }

        public ClAdministrador ObtenerAdministradorPorIdUsuario(int idUsuario)
        {
            if (idUsuario <= 0) throw new ArgumentException("Usuario inválido.");
            return administradorD.ObtenerAdministradorPorIdUsuario(idUsuario);
        }

        public bool ActualizarAdministrador(ClAdministrador administrador)
        {
            if (administrador.idAmin <= 0) throw new ArgumentException("Administrador inválido.");
            ValidarAdministrador(administrador);
            return administradorD.ActualizarAdministrador(administrador);
        }

        public bool EliminarAdministrador(int idAdministrador)
        {
            if (idAdministrador <= 0) throw new ArgumentException("Administrador inválido.");
            return administradorD.EliminarAdministrador(idAdministrador);
        }

        private void ValidarAdministrador(ClAdministrador administrador)
        {
            if (administrador == null) throw new ArgumentException("Administrador inválido.");
            if (string.IsNullOrWhiteSpace(administrador.nombres)) throw new ArgumentException("Nombres obligatorios.");
            if (string.IsNullOrWhiteSpace(administrador.apellidos)) throw new ArgumentException("Apellidos obligatorios.");
            if (string.IsNullOrWhiteSpace(administrador.tipoDocumento)) throw new ArgumentException("Tipo de documento obligatorio.");
            if (string.IsNullOrWhiteSpace(administrador.numeroDocumento)) throw new ArgumentException("Número de documento obligatorio.");
            if (string.IsNullOrWhiteSpace(administrador.correo)) throw new ArgumentException("Correo obligatorio.");
        }
    }
}
