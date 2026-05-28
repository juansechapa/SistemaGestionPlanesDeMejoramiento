using sistemaGestionPlanesDeMejoramiento.logica;
using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace sistemaGestionPlanesDeMejoramiento.vista.Instructor
{
    public partial class MisPlanes : System.Web.UI.Page
    {
        ClPlanMejoramientoL planL = new ClPlanMejoramientoL();
        ClAprendizL aprendizL = new ClAprendizL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null || Convert.ToInt32(Session["idRol"]) != 2)
            {
                Response.Redirect("~/vista/Login.aspx");
                return;
            }

            if (!IsPostBack)
                CargarPlanes();
        }

        private void CargarPlanes()
        {
            int idInstructor = Convert.ToInt32(Session["idInstructor"]);
            var planes = planL.ListarPlanesPorInstructor(idInstructor);
            var aprendices = aprendizL.ListarAprendices();
            var lista = planes.Select(p => new
            {
                p.idPlanMejoramiento,
                NombreAprendiz = ObtenerNombreAprendiz(aprendices, p.idAprendiz),
                p.TipoPlan,
                p.actividadesPropuestas,
                p.fechaEntrega,
                p.estadoPlan
            }).ToList();
            gvPlanes.DataSource = lista;
            gvPlanes.DataBind();
        }

        private string ObtenerNombreAprendiz(System.Collections.Generic.List<Modelo.ClAprendiz> aprendices, int idAprendiz)
        {
            var aprendiz = aprendices.FirstOrDefault(a => a.idAprendiz == idAprendiz);
            return aprendiz != null ? $"{aprendiz.nombres} {aprendiz.apellidos}" : "No encontrado";
        }

        protected void gvPlanes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvPlanes.PageIndex = e.NewPageIndex;
            CargarPlanes();
        }

        protected void gvPlanes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
        }
    }
}
