using sistemaGestionPlanesDeMejoramiento.logica;
using sistemaGestionPlanesDeMejoramiento.Modelo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace sistemaGestionPlanesDeMejoramiento.vista.Instructor
{
    public partial class MisAprendices : System.Web.UI.Page
    {
        ClAprendizL aprendizL = new ClAprendizL();
        ClPlanMejoramientoL planL = new ClPlanMejoramientoL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null || Convert.ToInt32(Session["idRol"]) != 2)
            {
                Response.Redirect("~/vista/Login.aspx");
                return;
            }

            if (!IsPostBack)
                CargarAprendices();
        }

        private void CargarAprendices()
        {
            int idInstructor = Convert.ToInt32(Session["idInstructor"]);
            gvAprendices.DataSource = aprendizL.ListarAprendicesPorInstructor(idInstructor);
            gvAprendices.DataBind();
        }

        protected void gvAprendices_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "CrearPlan")
            {
                int idAprendiz = Convert.ToInt32(e.CommandArgument);
                hfIdAprendiz.Value = idAprendiz.ToString();
                txtActividades.Text = "";
                txtObservaciones.Text = "";
                txtFechaEntrega.Text = "";
                lblError.Text = "";
                CargarResultadosPendientes(idAprendiz);
                if (cblResultados.Items.Count == 0)
                {
                    lblError.Text = "Este aprendiz no tiene resultados pendientes para crear un plan.";
                }
                ClientScript.RegisterStartupScript(GetType(), "showModal", "mostrarModal();", true);
            }
        }

        private void CargarResultadosPendientes(int idAprendiz)
        {
            cblResultados.DataSource = planL.ObtenerResultadosPendientes(idAprendiz);
            cblResultados.DataTextField = "descripcion";
            cblResultados.DataValueField = "idResultado";
            cblResultados.DataBind();
        }

        protected bool EsEstadoCancelado(object estado)
        {
            string valor = estado == null ? "" : estado.ToString().Trim().ToLowerInvariant();
            return valor == "cancelado" || valor == "canselado" || valor == "cancelada" || valor == "canselada";
        }

        protected void gvAprendices_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvAprendices.PageIndex = e.NewPageIndex;
            CargarAprendices();
        }

        protected void btnGuardarPlan_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(hfIdAprendiz.Value))
            {
                lblError.Text = "No se pudo identificar el aprendiz.";
                ClientScript.RegisterStartupScript(GetType(), "showModalAprendiz", "mostrarModal();", true);
                return;
            }

            List<int> idsSeleccionados = new List<int>();
            foreach (ListItem item in cblResultados.Items)
                if (item.Selected) idsSeleccionados.Add(Convert.ToInt32(item.Value));

            if (idsSeleccionados.Count == 0)
            {
                lblError.Text = "Seleccione al menos un resultado.";
                ClientScript.RegisterStartupScript(GetType(), "showModalResultados", "mostrarModal();", true);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtActividades.Text))
            {
                lblError.Text = "Las actividades son obligatorias.";
                ClientScript.RegisterStartupScript(GetType(), "showModalActividades", "mostrarModal();", true);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtFechaEntrega.Text))
            {
                lblError.Text = "La fecha límite es obligatoria.";
                ClientScript.RegisterStartupScript(GetType(), "showModalFecha", "mostrarModal();", true);
                return;
            }

            ClPlanMejoramiento plan = new ClPlanMejoramiento();
            plan.idAprendiz = Convert.ToInt32(hfIdAprendiz.Value);
            if (aprendizL.AprendizEstaCancelado(plan.idAprendiz))
            {
                lblError.Text = "No se pueden crear planes para un aprendiz cancelado.";
                ClientScript.RegisterStartupScript(GetType(), "showModalCancelado", "mostrarModal();", true);
                return;
            }
            plan.idInstructor = Convert.ToInt32(Session["idInstructor"]);
            plan.actividadesPropuestas = txtActividades.Text.Trim();
            plan.observaciones = txtObservaciones.Text.Trim();
            plan.fechaEntrega = DateTime.Parse(txtFechaEntrega.Text);

            try
            {
                int nuevoId = planL.CrearPlanInterno(plan, idsSeleccionados);
                if (nuevoId > 0)
                {
                    // Limpiar y cerrar modal
                    txtActividades.Text = "";
                    txtObservaciones.Text = "";
                    txtFechaEntrega.Text = "";
                    lblError.Text = "";
                    ClientScript.RegisterStartupScript(GetType(), "hideModal", "ocultarModal(); alert('Plan creado correctamente.');", true);
                    // Recargar grid (opcional)
                    CargarAprendices();
                }
                else
                {
                    lblError.Text = "Error al crear el plan.";
                    ClientScript.RegisterStartupScript(GetType(), "showModalCrearFallo", "mostrarModal(); alert('No se pudo crear el plan.');", true);
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
                ClientScript.RegisterStartupScript(GetType(), "showModalError", "mostrarModal(); alert('No se pudo crear el plan.');", true);
            }
        }
    }
}
