<%@ Page Title="Gestión de Administradores" Language="C#" MasterPageFile="~/vista/Admin/AdminMaster.Master"
    AutoEventWireup="true" CodeBehind="GestionAdministradores.aspx.cs"
    Inherits="sistemaGestionPlanesDeMejoramiento.vista.Admin.GestionAdministradores" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="page-header">
        <h2><i class="fas fa-user-shield"></i>Administradores</h2>
        <asp:Button ID="btnNuevo" runat="server" Text="+ Nuevo Administrador"
            CssClass="btn btn-primary" CausesValidation="false" OnClick="btnNuevo_Click" />
    </div>

    <!-- Mensaje del sistema (éxito/error) con estilo de alerta claro -->
    <asp:Label ID="lblMensaje" runat="server" CssClass="d-block mb-3" />

    <div class="card">
        <div class="card-body p-0">
            <div class="table-responsive">
                <asp:GridView ID="gvAdministradores" runat="server" AutoGenerateColumns="False"
                    CssClass="table align-middle"
                    DataKeyNames="idAmin" OnRowCommand="gvAdministradores_RowCommand"
                    AllowPaging="True" PageSize="15" OnPageIndexChanging="gvAdministradores_PageIndexChanging"
                    GridLines="None">
                    <Columns>
                        <asp:BoundField DataField="idAmin" HeaderText="ID"
                            ItemStyle-CssClass="text-muted font-monospace small fw-bold" />
                        <asp:BoundField DataField="nombres" HeaderText="Nombres" />
                        <asp:BoundField DataField="apellidos" HeaderText="Apellidos" />
                        <asp:BoundField DataField="tipoDocumento" HeaderText="Tipo"
                            ItemStyle-CssClass="text-center font-monospace opacity-75" />
                        <asp:BoundField DataField="numeroDocumento" HeaderText="Documento" />
                        <asp:BoundField DataField="correo" HeaderText="Email" />
                        <asp:BoundField DataField="telefono" HeaderText="Teléfono" ItemStyle-CssClass="text-nowrap" />

                        <asp:TemplateField HeaderText="Acciones" ItemStyle-CssClass="text-center text-nowrap">
                            <ItemTemplate>
                                <div class="d-flex justify-content-center gap-2">
                                    <asp:LinkButton ID="btnEditar" runat="server" CommandName="Editar"
                                        CommandArgument='<%# Eval("idAmin") %>'
                                        CssClass="btn-action btn-action-edit" ToolTip="Editar Administrador"
                                        CausesValidation="false">
                                        <i class="fas fa-pen"></i>
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="btnEliminar" runat="server" CommandName="Eliminar"
                                        CommandArgument='<%# Eval("idAmin") %>'
                                        CssClass="btn-action btn-action-delete" ToolTip="Eliminar Registro"
                                        CausesValidation="false"
                                        OnClientClick="return confirmarEliminacion(this, 'Seguro que desea eliminar permanentemente este administrador?');">
                                        <i class="fas fa-trash-alt"></i>
                                    </asp:LinkButton>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <PagerStyle CssClass="pagination" />
                    <EmptyDataTemplate>
                        <div class="empty-state">
                            <i class="fas fa-user-shield"></i>
                            <p>No se encontraron registros de administradores.</p>
                        </div>
                    </EmptyDataTemplate>
                </asp:GridView>
            </div>
        </div>
    </div>

    <asp:HiddenField ID="hfIdAdministrador" runat="server" />

    <!-- ═══ MODAL CREAR / EDITAR ═══ -->
    <div class="modal fade" id="modalAdministrador" tabindex="-1" aria-hidden="true">
        <div class="modal-dialog modal-lg modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">
                        <asp:Literal ID="litModalTitulo" runat="server" />
                    </h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-6 mb-3">
                            <label>Nombres</label>
                            <asp:TextBox ID="txtNombres" runat="server" CssClass="form-control" placeholder="Nombres completos" />
                            <asp:RequiredFieldValidator ID="rfvNombres" runat="server" ControlToValidate="txtNombres"
                                ErrorMessage="Campo requerido" CssClass="text-danger small mt-1 d-block" ValidationGroup="Administrador" />
                        </div>
                        <div class="col-md-6 mb-3">
                            <label>Apellidos</label>
                            <asp:TextBox ID="txtApellidos" runat="server" CssClass="form-control" placeholder="Apellidos" />
                            <asp:RequiredFieldValidator ID="rfvApellidos" runat="server" ControlToValidate="txtApellidos"
                                ErrorMessage="Campo requerido" CssClass="text-danger small mt-1 d-block" ValidationGroup="Administrador" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4 mb-3">
                            <label>Tipo documento</label>
                            <asp:DropDownList ID="ddlTipoDocumento" runat="server" CssClass="form-select">
                                <asp:ListItem Text="CC" Value="CC" />
                                <asp:ListItem Text="TI" Value="TI" />
                                <asp:ListItem Text="CE" Value="CE" />
                                <asp:ListItem Text="Pasaporte" Value="Pasaporte" />
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-4 mb-3">
                            <label>Número documento</label>
                            <asp:TextBox ID="txtNumeroDocumento" runat="server" CssClass="form-control" placeholder="Número de identificación" />
                            <asp:RequiredFieldValidator ID="rfvNumeroDocumento" runat="server" ControlToValidate="txtNumeroDocumento"
                                ErrorMessage="Campo requerido" CssClass="text-danger small mt-1 d-block" ValidationGroup="Administrador" />
                        </div>
                        <div class="col-md-4 mb-3">
                            <label>Teléfono</label>
                            <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control" placeholder="Celular (Opcional)" />
                        </div>
                    </div>
                    <div class="mb-4">
                        <label>Correo electrónico</label>
                        <asp:TextBox ID="txtCorreo" runat="server" CssClass="form-control" TextMode="Email" placeholder="nombre@ejemplo.com" />
                        <asp:RequiredFieldValidator ID="rfvCorreo" runat="server" ControlToValidate="txtCorreo"
                            ErrorMessage="Campo requerido" CssClass="text-danger small mt-1 d-block" ValidationGroup="Administrador" />
                    </div>

                    <!-- Bloque de credenciales con la nueva alerta estilizada -->
                    <div class="border-top pt-4 mt-2" id="divCredenciales" runat="server">
                        <div class="row">
                            <div class="col-md-6 mb-3">
                                <label>Usuario de Sistema</label>
                                <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" placeholder="Nombre de usuario único" />
                                <asp:RequiredFieldValidator ID="rfvUsername" runat="server" ControlToValidate="txtUsername"
                                    ErrorMessage="Campo requerido" CssClass="text-danger small mt-1 d-block" ValidationGroup="Administrador" />
                            </div>
                            <div class="col-md-6 mb-3">
                                <label>Contraseña Provisional</label>
                                <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password" placeholder="••••••••" />
                                <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtPassword"
                                    ErrorMessage="Campo requerido" CssClass="text-danger small mt-1 d-block" ValidationGroup="Administrador" />
                            </div>
                        </div>
                        <!-- Alerta rediseñada (clara, con borde izquierdo azul e icono) -->
                        <div class="alert alert-info d-flex align-items-start mt-2">
                            <i class="fas fa-shield-alt me-3 mt-1 fs-5"></i>
                            <div>
                                <strong>Seguridad:</strong> Las credenciales se procesan con funciones criptográficas seguras de un solo sentido. 
                                Cada administrador deberá cambiar su contraseña desde su perfil privado al iniciar sesión.
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                    <asp:Button ID="btnGuardar" runat="server" Text="Guardar Cambios"
                        CssClass="btn btn-primary" ValidationGroup="Administrador" OnClick="btnGuardar_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="server">
    <script>
        function mostrarModalAdministrador() {
            bootstrap.Modal.getOrCreateInstance(document.getElementById('modalAdministrador')).show();
        }
        function ocultarModalAdministrador() {
            bootstrap.Modal.getOrCreateInstance(document.getElementById('modalAdministrador')).hide();
        }
    </script>
</asp:Content>
