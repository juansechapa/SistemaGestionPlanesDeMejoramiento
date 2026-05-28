using sistemaGestionPlanesDeMejoramiento.logica;
using sistemaGestionPlanesDeMejoramiento.Modelo;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace sistemaGestionPlanesDeMejoramiento.vista.Admin
{
    public partial class GestionProgramas : System.Web.UI.Page
    {
        ClProgramaL programaL = new ClProgramaL();
        ClCompetenciaL competenciaL = new ClCompetenciaL();
        ClResultadoAprendizajeL resultadoL = new ClResultadoAprendizajeL();

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

        protected void gvProgramas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvProgramas.PageIndex = e.NewPageIndex;
            CargarGrid();
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
            else if (e.CommandName == "Competencias")
            {
                int idProgramaCompetencias = Convert.ToInt32(e.CommandArgument);
                var programa = programaL.ListarProgramas().Find(p => p.idPrograma == idProgramaCompetencias);
                if (programa == null)
                {
                    MostrarAlerta("Programa no encontrado.", "danger");
                    return;
                }

                hfIdProgramaCompetencias.Value = idProgramaCompetencias.ToString();
                hfIdCompetenciaSeleccionada.Value = "";
                litProgramaCompetencias.Text = programa.nombre;
                lblCompetenciaSeleccionada.Text = "Seleccione una competencia para ver o agregar resultados.";
                lblCompetenciasMensaje.Text = "";
                txtNombreCompetencia.Text = "";
                txtDescripcionCompetencia.Text = "";
                txtCodigoResultado.Text = "";
                txtDescripcionResultado.Text = "";
                CargarCompetencias(idProgramaCompetencias);
                CargarResultados(0);
                ClientScript.RegisterStartupScript(this.GetType(), "showCompetencias", "mostrarModalCompetencias();", true);
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

        private void CargarCompetencias(int idPrograma)
        {
            gvCompetencias.DataSource = competenciaL.ListarPorPrograma(idPrograma);
            gvCompetencias.DataBind();
        }

        private void CargarResultados(int idCompetencias)
        {
            if (idCompetencias <= 0)
                gvResultados.DataSource = new List<ClResultado>();
            else
                gvResultados.DataSource = resultadoL.ListarPorCompetencia(idCompetencias);

            gvResultados.DataBind();
        }

        protected void btnAgregarCompetencia_Click(object sender, EventArgs e)
        {
            try
            {
                int idPrograma = Convert.ToInt32(hfIdProgramaCompetencias.Value);
                ClCompetencias competencia = new ClCompetencias
                {
                    idPrograma = idPrograma,
                    nombre = txtNombreCompetencia.Text.Trim(),
                    descripcion = txtDescripcionCompetencia.Text.Trim()
                };

                if (competenciaL.Insertar(competencia))
                {
                    txtNombreCompetencia.Text = "";
                    txtDescripcionCompetencia.Text = "";
                    CargarCompetencias(idPrograma);
                    MostrarMensajeCompetencias("Competencia agregada correctamente.", "success");
                }
                else
                    MostrarMensajeCompetencias("No se pudo agregar la competencia.", "danger");
            }
            catch (Exception ex)
            {
                MostrarMensajeCompetencias("Error: " + ex.Message, "danger");
            }
            ClientScript.RegisterStartupScript(this.GetType(), "showCompetenciasAdd", "mostrarModalCompetencias();", true);
        }

        protected void gvCompetencias_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int idCompetencia = Convert.ToInt32(e.CommandArgument);
            int idPrograma = Convert.ToInt32(hfIdProgramaCompetencias.Value);

            if (e.CommandName == "Resultados")
            {
                hfIdCompetenciaSeleccionada.Value = idCompetencia.ToString();
                var competencia = competenciaL.ListarPorPrograma(idPrograma).Find(c => c.idCompetencias == idCompetencia);
                lblCompetenciaSeleccionada.Text = competencia != null ? "Competencia: " + competencia.nombre : "Competencia seleccionada";
                CargarResultados(idCompetencia);
            }
            else if (e.CommandName == "EliminarCompetencia")
            {
                try
                {
                    if (competenciaL.Eliminar(idCompetencia))
                    {
                        hfIdCompetenciaSeleccionada.Value = "";
                        CargarCompetencias(idPrograma);
                        CargarResultados(0);
                        MostrarMensajeCompetencias("Competencia eliminada correctamente.", "warning");
                    }
                    else
                        MostrarMensajeCompetencias("No se pudo eliminar la competencia.", "danger");
                }
                catch (Exception ex)
                {
                    MostrarMensajeCompetencias("Error: " + ex.Message, "danger");
                }
            }
            ClientScript.RegisterStartupScript(this.GetType(), "showCompetenciasCommand", "mostrarModalCompetencias();", true);
        }

        protected void btnAgregarResultado_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(hfIdCompetenciaSeleccionada.Value))
                {
                    MostrarMensajeCompetencias("Seleccione primero una competencia.", "danger");
                    ClientScript.RegisterStartupScript(this.GetType(), "showCompetenciasNoComp", "mostrarModalCompetencias();", true);
                    return;
                }

                int idCompetencia = Convert.ToInt32(hfIdCompetenciaSeleccionada.Value);
                ClResultado resultado = new ClResultado
                {
                    idCompetencias = idCompetencia,
                    codigo = txtCodigoResultado.Text.Trim(),
                    descripcion = txtDescripcionResultado.Text.Trim(),
                    estado = "Activo"
                };

                if (resultadoL.Insertar(resultado))
                {
                    txtCodigoResultado.Text = "";
                    txtDescripcionResultado.Text = "";
                    CargarResultados(idCompetencia);
                    MostrarMensajeCompetencias("Resultado agregado correctamente.", "success");
                }
                else
                    MostrarMensajeCompetencias("No se pudo agregar el resultado.", "danger");
            }
            catch (Exception ex)
            {
                MostrarMensajeCompetencias("Error: " + ex.Message, "danger");
            }
            ClientScript.RegisterStartupScript(this.GetType(), "showCompetenciasRes", "mostrarModalCompetencias();", true);
        }

        protected void gvResultados_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EliminarResultado")
            {
                try
                {
                    int idResultado = Convert.ToInt32(e.CommandArgument);
                    int idCompetencia = Convert.ToInt32(hfIdCompetenciaSeleccionada.Value);
                    if (resultadoL.Eliminar(idResultado))
                    {
                        CargarResultados(idCompetencia);
                        MostrarMensajeCompetencias("Resultado eliminado correctamente.", "warning");
                    }
                    else
                        MostrarMensajeCompetencias("No se pudo eliminar el resultado.", "danger");
                }
                catch (Exception ex)
                {
                    MostrarMensajeCompetencias("Error: " + ex.Message, "danger");
                }
                ClientScript.RegisterStartupScript(this.GetType(), "showCompetenciasDelRes", "mostrarModalCompetencias();", true);
            }
        }

        private void MostrarMensajeCompetencias(string mensaje, string tipo)
        {
            lblCompetenciasMensaje.Text = mensaje;
            lblCompetenciasMensaje.CssClass = "alert alert-" + tipo + " d-block mb-3";
        }

        private void MostrarAlerta(string mensaje, string tipo)
        {
            string script = $"alert('{mensaje}');";
            ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
        }
    }
}
