using sistemaGestionPlanesDeMejoramiento.logica;
using sistemaGestionPlanesDeMejoramiento.Modelo;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace sistemaGestionPlanesDeMejoramiento.vista.Admin
{
    public partial class GestionAdministradores : System.Web.UI.Page
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
                CargarGrid();
        }

        private void CargarGrid()
        {
            gvAdministradores.DataSource = administradorL.ListarAdministradores();
            gvAdministradores.DataBind();
        }

        protected void gvAdministradores_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvAdministradores.PageIndex = e.NewPageIndex;
            CargarGrid();
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            hfIdAdministrador.Value = "0";
            litModalTitulo.Text = "Nuevo Administrador";
            divCredenciales.Visible = true;
            rfvUsername.Enabled = true;
            rfvPassword.Enabled = true;
            LimpiarCampos();
            ScriptManager.RegisterStartupScript(this, GetType(), "showAdmin", "mostrarModalAdministrador();", true);
        }

        private void LimpiarCampos()
        {
            txtNombres.Text = "";
            txtApellidos.Text = "";
            ddlTipoDocumento.SelectedIndex = 0;
            txtNumeroDocumento.Text = "";
            txtTelefono.Text = "";
            txtCorreo.Text = "";
            txtUsername.Text = "";
            txtPassword.Text = "";
            lblMensaje.Text = "";
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showAdminInvalid", "mostrarModalAdministrador();", true);
                return;
            }

            try
            {
                ClAdministrador administrador = new ClAdministrador
                {
                    nombres = txtNombres.Text.Trim(),
                    apellidos = txtApellidos.Text.Trim(),
                    tipoDocumento = ddlTipoDocumento.SelectedValue,
                    numeroDocumento = txtNumeroDocumento.Text.Trim(),
                    telefono = txtTelefono.Text.Trim(),
                    correo = txtCorreo.Text.Trim()
                };

                bool ok;
                if (hfIdAdministrador.Value == "0")
                    ok = administradorL.InsertarAdministradorConUsuario(administrador, txtUsername.Text.Trim(), txtPassword.Text.Trim());
                else
                {
                    administrador.idAmin = Convert.ToInt32(hfIdAdministrador.Value);
                    ok = administradorL.ActualizarAdministrador(administrador);
                }

                if (ok)
                {
                    CargarGrid();
                    MostrarMensaje("Administrador guardado correctamente.", "success");
                    ScriptManager.RegisterStartupScript(this, GetType(), "hideAdmin", "ocultarModalAdministrador();", true);
                }
                else
                    MostrarMensaje("No se pudo guardar el administrador.", "danger");
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error: " + ex.Message, "danger");
                ScriptManager.RegisterStartupScript(this, GetType(), "showAdminError", "mostrarModalAdministrador();", true);
            }
        }

        protected void gvAdministradores_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int idAdministrador = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "Editar")
            {
                ClAdministrador administrador = administradorL.ObtenerAdministradorPorId(idAdministrador);
                if (administrador == null)
                {
                    MostrarMensaje("Administrador no encontrado.", "danger");
                    return;
                }

                hfIdAdministrador.Value = administrador.idAmin.ToString();
                litModalTitulo.Text = "Editar Administrador";
                txtNombres.Text = administrador.nombres;
                txtApellidos.Text = administrador.apellidos;
                SeleccionarValor(ddlTipoDocumento, administrador.tipoDocumento);
                txtNumeroDocumento.Text = administrador.numeroDocumento;
                txtTelefono.Text = administrador.telefono;
                txtCorreo.Text = administrador.correo;
                txtUsername.Text = "";
                txtPassword.Text = "";
                divCredenciales.Visible = false;
                rfvUsername.Enabled = false;
                rfvPassword.Enabled = false;
                ScriptManager.RegisterStartupScript(this, GetType(), "showAdminEdit", "mostrarModalAdministrador();", true);
            }
            else if (e.CommandName == "Eliminar")
            {
                try
                {
                    ClAdministrador administrador = administradorL.ObtenerAdministradorPorId(idAdministrador);
                    if (administrador != null && administrador.idUsuario == Convert.ToInt32(Session["idUsuario"]))
                    {
                        MostrarMensaje("No puede eliminar su propio usuario administrador desde esta vista.", "warning");
                        return;
                    }

                    if (administradorL.EliminarAdministrador(idAdministrador))
                    {
                        CargarGrid();
                        MostrarMensaje("Administrador eliminado correctamente.", "warning");
                    }
                    else
                        MostrarMensaje("No se pudo eliminar el administrador.", "danger");
                }
                catch (Exception ex)
                {
                    MostrarMensaje("Error: " + ex.Message, "danger");
                }
            }
        }

        private void SeleccionarValor(DropDownList ddl, string valor)
        {
            ListItem item = ddl.Items.FindByValue(valor ?? "");
            ddl.ClearSelection();
            if (item != null)
                item.Selected = true;
            else if (ddl.Items.Count > 0)
                ddl.SelectedIndex = 0;
        }

        private void MostrarMensaje(string mensaje, string tipo)
        {
            lblMensaje.Text = mensaje;
            lblMensaje.CssClass = "alert alert-" + tipo + " d-block mb-3";
        }
    }
}
