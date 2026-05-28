<%@ Page Title="Gestión de Centros" Language="C#" MasterPageFile="~/vista/Admin/AdminMaster.Master"
    AutoEventWireup="true" CodeBehind="GestionCentros.aspx.cs"
    Inherits="sistemaGestionPlanesDeMejoramiento.vista.Admin.GestionCentros" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <!-- Encabezado de página con estilo uniforme -->
    <div class="page-header">
        <h2><i class="fas fa-building"></i> Centros de Formación</h2>
        <asp:Button ID="btnNuevo" runat="server" Text="+ Nuevo Centro"
            CssClass="btn btn-primary" CausesValidation="false" OnClick="btnNuevo_Click" />
    </div>

    <!-- Mensaje de sistema (éxito/error) con estilo de alerta claro -->
    <asp:Label ID="lblMensaje" runat="server" CssClass="d-block mb-3" />

    <div class="card">
        <div class="card-body p-0">
            <div class="table-responsive">
                <asp:GridView ID="gvCentros" runat="server" AutoGenerateColumns="False"
                    CssClass="table align-middle"
                    DataKeyNames="idCentro"
                    OnRowCommand="gvCentros_RowCommand"
                    OnRowDataBound="gvCentros_RowDataBound"
                    AllowPaging="True" PageSize="15"
                    OnPageIndexChanging="gvCentros_PageIndexChanging"
                    GridLines="None">
                    <Columns>
                        <asp:BoundField DataField="idCentro" HeaderText="ID"
                            ItemStyle-CssClass="text-muted font-monospace small fw-bold" />
                        <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                        <asp:BoundField DataField="direccion" HeaderText="Dirección" />
                        <asp:BoundField DataField="telefono" HeaderText="Teléfono" />

                        <%-- Estado con badge estilizado --%>
                        <asp:TemplateField HeaderText="Estado" ItemStyle-CssClass="text-center">
                            <ItemTemplate>
                                <asp:Label ID="lblEstado" runat="server"
                                    Text='<%# Convert.ToBoolean(Eval("estado")) ? "Activo" : "Inactivo" %>'
                                    CssClass='<%# Convert.ToBoolean(Eval("estado")) ? "badge-soft-primary" : "badge bg-light text-muted border" %>' />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <%-- Acciones con botones claros y visibles --%>
                        <asp:TemplateField HeaderText="Acciones" ItemStyle-CssClass="text-center text-nowrap">
                            <ItemTemplate>
                                <div class="d-flex justify-content-center gap-2">
                                    <asp:LinkButton ID="btnEditar" runat="server"
                                        CommandName="Editar"
                                        CommandArgument='<%# Eval("idCentro") %>'
                                        CssClass="btn-action btn-action-edit"
                                        ToolTip="Editar centro"
                                        CausesValidation="false">
                                        <i class="fas fa-pen"></i>
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="btnEliminar" runat="server"
                                        CommandName="Eliminar"
                                        CommandArgument='<%# Eval("idCentro") %>'
                                        CssClass="btn-action btn-action-delete"
                                        ToolTip="Eliminar centro"
                                        CausesValidation="false"
                                        OnClientClick="return confirmarEliminacion(this, 'Seguro que desea eliminar este centro?');">
                                        <i class="fas fa-trash-alt"></i>
                                    </asp:LinkButton>
                                </div>
                            </ItemTemplate>
                            <ItemStyle Width="100px" />
                        </asp:TemplateField>
                    </Columns>

                    <PagerStyle CssClass="pagination" />

                    <EmptyDataTemplate>
                        <div class="empty-state">
                            <i class="fas fa-building"></i>
                            <p>No hay centros registrados.</p>
                        </div>
                    </EmptyDataTemplate>
                </asp:GridView>
            </div>
        </div>
    </div>

    <asp:HiddenField ID="hfIdCentro" runat="server" />

    <!-- Modal Agregar/Editar (diseño claro unificado) -->
    <div class="modal fade" id="modalCentro" tabindex="-1" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">
                        <i class="fas fa-building me-2 text-primary"></i>
                        <asp:Literal ID="litModalTitulo" runat="server" Text="Centro de Formación" />
                    </h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Cerrar"></button>
                </div>
                <div class="modal-body">
                    <div class="mb-3">
                        <label>Nombre *</label>
                        <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" placeholder="Nombre del centro" />
                        <asp:RequiredFieldValidator ID="rfvNombre" runat="server"
                            ControlToValidate="txtNombre"
                            ErrorMessage="* Campo obligatorio"
                            CssClass="text-danger small mt-1 d-block"
                            ValidationGroup="Centro" />
                    </div>
                    <div class="mb-3">
                        <label>Dirección</label>
                        <asp:TextBox ID="txtDireccion" runat="server" CssClass="form-control" placeholder="Dirección física" />
                    </div>
                    <div class="mb-3">
                        <label>Teléfono</label>
                        <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control" placeholder="Número de contacto" />
                    </div>
                    <div class="mb-3 form-check">
                        <asp:CheckBox ID="chkEstado" runat="server" CssClass="form-check-input" Checked="true" />
                        <label class="form-check-label" for="<%= chkEstado.ClientID %>">¿Centro activo?</label>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                    <asp:Button ID="btnGuardar" runat="server" Text="Guardar Cambios"
                        CssClass="btn btn-primary" ValidationGroup="Centro"
                        OnClick="btnGuardar_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="server">
    <script>
        function obtenerModalCentro() {
            return bootstrap.Modal.getOrCreateInstance(document.getElementById('modalCentro'));
        }
        function mostrarModal() {
            obtenerModalCentro().show();
        }
        function ocultarModal() {
            obtenerModalCentro().hide();
        }
    </script>
</asp:Content>
