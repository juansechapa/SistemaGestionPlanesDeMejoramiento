using sistemaGestionPlanesDeMejoramiento.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace sistemaGestionPlanesDeMejoramiento.vista
{
    public partial class admin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
            {
                Response.Redirect("~/Login.aspx");
                return;
            }
            int idRol = Convert.ToInt32(Session["idRol"]);
            if (idRol != 1)
            {
                if (idRol == 2)
                    Response.Redirect("~/Instructor/InicioInstructor.aspx");
                else if (idRol == 3)
                    Response.Redirect("~/Aprendiz/InicioAprendiz.aspx");
                else
                    Response.Redirect("~/Login.aspx");
                return;
            }
            lblUsuario.Text = Session["username"].ToString();
        }
    }
}