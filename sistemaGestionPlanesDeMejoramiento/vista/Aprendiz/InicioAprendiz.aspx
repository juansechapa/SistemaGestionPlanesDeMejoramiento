<%@ Page Title="" Language="C#" MasterPageFile="~/vista/Aprendiz/AprendizMaster.Master" AutoEventWireup="true" CodeBehind="InicioAprendiz.aspx.cs" Inherits="sistemaGestionPlanesDeMejoramiento.vista.Aprendiz.InicioAprendiz" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="alert alert-info">
        <i class="fas fa-circle-info"></i>
        <div>
        <h2>¡Hola,
            <asp:Label ID="lblBienvenida" runat="server"></asp:Label>!</h2>
        <p>Bienvenido a tu panel de control. Aqui puedes consultar tus datos y el estado de tus planes de mejoramiento.</p>
        </div>
    </div>

    <div class="row">
        <div class="col-md-6 mb-3">
            <div class="card shadow">
                <div class="card-header bg-primary text-white d-flex justify-content-between align-items-center">
                    <h5 class="mb-0">Mis datos personales</h5>
                    <asp:Button ID="btnEditarDatos" runat="server" Text="Editar" CssClass="btn btn-secondary btn-sm" CausesValidation="false" OnClick="btnEditarDatos_Click" />
                </div>
                <div class="card-body">
                    <asp:Label ID="lblMensaje" runat="server" CssClass="message-label" />
                    <p><strong>Nombres:</strong>
                        <asp:Label ID="lblNombres" runat="server" /></p>
                    <p><strong>Apellidos:</strong>
                        <asp:Label ID="lblApellidos" runat="server" /></p>
                    <p><strong>Tipo documento:</strong>
                        <asp:Label ID="lblTipoDoc" runat="server" /></p>
                    <p><strong>Numero documento:</strong>
                        <asp:Label ID="lblNumDoc" runat="server" /></p>
                    <p><strong>Correo:</strong>
                        <asp:Label ID="lblCorreo" runat="server" /></p>
                    <p><strong>Telefono:</strong>
                        <asp:Label ID="lblTelefono" runat="server" /></p>
                    <p><strong>Fecha nacimiento:</strong>
                        <asp:Label ID="lblFechaNac" runat="server" /></p>
                </div>
            </div>
        </div>
        <div class="col-md-6 mb-3">
            <div class="card shadow">
                <div class="card-header bg-success text-white">
                    <h5 class="mb-0">Información académica</h5>
                </div>
                <div class="card-body">
                    <p>
                        <strong>Estado académico:</strong>
                        <asp:Label ID="lblEstado" runat="server" CssClass="badge bg-secondary" />
                    </p>
                    <p><strong>Ficha:</strong>
                        <asp:Label ID="lblFicha" runat="server" /></p>
                    <p><strong>Programa:</strong>
                        <asp:Label ID="lblPrograma" runat="server" /></p>
                    <p><strong>Centro:</strong>
                        <asp:Label ID="lblCentro" runat="server" /></p>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="modalDatosAprendiz" tabindex="-1" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Editar datos personales</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Cerrar"></button>
                </div>
                <div class="modal-body">
                    <div class="mb-3">
                        <label>Nombres</label>
                        <asp:TextBox ID="txtNombres" runat="server" CssClass="form-control" />
                    </div>
                    <div class="mb-3">
                        <label>Apellidos</label>
                        <asp:TextBox ID="txtApellidos" runat="server" CssClass="form-control" />
                    </div>
                    <div class="mb-3">
                        <label>Correo</label>
                        <asp:TextBox ID="txtCorreo" runat="server" CssClass="form-control" TextMode="Email" />
                    </div>
                    <div class="mb-3">
                        <label>Teléfono</label>
                        <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control" />
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                    <asp:Button ID="btnGuardarDatos" runat="server" Text="Guardar cambios" CssClass="btn btn-primary" OnClick="btnGuardarDatos_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="server">
    <script>
        function mostrarModalDatosAprendiz() {
            bootstrap.Modal.getOrCreateInstance(document.getElementById('modalDatosAprendiz')).show();
        }
        function ocultarModalDatosAprendiz() {
            bootstrap.Modal.getOrCreateInstance(document.getElementById('modalDatosAprendiz')).hide();
        }
    </script>
</asp:Content>
