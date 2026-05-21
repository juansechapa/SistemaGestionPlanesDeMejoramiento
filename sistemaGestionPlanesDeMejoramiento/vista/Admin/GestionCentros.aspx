<%@ Page Title="" Language="C#" MasterPageFile="~/vista/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="GestionCentros.aspx.cs" Inherits="sistemaGestionPlanesDeMejoramiento.vista.Admin.GestionCentros" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="d-flex justify-content-between align-items-center mb-3">
        <h2><i class="fas fa-building"></i>Centros de Formación</h2>
        <asp:Button ID="btnNuevo" runat="server" Text="+ Nuevo Centro" CssClass="btn btn-primary" CausesValidation="false" OnClick="btnNuevo_Click" />
    </div>

    <div class="card shadow">
        <div class="card-body">
            <asp:GridView ID="gvCentros" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-hover"
                DataKeyNames="idCentro" OnRowCommand="gvCentros_RowCommand" OnRowDataBound="gvCentros_RowDataBound">
                <Columns>
                    <asp:BoundField DataField="idCentro" HeaderText="ID" ReadOnly="True" />
                    <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                    <asp:BoundField DataField="direccion" HeaderText="Dirección" />
                    <asp:BoundField DataField="telefono" HeaderText="Teléfono" />
                    <asp:TemplateField HeaderText="Estado">
                        <ItemTemplate>
                            <asp:Label ID="lblEstado" runat="server" Text='<%# Convert.ToBoolean(Eval("estado")) ? "Activo" : "Inactivo" %>' CssClass='<%# Convert.ToBoolean(Eval("estado")) ? "badge bg-success" : "badge bg-secondary" %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Acciones">
                        <ItemTemplate>
                            <asp:ImageButton ID="btnEditar" runat="server" CommandName="Editar" CommandArgument='<%# Eval("idCentro")%>' ImageUrl="https://img.icons8.com/ios-glyphs/20/000000/edit--v1.png"
                                ToolTip="Editar" CausesValidation="false" />
                            <asp:ImageButton ID="btnEliminar" runat="server" CommandName="Eliminar" CommandArgument='<%# Eval("idCentro") %>' ImageUrl="https://img.icons8.com/ios-glyphs/20/000000/trash.png" ToolTip="Eliminar" CausesValidation="false" OnClientClick="return confirm('¿Está seguro de eliminar este centro?');" />
                        </ItemTemplate>
                        <ItemStyle Width="80px" />
                    </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate>
                    <div class="alert alert-info">No hay centros registrados.</div>
                </EmptyDataTemplate>
            </asp:GridView>
        </div>
    </div>

    <!-- Modal agregar/editar -->
    <asp:HiddenField ID="hfIdCentro" runat="server" />
    <div class="modal fade" id="modalCentro" tabindex="-1" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header bg-primary text-white">
                    <h5 class="modal-title">
                        <asp:Literal ID="litModalTitulo" runat="server" Text="Centro de Formación" /></h5>
                    <button type="button" class="btn-close btn-close-white" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <div class="mb-3">
                        <label>Nombre</label>
                        <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" />
                        <asp:RequiredFieldValidator ID="rfvNombre" runat="server" ControlToValidate="txtNombre" ErrorMessage="* Obligatorio" CssClass="text-danger" ValidationGroup="Centro" />
                    </div>
                    <div class="mb-3">
                        <label>Dirección</label>
                        <asp:TextBox ID="txtDireccion" runat="server" CssClass="form-control" />
                    </div>
                    <div class="mb-3">
                        <label>Teléfono</label>
                        <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control" />
                    </div>
                    <div class="mb-3 form-check">
                        <asp:CheckBox ID="chkEstado" runat="server" CssClass="form-check-input" Checked="true" />
                        <label class="form-check-label">Activo</label>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                    <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-primary" ValidationGroup="Centro" OnClick="btnGuardar_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="server">
    <script>
        function obtenerModalCentro() {
            const modalElement = document.getElementById('modalCentro');
            return bootstrap.Modal.getOrCreateInstance(modalElement);
        }

        function mostrarModal() {
            obtenerModalCentro().show();
        }

        function ocultarModal() {
            obtenerModalCentro().hide();
        }
    </script>
</asp:Content>
