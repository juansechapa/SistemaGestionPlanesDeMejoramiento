using sistemaGestionPlanesDeMejoramiento.logica;
using sistemaGestionPlanesDeMejoramiento.Modelo;
using System;
using System.Web.UI;

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
            var aprendiz = ObtenerAprendizActual();
            if (aprendiz == null)
                return;

            lblBienvenida.Text = aprendiz.nombres;
            lblNombres.Text = aprendiz.nombres;
            lblApellidos.Text = aprendiz.apellidos;
            lblTipoDoc.Text = aprendiz.tipoDocumento;
            lblNumDoc.Text = aprendiz.numeroDocumento;
            lblCorreo.Text = aprendiz.correo;
            lblTelefono.Text = aprendiz.telefono;
            lblFechaNac.Text = aprendiz.fechaNacimiento.ToString("dd/MM/yyyy");
            lblEstado.Text = aprendiz.estado;

            if (aprendiz.estado == "Cancelado")
                lblEstado.CssClass = "badge bg-danger";
            else if (aprendiz.estado == "En Mejoramiento")
                lblEstado.CssClass = "badge bg-warning";
            else
                lblEstado.CssClass = "badge bg-success";

            ClFichaL fichaL = new ClFichaL();
            var ficha = fichaL.ObtenerFichaPorId(aprendiz.idFicha);
            if (ficha != null)
            {
                lblFicha.Text = ficha.codigoFicha;
                ClCentroProgramaL cpL = new ClCentroProgramaL();
                var info = cpL.ObtenerInfoCentroPrograma(ficha.idCentroPrograma);
                if (info != null)
                {
                    lblPrograma.Text = info.Programa;
                    lblCentro.Text = info.Centro;
                }
            }
        }

        protected void btnEditarDatos_Click(object sender, EventArgs e)
        {
            var aprendiz = ObtenerAprendizActual();
            if (aprendiz == null)
            {
                MostrarMensaje("No se pudo cargar la informacion del aprendiz.", "danger");
                return;
            }

            txtNombres.Text = aprendiz.nombres;
            txtApellidos.Text = aprendiz.apellidos;
            txtCorreo.Text = aprendiz.correo;
            txtTelefono.Text = aprendiz.telefono;
            ScriptManager.RegisterStartupScript(this, GetType(), "showDatosAprendiz", "mostrarModalDatosAprendiz();", true);
        }

        protected void btnGuardarDatos_Click(object sender, EventArgs e)
        {
            try
            {
                var aprendiz = ObtenerAprendizActual();
                if (aprendiz == null)
                {
                    MostrarMensaje("No se pudo cargar la informacion del aprendiz.", "danger");
                    return;
                }

                aprendiz.nombres = txtNombres.Text.Trim();
                aprendiz.apellidos = txtApellidos.Text.Trim();
                aprendiz.correo = txtCorreo.Text.Trim();
                aprendiz.telefono = txtTelefono.Text.Trim();

                if (aprendizL.ActualizarAprendiz(aprendiz))
                {
                    CargarDatosAprendiz();
                    MostrarMensaje("Informacion actualizada correctamente.", "success");
                    ScriptManager.RegisterStartupScript(this, GetType(), "hideDatosAprendiz", "ocultarModalDatosAprendiz();", true);
                }
                else
                {
                    MostrarMensaje("No se pudo actualizar la informacion.", "danger");
                    ScriptManager.RegisterStartupScript(this, GetType(), "showDatosAprendizFail", "mostrarModalDatosAprendiz();", true);
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error: " + ex.Message, "danger");
                ScriptManager.RegisterStartupScript(this, GetType(), "showDatosAprendizError", "mostrarModalDatosAprendiz();", true);
            }
        }

        private ClAprendiz ObtenerAprendizActual()
        {
            int idUsuario = Convert.ToInt32(Session["idUsuario"]);
            return aprendizL.ObtenerAprendizPorIdUsuario(idUsuario);
        }

        private void MostrarMensaje(string mensaje, string tipo)
        {
            lblMensaje.Text = mensaje;
            lblMensaje.CssClass = "alert alert-" + tipo + " d-block mb-3";
        }
    }
}
