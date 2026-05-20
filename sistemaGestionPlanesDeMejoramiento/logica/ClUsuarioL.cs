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
    }
}