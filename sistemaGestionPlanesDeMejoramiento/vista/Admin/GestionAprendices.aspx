<%@ Page Title="" Language="C#" MasterPageFile="~/vista/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="GestionAprendices.aspx.cs" Inherits="sistemaGestionPlanesDeMejoramiento.vista.Admin.GestionAprendices" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="d-flex justify-content-between mb-3">
        <h2><i class="fas fa-users"></i>Aprendices</h2>
        <asp:Button ID="btnNuevo" runat="server" Text="+ Nuevo" CssClass="btn btn-primary" CausesValidation="false" OnClick="btnNuevo_Click" />
    </div>
    <div class="card shadow">
        <div class="card-body">
            <asp:GridView ID="gvAprendices" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-hover"
                DataKeyNames="idAprendiz" OnRowCommand="gvAprendices_RowCommand">
                <Columns>
                    <asp:BoundField DataField="idAprendiz" HeaderText="ID" />
                    <asp:BoundField DataField="nombres" HeaderText="Nombres" />
                    <asp:BoundField DataField="apellidos" HeaderText="Apellidos" />
                    <asp:BoundField DataField="tipoDocumento" HeaderText="Tipo Doc" />
                    <asp:BoundField DataField="numeroDocumento" HeaderText="N° Documento" />
                    <asp:BoundField DataField="correo" HeaderText="Correo" />
                    <asp:BoundField DataField="telefono" HeaderText="Teléfono" />
                    <asp:BoundField DataField="fechaNacimiento" HeaderText="Fecha Nac." DataFormatString="{0:dd/MM/yyyy}" />
                    <asp:TemplateField HeaderText="Acciones">
                        <ItemTemplate>
                            <asp:ImageButton runat="server" CommandName="Editar" CommandArgument='<%# Eval("idAprendiz") %>'
                                CausesValidation="false"
                                ImageUrl="https://img.icons8.com/ios-glyphs/20/edit.png" ToolTip="Editar" />
                            <asp:ImageButton runat="server" CommandName="Eliminar" CommandArgument='<%# Eval("idAprendiz") %>'
                                ImageUrl="https://img.icons8.com/ios-glyphs/20/trash.png" ToolTip="Eliminar"
                                CausesValidation="false" OnClientClick="return confirm('¿Eliminar este aprendiz?');" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate>
                    <div class="alert alert-info">No hay aprendices registrados.</div>
                </EmptyDataTemplate>
            </asp:GridView>
        </div>
    </div>

    <!-- Modal -->
    <asp:HiddenField ID="hfIdAprendiz" runat="server" />
    <div class="modal fade" id="modalAprendiz" tabindex="-1">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header bg-primary text-white">
                    <h5 class="modal-title">
                        <asp:Literal ID="litTitulo" runat="server" /></h5>
                    <button type="button" class="btn-close btn-close-white" data-bs-dismiss="modal"></button>
                </div>
                <div class="modal-body">
                    <div class="mb-3">
                        <label>Nombres</label>
                        <asp:TextBox ID="txtNombres" runat="server" CssClass="form-control" />
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtNombres" ErrorMessage="*" CssClass="text-danger" ValidationGroup="Aprendiz" />
                    </div>
                    <div class="mb-3">
                        <label>Apellidos</label>
                        <asp:TextBox ID="txtApellidos" runat="server" CssClass="form-control" />
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtApellidos" ErrorMessage="*" CssClass="text-danger" ValidationGroup="Aprendiz" />
                    </div>
                    <div class="mb-3">
                        <label>Tipo Documento</label>
                        <asp:DropDownList ID="ddlTipoDoc" runat="server" CssClass="form-select">
                            <asp:ListItem Text="CC" Value="CC" />
                            <asp:ListItem Text="TI" Value="TI" />
                            <asp:ListItem Text="CE" Value="CE" />
                        </asp:DropDownList>
                    </div>
                    <div class="mb-3">
                        <label>Número Documento</label>
                        <asp:TextBox ID="txtNumDoc" runat="server" CssClass="form-control" />
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtNumDoc" ErrorMessage="*" CssClass="text-danger" ValidationGroup="Aprendiz" />
                    </div>
                    <div class="mb-3">
                        <label>Correo</label>
                        <asp:TextBox ID="txtCorreo" runat="server" CssClass="form-control" TextMode="Email" />
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtCorreo" ErrorMessage="*" CssClass="text-danger" ValidationGroup="Aprendiz" />
                    </div>
                    <div class="mb-3">
                        <label>Teléfono</label>
                        <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control" />
                    </div>
                    <div class="mb-3">
                        <label>Fecha Nacimiento</label>
                        <asp:TextBox ID="txtFechaNac" runat="server" CssClass="form-control" TextMode="Date" />
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtFechaNac" ErrorMessage="*" CssClass="text-danger" ValidationGroup="Aprendiz" />
                    </div>
                    <div class="mb-3">
                        <label>ID Ficha</label>
                        <asp:TextBox ID="txtIdFicha" runat="server" CssClass="form-control" TextMode="Number" />
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtIdFicha" ErrorMessage="*" CssClass="text-danger" ValidationGroup="Aprendiz" />
                    </div>
                    <div class="mb-3">
                        <label>ID Usuario</label>
                        <asp:TextBox ID="txtIdUsuario" runat="server" CssClass="form-control" TextMode="Number" />
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtIdUsuario" ErrorMessage="*" CssClass="text-danger" ValidationGroup="Aprendiz" />
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                    <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-primary" ValidationGroup="Aprendiz" OnClick="btnGuardar_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="server">
    <script>
        function obtenerModalAprendiz() {
            const modalElement = document.getElementById('modalAprendiz');
            return bootstrap.Modal.getOrCreateInstance(modalElement);
        }

        function mostrarModal() {
            obtenerModalAprendiz().show();
        }

        function ocultarModal() {
            obtenerModalAprendiz().hide();
        }
    </script>
</asp:Content>
