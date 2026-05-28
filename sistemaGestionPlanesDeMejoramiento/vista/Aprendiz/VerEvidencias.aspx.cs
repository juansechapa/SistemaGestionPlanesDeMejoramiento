using sistemaGestionPlanesDeMejoramiento.logica;
using sistemaGestionPlanesDeMejoramiento.Modelo;
using System;
using System.IO;
using System.Web;
using System.Web.UI.WebControls;

namespace sistemaGestionPlanesDeMejoramiento.vista.Aprendiz
{
    public partial class VerEvidencias : System.Web.UI.Page
    {
        ClEvidenciaL evidenciaL = new ClEvidenciaL();
        ClAprendizL aprendizL = new ClAprendizL();
        ClPlanMejoramientoL planL = new ClPlanMejoramientoL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null || Convert.ToInt32(Session["idRol"]) != 3)
            {
                Response.Redirect("~/vista/Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                int idPlan;
                if (!int.TryParse(Request.QueryString["idPlan"], out idPlan))
                {
                    Response.Redirect("~/vista/Aprendiz/MisPlanes.aspx");
                    return;
                }

                ClAprendiz aprendiz = ObtenerAprendizActual();
                if (aprendiz == null || !planL.PlanPerteneceAAprendiz(idPlan, aprendiz.idAprendiz))
                {
                    Response.Redirect("~/vista/Aprendiz/MisPlanes.aspx");
                    return;
                }

                CargarEvidencias(idPlan);
            }
        }

        private ClAprendiz ObtenerAprendizActual()
        {
            int idUsuario = Convert.ToInt32(Session["idUsuario"]);
            return aprendizL.ObtenerAprendizPorIdUsuario(idUsuario);
        }

        private void CargarEvidencias(int idPlan)
        {
            gvEvidencias.DataSource = evidenciaL.ListarEvidenciasPorPlan(idPlan);
            gvEvidencias.DataBind();
        }

        protected void gvEvidencias_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvEvidencias.PageIndex = e.NewPageIndex;
            int idPlan = Convert.ToInt32(Request.QueryString["idPlan"]);
            CargarEvidencias(idPlan);
        }

        protected void gvEvidencias_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName != "Descargar")
                return;

            int idPlan;
            int idEvidencia;
            if (!int.TryParse(Request.QueryString["idPlan"], out idPlan) || !int.TryParse(e.CommandArgument.ToString(), out idEvidencia))
            {
                lblMensaje.Text = "No se pudo identificar la evidencia.";
                return;
            }

            ClAprendiz aprendiz = ObtenerAprendizActual();
            if (aprendiz == null || !planL.PlanPerteneceAAprendiz(idPlan, aprendiz.idAprendiz))
            {
                Response.Redirect("~/vista/Aprendiz/MisPlanes.aspx");
                return;
            }

            ClEvidencia evidencia = evidenciaL.ObtenerEvidenciaPorId(idEvidencia);
            if (evidencia == null || evidencia.idPlanMejoramiento != idPlan)
            {
                lblMensaje.Text = "La evidencia solicitada no pertenece a este plan.";
                return;
            }

            string rutaFisica = Server.MapPath(evidencia.rutaArchivo);
            if (!File.Exists(rutaFisica))
            {
                lblMensaje.Text = "El archivo ya no existe en la carpeta de evidencias.";
                return;
            }

            string nombreDescarga = Path.GetFileName(evidencia.nombreArchivo);
            Response.Clear();
            Response.ContentType = "application/octet-stream";
            Response.AppendHeader("Content-Disposition", "attachment; filename=\"" + nombreDescarga + "\"");
            Response.TransmitFile(rutaFisica);
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }
    }
}
