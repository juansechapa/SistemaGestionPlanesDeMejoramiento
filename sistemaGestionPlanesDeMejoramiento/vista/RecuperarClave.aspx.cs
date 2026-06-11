using sistemaGestionPlanesDeMejoramiento.logica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace sistemaGestionPlanesDeMejoramiento.vista
{
    public partial class RecuperarContrasena : System.Web.UI.Page
    {
        protected async void btnEnviar_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;

            string correo = txtCorreo.Text.Trim();
            //revisar esto
            var logica = new ClPasswordResetL();
            try
            {
                bool enviado = await logica.SolicitarCodigo(correo);
                if (enviado)
                {
                    lblMensaje.Text = "Código enviado correctamente. Revisa tu correo e ingrésalo en la opción Ya tengo un código.";
                    lblMensaje.CssClass = "alert alert-success w-100";
                }
                else
                {
                    lblMensaje.Text = "No se encontró un usuario asociado a ese correo.";
                    lblMensaje.CssClass = "alert alert-warning w-100";
                }
                lblMensaje.Visible = true;
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "No se pudo enviar el código: " + ex.Message;
                lblMensaje.CssClass = "alert alert-danger w-100";
                lblMensaje.Visible = true;
            }
        }
    }
}
