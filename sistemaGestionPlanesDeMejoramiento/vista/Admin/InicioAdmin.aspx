<%@ Page Title="" Language="C#" MasterPageFile="~/vista/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="InicioAdmin.aspx.cs" Inherits="sistemaGestionPlanesDeMejoramiento.vista.Admin.InicioAdmin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="alert alert-info">
        <i class="fas fa-circle-info"></i>
        <div>
            <h3>Bienvenido, <asp:Label ID="lblBienvenida" runat="server" Text=""></asp:Label></h3>
            <p>Panel de control del administrador. Desde aqui puedes gestionar centros, instructores, aprendices y planes.</p>
        </div>
    </div>

    <asp:Label ID="lblMensaje" runat="server" CssClass="message-label" />

    <div class="row">
        <div class="col-lg-6 mb-3">
            <div class="card shadow">
                <div class="card-header bg-primary text-white d-flex justify-content-between align-items-center">
                    <h5 class="mb-0">Mi informacion</h5>
                    <asp:Button ID="btnEditarDatos" runat="server" Text="Editar" CssClass="btn btn-secondary btn-sm" CausesValidation="false" OnClick="btnEditarDatos_Click" />
                </div>
                <div class="card-body">
                    <p><strong>Nombres:</strong> <asp:Label ID="lblNombres" runat="server" /></p>
                    <p><strong>Apellidos:</strong> <asp:Label ID="lblApellidos" runat="server" /></p>
                    <p><strong>Tipo documento:</strong> <asp:Label ID="lblTipoDoc" runat="server" /></p>
                    <p><strong>Numero documento:</strong> <asp:Label ID="lblNumDoc" runat="server" /></p>
                    <p><strong>Correo:</strong> <asp:Label ID="lblCorreo" runat="server" /></p>
                    <p><strong>Telefono:</strong> <asp:Label ID="lblTelefono" runat="server" /></p>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="modalDatosAdmin" tabindex="-1" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Editar informacion basica</h5>
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
                        <label>Telefono</label>
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
        function mostrarModalDatosAdmin() {
            bootstrap.Modal.getOrCreateInstance(document.getElementById('modalDatosAdmin')).show();
        }
        function ocultarModalDatosAdmin() {
            bootstrap.Modal.getOrCreateInstance(document.getElementById('modalDatosAdmin')).hide();
        }
    </script>
</asp:Content>
