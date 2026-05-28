<%@ Page Title="" Language="C#" MasterPageFile="~/vista/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="GestionAprendices.aspx.cs" Inherits="sistemaGestionPlanesDeMejoramiento.vista.Admin.GestionAprendices" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="d-flex justify-content-between mb-3">
        <h2><i class="fas fa-users"></i>Aprendices</h2>
        <asp:Button ID="btnNuevo" runat="server" Text="+ Nuevo" CssClass="btn btn-primary" CausesValidation="false" OnClick="btnNuevo_Click" />
        <asp:Button ID="btnCargaMasiva" runat="server" Text="Carga Masiva" CssClass="btn btn-info ms-2" CausesValidation="false" OnClick="btnCargaMasiva_Click" />
    </div>
    <div class="card shadow">
        <div class="card-body">
            <asp:GridView ID="gvAprendices" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-hover"
                DataKeyNames="idAprendiz" OnRowCommand="gvAprendices_RowCommand"
                AllowPaging="True" PageSize="15" OnPageIndexChanging="gvAprendices_PageIndexChanging">
                <Columns>
                    <asp:BoundField DataField="idAprendiz" HeaderText="ID" />
                    <asp:BoundField DataField="nombres" HeaderText="Nombres" />
                    <asp:BoundField DataField="apellidos" HeaderText="Apellidos" />
                    <asp:BoundField DataField="tipoDocumento" HeaderText="Tipo Doc" />
                    <asp:BoundField DataField="numeroDocumento" HeaderText="N° Documento" />
                    <asp:BoundField DataField="correo" HeaderText="Correo" />
                    <asp:BoundField DataField="telefono" HeaderText="Teléfono" />
                    <asp:BoundField DataField="fechaNacimiento" HeaderText="Fecha Nac." DataFormatString="{0:dd/MM/yyyy}" />
                    <asp:TemplateField HeaderText="Acciones" ItemStyle-CssClass="text-center text-nowrap">
                        <ItemTemplate>
                            <div class="d-flex justify-content-center gap-2">
                                <asp:LinkButton runat="server" CommandName="Editar" CommandArgument='<%# Eval("idAprendiz") %>'
                                    CausesValidation="false" CssClass="btn-action btn-action-edit" ToolTip="Editar">
                                    <i class="fas fa-pen"></i>
                                </asp:LinkButton>
                                <asp:LinkButton runat="server" CommandName="Eliminar" CommandArgument='<%# Eval("idAprendiz") %>'
                                    CssClass="btn-action btn-action-delete" ToolTip="Eliminar"
                                    CausesValidation="false" OnClientClick="return confirmarEliminacion(this, 'Desea eliminar este aprendiz?');">
                                    <i class="fas fa-trash-alt"></i>
                                </asp:LinkButton>
                            </div>
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
                        <label>Ficha</label>
                        <asp:DropDownList ID="ddlFicha" runat="server" CssClass="form-select" DataTextField="codigoFicha" DataValueField="idFicha" AppendDataBoundItems="true">
                            <asp:ListItem Text="-- Seleccione ficha --" Value="" />
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvFicha" runat="server" ControlToValidate="ddlFicha" InitialValue="" ErrorMessage="* Seleccione una ficha" CssClass="text-danger" ValidationGroup="Aprendiz" />
                    </div>
                    <!-- Después del campo idFicha (o antes del footer) -->
                    <div class="border-top pt-3 mt-2" id="divCredenciales" runat="server">
                        <h6><i class="fas fa-key"></i>Credenciales de acceso</h6>
                        <div class="row">
                            <div class="col-md-6">
                                <div class="mb-3">
                                    <label>Nombre de Usuario</label>
                                    <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" />
                                    <asp:RequiredFieldValidator ID="rfvUsername" runat="server" ControlToValidate="txtUsername" ErrorMessage="*" CssClass="text-danger" ValidationGroup="Aprendiz" />
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="mb-3">
                                    <label>Contraseña</label>
                                    <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password" />
                                    <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtPassword" ErrorMessage="*" CssClass="text-danger" ValidationGroup="Aprendiz" />
                                </div>
                            </div>
                        </div>
                        <div class="alert alert-info small">Estas credenciales se usarán para que el aprendiz acceda al sistema con rol de Aprendiz.</div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                    <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-primary" ValidationGroup="Aprendiz" OnClick="btnGuardar_Click" />
                </div>
            </div>
        </div>
    </div>
    <!-- Modal para carga masiva -->
    <div class="modal fade" id="modalCargaMasiva" tabindex="-1" aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header bg-info text-white">
                    <h5 class="modal-title">Carga masiva de aprendices</h5>
                    <button type="button" class="btn-close btn-close-white" data-bs-dismiss="modal"></button>
                </div>
                <div class="modal-body">
                    <div class="mb-3">
                        <label>Ficha de formación (aplica a todos los aprendices del archivo)</label>
                        <asp:DropDownList ID="ddlFichaCarga" runat="server" CssClass="form-select" DataTextField="codigoFicha" DataValueField="idFicha" AppendDataBoundItems="true">
                            <asp:ListItem Text="-- Seleccione ficha --" Value="" />
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvFichaCarga" runat="server" ControlToValidate="ddlFichaCarga" InitialValue="" ErrorMessage="* Seleccione una ficha" CssClass="text-danger" ValidationGroup="CargaMasiva" />
                    </div>
                    <div class="mb-3">
                        <label>Archivo Excel (.xlsx)</label>
                        <asp:FileUpload ID="fuExcelCarga" runat="server" CssClass="form-control" accept=".xlsx" />
                        <asp:RequiredFieldValidator ID="rfvArchivoCarga" runat="server" ControlToValidate="fuExcelCarga" ErrorMessage="* Seleccione un archivo" CssClass="text-danger" ValidationGroup="CargaMasiva" />
                    </div>
                    <div class="alert alert-info">
                        <strong>Formato esperado (primer fila encabezados):</strong><br />
                        <code>nombres, apellidos, tipoDocumento, numeroDocumento, correo, telefono, fechaNacimiento (YYYY-MM-DD)</code>
                    </div>
                    <asp:Label ID="lblResumenCarga" runat="server" CssClass="fw-bold" />
                    <asp:GridView ID="gvErroresCarga" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-sm mt-2"
                        EmptyDataText="Sin errores de validación." Visible="false">
                        <Columns>
                            <asp:BoundField DataField="Fila" HeaderText="Fila" />
                            <asp:BoundField DataField="Campo" HeaderText="Campo" />
                            <asp:BoundField DataField="Error" HeaderText="Error" />
                        </Columns>
                    </asp:GridView>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                    <asp:Button ID="btnValidarCarga" runat="server" Text="Validar archivo" CssClass="btn btn-primary" ValidationGroup="CargaMasiva" OnClick="btnValidarCarga_Click" />
                    <asp:Button ID="btnInsertarCarga" runat="server" Text="Insertar aprendices válidos" CssClass="btn btn-success" CausesValidation="false" Enabled="false" OnClick="btnInsertarCarga_Click" />
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

        function obtenerModalCargaMasiva() {
            const modalElement = document.getElementById('modalCargaMasiva');
            return bootstrap.Modal.getOrCreateInstance(modalElement);
        }

        function mostrarModalCargaMasiva() {
            obtenerModalCargaMasiva().show();
        }

        function ocultarModalCargaMasiva() {
            obtenerModalCargaMasiva().hide();
        }

    </script>
</asp:Content>
