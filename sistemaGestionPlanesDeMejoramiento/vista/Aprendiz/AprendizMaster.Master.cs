using sistemaGestionPlanesDeMejoramiento.logica;
using sistemaGestionPlanesDeMejoramiento.Modelo;
using System;
using System.Web.UI;

namespace sistemaGestionPlanesDeMejoramiento.vista.Aprendiz
{
    public partial class InstructoMaster : System.Web.UI.MasterPage
    {
        ClUsuarioL usuarioL = new ClUsuarioL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null || Convert.ToInt32(Session["idRol"]) != 3)
            {
                Response.Redirect("~/vista/Login.aspx");
                return;
            }

            if (!IsPostBack)
                lblUsuario.Text = Session["username"].ToString();
        }

        protected void btnCambiarAcceso_Click(object sender, EventArgs e)
        {
            txtUsuarioAcceso.Text = Session["username"].ToString();
            txtClaveAcceso.Text = "";
            txtConfirmarClaveAcceso.Text = "";
            lblAccesoMensaje.Text = "";
            ScriptManager.RegisterStartupScript(this, GetType(), "showAcceso", "mostrarModalAcceso();", true);
        }

        protected void btnGuardarAcceso_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showAccesoInvalid", "mostrarModalAcceso();", true);
                return;
            }

            string password = txtClaveAcceso.Text.Trim();
            string confirmar = txtConfirmarClaveAcceso.Text.Trim();
            if (password != confirmar)
            {
                lblAccesoMensaje.CssClass = "alert alert-danger d-block mt-2";
                lblAccesoMensaje.Text = "Las contraseñas no coinciden.";
                ScriptManager.RegisterStartupScript(this, GetType(), "showAccesoPass", "mostrarModalAcceso(); alert('No se pudieron actualizar las credenciales.');", true);
                return;
            }

            try
            {
                int idUsuario = Convert.ToInt32(Session["idUsuario"]);
                string username = txtUsuarioAcceso.Text.Trim();
                if (usuarioL.ActualizarCredenciales(idUsuario, username, password))
                {
                    Session["username"] = username;
                    ClUsuario usuario = Session["Usuario"] as ClUsuario;
                    if (usuario != null) usuario.username = username;
                    lblUsuario.Text = username;
                    lblAccesoMensaje.CssClass = "alert alert-success d-block mt-2";
                    lblAccesoMensaje.Text = "Acceso actualizado correctamente.";
                    ScriptManager.RegisterStartupScript(this, GetType(), "hideAcceso", "ocultarModalAcceso(); alert('Credenciales actualizadas correctamente.');", true);
                }
                else
                {
                    lblAccesoMensaje.CssClass = "alert alert-danger d-block mt-2";
                    lblAccesoMensaje.Text = "No se pudo actualizar el acceso.";
                    ScriptManager.RegisterStartupScript(this, GetType(), "showAccesoFail", "mostrarModalAcceso(); alert('No se pudieron actualizar las credenciales.');", true);
                }
            }
            catch (Exception ex)
            {
                lblAccesoMensaje.CssClass = "alert alert-danger d-block mt-2";
                lblAccesoMensaje.Text = ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "showAccesoError", "mostrarModalAcceso(); alert('No se pudieron actualizar las credenciales.');", true);
            }
        }

        protected void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("~/vista/Login.aspx");
        }
    }
}
