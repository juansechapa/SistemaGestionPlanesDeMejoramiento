using sistemaGestionPlanesDeMejoramiento.logica;
using sistemaGestionPlanesDeMejoramiento.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace sistemaGestionPlanesDeMejoramiento.vista.Admin
{
    public partial class GestionPlanes : System.Web.UI.Page
    {
        ClPlanMejoramientoL planL = new ClPlanMejoramientoL();
        ClAprendizL aprendizL = new ClAprendizL();
        ClInstructorL instructorL = new ClInstructorL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null || Convert.ToInt32(Session["idRol"]) != 1)
            {
                Response.Redirect("~/vista/Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                CargarCombos();
                CargarGrid();
            }
        }

        private void CargarCombos()
        {
            var aprendices = aprendizL.ListarAprendices()
                .Select(a => new
                {
                    a.idAprendiz,
                    NombreCompleto = a.nombres + " " + a.apellidos + " - " + a.numeroDocumento
                })
                .ToList();

            ddlAprendiz.DataSource = aprendices;
            ddlAprendiz.DataTextField = "NombreCompleto";
            ddlAprendiz.DataValueField = "idAprendiz";
            ddlAprendiz.DataBind();
            ddlAprendiz.Items.Insert(0, new ListItem("-- Seleccione aprendiz --", ""));

            var instructores = instructorL.ListarInstructores()
                .Select(i => new
                {
                    i.idInstructor,
                    NombreCompleto = i.nombres + " " + i.apellidos + " - " + i.numeroDocumento
                })
                .ToList();

            ddlInstructor.DataSource = instructores;
            ddlInstructor.DataTextField = "NombreCompleto";
            ddlInstructor.DataValueField = "idInstructor";
            ddlInstructor.DataBind();
            ddlInstructor.Items.Insert(0, new ListItem("-- Seleccione instructor --", ""));
        }

        private void CargarGrid()
        {
            List<ClAprendiz> aprendices = aprendizL.ListarAprendices();
            List<ClInstructor> instructores = instructorL.ListarInstructores();
            var planes = planL.ListarTodosLosPlanes().Select(p => new
            {
                p.idPlanMejoramiento,
                NombreAprendiz = ObtenerNombreAprendiz(aprendices, p.idAprendiz),
                NombreInstructor = ObtenerNombreInstructor(instructores, p.idInstructor),
                p.TipoPlan,
                p.actividadesPropuestas,
                p.observaciones,
                p.fechaAsignacion,
                p.fechaEntrega,
                p.estadoPlan
            }).ToList();

            gvPlanes.DataSource = planes;
            gvPlanes.DataBind();
        }

        private string ObtenerNombreAprendiz(List<ClAprendiz> aprendices, int idAprendiz)
        {
            ClAprendiz aprendiz = aprendices.FirstOrDefault(a => a.idAprendiz == idAprendiz);
            return aprendiz != null ? aprendiz.nombres + " " + aprendiz.apellidos : "No encontrado";
        }

        private string ObtenerNombreInstructor(List<ClInstructor> instructores, int idInstructor)
        {
            ClInstructor instructor = instructores.FirstOrDefault(i => i.idInstructor == idInstructor);
            return instructor != null ? instructor.nombres + " " + instructor.apellidos : "No encontrado";
        }

        protected string ObtenerClaseEstado(object estado)
        {
            string valor = Convert.ToString(estado);
            if (string.Equals(valor, "Aprobado", StringComparison.OrdinalIgnoreCase))
                return "badge bg-success";
            if (string.Equals(valor, "No Aprobado", StringComparison.OrdinalIgnoreCase))
                return "badge bg-danger";
            return "badge bg-secondary";
        }

        protected void gvPlanes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvPlanes.PageIndex = e.NewPageIndex;
            CargarGrid();
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            hfIdPlan.Value = "0";
            litModalTitulo.Text = "Nuevo Plan";
            LimpiarCampos();
            ClientScript.RegisterStartupScript(GetType(), "showPlan", "mostrarModalPlan();", true);
        }

        private void LimpiarCampos()
        {
            ddlAprendiz.SelectedIndex = 0;
            ddlInstructor.SelectedIndex = 0;
            ddlTipoPlan.SelectedValue = "Interno";
            txtFechaAsignacion.Text = DateTime.Now.ToString("yyyy-MM-dd");
            txtFechaEntrega.Text = "";
            txtActividades.Text = "";
            txtObservaciones.Text = "";
            ddlEstadoPlan.SelectedValue = "Pendiente";
            lblMensaje.Text = "";
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
            {
                ClientScript.RegisterStartupScript(GetType(), "showPlanInvalid", "mostrarModalPlan();", true);
                return;
            }

            try
            {
                ClPlanMejoramiento plan = new ClPlanMejoramiento
                {
                    idAprendiz = Convert.ToInt32(ddlAprendiz.SelectedValue),
                    idInstructor = Convert.ToInt32(ddlInstructor.SelectedValue),
                    TipoPlan = ddlTipoPlan.SelectedValue,
                    actividadesPropuestas = txtActividades.Text.Trim(),
                    observaciones = string.IsNullOrWhiteSpace(txtObservaciones.Text) ? null : txtObservaciones.Text.Trim(),
                    fechaAsignacion = DateTime.Parse(txtFechaAsignacion.Text),
                    fechaEntrega = DateTime.Parse(txtFechaEntrega.Text),
                    estadoPlan = ddlEstadoPlan.SelectedValue
                };

                bool ok;
                if (hfIdPlan.Value == "0")
                    ok = planL.InsertarPlan(plan);
                else
                {
                    plan.idPlanMejoramiento = Convert.ToInt32(hfIdPlan.Value);
                    ok = planL.ActualizarPlan(plan);
                }

                if (ok)
                {
                    CargarGrid();
                    MostrarMensaje("Plan guardado correctamente.", "success");
                    ClientScript.RegisterStartupScript(GetType(), "hidePlan", "ocultarModalPlan();", true);
                }
                else
                    MostrarMensaje("No se pudo guardar el plan.", "danger");
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error: " + ex.Message, "danger");
                ClientScript.RegisterStartupScript(GetType(), "showPlanError", "mostrarModalPlan();", true);
            }
        }

        protected void gvPlanes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int idPlan = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "Editar")
            {
                ClPlanMejoramiento plan = planL.ObtenerPlanPorId(idPlan);
                if (plan == null)
                {
                    MostrarMensaje("Plan no encontrado.", "danger");
                    return;
                }

                hfIdPlan.Value = plan.idPlanMejoramiento.ToString();
                litModalTitulo.Text = "Editar Plan";
                SeleccionarValor(ddlAprendiz, plan.idAprendiz.ToString());
                SeleccionarValor(ddlInstructor, plan.idInstructor.ToString());
                SeleccionarValor(ddlTipoPlan, plan.TipoPlan);
                txtFechaAsignacion.Text = plan.fechaAsignacion.HasValue ? plan.fechaAsignacion.Value.ToString("yyyy-MM-dd") : DateTime.Now.ToString("yyyy-MM-dd");
                txtFechaEntrega.Text = plan.fechaEntrega.HasValue ? plan.fechaEntrega.Value.ToString("yyyy-MM-dd") : "";
                txtActividades.Text = plan.actividadesPropuestas;
                txtObservaciones.Text = plan.observaciones;
                SeleccionarValor(ddlEstadoPlan, plan.estadoPlan);
                ClientScript.RegisterStartupScript(GetType(), "showPlanEdit", "mostrarModalPlan();", true);
            }
            else if (e.CommandName == "Eliminar")
            {
                try
                {
                    if (planL.EliminarPlan(idPlan))
                    {
                        CargarGrid();
                        MostrarMensaje("Plan eliminado correctamente.", "warning");
                    }
                    else
                        MostrarMensaje("No se pudo eliminar el plan.", "danger");
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
