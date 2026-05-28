using sistemaGestionPlanesDeMejoramiento.logica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace sistemaGestionPlanesDeMejoramiento.vista.Aprendiz
{
    public partial class InicioAprendiz : System.Web.UI.Page
    {
        ClAprendizL aprendizL = new ClAprendizL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null || Convert.ToInt32(Session["idRol"]) != 3)
            {
                Response.Redirect("~/vista/Login.aspx");
                return;
            }

            if (!IsPostBack)
                CargarDatosAprendiz();
        }

        private void CargarDatosAprendiz()
        {
            int idUsuario = Convert.ToInt32(Session["idUsuario"]);
            var aprendiz = aprendizL.ObtenerAprendizPorIdUsuario(idUsuario);
            if (aprendiz == null)
            {
                // Si no se encuentra, redirigir o mostrar error
                return;
            }

            lblBienvenida.Text = aprendiz.nombres;
            lblNombres.Text = aprendiz.nombres;
            lblApellidos.Text = aprendiz.apellidos;
            lblTipoDoc.Text = aprendiz.tipoDocumento;
            lblNumDoc.Text = aprendiz.numeroDocumento;
            lblCorreo.Text = aprendiz.correo;
            lblTelefono.Text = aprendiz.telefono;
            lblFechaNac.Text = aprendiz.fechaNacimiento.ToString("dd/MM/yyyy");
            lblEstado.Text = aprendiz.estado;
            // Cambiar color según estado (opcional)
            if (aprendiz.estado == "Cancelado")
                lblEstado.CssClass = "badge bg-danger";
            else if (aprendiz.estado == "En Mejoramiento")
                lblEstado.CssClass = "badge bg-warning";
            else
                lblEstado.CssClass = "badge bg-success";

            // Obtener ficha, programa, centro
            // Necesitamos métodos adicionales en ClFichaL, etc.
            ClFichaL fichaL = new ClFichaL();
            var ficha = fichaL.ObtenerFichaPorId(aprendiz.idFicha);
            if (ficha != null)
            {
                lblFicha.Text = ficha.codigoFicha;
                // Obtener programa y centro desde ficha.idCentroPrograma
                ClCentroProgramaL cpL = new ClCentroProgramaL();
                var info = cpL.ObtenerInfoCentroPrograma(ficha.idCentroPrograma);
                if (info != null)
                {
                    lblPrograma.Text = info.Programa;
                    lblCentro.Text = info.Centro;
                }
            }
        }
    }
}
