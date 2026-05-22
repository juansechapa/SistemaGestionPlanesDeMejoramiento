<%@ Page Title="" Language="C#" MasterPageFile="~/vista/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="GestionProgramas.aspx.cs" Inherits="sistemaGestionPlanesDeMejoramiento.vista.Admin.GestionProgramas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="d-flex justify-content-between align-items-center mb-3">
        <h2><i class="fas fa-graduation-cap"></i> Programas de Formación</h2>
        <asp:Button ID="btnNuevo" runat="server" Text="+ Nuevo Programa" CssClass="btn btn-primary" CausesValidation="false" OnClick="btnNuevo_Click" />
    </div>

    <div class="card shadow">
        <div class="card-body">
            <asp:GridView ID="gvProgramas" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-hover"
                DataKeyNames="idPrograma" OnRowCommand="gvProgramas_RowCommand">
                <Columns>
                    <asp:BoundField DataField="idPrograma" HeaderText="ID" />
                    <asp:BoundField DataField="codigoPrograma" HeaderText="Código" />
                    <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                    <asp:BoundField DataField="version" HeaderText="Versión" />
                    <asp:BoundField DataField="nivel" HeaderText="Nivel" />
                    <asp:BoundField DataField="duracionHoras" HeaderText="Horas" />
                    <asp:TemplateField HeaderText="Estado">
                        <ItemTemplate>
                            <asp:Label ID="lblEstado" runat="server" 
                                Text='<%# Eval("estado") != DBNull.Value ? (Convert.ToBoolean(Eval("estado")) ? "Activo" : "Inactivo") : "Sin definir" %>'
                                CssClass='<%# Eval("estado") != DBNull.Value ? (Convert.ToBoolean(Eval("estado")) ? "badge bg-success" : "badge bg-secondary") : "badge bg-dark" %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Acciones">
                        <ItemTemplate>
                            <asp:ImageButton ID="btnEditar" runat="server" CommandName="Editar" CommandArgument='<%# Eval("idPrograma") %>'
                                ImageUrl="https://img.icons8.com/ios-glyphs/20/000000/edit--v1.png" ToolTip="Editar" CausesValidation="false" />
                            <asp:ImageButton ID="btnEliminar" runat="server" CommandName="Eliminar" CommandArgument='<%# Eval("idPrograma") %>'
                                ImageUrl="https://img.icons8.com/ios-glyphs/20/000000/trash.png" ToolTip="Eliminar"
                                CausesValidation="false" OnClientClick="return confirm('¿Eliminar este programa?');" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate>
                    <div class="alert alert-info">No hay programas registrados.</div>
                </EmptyDataTemplate>
            </asp:GridView>
        </div>
    </div>

    <!-- Modal agregar/editar -->
    <asp:HiddenField ID="hfIdPrograma" runat="server" />
    <div class="modal fade" id="modalPrograma" tabindex="-1" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header bg-primary text-white">
                    <h5 class="modal-title"><asp:Literal ID="litModalTitulo" runat="server" /></h5>
                    <button type="button" class="btn-close btn-close-white" data-bs-dismiss="modal"></button>
                </div>
                <div class="modal-body">
                    <div class="mb-3">
                        <label>Código del Programa *</label>
                        <asp:TextBox ID="txtCodigo" runat="server" CssClass="form-control" />
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtCodigo" ErrorMessage="*" CssClass="text-danger" ValidationGroup="Programa" />
                    </div>
                    <div class="mb-3">
                        <label>Nombre *</label>
                        <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" />
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtNombre" ErrorMessage="*" CssClass="text-danger" ValidationGroup="Programa" />
                    </div>
                    <div class="mb-3">
                        <label>Descripción</label>
                        <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" />
                    </div>
                    <div class="mb-3">
                        <label>Versión</label>
                        <asp:TextBox ID="txtVersion" runat="server" CssClass="form-control" />
                    </div>
                    <div class="mb-3">
                        <label>Nivel</label>
                        <asp:DropDownList ID="ddlNivel" runat="server" CssClass="form-select">
                            <asp:ListItem Text="Tecnólogo" Value="Tecnólogo" />
                            <asp:ListItem Text="Técnico" Value="Técnico" />
                            <asp:ListItem Text="Especialización" Value="Especialización" />
                            <asp:ListItem Text="Complementaria" Value="Complementaria" />
                        </asp:DropDownList>
                    </div>
                    <div class="mb-3">
                        <label>Duración (horas)</label>
                        <asp:TextBox ID="txtDuracionHoras" runat="server" CssClass="form-control" TextMode="Number" />
                    </div>
                    <div class="mb-3 form-check">
                        <asp:CheckBox ID="chkEstado" runat="server" CssClass="form-check-input" />
                        <label class="form-check-label">Activo</label>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                    <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-primary" ValidationGroup="Programa" OnClick="btnGuardar_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="server">
    <script>
        function obtenerModalPrograma() {
            const modalElement = document.getElementById('modalPrograma');
            return bootstrap.Modal.getOrCreateInstance(modalElement);
        }

        function mostrarModal() {
            obtenerModalPrograma().show();
        }

        function ocultarModal() {
            obtenerModalPrograma().hide();
        }
    </script>
</asp:Content>
