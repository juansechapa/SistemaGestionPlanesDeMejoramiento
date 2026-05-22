<%@ Page Title="Gestión de Instructores" Language="C#" MasterPageFile="~/vista/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="GestionInstructores.aspx.cs" Inherits="sistemaGestionPlanesDeMejoramiento.vista.Admin.GestionInstructores" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="d-flex justify-content-between align-items-center mb-3">
        <h2><i class="fas fa-chalkboard-teacher"></i>Instructores</h2>
        <asp:Button ID="btnNuevo" runat="server" Text="+ Nuevo Instructor" CssClass="btn btn-primary" CausesValidation="false" OnClick="btnNuevo_Click" />
    </div>

    <div class="card shadow">
        <div class="card-body">
            <asp:GridView ID="gvInstructores" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-hover"
                DataKeyNames="idInstructor" OnRowCommand="gvInstructores_RowCommand">
                <columns>
                    <asp:BoundField DataField="idInstructor" HeaderText="ID" ReadOnly="True" />
                    <asp:BoundField DataField="nombres" HeaderText="Nombres" />
                    <asp:BoundField DataField="apellidos" HeaderText="Apellidos" />
                    <asp:BoundField DataField="tipoDocumento" HeaderText="Tipo Doc" />
                    <asp:BoundField DataField="numeroDocumento" HeaderText="N° Documento" />
                    <asp:BoundField DataField="correo" HeaderText="Correo" />
                    <asp:BoundField DataField="telefono" HeaderText="Teléfono" />
                    <asp:BoundField DataField="especialidad" HeaderText="Especialidad" />
                    <asp:TemplateField HeaderText="Acciones">
                        <itemtemplate>
                            <asp:ImageButton ID="btnEditar" runat="server" CommandName="Editar" CommandArgument='<%# Eval("idInstructor") %>'
                                ImageUrl="https://img.icons8.com/ios-glyphs/20/000000/edit--v1.png" ToolTip="Editar" CausesValidation="false" />
                            <asp:ImageButton ID="btnEliminar" runat="server" CommandName="Eliminar" CommandArgument='<%# Eval("idInstructor") %>'
                                ImageUrl="https://img.icons8.com/ios-glyphs/20/000000/trash.png" ToolTip="Eliminar" CausesValidation="false"
                                OnClientClick="return confirm('¿Está seguro de eliminar este instructor?');" />
                        </itemtemplate>
                        <itemstyle width="80px" />
                    </asp:TemplateField>
                </columns>
                <emptydatatemplate>
                    <div class="alert alert-info">No hay instructores registrados.</div>
                </emptydatatemplate>
            </asp:GridView>
        </div>
    </div>

    <!-- Modal nuevo/editar instructor -->
    <asp:HiddenField ID="hfIdInstructor" runat="server" />
    <div class="modal fade" id="modalInstructor" tabindex="-1" aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header bg-primary text-white">
                    <h5 class="modal-title">
                        <asp:Literal ID="litModalTitulo" runat="server" Text="Instructor" /></h5>
                    <button type="button" class="btn-close btn-close-white" data-bs-dismiss="modal"></button>
                </div>
                <div class="modal-body">
                    <!-- Datos personales -->
                    <div class="row">
                        <div class="col-md-6">
                            <div class="mb-3">
                                <label>Nombres</label>
                                <asp:TextBox ID="txtNombres" runat="server" CssClass="form-control" />
                                <asp:RequiredFieldValidator ID="rfvNombres" runat="server" ControlToValidate="txtNombres" ErrorMessage="*" CssClass="text-danger" ValidationGroup="Instructor" />
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="mb-3">
                                <label>Apellidos</label>
                                <asp:TextBox ID="txtApellidos" runat="server" CssClass="form-control" />
                                <asp:RequiredFieldValidator ID="rfvApellidos" runat="server" ControlToValidate="txtApellidos" ErrorMessage="*" CssClass="text-danger" ValidationGroup="Instructor" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            <div class="mb-3">
                                <label>Tipo Documento</label>
                                <asp:DropDownList ID="ddlTipoDocumento" runat="server" CssClass="form-select">
                                    <asp:ListItem Text="CC" Value="CC" />
                                    <asp:ListItem Text="TI" Value="TI" />
                                    <asp:ListItem Text="CE" Value="CE" />
                                    <asp:ListItem Text="Pasaporte" Value="Pasaporte" />
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvTipoDoc" runat="server" ControlToValidate="ddlTipoDocumento" InitialValue="" ErrorMessage="*" CssClass="text-danger" ValidationGroup="Instructor" />
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="mb-3">
                                <label>Número Documento</label>
                                <asp:TextBox ID="txtNumeroDocumento" runat="server" CssClass="form-control" />
                                <asp:RequiredFieldValidator ID="rfvNumDoc" runat="server" ControlToValidate="txtNumeroDocumento" ErrorMessage="*" CssClass="text-danger" ValidationGroup="Instructor" />
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="mb-3">
                                <label>Teléfono</label>
                                <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="mb-3">
                                <label>Correo Electrónico</label>
                                <asp:TextBox ID="txtCorreo" runat="server" CssClass="form-control" TextMode="Email" />
                                <asp:RequiredFieldValidator ID="rfvCorreo" runat="server" ControlToValidate="txtCorreo" ErrorMessage="*" CssClass="text-danger" ValidationGroup="Instructor" />
                                <asp:RegularExpressionValidator ID="revCorreo" runat="server" ControlToValidate="txtCorreo"
                                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                    ErrorMessage="Correo inválido" CssClass="text-danger" ValidationGroup="Instructor" />
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="mb-3">
                                <label>Especialidad</label>
                                <asp:TextBox ID="txtEspecialidad" runat="server" CssClass="form-control" />
                            </div>
                        </div>
                    </div>

                    <!-- Credenciales de acceso, solo cuando se ingresa un nuevo instructor -->
                    <div class="border-top pt-3 mt-2" id="divCredenciales" runat="server">
                        <h6><i class="fas fa-key"></i>Credenciales de acceso</h6>
                        <div class="row">
                            <div class="col-md-6">
                                <div class="mb-3">
                                    <label>Nombre de Usuario</label>
                                    <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" />
                                    <asp:RequiredFieldValidator ID="rfvUsername" runat="server" ControlToValidate="txtUsername" ErrorMessage="*" CssClass="text-danger" ValidationGroup="Instructor" />
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="mb-3">
                                    <label>Contraseña</label>
                                    <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password" />
                                    <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtPassword" ErrorMessage="*" CssClass="text-danger" ValidationGroup="Instructor" />
                                </div>
                            </div>
                        </div>
                        <div class="alert alert-info small">
                            <i class="fas fa-info-circle"></i>Estas credenciales se usarán para que el instructor acceda al sistema con rol de Instructor.
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                    <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-primary" ValidationGroup="Instructor" OnClick="btnGuardar_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="server">
    <script>
        function mostrarModal() {
            var modal = new bootstrap.Modal(document.getElementById('modalInstructor'));
            modal.show();
        }
        function ocultarModal() {
            var modal = bootstrap.Modal.getInstance(document.getElementById('modalInstructor'));
            if (modal) modal.hide();
        }
    </script>
</asp:Content>
