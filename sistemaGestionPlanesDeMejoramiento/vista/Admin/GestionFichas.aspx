<%@ Page Title="" Language="C#" MasterPageFile="~/vista/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="GestionFichas.aspx.cs" Inherits="sistemaGestionPlanesDeMejoramiento.vista.Admin.GestionFichas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="d-flex justify-content-between align-items-center mb-3">
        <h2><i class="fas fa-id-card"></i>Fichas de Formación</h2>
        <asp:Button ID="btnNuevo" runat="server" Text="+ Nueva Ficha" CssClass="btn btn-primary" CausesValidation="false" OnClick="btnNuevo_Click" />
    </div>

    <div class="card shadow">
        <div class="card-body">
            <asp:GridView ID="gvFichas" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-hover"
                DataKeyNames="idFicha" OnRowCommand="gvFichas_RowCommand"
                AllowPaging="True" PageSize="15" OnPageIndexChanging="gvFichas_PageIndexChanging">
                <columns>
                    <asp:BoundField DataField="idFicha" HeaderText="ID" />
                    <asp:BoundField DataField="codigoFicha" HeaderText="Código Ficha" />
                    <asp:BoundField DataField="fechaInicio" HeaderText="Fecha Inicio" DataFormatString="{0:dd/MM/yyyy}" />
                    <asp:BoundField DataField="fechaFinalizacion" HeaderText="Fecha Fin" DataFormatString="{0:dd/MM/yyyy}" />
                    <asp:BoundField DataField="jornada" HeaderText="Jornada" />
                    <asp:BoundField DataField="nivel" HeaderText="Nivel" />
                    <asp:BoundField DataField="duracion" HeaderText="Duración" />
                    <asp:BoundField DataField="estado" HeaderText="Estado" />
                    <asp:TemplateField HeaderText="Acciones" ItemStyle-CssClass="text-center text-nowrap">
                        <itemtemplate>
                            <div class="d-flex justify-content-center gap-2">
                                <asp:LinkButton ID="btnEditar" runat="server" CommandName="Editar" CommandArgument='<%# Eval("idFicha") %>'
                                    CssClass="btn-action btn-action-edit" ToolTip="Editar" CausesValidation="false">
                                    <i class="fas fa-pen"></i>
                                </asp:LinkButton>
                                <asp:LinkButton ID="btnEliminar" runat="server" CommandName="Eliminar" CommandArgument='<%# Eval("idFicha") %>'
                                    CssClass="btn-action btn-action-delete" ToolTip="Eliminar" CausesValidation="false"
                                    OnClientClick="return confirmarEliminacion(this, 'Desea eliminar esta ficha?');">
                                    <i class="fas fa-trash-alt"></i>
                                </asp:LinkButton>
                            </div>
                        </itemtemplate>
                    </asp:TemplateField>
                </columns>
                <emptydatatemplate>
                    <div class="alert alert-info">No hay fichas registradas.</div>
                </emptydatatemplate>
            </asp:GridView>
        </div>
    </div>

    <!-- Modal agregar/editar -->
    <asp:HiddenField ID="hfIdFicha" runat="server" />
    <div class="modal fade" id="modalFicha" tabindex="-1" aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header bg-primary text-white">
                    <h5 class="modal-title">
                        <asp:Literal ID="litTitulo" runat="server" /></h5>
                    <button type="button" class="btn-close btn-close-white" data-bs-dismiss="modal"></button>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="mb-3">
                                <label>Código Ficha *</label>
                                <asp:TextBox ID="txtCodigoFicha" runat="server" CssClass="form-control" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtCodigoFicha" ErrorMessage="*" CssClass="text-danger" ValidationGroup="Ficha" />
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="mb-3">
                                <label>Programa / Centro *</label>
                                <asp:DropDownList ID="ddlCentroPrograma" runat="server" CssClass="form-select" DataTextField="Descripcion" DataValueField="idCentroPrograma" AppendDataBoundItems="true">
                                    <asp:ListItem Text="-- Seleccione --" Value="" />
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlCentroPrograma" InitialValue="" ErrorMessage="*" CssClass="text-danger" ValidationGroup="Ficha" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="mb-3">
                                <label>Fecha Inicio *</label>
                                <asp:TextBox ID="txtFechaInicio" runat="server" CssClass="form-control" TextMode="Date" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtFechaInicio" ErrorMessage="*" CssClass="text-danger" ValidationGroup="Ficha" />
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="mb-3">
                                <label>Fecha Finalización *</label>
                                <asp:TextBox ID="txtFechaFin" runat="server" CssClass="form-control" TextMode="Date" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtFechaFin" ErrorMessage="*" CssClass="text-danger" ValidationGroup="Ficha" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            <div class="mb-3">
                                <label>Jornada</label>
                                <asp:DropDownList ID="ddlJornada" runat="server" CssClass="form-select">
                                    <asp:ListItem Text="Mañana" Value="Mañana" />
                                    <asp:ListItem Text="Tarde" Value="Tarde" />
                                    <asp:ListItem Text="Noche" Value="Noche" />
                                    <asp:ListItem Text="Fin de semana" Value="Fin de semana" />
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="mb-3">
                                <label>Nivel</label>
                                <asp:DropDownList ID="ddlNivel" runat="server" CssClass="form-select">
                                    <asp:ListItem Text="Tecnólogo" Value="Tecnólogo" />
                                    <asp:ListItem Text="Técnico" Value="Técnico" />
                                    <asp:ListItem Text="Especialización" Value="Especialización" />
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="mb-3">
                                <label>Duración</label>
                                <asp:TextBox ID="txtDuracion" runat="server" CssClass="form-control" placeholder="Ej: 24 meses" />
                            </div>
                        </div>
                    </div>
                    <div class="mb-3">
                        <label>Estado *</label>
                        <asp:DropDownList ID="ddlEstado" runat="server" CssClass="form-select">
                            <asp:ListItem Text="Activa" Value="Activa" />
                            <asp:ListItem Text="Inactiva" Value="Inactiva" />
                            <asp:ListItem Text="Finalizada" Value="Finalizada" />
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlEstado" InitialValue="" ErrorMessage="*" CssClass="text-danger" ValidationGroup="Ficha" />
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                    <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-primary" ValidationGroup="Ficha" OnClick="btnGuardar_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="server">
    <script>
        function mostrarModal() {
            new bootstrap.Modal(document.getElementById('modalFicha')).show();
        }
        function ocultarModal() {
            var modal = bootstrap.Modal.getInstance(document.getElementById('modalFicha'));
            if (modal) modal.hide();
        }
    </script>
</asp:Content>
