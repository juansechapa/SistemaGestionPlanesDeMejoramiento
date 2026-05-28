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
                DataKeyNames="idPrograma" OnRowCommand="gvProgramas_RowCommand"
                AllowPaging="True" PageSize="15" OnPageIndexChanging="gvProgramas_PageIndexChanging">
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
                    <asp:TemplateField HeaderText="Acciones" ItemStyle-CssClass="text-center text-nowrap">
                        <ItemTemplate>
                            <div class="d-flex justify-content-center gap-2">
                                <asp:LinkButton ID="btnEditar" runat="server" CommandName="Editar" CommandArgument='<%# Eval("idPrograma") %>'
                                    CssClass="btn-action btn-action-edit" ToolTip="Editar" CausesValidation="false">
                                    <i class="fas fa-pen"></i>
                                </asp:LinkButton>
                                <asp:LinkButton ID="btnCompetencias" runat="server" CommandName="Competencias" CommandArgument='<%# Eval("idPrograma") %>'
                                    CssClass="btn-action btn-action-view" ToolTip="Competencias y resultados" CausesValidation="false">
                                    <i class="fas fa-list"></i>
                                </asp:LinkButton>
                                <asp:LinkButton ID="btnEliminar" runat="server" CommandName="Eliminar" CommandArgument='<%# Eval("idPrograma") %>'
                                    CssClass="btn-action btn-action-delete" ToolTip="Eliminar"
                                    CausesValidation="false" OnClientClick="return confirmarEliminacion(this, 'Desea eliminar este programa?');">
                                    <i class="fas fa-trash-alt"></i>
                                </asp:LinkButton>
                            </div>
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

    <asp:HiddenField ID="hfIdProgramaCompetencias" runat="server" />
    <asp:HiddenField ID="hfIdCompetenciaSeleccionada" runat="server" />
    <div class="modal fade" id="modalCompetencias" tabindex="-1" aria-hidden="true">
        <div class="modal-dialog modal-xl">
            <div class="modal-content">
                <div class="modal-header bg-primary text-white">
                    <h5 class="modal-title">Competencias y resultados - <asp:Literal ID="litProgramaCompetencias" runat="server" /></h5>
                    <button type="button" class="btn-close btn-close-white" data-bs-dismiss="modal"></button>
                </div>
                <div class="modal-body">
                    <asp:Label ID="lblCompetenciasMensaje" runat="server" CssClass="d-block mb-3" />
                    <div class="row">
                        <div class="col-md-5">
                            <h5>Competencias</h5>
                            <asp:GridView ID="gvCompetencias" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-hover"
                                OnRowCommand="gvCompetencias_RowCommand">
                                <Columns>
                                    <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                                    <asp:BoundField DataField="descripcion" HeaderText="Descripción" />
                                    <asp:TemplateField HeaderText="Acciones">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnVerResultados" runat="server" CommandName="Resultados" CommandArgument='<%# Eval("idCompetencias") %>'
                                                CssClass="btn btn-sm btn-info" CausesValidation="false">Resultados</asp:LinkButton>
                                            <asp:LinkButton ID="btnEliminarCompetencia" runat="server" CommandName="EliminarCompetencia" CommandArgument='<%# Eval("idCompetencias") %>'
                                                CssClass="btn btn-sm btn-danger" CausesValidation="false" OnClientClick="return confirmarEliminacion(this, 'Desea eliminar esta competencia y sus resultados?');">Eliminar</asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>
                                    <div class="alert alert-info">Este programa no tiene competencias registradas.</div>
                                </EmptyDataTemplate>
                            </asp:GridView>
                            <div class="border-top pt-3">
                                <div class="mb-3">
                                    <label>Nombre competencia *</label>
                                    <asp:TextBox ID="txtNombreCompetencia" runat="server" CssClass="form-control" />
                                </div>
                                <div class="mb-3">
                                    <label>Descripción</label>
                                    <asp:TextBox ID="txtDescripcionCompetencia" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="2" />
                                </div>
                                <asp:Button ID="btnAgregarCompetencia" runat="server" Text="Agregar competencia" CssClass="btn btn-primary" CausesValidation="false" OnClick="btnAgregarCompetencia_Click" />
                            </div>
                        </div>
                        <div class="col-md-7">
                            <h5>Resultados de aprendizaje</h5>
                            <asp:Label ID="lblCompetenciaSeleccionada" runat="server" CssClass="text-muted d-block mb-2" />
                            <asp:GridView ID="gvResultados" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-hover"
                                OnRowCommand="gvResultados_RowCommand">
                                <Columns>
                                    <asp:BoundField DataField="codigo" HeaderText="Código" />
                                    <asp:BoundField DataField="descripcion" HeaderText="Descripción" />
                                    <asp:BoundField DataField="estado" HeaderText="Estado" />
                                    <asp:TemplateField HeaderText="Acciones">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnEliminarResultado" runat="server" CommandName="EliminarResultado" CommandArgument='<%# Eval("idResultado") %>'
                                                CssClass="btn btn-sm btn-danger" CausesValidation="false" OnClientClick="return confirmarEliminacion(this, 'Desea eliminar este resultado?');">Eliminar</asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>
                                    <div class="alert alert-info">Seleccione una competencia o agregue resultados.</div>
                                </EmptyDataTemplate>
                            </asp:GridView>
                            <div class="border-top pt-3">
                                <div class="row">
                                    <div class="col-md-4 mb-3">
                                        <label>Código *</label>
                                        <asp:TextBox ID="txtCodigoResultado" runat="server" CssClass="form-control" />
                                    </div>
                                    <div class="col-md-8 mb-3">
                                        <label>Descripción *</label>
                                        <asp:TextBox ID="txtDescripcionResultado" runat="server" CssClass="form-control" />
                                    </div>
                                </div>
                                <asp:Button ID="btnAgregarResultado" runat="server" Text="Agregar resultado" CssClass="btn btn-primary" CausesValidation="false" OnClick="btnAgregarResultado_Click" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>
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

        function mostrarModalCompetencias() {
            bootstrap.Modal.getOrCreateInstance(document.getElementById('modalCompetencias')).show();
        }
    </script>
</asp:Content>
