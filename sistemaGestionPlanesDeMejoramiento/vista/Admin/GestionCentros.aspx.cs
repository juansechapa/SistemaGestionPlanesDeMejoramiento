using sistemaGestionPlanesDeMejoramiento.logica;   // <-- Importante
using sistemaGestionPlanesDeMejoramiento.Modelo;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace sistemaGestionPlanesDeMejoramiento.vista.Admin
{
    public partial class GestionCentros : System.Web.UI.Page
    {
        private ClCentroFormacionL centroL = new ClCentroFormacionL(); 

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null || Convert.ToInt32(Session["idRol"]) != 1)
            {
                Response.Redirect("~/vista/Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                CargarGrid();
            }
        }

        private void CargarGrid()
        {
            gvCentros.DataSource = centroL.ListarCentros();   
            gvCentros.DataBind();
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            hfIdCentro.Value = "0";
            litModalTitulo.Text = "Nuevo Centro";
            txtNombre.Text = "";
            txtDireccion.Text = "";
            txtTelefono.Text = "";
            chkEstado.Checked = true;
            ClientScript.RegisterStartupScript(this.GetType(), "ShowModal", "mostrarModal();", true);
           
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;

            try
            {
                ClCentroFormacion centro = new ClCentroFormacion();
                centro.nombre = txtNombre.Text.Trim();
                centro.direccion = txtDireccion.Text.Trim();
                centro.telefono = txtTelefono.Text.Trim();
                centro.estado = chkEstado.Checked;

                bool ok;
                if (hfIdCentro.Value == "0")
                {
                    ok = centroL.InsertarCentro(centro);
                }
                else
                {
                    centro.idCentro = Convert.ToInt32(hfIdCentro.Value);
                    ok = centroL.ActualizarCentro(centro);
                }

                if (ok)
                {
                    CargarGrid();
                    ClientScript.RegisterStartupScript(this.GetType(), "HideModal", "ocultarModal();", true);
                    MostrarAlerta("Centro guardado correctamente.", "success");
                }
                else
                {
                    MostrarAlerta("Error al guardar centro.", "danger");
                }
            }
            catch (Exception ex)
            {
                MostrarAlerta("Error: " + ex.Message, "danger");
            }
        }

        protected void gvCentros_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int idCentro = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "Editar")
            {
                var centro = centroL.ObtenerCentroPorId(idCentro); 
                if (centro != null)
                {
                    hfIdCentro.Value = centro.idCentro.ToString();
                    litModalTitulo.Text = "Editar Centro";
                    txtNombre.Text = centro.nombre;
                    txtDireccion.Text = centro.direccion;
                    txtTelefono.Text = centro.telefono;
                    chkEstado.Checked = centro.estado;
                    ClientScript.RegisterStartupScript(this.GetType(), "ShowModal", "mostrarModal();", true);
                }
            }
            else if (e.CommandName == "Eliminar")
            {
                try
                {
                    if (centroL.EliminarCentro(idCentro))
                    {
                        CargarGrid();
                        MostrarAlerta("Centro eliminado.", "warning");
                    }
                    else
                    {
                        MostrarAlerta("No se pudo eliminar.", "danger");
                    }
                }
                catch (Exception ex)
                {
                    MostrarAlerta(ex.Message, "danger");
                }
            }
        }

        protected void gvCentros_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //Ajustar para que mas adelante dependiendo el estado cambie de color
        }

        private void MostrarAlerta(string mensaje, string tipo)
        {
            string script = $"alert('{mensaje}');";
            ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
        }
    }
}
