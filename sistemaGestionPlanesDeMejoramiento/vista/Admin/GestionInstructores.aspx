<%@ Page Title="" Language="C#" MasterPageFile="~/vista/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="GestionInstructores.aspx.cs" Inherits="sistemaGestionPlanesDeMejoramiento.vista.Admin.GestionInstructores" %>

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
                <Columns>
                    <asp:BoundField DataField="idInstructor" HeaderText="ID" ReadOnly="True" />
                    <asp:BoundField DataField="nombres" HeaderText="Nombres" />
                    <asp:BoundField DataField="apellidos" HeaderText="Apellidos" />
                    <asp:BoundField DataField="correo" HeaderText="Correo" />
                    <asp:BoundField DataField="telefono" HeaderText="Teléfono" />
                    <asp:BoundField DataField="especialidad" HeaderText="Especialidad" />
                    <asp:TemplateField HeaderText="Acciones">
                        <ItemTemplate>
                            <asp:ImageButton ID="btnEditar" runat="server" CommandName="Editar" CommandArgument='<%# Eval("idInstructor") %>'
                                ImageUrl="https://img.icons8.com/ios-glyphs/20/000000/edit--v1.png" ToolTip="Editar" CausesValidation="false" />
                            <asp:ImageButton ID="btnEliminar" runat="server" CommandName="Eliminar" CommandArgument='<%# Eval("idInstructor") %>'
                                ImageUrl="https://img.icons8.com/ios-glyphs/20/000000/trash.png" ToolTip="Eliminar" CausesValidation="false"
                                OnClientClick="return confirm('¿Está seguro de eliminar este instructor?');" />
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

    <!-- Modal agregar/editar -->
    <asp:HiddenField ID="hfIdInstructor" runat="server" />
    <div class="modal fade" id="modalInstructor" tabindex="-1" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header bg-primary text-white">
                    <h5 class="modal-title">
                        <asp:Literal ID="litModalTitulo" runat="server" Text="Instructor" /></h5>
                    <button type="button" class="btn-close btn-close-white" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <div class="mb-3">
                        <label>Nombres</label>
                        <asp:TextBox ID="txtNombres" runat="server" CssClass="form-control" />
                        <asp:RequiredFieldValidator ID="rfvNombres" runat="server" ControlToValidate="txtNombres" ErrorMessage="* Obligatorio" CssClass="text-danger" ValidationGroup="Instructor" />
                    </div>
                    <div class="mb-3">
                        <label>Apellidos</label>
                        <asp:TextBox ID="txtApellidos" runat="server" CssClass="form-control" />
                        <asp:RequiredFieldValidator ID="rfvApellidos" runat="server" ControlToValidate="txtApellidos" ErrorMessage="* Obligatorio" CssClass="text-danger" ValidationGroup="Instructor" />
                    </div>
                    <div class="mb-3">
                        <label>Correo</label>
                        <asp:TextBox ID="txtCorreo" runat="server" CssClass="form-control" TextMode="Email" />
                        <asp:RequiredFieldValidator ID="rfvCorreo" runat="server" ControlToValidate="txtCorreo" ErrorMessage="* Obligatorio" CssClass="text-danger" ValidationGroup="Instructor" />
                        <asp:RegularExpressionValidator ID="revCorreo" runat="server" ControlToValidate="txtCorreo"
                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                            ErrorMessage="Correo inválido" CssClass="text-danger" ValidationGroup="Instructor" />
                    </div>
                    <div class="mb-3">
                        <label>Teléfono</label>
                        <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control" />
                    </div>
                    <div class="mb-3">
                        <label>Especialidad</label>
                        <asp:TextBox ID="txtEspecialidad" runat="server" CssClass="form-control" />
                    </div>
                    <div class="mb-3">
                        <label>ID Usuario</label>
                        <asp:TextBox ID="txtIdUsuario" runat="server" CssClass="form-control" TextMode="Number" />
                        <asp:RequiredFieldValidator ID="rfvIdUsuario" runat="server" ControlToValidate="txtIdUsuario" ErrorMessage="* Obligatorio" CssClass="text-danger" ValidationGroup="Instructor" />
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
        function obtenerModalInstructor() {
            const modalElement = document.getElementById('modalInstructor');
            return bootstrap.Modal.getOrCreateInstance(modalElement);
        }

        function mostrarModal() {
            obtenerModalInstructor().show();
        }

        function ocultarModal() {
            obtenerModalInstructor().hide();
        }
    </script>
</asp:Content>
