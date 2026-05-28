using sistemaGestionPlanesDeMejoramiento.logica;
using sistemaGestionPlanesDeMejoramiento.Modelo;
using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace sistemaGestionPlanesDeMejoramiento.vista.Aprendiz
{
    public partial class MisPlanes : System.Web.UI.Page
    {
        ClPlanMejoramientoL planL = new ClPlanMejoramientoL();
        ClAprendizL aprendizL = new ClAprendizL();
        ClEvidenciaL evidenciaL = new ClEvidenciaL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null || Convert.ToInt32(Session["idRol"]) != 3)
            {
                Response.Redirect("~/vista/Login.aspx");
                return;
            }

            if (!IsPostBack)
                CargarPlanes();
        }

        private ClAprendiz ObtenerAprendizActual()
        {
            int idUsuario = Convert.ToInt32(Session["idUsuario"]);
            return aprendizL.ObtenerAprendizPorIdUsuario(idUsuario);
        }

        private void CargarPlanes()
        {
            ClAprendiz aprendiz = ObtenerAprendizActual();
            if (aprendiz == null)
            {
                Response.Redirect("~/vista/Login.aspx");
                return;
            }

            var planes = planL.ListarPlanesPorAprendiz(aprendiz.idAprendiz);
            gvPlanes.DataSource = planes.Select(p => new
            {
                p.idPlanMejoramiento,
                p.TipoPlan,
                p.actividadesPropuestas,
                p.observaciones,
                p.fechaAsignacion,
                p.fechaEntrega,
                p.estadoPlan
            }).ToList();
            gvPlanes.DataBind();
        }

        protected void gvPlanes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvPlanes.PageIndex = e.NewPageIndex;
            CargarPlanes();
        }

        protected void gvPlanes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int idPlan = Convert.ToInt32(e.CommandArgument);
            ClAprendiz aprendiz = ObtenerAprendizActual();
            if (aprendiz == null || !planL.PlanPerteneceAAprendiz(idPlan, aprendiz.idAprendiz))
            {
                Response.Redirect("~/vista/Aprendiz/MisPlanes.aspx");
                return;
            }

            if (e.CommandName == "SubirEvidencia")
            {
                hfIdPlan.Value = idPlan.ToString();
                txtObservaciones.Text = string.Empty;
                lblError.Text = string.Empty;
                ClientScript.RegisterStartupScript(GetType(), "showModal", "mostrarModal();", true);
            }
            else if (e.CommandName == "VerEvidencias")
            {
                Response.Redirect("VerEvidencias.aspx?idPlan=" + idPlan);
            }
        }

        protected void btnGuardarEvidencia_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
            {
                ClientScript.RegisterStartupScript(GetType(), "showModalInvalid", "mostrarModal();", true);
                return;
            }

            if (fuArchivo.PostedFile == null || fuArchivo.PostedFile.ContentLength == 0)
            {
                lblError.Text = "Seleccione un archivo.";
                ClientScript.RegisterStartupScript(GetType(), "showModalNoFile", "mostrarModal(); alert('No se pudo subir la evidencia. Seleccione un archivo.');", true);
                return;
            }

            int idPlan;
            if (!int.TryParse(hfIdPlan.Value, out idPlan))
            {
                lblError.Text = "No se pudo identificar el plan.";
                ClientScript.RegisterStartupScript(GetType(), "showModalNoPlan", "mostrarModal();", true);
                return;
            }

            ClAprendiz aprendiz = ObtenerAprendizActual();
            if (aprendiz == null || !planL.PlanPerteneceAAprendiz(idPlan, aprendiz.idAprendiz))
            {
                Response.Redirect("~/vista/Aprendiz/MisPlanes.aspx");
                return;
            }

            const int tamanoMaximo = 10 * 1024 * 1024;
            if (fuArchivo.PostedFile.ContentLength > tamanoMaximo)
            {
                lblError.Text = "El archivo no puede superar 10 MB.";
                ClientScript.RegisterStartupScript(GetType(), "showModalSize", "mostrarModal(); alert('No se pudo subir la evidencia. El archivo supera 10 MB.');", true);
                return;
            }

            string extension = System.IO.Path.GetExtension(fuArchivo.FileName).ToLower();
            string[] permitidas = { ".pdf", ".docx", ".jpg", ".jpeg", ".png", ".zip" };
            if (!permitidas.Contains(extension))
            {
                lblError.Text = "Formato no permitido. Use PDF, DOCX, JPG, PNG o ZIP.";
                ClientScript.RegisterStartupScript(GetType(), "showModalExtension", "mostrarModal(); alert('No se pudo subir la evidencia. Formato no permitido.');", true);
                return;
            }

            string carpeta = Server.MapPath("~/evidencias/");
            if (!System.IO.Directory.Exists(carpeta))
                System.IO.Directory.CreateDirectory(carpeta);

            string nombreUnico = Guid.NewGuid().ToString() + extension;
            string rutaFisica = System.IO.Path.Combine(carpeta, nombreUnico);
            fuArchivo.SaveAs(rutaFisica);

            ClEvidencia evidencia = new ClEvidencia
            {
                idPlanMejoramiento = idPlan,
                nombreArchivo = System.IO.Path.GetFileName(fuArchivo.FileName),
                rutaArchivo = "~/evidencias/" + nombreUnico,
                tipoArchivo = extension,
                fechaSubida = DateTime.Now,
                observaciones = txtObservaciones.Text.Trim()
            };

            try
            {
                if (evidenciaL.InsertarEvidencia(evidencia))
                {
                    txtObservaciones.Text = string.Empty;
                    hfIdPlan.Value = string.Empty;
                    CargarPlanes();
                    ClientScript.RegisterStartupScript(GetType(), "hideModal", "ocultarModal(); alert('Evidencia subida correctamente.');", true);
                }
                else
                {
                    lblError.Text = "Error al guardar evidencia.";
                    ClientScript.RegisterStartupScript(GetType(), "showModalSave", "mostrarModal(); alert('No se pudo subir la evidencia.');", true);
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
                ClientScript.RegisterStartupScript(GetType(), "showModalException", "mostrarModal(); alert('No se pudo subir la evidencia.');", true);
            }
        }
    }
}
