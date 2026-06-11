using sistemaGestionPlanesDeMejoramiento.logica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace sistemaGestionPlanesDeMejoramiento.vista
{
    public partial class ReestablecerClave : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && !string.IsNullOrWhiteSpace(Request.QueryString["codigo"]))
                txtCodigo.Text = Request.QueryString["codigo"].Trim().ToUpperInvariant();
        }

        protected void btnRestablecer_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;

            string codigo = txtCodigo.Text.Trim().ToUpperInvariant();
            string nuevaPassword = txtNuevaPassword.Text;
            string confirmar = txtConfirmarPassword.Text;

            if (nuevaPassword != confirmar)
            {
                MostrarError("Las contraseñas no coinciden.");
                return;
            }

            //yoca revisar este metodo
            var logica = new ClPasswordResetL();
            try
            {
                if (logica.RestablecerPassword(codigo, nuevaPassword))
                {
                    lblMensaje.Text = "Contraseña restablecida correctamente. Redirigiendo al login...";
                    lblMensaje.CssClass = "alert alert-success w-100";
                    lblMensaje.Visible = true;
                    Response.AddHeader("REFRESH", "3;URL=Login.aspx");
                }
                else
                {
                    MostrarError("Código inválido o expirado. Solicita uno nuevo.");
                }
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message);
            }
        }

        private void MostrarError(string mensaje)
        {
            lblMensaje.Text = mensaje;
            lblMensaje.CssClass = "alert alert-danger w-100";
            lblMensaje.Visible = true;
        }
    }
}
