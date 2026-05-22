using sistemaGestionPlanesDeMejoramiento.logica;
using sistemaGestionPlanesDeMejoramiento.Modelo;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace sistemaGestionPlanesDeMejoramiento.vista.Admin
{
    public partial class GestionFichas : System.Web.UI.Page
    {
        private ClFichaL fichaL = new ClFichaL();
        private ClCentroProgramaL cpL = new ClCentroProgramaL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null || Convert.ToInt32(Session["idRol"]) != 1)
                Response.Redirect("~/vista/Login.aspx");

            if (!IsPostBack)
            {
                CargarCentroPrograma();
                CargarGrid();
            }
        }

        private void CargarCentroPrograma()
        {
            ddlCentroPrograma.DataSource = cpL.ListarCentroProgramaInfo();
            ddlCentroPrograma.DataBind();
            ddlCentroPrograma.Items.Insert(0, new ListItem("-- Seleccione programa/centro --", ""));
        }

        private void CargarGrid()
        {
            gvFichas.DataSource = fichaL.ListarFichas();
            gvFichas.DataBind();
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            hfIdFicha.Value = "0";
            litTitulo.Text = "Nueva Ficha";
            LimpiarCampos();
            ScriptManager.RegisterStartupScript(this, GetType(), "show", "mostrarModal();", true);
        }

        private void LimpiarCampos()
        {
            txtCodigoFicha.Text = "";
            ddlCentroPrograma.SelectedIndex = 0;
            txtFechaInicio.Text = "";
            txtFechaFin.Text = "";
            ddlJornada.SelectedIndex = 0;
            ddlNivel.SelectedIndex = 0;
            txtDuracion.Text = "";
            ddlEstado.SelectedIndex = 0;
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;
            try
            {
                ClFicha ficha = new ClFicha();
                ficha.codigoFicha = txtCodigoFicha.Text.Trim();
                ficha.idCentroPrograma = Convert.ToInt32(ddlCentroPrograma.SelectedValue);
                ficha.fechaInicio = DateTime.Parse(txtFechaInicio.Text);
                ficha.fechaFinalizacion = DateTime.Parse(txtFechaFin.Text);
                ficha.jornada = ddlJornada.SelectedValue;
                ficha.nivel = ddlNivel.SelectedValue;
                ficha.duracion = txtDuracion.Text.Trim();
                ficha.estado = ddlEstado.SelectedValue;

                bool ok;
                if (hfIdFicha.Value == "0")
                    ok = fichaL.InsertarFicha(ficha);
                else
                {
                    ficha.idFicha = Convert.ToInt32(hfIdFicha.Value);
                    ok = fichaL.ActualizarFicha(ficha);
                }

                if (ok)
                {
                    CargarGrid();
                    ScriptManager.RegisterStartupScript(this, GetType(), "hide", "ocultarModal();", true);
                    MostrarAlerta("Ficha guardada correctamente.", "success");
                }
                else
                    MostrarAlerta("Error al guardar ficha.", "danger");
            }
            catch (Exception ex)
            {
                MostrarAlerta("Error: " + ex.Message, "danger");
            }
        }

        protected void gvFichas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int idFicha = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "Editar")
            {
                var ficha = fichaL.ObtenerFichaPorId(idFicha);
                if (ficha != null)
                {
                    hfIdFicha.Value = ficha.idFicha.ToString();
                    litTitulo.Text = "Editar Ficha";
                    txtCodigoFicha.Text = ficha.codigoFicha;
                    ddlCentroPrograma.SelectedValue = ficha.idCentroPrograma.ToString();
                    txtFechaInicio.Text = ficha.fechaInicio.ToString("yyyy-MM-dd");
                    txtFechaFin.Text = ficha.fechaFinalizacion.ToString("yyyy-MM-dd");
                    ddlJornada.SelectedValue = ficha.jornada ?? ddlJornada.Items[0].Value;
                    ddlNivel.SelectedValue = ficha.nivel ?? ddlNivel.Items[0].Value;
                    txtDuracion.Text = ficha.duracion;
                    ddlEstado.SelectedValue = ficha.estado;
                    ScriptManager.RegisterStartupScript(this, GetType(), "show", "mostrarModal();", true);
                }
            }
            else if (e.CommandName == "Eliminar")
            {
                try
                {
                    if (fichaL.EliminarFicha(idFicha))
                    {
                        CargarGrid();
                        MostrarAlerta("Ficha eliminada.", "warning");
                    }
                    else
                        MostrarAlerta("No se pudo eliminar la ficha.", "danger");
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