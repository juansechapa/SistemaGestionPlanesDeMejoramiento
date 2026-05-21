using sistemaGestionPlanesDeMejoramiento.logica;
using sistemaGestionPlanesDeMejoramiento.Modelo;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace sistemaGestionPlanesDeMejoramiento.vista.Admin
{
    public partial class GestionInstructores : System.Web.UI.Page
    {
        private ClInstructorL instructorL = new ClInstructorL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null || Convert.ToInt32(Session["idRol"]) != 1)
            {
                Response.Redirect("~/vista/Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                CargarGrid();
            }
        }

        private void CargarGrid()
        {
            gvInstructores.DataSource = instructorL.ListarInstructores();
            gvInstructores.DataBind();
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            hfIdInstructor.Value = "0";
            litModalTitulo.Text = "Nuevo Instructor";
            LimpiarCampos();
            ClientScript.RegisterStartupScript(this.GetType(), "ShowModal", "mostrarModal();", true);
        }

        private void LimpiarCampos()
        {
            txtNombres.Text = "";
            txtApellidos.Text = "";
            txtCorreo.Text = "";
            txtTelefono.Text = "";
            txtEspecialidad.Text = "";
            txtIdUsuario.Text = "";
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;

            try
            {
                ClInstructor instructor = new ClInstructor();
                instructor.nombres = txtNombres.Text.Trim();
                instructor.apellidos = txtApellidos.Text.Trim();
                instructor.correo = txtCorreo.Text.Trim();
                instructor.telefono = txtTelefono.Text.Trim();
                instructor.especialidad = txtEspecialidad.Text.Trim();
                instructor.idUsuario = Convert.ToInt32(txtIdUsuario.Text);

                bool ok;
                if (hfIdInstructor.Value == "0")
                {
                    ok = instructorL.InsertarInstructor(instructor);
                }
                else
                {
                    instructor.idInstructor = Convert.ToInt32(hfIdInstructor.Value);
                    ok = instructorL.ActualizarInstructor(instructor);
                }

                if (ok)
                {
                    CargarGrid();
                    ClientScript.RegisterStartupScript(this.GetType(), "HideModal", "ocultarModal();", true);
                    MostrarAlerta("Instructor guardado correctamente.", "success");
                }
                else
                {
                    MostrarAlerta("Error al guardar instructor.", "danger");
                }
            }
            catch (Exception ex)
            {
                MostrarAlerta("Error: " + ex.Message, "danger");
            }
        }

        protected void gvInstructores_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int idInstructor = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "Editar")
            {
                var instructor = instructorL.ListarInstructores().Find(i => i.idInstructor == idInstructor);
                if (instructor != null)
                {
                    hfIdInstructor.Value = instructor.idInstructor.ToString();
                    litModalTitulo.Text = "Editar Instructor";
                    txtNombres.Text = instructor.nombres;
                    txtApellidos.Text = instructor.apellidos;
                    txtCorreo.Text = instructor.correo;
                    txtTelefono.Text = instructor.telefono;
                    txtEspecialidad.Text = instructor.especialidad;
                    txtIdUsuario.Text = instructor.idUsuario.ToString();
                    ClientScript.RegisterStartupScript(this.GetType(), "ShowModal", "mostrarModal();", true);
                }
            }
            else if (e.CommandName == "Eliminar")
            {
                try
                {
                    if (instructorL.EliminarInstructor(idInstructor))
                    {
                        CargarGrid();
                        MostrarAlerta("Instructor eliminado correctamente.", "warning");
                    }
                    else
                    {
                        MostrarAlerta("No se pudo eliminar el instructor.", "danger");
                    }
                }
                catch (Exception ex)
                {
                    MostrarAlerta("Error: " + ex.Message, "danger");
                }
            }
        }

        private void MostrarAlerta(string mensaje, string tipo)
        {
            string script = $"alert('{mensaje}');";
            ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
        }
    }
}