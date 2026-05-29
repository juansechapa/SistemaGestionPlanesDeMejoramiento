using sistemaGestionPlanesDeMejoramiento.logica;
using sistemaGestionPlanesDeMejoramiento.Modelo;
using System;
using System.Web.UI;

namespace sistemaGestionPlanesDeMejoramiento.vista.Instructor
{
    public partial class InicioInstructor : System.Web.UI.Page
    {
        ClInstructorL instructorL = new ClInstructorL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null || Convert.ToInt32(Session["idRol"]) != 2)
            {
                Response.Redirect("~/vista/Login.aspx");
                return;
            }

            if (!IsPostBack)
                CargarDatosInstructor();
        }

        private void CargarDatosInstructor()
        {
            var instructor = ObtenerInstructorActual();
            if (instructor == null)
                return;

            lblBienvenida.Text = instructor.nombres;
            lblNombres.Text = instructor.nombres;
            lblApellidos.Text = instructor.apellidos;
            lblTipoDoc.Text = instructor.tipoDocumento;
            lblNumDoc.Text = instructor.numeroDocumento;
            lblCorreo.Text = instructor.correo;
            lblTelefono.Text = instructor.telefono;
            lblEspecialidad.Text = instructor.especialidad;
        }

        protected void btnEditarDatos_Click(object sender, EventArgs e)
        {
            var instructor = ObtenerInstructorActual();
            if (instructor == null)
            {
                MostrarMensaje("No se pudo cargar la informacion del instructor.", "danger");
                return;
            }

            txtNombres.Text = instructor.nombres;
            txtApellidos.Text = instructor.apellidos;
            txtCorreo.Text = instructor.correo;
            txtTelefono.Text = instructor.telefono;
            txtEspecialidad.Text = instructor.especialidad;
            ScriptManager.RegisterStartupScript(this, GetType(), "showDatosInstructor", "mostrarModalDatosInstructor();", true);
        }

        protected void btnGuardarDatos_Click(object sender, EventArgs e)
        {
            try
            {
                var instructor = ObtenerInstructorActual();
                if (instructor == null)
                {
                    MostrarMensaje("No se pudo cargar la informacion del instructor.", "danger");
                    return;
                }

                instructor.nombres = txtNombres.Text.Trim();
                instructor.apellidos = txtApellidos.Text.Trim();
                instructor.correo = txtCorreo.Text.Trim();
                instructor.telefono = txtTelefono.Text.Trim();
                instructor.especialidad = txtEspecialidad.Text.Trim();

                if (instructorL.ActualizarInstructor(instructor))
                {
                    CargarDatosInstructor();
                    MostrarMensaje("Informacion actualizada correctamente.", "success");
                    ScriptManager.RegisterStartupScript(this, GetType(), "hideDatosInstructor", "ocultarModalDatosInstructor();", true);
                }
                else
                {
                    MostrarMensaje("No se pudo actualizar la informacion.", "danger");
                    ScriptManager.RegisterStartupScript(this, GetType(), "showDatosInstructorFail", "mostrarModalDatosInstructor();", true);
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error: " + ex.Message, "danger");
                ScriptManager.RegisterStartupScript(this, GetType(), "showDatosInstructorError", "mostrarModalDatosInstructor();", true);
            }
        }

        private ClInstructor ObtenerInstructorActual()
        {
            int idUsuario = Convert.ToInt32(Session["idUsuario"]);
            return instructorL.ObtenerInstructorPorIdUsuario(idUsuario);
        }

        private void MostrarMensaje(string mensaje, string tipo)
        {
            lblMensaje.Text = mensaje;
            lblMensaje.CssClass = "alert alert-" + tipo + " d-block mb-3";
        }
    }
}
