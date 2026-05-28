using sistemaGestionPlanesDeMejoramiento.logica;
using sistemaGestionPlanesDeMejoramiento.Modelo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace sistemaGestionPlanesDeMejoramiento.vista.Instructor
{
    public partial class Evaluaciones : System.Web.UI.Page
    {
        ClPlanMejoramientoL planL = new ClPlanMejoramientoL();
        ClAprendizL aprendizL = new ClAprendizL();
        ClEvidenciaL evidenciaL = new ClEvidenciaL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null || Convert.ToInt32(Session["idRol"]) != 2)
            {
                Response.Redirect("~/vista/Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                int idPlan;
                if (int.TryParse(Request.QueryString["idPlan"], out idPlan) && idPlan > 0)
                    MostrarDetalle(idPlan);
                else
                    MostrarListado();
            }
        }

        private void MostrarListado()
        {
            pnlListado.Visible = true;
            pnlDetalle.Visible = false;
            CargarListadoPlanes();
        }

        private void MostrarDetalle(int idPlan)
        {
            pnlListado.Visible = false;
            pnlDetalle.Visible = true;
            hfIdPlan.Value = idPlan.ToString();
            CargarPlan(idPlan);
            CargarEvidencias(idPlan);
        }

        private void CargarListadoPlanes()
        {
            int idInstructor = Convert.ToInt32(Session["idInstructor"]);
            List<ClAprendiz> aprendices = aprendizL.ListarAprendices();
            var planes = planL.ListarPlanesPorInstructor(idInstructor).Select(p => new
            {
                p.idPlanMejoramiento,
                NombreAprendiz = ObtenerNombreAprendiz(aprendices, p.idAprendiz),
                p.TipoPlan,
                p.actividadesPropuestas,
                p.fechaEntrega,
                p.estadoPlan
            }).ToList();

            gvPlanesEvaluacion.DataSource = planes;
            gvPlanesEvaluacion.DataBind();
        }

        private string ObtenerNombreAprendiz(List<ClAprendiz> aprendices, int idAprendiz)
        {
            ClAprendiz aprendiz = aprendices.FirstOrDefault(a => a.idAprendiz == idAprendiz);
            return aprendiz != null ? aprendiz.nombres + " " + aprendiz.apellidos : "No encontrado";
        }

        private ClPlanMejoramiento ObtenerPlanAutorizado(int idPlan)
        {
            ClPlanMejoramiento plan = planL.ObtenerPlanPorId(idPlan);
            int idInstructor = Convert.ToInt32(Session["idInstructor"]);
            if (plan == null || plan.idInstructor != idInstructor)
                return null;
            return plan;
        }

        private void CargarPlan(int idPlan)
        {
            ClPlanMejoramiento plan = ObtenerPlanAutorizado(idPlan);
            if (plan == null)
            {
                Response.Redirect("~/vista/Instructor/Evaluaciones.aspx");
                return;
            }

            ClAprendiz aprendiz = aprendizL.ListarAprendices().Find(a => a.idAprendiz == plan.idAprendiz);
            lblAprendiz.Text = aprendiz != null ? aprendiz.nombres + " " + aprendiz.apellidos : "No encontrado";
            lblEstadoPlan.Text = plan.estadoPlan;
            lblFechaEntrega.Text = plan.fechaEntrega.HasValue ? plan.fechaEntrega.Value.ToString("dd/MM/yyyy") : "Sin fecha";
            lblActividades.Text = plan.actividadesPropuestas;
            lblObservaciones.Text = plan.observaciones;

            bool pendiente = string.Equals(plan.estadoPlan, "Pendiente", StringComparison.OrdinalIgnoreCase);
            ddlProducto.Enabled = pendiente;
            ddlConocimiento.Enabled = pendiente;
            ddlDesempeno.Enabled = pendiente;
            txtObservacionesEval.Enabled = pendiente;
            btnGuardar.Enabled = pendiente;

            if (!pendiente)
            {
                lblMensaje.CssClass = "alert alert-info d-block mt-3";
                lblMensaje.Text = "Este plan ya fue evaluado. Las evidencias siguen disponibles para descarga.";
            }
        }

        private void CargarEvidencias(int idPlan)
        {
            gvEvidencias.DataSource = evidenciaL.ListarEvidenciasPorPlan(idPlan);
            gvEvidencias.DataBind();
        }

        protected void gvPlanesEvaluacion_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvPlanesEvaluacion.PageIndex = e.NewPageIndex;
            CargarListadoPlanes();
        }

        protected void gvPlanesEvaluacion_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int idPlan = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "Evaluar" || e.CommandName == "VerEvidencias")
                Response.Redirect("Evaluaciones.aspx?idPlan=" + idPlan);
        }

        protected void gvEvidencias_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvEvidencias.PageIndex = e.NewPageIndex;
            int idPlan;
            if (int.TryParse(hfIdPlan.Value, out idPlan))
                CargarEvidencias(idPlan);
        }

        protected void gvEvidencias_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName != "Descargar")
                return;

            int idPlan;
            int idEvidencia;
            if (!int.TryParse(hfIdPlan.Value, out idPlan) || !int.TryParse(e.CommandArgument.ToString(), out idEvidencia))
            {
                lblMensajeEvidencias.Text = "No se pudo identificar la evidencia.";
                return;
            }

            ClPlanMejoramiento plan = ObtenerPlanAutorizado(idPlan);
            if (plan == null)
            {
                Response.Redirect("~/vista/Instructor/Evaluaciones.aspx");
                return;
            }

            ClEvidencia evidencia = evidenciaL.ObtenerEvidenciaPorId(idEvidencia);
            if (evidencia == null || evidencia.idPlanMejoramiento != idPlan)
            {
                lblMensajeEvidencias.Text = "La evidencia no pertenece a este plan.";
                return;
            }

            string rutaFisica = Server.MapPath(evidencia.rutaArchivo);
            if (!File.Exists(rutaFisica))
            {
                lblMensajeEvidencias.Text = "El archivo no existe en la carpeta de evidencias.";
                return;
            }

            string nombreDescarga = Path.GetFileName(evidencia.nombreArchivo);
            Response.Clear();
            Response.ContentType = "application/octet-stream";
            Response.AppendHeader("Content-Disposition", "attachment; filename=\"" + nombreDescarga + "\"");
            Response.TransmitFile(rutaFisica);
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            int idPlan;
            if (!int.TryParse(hfIdPlan.Value, out idPlan) || idPlan <= 0)
            {
                lblMensaje.CssClass = "alert alert-danger d-block mt-3";
                lblMensaje.Text = "No se pudo identificar el plan.";
                return;
            }

            ClPlanMejoramiento plan = ObtenerPlanAutorizado(idPlan);
            if (plan == null)
            {
                Response.Redirect("~/vista/Instructor/Evaluaciones.aspx");
                return;
            }

            string producto = ddlProducto.SelectedValue;
            string conocimiento = ddlConocimiento.SelectedValue;
            string desempeno = ddlDesempeno.SelectedValue;
            string observaciones = txtObservacionesEval.Text.Trim();

            try
            {
                bool ok = planL.EvaluarPlan(idPlan, producto, conocimiento, desempeno, observaciones);
                if (ok)
                {
                    CargarPlan(idPlan);
                    bool aprobado = producto == "Aprueba" && conocimiento == "Aprueba" && desempeno == "Aprueba";
                    string alerta = aprobado ? "El aprendiz aprobó el plan." : "El aprendiz no aprobó el plan.";
                    lblMensaje.CssClass = "alert alert-success d-block mt-3";
                    lblMensaje.Text = alerta + " Evaluación guardada correctamente.";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertEvaluacionOk", "alert('" + alerta + "');", true);
                }
                else
                {
                    lblMensaje.CssClass = "alert alert-danger d-block mt-3";
                    lblMensaje.Text = "Error al guardar la evaluación.";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertEvaluacionFail", "alert('No se pudo guardar la evaluación.');", true);
                }
            }
            catch (Exception ex)
            {
                lblMensaje.CssClass = "alert alert-danger d-block mt-3";
                lblMensaje.Text = ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alertEvaluacionError", "alert('No se pudo guardar la evaluación.');", true);
            }
        }
    }
}
