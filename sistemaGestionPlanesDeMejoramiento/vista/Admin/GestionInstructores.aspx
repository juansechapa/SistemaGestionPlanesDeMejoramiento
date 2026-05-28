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
                DataKeyNames="idInstructor" OnRowCommand="gvInstructores_RowCommand"
                AllowPaging="True" PageSize="15" OnPageIndexChanging="gvInstructores_PageIndexChanging">
                <Columns>
                    <asp:BoundField DataField="idInstructor" HeaderText="ID" ReadOnly="True" />
                    <asp:BoundField DataField="nombres" HeaderText="Nombres" />
                    <asp:BoundField DataField="apellidos" HeaderText="Apellidos" />
                    <asp:BoundField DataField="tipoDocumento" HeaderText="Tipo Doc" />
                    <asp:BoundField DataField="numeroDocumento" HeaderText="N° Documento" />
                    <asp:BoundField DataField="correo" HeaderText="Correo" />
                    <asp:BoundField DataField="telefono" HeaderText="Teléfono" />
                    <asp:BoundField DataField="especialidad" HeaderText="Especialidad" />
                    <asp:TemplateField HeaderText="Fichas" ItemStyle-CssClass="text-center text-nowrap">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnFichas" runat="server" CommandName="AsignarFichas"
                                CommandArgument='<%# Eval("idInstructor") %>'
                                CssClass="btn-action btn-action-view" ToolTip="Asignar fichas" CausesValidation="false">
                                <i class="fas fa-chalkboard-teacher"></i>
                            </asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="60px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Acciones" ItemStyle-CssClass="text-center text-nowrap">
                        <ItemTemplate>
                            <div class="d-flex justify-content-center gap-2">
                                <asp:LinkButton ID="btnEditar" runat="server" CommandName="Editar" CommandArgument='<%# Eval("idInstructor") %>'
                                    CssClass="btn-action btn-action-edit" ToolTip="Editar" CausesValidation="false">
                                    <i class="fas fa-pen"></i>
                                </asp:LinkButton>
                                <asp:LinkButton ID="btnEliminar" runat="server" CommandName="Eliminar" CommandArgument='<%# Eval("idInstructor") %>'
                                    CssClass="btn-action btn-action-delete" ToolTip="Eliminar" CausesValidation="false"
                                    OnClientClick="return confirmarEliminacion(this, 'Seguro que desea eliminar este instructor?');">
                                    <i class="fas fa-trash-alt"></i>
                                </asp:LinkButton>
                            </div>
                        </ItemTemplate>
                        <ItemStyle Width="80px" />
                    </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate>
                    <div class="alert alert-info">No hay instructores registrados.</div>
                </EmptyDataTemplate>
            </asp:GridView>
        </div>
    </div>

    <!-- Modal nuevo/editar instructor (sin cambios) -->
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

    <!-- NUEVO MODAL para asignar fichas -->
    <div class="modal fade" id="modalFichas" tabindex="-1" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header bg-info text-white">
                    <h5 class="modal-title">Asignar fichas al instructor</h5>
                    <button type="button" class="btn-close btn-close-white" data-bs-dismiss="modal"></button>
                </div>
                <div class="modal-body">
                    <asp:CheckBoxList ID="cblFichas" runat="server" CssClass="list-unstyled" DataTextField="codigoFicha" DataValueField="idFicha" />
                    <asp:HiddenField ID="hfIdInstructorFichas" runat="server" />
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                    <asp:Button ID="btnGuardarFichas" runat="server" Text="Guardar asignación" CssClass="btn btn-primary" CausesValidation="false" OnClick="btnGuardarFichas_Click" />
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

        function mostrarModalFichas() {
            var modal = bootstrap.Modal.getOrCreateInstance(document.getElementById('modalFichas'));
            modal.show();
        }

        function ocultarModalFichas() {
            var modal = bootstrap.Modal.getInstance(document.getElementById('modalFichas'));
            if (modal) modal.hide();
        }
    </script>
</asp:Content>
