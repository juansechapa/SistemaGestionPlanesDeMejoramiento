using sistemaGestionPlanesDeMejoramiento.logica;
using sistemaGestionPlanesDeMejoramiento.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace sistemaGestionPlanesDeMejoramiento.vista
{
    public partial class Login : System.Web.UI.Page
    {
        ClUsuarioL usuarioL =  new ClUsuarioL();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnIngresar_Click(object sender, EventArgs e)
        {
            string usuario = txtUsuario.Text.Trim();
            string password = txtPassword.Text.Trim();

            if(string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(password))
            {
                lblMensaje.Text = "Usuario o contraseña son obligatorios.";
                return;
            }

            ClUsuario objUsuario = usuarioL.validarLogin(usuario, password);

            if (objUsuario != null) 
            {
                Session["Usuario"] = objUsuario;
                Session["idUsuario"] = objUsuario.idUsuario;
                Session["username"] = objUsuario.username;
                Session["idRol"] = objUsuario.idRol;

                switch (objUsuario.idRol)
                {
                    case 1:  
                        Response.Redirect("Admin/InicioAdmin.aspx");
                        break;
                    case 2:  
                        Response.Redirect("Instructor/InicioInstructor.aspx");
                        break;
                    case 3:
                        Response.Redirect("Aprendiz/InicioAprendiz.aspx");
                        break;
                    default:
                        lblMensaje.Text = "Rol no reconocido. Contacte al administrador.";
                        break;
                }
            }
            else
            {
                lblMensaje.Text = "Usuario o contraseña incorrectos.";
            }
        }  
    }
}