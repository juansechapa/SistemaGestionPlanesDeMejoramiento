using sistemaGestionPlanesDeMejoramiento.datos;
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
            divCredenciales.Visible = true;
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
            ddlFicha.SelectedIndex = 0;
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;
            try
            {
                ClAprendiz aprendiz = new ClAprendiz();
                aprendiz.nombres = txtNombres.Text.Trim();
                aprendiz.apellidos = txtApellidos.Text.Trim();
                aprendiz.tipoDocumento = ddlTipoDoc.SelectedValue;
                aprendiz.numeroDocumento = txtNumDoc.Text.Trim();
                aprendiz.correo = txtCorreo.Text.Trim();
                aprendiz.telefono = txtTelefono.Text.Trim();
                aprendiz.fechaNacimiento = DateTime.Parse(txtFechaNac.Text);
                aprendiz.idFicha = Convert.ToInt32(ddlFicha.SelectedValue);

                bool ok;
                if (hfIdAprendiz.Value == "0")
                {
                    string username = txtUsername.Text.Trim();
                    string password = txtPassword.Text.Trim();
                    ok = aprendizL.InsertarAprendizConUsuario(aprendiz, username, password);
                }
                else
                {
                    aprendiz.idAprendiz = Convert.ToInt32(hfIdAprendiz.Value);
                    ok = aprendizL.ActualizarAprendiz(aprendiz);
                }

                if (ok)
                {
                    CargarGrid();
                    MostrarAlerta("Aprendiz guardado correctamente.", "success");
                }
            }
            catch (Exception ex)
            {
                MostrarAlerta("Error: " + ex.Message, "danger");
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
                    ddlFicha.SelectedIndex = 0;
                    divCredenciales.Visible = false;
                    txtUsername.Text = "";
                    txtPassword.Text = "";
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


        private void CargarFichas()
        {
            ClFichaD fichaD = new ClFichaD();
            ddlFicha.DataSource = fichaD.ListarFichas();
            ddlFicha.DataBind();
            ddlFicha.Items.Insert(0, new ListItem("-- Seleccione ficha --", ""));
        }

        private void MostrarAlerta(string msg, string tipo)
        {
            string script = $"alert('{msg}');";
            ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
        }
    }
}
