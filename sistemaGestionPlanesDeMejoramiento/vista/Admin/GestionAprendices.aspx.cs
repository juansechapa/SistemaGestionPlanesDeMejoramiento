using sistemaGestionPlanesDeMejoramiento.logica;
using sistemaGestionPlanesDeMejoramiento.Modelo;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace sistemaGestionPlanesDeMejoramiento.vista.Admin
{
    public partial class GestionAprendices : System.Web.UI.Page
    {
        ClAprendizL aprendizL = new ClAprendizL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null || Convert.ToInt32(Session["idRol"]) != 1)
                Response.Redirect("~/vista/Login.aspx");
            if (!IsPostBack)
                CargarGrid();
        }

        private void CargarGrid()
        {
            gvAprendices.DataSource = aprendizL.ListarAprendices();
            gvAprendices.DataBind();
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            hfIdAprendiz.Value = "0";
            litTitulo.Text = "Nuevo Aprendiz";
            LimpiarCampos();
            ClientScript.RegisterStartupScript(this.GetType(), "show", "mostrarModal();", true);
        }

        private void LimpiarCampos()
        {
            txtNombres.Text = "";
            txtApellidos.Text = "";
            ddlTipoDoc.SelectedIndex = 0;
            txtNumDoc.Text = "";
            txtCorreo.Text = "";
            txtTelefono.Text = "";
            txtFechaNac.Text = "";
            txtIdFicha.Text = "";
            txtIdUsuario.Text = "";
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;

            try
            {
                ClAprendiz a = new ClAprendiz();
                a.nombres = txtNombres.Text.Trim();
                a.apellidos = txtApellidos.Text.Trim();
                a.tipoDocumento = ddlTipoDoc.SelectedValue;
                a.numeroDocumento = txtNumDoc.Text.Trim();
                a.correo = txtCorreo.Text.Trim();
                a.telefono = txtTelefono.Text.Trim();
                a.fechaNacimiento = DateTime.Parse(txtFechaNac.Text);
                a.idFicha = int.Parse(txtIdFicha.Text);
                a.idUsuario = int.Parse(txtIdUsuario.Text);

                bool ok;
                if (hfIdAprendiz.Value == "0")
                    ok = aprendizL.InsertarAprendiz(a);
                else
                {
                    a.idAprendiz = int.Parse(hfIdAprendiz.Value);
                    ok = aprendizL.ActualizarAprendiz(a);
                }

                if (ok)
                {
                    CargarGrid();
                    ClientScript.RegisterStartupScript(this.GetType(), "hide", "ocultarModal();", true);
                    MostrarAlerta("Guardado correctamente.", "success");
                }
            }
            catch (Exception ex)
            {
                MostrarAlerta(ex.Message, "danger");
            }
        }

        protected void gvAprendices_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "Editar")
            {
                var aprendiz = aprendizL.ListarAprendices().Find(a => a.idAprendiz == id);
                if (aprendiz != null)
                {
                    hfIdAprendiz.Value = aprendiz.idAprendiz.ToString();
                    litTitulo.Text = "Editar Aprendiz";
                    txtNombres.Text = aprendiz.nombres;
                    txtApellidos.Text = aprendiz.apellidos;
                    ddlTipoDoc.SelectedValue = aprendiz.tipoDocumento;
                    txtNumDoc.Text = aprendiz.numeroDocumento;
                    txtCorreo.Text = aprendiz.correo;
                    txtTelefono.Text = aprendiz.telefono;
                    txtFechaNac.Text = aprendiz.fechaNacimiento.ToString("yyyy-MM-dd");
                    txtIdFicha.Text = aprendiz.idFicha.ToString();
                    txtIdUsuario.Text = aprendiz.idUsuario.ToString();
                    ClientScript.RegisterStartupScript(this.GetType(), "show", "mostrarModal();", true);
                }
            }
            else if (e.CommandName == "Eliminar")
            {
                if (aprendizL.EliminarAprendiz(id))
                {
                    CargarGrid();
                    MostrarAlerta("Eliminado correctamente.", "warning");
                }
                else MostrarAlerta("No se pudo eliminar.", "danger");
            }
        }

        private void MostrarAlerta(string msg, string tipo)
        {
            string script = $"alert('{msg}');";
            ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
        }
    }
}
