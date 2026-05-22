using sistemaGestionPlanesDeMejoramiento.datos;
using sistemaGestionPlanesDeMejoramiento.logica;
using sistemaGestionPlanesDeMejoramiento.Modelo;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace sistemaGestionPlanesDeMejoramiento.vista.Admin
{
    public partial class GestionCentroPrograma : System.Web.UI.Page
    {
        ClCentroFormacionL centroL = new ClCentroFormacionL();
        ClCentroProgramaL cpL = new ClCentroProgramaL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null || Convert.ToInt32(Session["idRol"]) != 1)
                Response.Redirect("~/vista/Login.aspx");

            if (!IsPostBack)
            {
                CargarCentros();
            }
        }

        private void CargarCentros()
        {
            List<ClCentroFormacion> centros = centroL.ListarCentros();
            ddlCentro.DataSource = centros;
            ddlCentro.DataTextField = "nombre";
            ddlCentro.DataValueField = "idCentro";
            ddlCentro.DataBind();
            if (ddlCentro.Items.Count > 0)
                ddlCentro.Items.Insert(0, new ListItem("-- Seleccione un centro --", "0"));
            else
                ddlCentro.Items.Insert(0, new ListItem("No hay centros registrados", "0"));
        }

        protected void ddlCentro_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idCentro;
            if (int.TryParse(ddlCentro.SelectedValue, out idCentro) && idCentro > 0)
            {
                CargarAsignacion(idCentro);
            }
            else
            {
                gvAsignados.DataSource = null;
                gvAsignados.DataBind();
                gvDisponibles.DataSource = null;
                gvDisponibles.DataBind();
            }
        }

        private void CargarAsignacion(int idCentro)
        {
            gvAsignados.DataSource = cpL.ListarProgramasPorCentro(idCentro);
            gvAsignados.DataBind();
            gvDisponibles.DataSource = cpL.ListarProgramasNoAsignados(idCentro);
            gvDisponibles.DataBind();
        }


        protected void gvAsignados_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Quitar")
            {
                int idPrograma = Convert.ToInt32(e.CommandArgument);
                int idCentro = Convert.ToInt32(ddlCentro.SelectedValue);
                try
                {
                    if (cpL.DesasignarPrograma(idCentro, idPrograma))
                    {
                        CargarAsignacion(idCentro);
                        MostrarAlerta("Programa removido del centro.", "success");
                    }
                }
                catch (Exception ex)
                {
                    MostrarAlerta(ex.Message, "danger");
                }
            }
        }

        protected void gvDisponibles_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Asignar")
            {
                int idPrograma = Convert.ToInt32(e.CommandArgument);
                int idCentro = Convert.ToInt32(ddlCentro.SelectedValue);
                try
                {
                    if (cpL.AsignarPrograma(idCentro, idPrograma))
                    {
                        CargarAsignacion(idCentro);
                        MostrarAlerta("Programa asignado correctamente.", "success");
                    }
                }
                catch (Exception ex)
                {
                    MostrarAlerta(ex.Message, "danger");
                }
            }
        }

        private void MostrarAlerta(string mensaje, string tipo)
        {
            string script = $"alert('{mensaje}');";
            ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
        }
    }
}