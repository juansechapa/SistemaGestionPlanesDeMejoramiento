using sistemaGestionPlanesDeMejoramiento.logica;
using sistemaGestionPlanesDeMejoramiento.Modelo;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace sistemaGestionPlanesDeMejoramiento.vista.Admin
{
    public partial class GestionProgramas : System.Web.UI.Page
    {
        ClProgramaL programaL = new ClProgramaL();

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
            gvProgramas.DataSource = programaL.ListarProgramas();
            gvProgramas.DataBind();
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            hfIdPrograma.Value = "0";
            litModalTitulo.Text = "Nuevo Programa";
            LimpiarCampos();
            ClientScript.RegisterStartupScript(this.GetType(), "show", "mostrarModal();", true);
        }

        private void LimpiarCampos()
        {
            txtCodigo.Text = "";
            txtNombre.Text = "";
            txtDescripcion.Text = "";
            txtVersion.Text = "";
            ddlNivel.SelectedIndex = 0;
            txtDuracionHoras.Text = "";
            chkEstado.Checked = true;
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;

            try
            {
                ClPrograma programa = new ClPrograma();
                programa.codigoPrograma = txtCodigo.Text.Trim();
                programa.nombre = txtNombre.Text.Trim();
                programa.descripcion = string.IsNullOrWhiteSpace(txtDescripcion.Text) ? null : txtDescripcion.Text.Trim();
                programa.version = string.IsNullOrWhiteSpace(txtVersion.Text) ? null : txtVersion.Text.Trim();
                programa.nivel = ddlNivel.SelectedValue;
                programa.duracionHoras = string.IsNullOrWhiteSpace(txtDuracionHoras.Text) ? (int?)null : Convert.ToInt32(txtDuracionHoras.Text);
                programa.estado = chkEstado.Checked;

                bool ok;
                if (hfIdPrograma.Value == "0")
                    ok = programaL.InsertarPrograma(programa);
                else
                {
                    programa.idPrograma = Convert.ToInt32(hfIdPrograma.Value);
                    ok = programaL.ActualizarPrograma(programa);
                }

                if (ok)
                {
                    CargarGrid();
                    ClientScript.RegisterStartupScript(this.GetType(), "hide", "ocultarModal();", true);
                    MostrarAlerta("Programa guardado correctamente.", "success");
                }
                else
                    MostrarAlerta("Error al guardar programa.", "danger");
            }
            catch (Exception ex)
            {
                MostrarAlerta("Error: " + ex.Message, "danger");
            }
        }

        protected void gvProgramas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int idPrograma = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "Editar")
            {
                var programa = programaL.ListarProgramas().Find(p => p.idPrograma == idPrograma);
                if (programa != null)
                {
                    hfIdPrograma.Value = programa.idPrograma.ToString();
                    litModalTitulo.Text = "Editar Programa";
                    txtCodigo.Text = programa.codigoPrograma;
                    txtNombre.Text = programa.nombre;
                    txtDescripcion.Text = programa.descripcion;
                    txtVersion.Text = programa.version;
                    if (!string.IsNullOrEmpty(programa.nivel))
                    {
                        ListItem itemNivel = ddlNivel.Items.FindByValue(programa.nivel);
                        if (itemNivel != null)
                        {
                            ddlNivel.ClearSelection();
                            itemNivel.Selected = true;
                        }
                        else
                        {
                            ddlNivel.SelectedIndex = 0;
                        }
                    }
                    else
                    {
                        ddlNivel.SelectedIndex = 0;
                    }
                    txtDuracionHoras.Text = programa.duracionHoras.HasValue ? programa.duracionHoras.Value.ToString() : "";
                    chkEstado.Checked = programa.estado;
                    ClientScript.RegisterStartupScript(this.GetType(), "show", "mostrarModal();", true);
                }
            }
            else if (e.CommandName == "Eliminar")
            {
                try
                {
                    if (programaL.EliminarPrograma(idPrograma))
                    {
                        CargarGrid();
                        MostrarAlerta("Programa eliminado.", "warning");
                    }
                    else
                        MostrarAlerta("No se pudo eliminar el programa.", "danger");
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
