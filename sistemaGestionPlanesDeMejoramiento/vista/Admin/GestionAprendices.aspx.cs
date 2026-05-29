using ExcelDataReader;
using sistemaGestionPlanesDeMejoramiento.logica;
using sistemaGestionPlanesDeMejoramiento.Modelo;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace sistemaGestionPlanesDeMejoramiento.vista.Admin
{
    public partial class GestionAprendices : System.Web.UI.Page
    {
        ClAprendizL aprendizL = new ClAprendizL();
        ClFichaL fichaL = new ClFichaL();

        [Serializable]
        private class AprendizTemp
        {
            public int Fila { get; set; }
            public string Nombres { get; set; }
            public string Apellidos { get; set; }
            public string TipoDocumento { get; set; }
            public string NumeroDocumento { get; set; }
            public string Correo { get; set; }
            public string Telefono { get; set; }
            public DateTime FechaNacimiento { get; set; }
        }
        private List<AprendizTemp> aprendicesValidos = new List<AprendizTemp>();
        private List<ErrorValidacion> erroresCarga = new List<ErrorValidacion>();

        private List<AprendizTemp> AprendicesCargaValidos
        {
            get
            {
                return Session["AprendicesCargaValidos"] as List<AprendizTemp> ?? new List<AprendizTemp>();
            }
            set
            {
                Session["AprendicesCargaValidos"] = value;
            }
        }

        public class ErrorValidacion
        {
            public int Fila { get; set; }
            public string Campo { get; set; }
            public string Error { get; set; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null || Convert.ToInt32(Session["idRol"]) != 1)
                Response.Redirect("~/vista/Login.aspx");

            if (!IsPostBack)
            {
                CargarGrid();
                CargarFichas();
                CargarFichasCarga();
            }
        }

        private void CargarGrid()
        {
            gvAprendices.DataSource = aprendizL.ListarAprendices();
            gvAprendices.DataBind();
        }

        private void CargarFichas()
        {
            var fichas = fichaL.ListarFichas();
            ddlFicha.Items.Clear();
            ddlFicha.DataSource = fichas;
            ddlFicha.DataTextField = "codigoFicha";
            ddlFicha.DataValueField = "idFicha";
            ddlFicha.DataBind();
            ddlFicha.Items.Insert(0, new ListItem("-- Seleccione ficha --", ""));
            MarcarFichasLlenas(ddlFicha);
        }

        private void CargarFichasCarga()
        {
            var fichas = fichaL.ListarFichas();
            ddlFichaCarga.Items.Clear();
            ddlFichaCarga.DataSource = fichas;
            ddlFichaCarga.DataTextField = "codigoFicha";
            ddlFichaCarga.DataValueField = "idFicha";
            ddlFichaCarga.DataBind();
            ddlFichaCarga.Items.Insert(0, new ListItem("-- Seleccione ficha --", ""));
            MarcarFichasLlenas(ddlFichaCarga);
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            hfIdAprendiz.Value = "0";
            litTitulo.Text = "Nuevo Aprendiz";
            divCredenciales.Visible = true;
            LimpiarCampos();
            ClientScript.RegisterStartupScript(this.GetType(), "show", "mostrarModal();", true);
        }

        private void LimpiarCampos()
        {
            txtNombres.Text = "";
            txtApellidos.Text = "";
            ddlTipoDoc.SelectedIndex = 0;
            txtNumDoc.Text = "";
            txtCorreo.Text = "";
            txtTelefono.Text = "";
            txtFechaNac.Text = "";
            ddlFicha.SelectedIndex = 0;
            txtUsername.Text = "";
            txtPassword.Text = "";
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;
            try
            {
                ClAprendiz aprendiz = new ClAprendiz();
                aprendiz.nombres = txtNombres.Text.Trim();
                aprendiz.apellidos = txtApellidos.Text.Trim();
                aprendiz.tipoDocumento = ddlTipoDoc.SelectedValue;
                aprendiz.numeroDocumento = txtNumDoc.Text.Trim();
                aprendiz.correo = txtCorreo.Text.Trim();
                aprendiz.telefono = txtTelefono.Text.Trim();
                aprendiz.fechaNacimiento = DateTime.Parse(txtFechaNac.Text);
                aprendiz.idFicha = Convert.ToInt32(ddlFicha.SelectedValue);

                bool ok;
                if (hfIdAprendiz.Value == "0")
                {
                    string username = txtUsername.Text.Trim();
                    string password = txtPassword.Text.Trim();
                    ok = aprendizL.InsertarAprendizConUsuario(aprendiz, username, password);
                }
                else
                {
                    aprendiz.idAprendiz = Convert.ToInt32(hfIdAprendiz.Value);
                    ok = aprendizL.ActualizarAprendiz(aprendiz);
                }

                if (ok)
                {
                    CargarGrid();
                    ClientScript.RegisterStartupScript(this.GetType(), "hide", "ocultarModal();", true);
                    MostrarAlerta("Aprendiz guardado correctamente.", "success");
                }
                else
                {
                    MostrarAlerta("Error al guardar aprendiz.", "danger");
                }
            }
            catch (Exception ex)
            {
                MostrarAlerta("Error: " + ex.Message, "danger");
            }
        }

        protected void gvAprendices_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "Editar")
            {
                var aprendiz = aprendizL.ListarAprendices().Find(a => a.idAprendiz == id);
                if (aprendiz != null)
                {
                    hfIdAprendiz.Value = aprendiz.idAprendiz.ToString();
                    litTitulo.Text = "Editar Aprendiz";
                    txtNombres.Text = aprendiz.nombres;
                    txtApellidos.Text = aprendiz.apellidos;
                    ddlTipoDoc.SelectedValue = aprendiz.tipoDocumento;
                    txtNumDoc.Text = aprendiz.numeroDocumento;
                    txtCorreo.Text = aprendiz.correo;
                    txtTelefono.Text = aprendiz.telefono;
                    txtFechaNac.Text = aprendiz.fechaNacimiento.ToString("yyyy-MM-dd");
                    ddlFicha.SelectedValue = aprendiz.idFicha.ToString();
                    divCredenciales.Visible = false;
                    txtUsername.Text = "";
                    txtPassword.Text = "";
                    ClientScript.RegisterStartupScript(this.GetType(), "show", "mostrarModal();", true);
                }
            }
            else if (e.CommandName == "Eliminar")
            {
                try
                {
                    if (aprendizL.EliminarAprendiz(id))
                    {
                        CargarGrid();
                        MostrarAlerta("Eliminado correctamente.", "warning");
                    }
                    else
                        MostrarAlerta("No se pudo eliminar.", "danger");
                }
                catch (Exception ex)
                {
                    MostrarAlerta("Error: " + ex.Message, "danger");
                }
            }
        }

        protected void gvAprendices_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvAprendices.PageIndex = e.NewPageIndex;
            CargarGrid();
        }

        protected void btnCargaMasiva_Click(object sender, EventArgs e)
        {
            CargarFichasCarga();
            //Limpiar resultados anteriores
            lblResumenCarga.Text = "";
            gvErroresCarga.Visible = false;
            btnInsertarCarga.Enabled = false;
            AprendicesCargaValidos = new List<AprendizTemp>();
            ScriptManager.RegisterStartupScript(this, GetType(), "showCarga", "mostrarModalCargaMasiva();", true);
        }

        protected void btnValidarCarga_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;
            if (fuExcelCarga.PostedFile == null || fuExcelCarga.PostedFile.ContentLength == 0)
            {
                MostrarAlerta("Seleccione un archivo Excel.", "danger");
                return;
            }
            if (string.IsNullOrEmpty(ddlFichaCarga.SelectedValue))
            {
                MostrarAlerta("Seleccione una ficha.", "danger");
                return;
            }

            // Limpiar listas
            aprendicesValidos.Clear();
            erroresCarga.Clear();
            AprendicesCargaValidos = new List<AprendizTemp>();

            // Registrar codificación (necesario para caracteres especiales)
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            using (var stream = fuExcelCarga.PostedFile.InputStream)
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                    {
                        ConfigureDataTable = _ => new ExcelDataTableConfiguration()
                        {
                            UseHeaderRow = true
                        }
                    });
                    DataTable dt = result.Tables[0];
                    string[] columnasRequeridas = { "nombres", "apellidos", "tipoDocumento", "numeroDocumento", "correo", "fechaNacimiento" };
                    foreach (string columna in columnasRequeridas)
                    {
                        if (!dt.Columns.Contains(columna))
                        {
                            AgregarError(1, columna, "No existe esta columna en el archivo.");
                        }
                    }

                    if (erroresCarga.Count > 0)
                    {
                        MostrarResultadoCarga();
                        return;
                    }

                    HashSet<string> correosArchivo = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                    HashSet<string> documentosArchivo = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow row = dt.Rows[i];
                        int filaNum = i + 2;

                        string nombres = row["nombres"]?.ToString().Trim();
                        string apellidos = row["apellidos"]?.ToString().Trim();
                        string tipoDocumento = row["tipoDocumento"]?.ToString().Trim();
                        string numeroDocumento = row["numeroDocumento"]?.ToString().Trim();
                        string correo = row["correo"]?.ToString().Trim();
                        string telefono = row["telefono"]?.ToString().Trim();

                        bool valido = true;
                        DateTime fechaNacimiento = DateTime.MinValue;

                        // Validar obligatorios
                        if (string.IsNullOrWhiteSpace(nombres)) { AgregarError(filaNum, "nombres", "Campo obligatorio"); valido = false; }
                        if (string.IsNullOrWhiteSpace(apellidos)) { AgregarError(filaNum, "apellidos", "Campo obligatorio"); valido = false; }
                        if (string.IsNullOrWhiteSpace(tipoDocumento)) { AgregarError(filaNum, "tipoDocumento", "Campo obligatorio"); valido = false; }
                        if (string.IsNullOrWhiteSpace(numeroDocumento)) { AgregarError(filaNum, "numeroDocumento", "Campo obligatorio"); valido = false; }
                        if (string.IsNullOrWhiteSpace(correo)) { AgregarError(filaNum, "correo", "Campo obligatorio"); valido = false; }
                        else if (!IsValidEmail(correo)) { AgregarError(filaNum, "correo", "Formato inválido"); valido = false; }
                        else if (!correosArchivo.Add(correo)) { AgregarError(filaNum, "correo", "Está repetido en el archivo"); valido = false; }

                        if (!string.IsNullOrWhiteSpace(numeroDocumento) && !documentosArchivo.Add(numeroDocumento))
                        {
                            AgregarError(filaNum, "numeroDocumento", "Está repetido en el archivo");
                            valido = false;
                        }

                        // Validar fecha
                        if (row["fechaNacimiento"] == DBNull.Value || string.IsNullOrWhiteSpace(row["fechaNacimiento"]?.ToString()))
                        {
                            AgregarError(filaNum, "fechaNacimiento", "Campo obligatorio");
                            valido = false;
                        }
                        else if (!TryParseFechaCarga(row["fechaNacimiento"], out fechaNacimiento))
                        {
                            AgregarError(filaNum, "fechaNacimiento", "Formato inválido. Use yyyy-MM-dd");
                            valido = false;
                        }

                        if (valido)
                        {
                            if (ExisteCorreo(correo))
                            {
                                AgregarError(filaNum, "correo", "Ya se encuantra registrado");
                                valido = false;
                            }
                            if (ExisteDocumento(numeroDocumento))
                            {
                                AgregarError(filaNum, "numeroDocumento", "Ya se encuentra registrado");
                                valido = false;
                            }
                        }

                        if (valido)
                        {
                            aprendicesValidos.Add(new AprendizTemp
                            {
                                Fila = filaNum,
                                Nombres = nombres,
                                Apellidos = apellidos,
                                TipoDocumento = tipoDocumento,
                                NumeroDocumento = numeroDocumento,
                                Correo = correo,
                                Telefono = telefono,
                                FechaNacimiento = fechaNacimiento
                            });
                        }
                    }
                }
            }

            AprendicesCargaValidos = aprendicesValidos;
            ValidarCupoCargaMasiva();
            MostrarResultadoCarga();
        }

        private void ValidarCupoCargaMasiva()
        {
            if (string.IsNullOrEmpty(ddlFichaCarga.SelectedValue) || aprendicesValidos.Count == 0)
                return;

            int idFicha = Convert.ToInt32(ddlFichaCarga.SelectedValue);
            int asignados = aprendizL.ContarAprendicesPorFicha(idFicha);
            int cuposDisponibles = Math.Max(0, 30 - asignados);

            if (aprendicesValidos.Count > cuposDisponibles)
            {
                AgregarError(0, "idFicha", $"La ficha seleccionada tiene {asignados} aprendices. Solo quedan {cuposDisponibles} cupos disponibles.");
                aprendicesValidos.Clear();
                AprendicesCargaValidos = new List<AprendizTemp>();
            }
        }

        private void MostrarResultadoCarga()
        {
            gvErroresCarga.DataSource = erroresCarga;
            gvErroresCarga.DataBind();
            gvErroresCarga.Visible = erroresCarga.Count > 0;
            lblResumenCarga.Text = $"Resumen: {aprendicesValidos.Count} registros válidos, {erroresCarga.Count} errores.";
            btnInsertarCarga.Enabled = aprendicesValidos.Count > 0;
            ScriptManager.RegisterStartupScript(this, GetType(), "showCargaResultado", "mostrarModalCargaMasiva();", true);
        }

        protected void btnInsertarCarga_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ddlFichaCarga.SelectedValue))
            {
                MostrarAlerta("Seleccione una ficha.", "danger");
                ScriptManager.RegisterStartupScript(this, GetType(), "showCargaSinFicha", "mostrarModalCargaMasiva();", true);
                return;
            }

            List<AprendizTemp> validos = AprendicesCargaValidos;
            if (validos.Count == 0)
            {
                MostrarAlerta("Primero valide un archivo con registros válidos.", "warning");
                ScriptManager.RegisterStartupScript(this, GetType(), "showCargaSinValidos", "mostrarModalCargaMasiva();", true);
                return;
            }

            int idFicha = Convert.ToInt32(ddlFichaCarga.SelectedValue);
            int asignados = aprendizL.ContarAprendicesPorFicha(idFicha);
            if (asignados + validos.Count > 30)
            {
                MostrarAlerta($"No se puede insertar la carga. La ficha tiene {asignados} aprendices y solo admite 30.", "danger");
                ScriptManager.RegisterStartupScript(this, GetType(), "showCargaCupo", "mostrarModalCargaMasiva();", true);
                return;
            }

            int insertados = 0;
            int errores = 0;

            foreach (var temp in validos)
            {
                try
                {
                    ClAprendiz aprendiz = new ClAprendiz
                    {
                        nombres = temp.Nombres,
                        apellidos = temp.Apellidos,
                        tipoDocumento = temp.TipoDocumento,
                        numeroDocumento = temp.NumeroDocumento,
                        correo = temp.Correo,
                        telefono = temp.Telefono,
                        fechaNacimiento = temp.FechaNacimiento,
                        idFicha = idFicha
                    };
                    string username = temp.NumeroDocumento; // usando el número de documento como username
                    string password = temp.NumeroDocumento; // contraseña inicial igual al documento
                    if (aprendizL.InsertarAprendizConUsuario(aprendiz, username, password))
                        insertados++;
                    else
                        errores++;
                }
                catch
                {
                    errores++;
                }
            }

            MostrarAlerta($"Insertados: {insertados}. Errores: {errores}.", insertados > 0 ? "success" : "warning");
            btnInsertarCarga.Enabled = false;
            AprendicesCargaValidos = new List<AprendizTemp>();
            CargarGrid(); 
            // Cerrar modal
            ScriptManager.RegisterStartupScript(this, GetType(), "hideCarga", "ocultarModalCargaMasiva();", true);
        }

        private void AgregarError(int fila, string campo, string error)
        {
            erroresCarga.Add(new ErrorValidacion { Fila = fila, Campo = campo, Error = error });
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private bool TryParseFechaCarga(object valor, out DateTime fecha)
        {
            fecha = DateTime.MinValue;
            if (valor == null || valor == DBNull.Value)
                return false;

            if (valor is DateTime fechaExcel)
            {
                fecha = fechaExcel;
                return true;
            }

            string texto = valor.ToString().Trim();
            return DateTime.TryParseExact(texto, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out fecha)
                || DateTime.TryParse(texto, out fecha);
        }

        private bool ExisteCorreo(string correo)
        {
            return aprendizL.ListarAprendices().Any(a => a.correo.Equals(correo, StringComparison.OrdinalIgnoreCase));
        }

        private bool ExisteDocumento(string documento)
        {
            return aprendizL.ListarAprendices().Any(a => a.numeroDocumento == documento);
        }

        private void MarcarFichasLlenas(DropDownList ddl)
        {
            foreach (ListItem item in ddl.Items)
            {
                if (string.IsNullOrWhiteSpace(item.Value))
                    continue;

                int idFicha;
                if (!int.TryParse(item.Value, out idFicha))
                    continue;

                int total = aprendizL.ContarAprendicesPorFicha(idFicha);
                if (total >= 30)
                {
                    item.Text = item.Text + " (cupo lleno)";
                    item.Enabled = false;
                }
            }
        }

        private void MostrarAlerta(string msg, string tipo)
        {
            string script = $"alert('{msg}');";
            ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
        }
    }
}
