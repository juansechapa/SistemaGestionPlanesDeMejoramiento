using sistemaGestionPlanesDeMejoramiento.logica;
using sistemaGestionPlanesDeMejoramiento.Modelo;
using System;
using System.Web.UI;

namespace sistemaGestionPlanesDeMejoramiento.vista.Admin
{
    public partial class InicioAdmin : System.Web.UI.Page
    {
        ClAdministradorL administradorL = new ClAdministradorL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null || Convert.ToInt32(Session["idRol"]) != 1)
            {
                Response.Redirect("~/vista/Login.aspx");
                return;
            }

            if (!IsPostBack)
                CargarDatosAdministrador();
        }

        private void CargarDatosAdministrador()
        {
            var administrador = ObtenerAdministradorActual();
            if (administrador == null)
            {
                lblBienvenida.Text = Session["username"].ToString();
                return;
            }

            lblBienvenida.Text = administrador.nombres;
            lblNombres.Text = administrador.nombres;
            lblApellidos.Text = administrador.apellidos;
            lblTipoDoc.Text = administrador.tipoDocumento;
            lblNumDoc.Text = administrador.numeroDocumento;
            lblCorreo.Text = administrador.correo;
            lblTelefono.Text = administrador.telefono;
        }

        protected void btnEditarDatos_Click(object sender, EventArgs e)
        {
            var administrador = ObtenerAdministradorActual();
            if (administrador == null)
            {
                MostrarMensaje("No se pudo cargar la informacion del administrador.", "danger");
                return;
            }

            txtNombres.Text = administrador.nombres;
            txtApellidos.Text = administrador.apellidos;
            txtCorreo.Text = administrador.correo;
            txtTelefono.Text = administrador.telefono;
            ScriptManager.RegisterStartupScript(this, GetType(), "showDatosAdmin", "mostrarModalDatosAdmin();", true);
        }

        protected void btnGuardarDatos_Click(object sender, EventArgs e)
        {
            try
            {
                var administrador = ObtenerAdministradorActual();
                if (administrador == null)
                {
                    MostrarMensaje("No se pudo cargar la informacion del administrador.", "danger");
                    return;
                }

                administrador.nombres = txtNombres.Text.Trim();
                administrador.apellidos = txtApellidos.Text.Trim();
                administrador.correo = txtCorreo.Text.Trim();
                administrador.telefono = txtTelefono.Text.Trim();

                if (administradorL.ActualizarAdministrador(administrador))
                {
                    CargarDatosAdministrador();
                    MostrarMensaje("Informacion actualizada correctamente.", "success");
                    ScriptManager.RegisterStartupScript(this, GetType(), "hideDatosAdmin", "ocultarModalDatosAdmin();", true);
                }
                else
                {
                    MostrarMensaje("No se pudo actualizar la informacion.", "danger");
                    ScriptManager.RegisterStartupScript(this, GetType(), "showDatosAdminFail", "mostrarModalDatosAdmin();", true);
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error: " + ex.Message, "danger");
                ScriptManager.RegisterStartupScript(this, GetType(), "showDatosAdminError", "mostrarModalDatosAdmin();", true);
            }
        }

        private ClAdministrador ObtenerAdministradorActual()
        {
            int idUsuario = Convert.ToInt32(Session["idUsuario"]);
            return administradorL.ObtenerAdministradorPorIdUsuario(idUsuario);
        }

        private void MostrarMensaje(string mensaje, string tipo)
        {
            lblMensaje.Text = mensaje;
            lblMensaje.CssClass = "alert alert-" + tipo + " d-block mb-3";
        }
    }
}
