<%@ Page Title="Gestión de Planes" Language="C#" MasterPageFile="~/vista/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="GestionPlanes.aspx.cs" Inherits="sistemaGestionPlanesDeMejoramiento.vista.Admin.GestionPlanes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="d-flex justify-content-between align-items-center mb-3">
        <h2><i class="fas fa-clipboard-list"></i>Planes de Mejoramiento</h2>
        <asp:Button ID="btnNuevo" runat="server" Text="+ Nuevo Plan" CssClass="btn btn-primary" CausesValidation="false" OnClick="btnNuevo_Click" />
    </div>

    <asp:Label ID="lblMensaje" runat="server" CssClass="d-block mb-3" />

    <div class="card shadow">
        <div class="card-body">
            <asp:GridView ID="gvPlanes" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-hover"
                DataKeyNames="idPlanMejoramiento" OnRowCommand="gvPlanes_RowCommand"
                AllowPaging="True" PageSize="15" OnPageIndexChanging="gvPlanes_PageIndexChanging">
                <Columns>
                    <asp:BoundField DataField="idPlanMejoramiento" HeaderText="ID" />
                    <asp:BoundField DataField="NombreAprendiz" HeaderText="Aprendiz" />
                    <asp:BoundField DataField="NombreInstructor" HeaderText="Instructor" />
                    <asp:BoundField DataField="TipoPlan" HeaderText="Tipo" />
                    <asp:BoundField DataField="actividadesPropuestas" HeaderText="Actividades" />
                    <asp:BoundField DataField="observaciones" HeaderText="Observaciones" />
                    <asp:BoundField DataField="fechaAsignacion" HeaderText="Asignación" DataFormatString="{0:dd/MM/yyyy}" />
                    <asp:BoundField DataField="fechaEntrega" HeaderText="Entrega" DataFormatString="{0:dd/MM/yyyy}" />
                    <asp:TemplateField HeaderText="Estado">
                        <ItemTemplate>
                            <asp:Label ID="lblEstadoPlan" runat="server"
                                Text='<%# Eval("estadoPlan") %>'
                                CssClass='<%# ObtenerClaseEstado(Eval("estadoPlan")) %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Acciones" ItemStyle-CssClass="text-center text-nowrap">
                        <ItemTemplate>
                            <div class="d-flex justify-content-center gap-2">
                                <asp:LinkButton ID="btnEditar" runat="server" CommandName="Editar" CommandArgument='<%# Eval("idPlanMejoramiento") %>'
                                    CssClass="btn-action btn-action-edit" ToolTip="Editar" CausesValidation="false">
                                    <i class="fas fa-pen"></i>
                                </asp:LinkButton>
                                <asp:LinkButton ID="btnEliminar" runat="server" CommandName="Eliminar" CommandArgument='<%# Eval("idPlanMejoramiento") %>'
                                    CssClass="btn-action btn-action-delete" ToolTip="Eliminar"
                                    CausesValidation="false" OnClientClick="return confirmarEliminacion(this, 'Desea eliminar este plan de mejoramiento?');">
                                    <i class="fas fa-trash-alt"></i>
                                </asp:LinkButton>
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate>
                    <div class="alert alert-info">No hay planes registrados.</div>
                </EmptyDataTemplate>
            </asp:GridView>
        </div>
    </div>

    <asp:HiddenField ID="hfIdPlan" runat="server" />
    <div class="modal fade" id="modalPlan" tabindex="-1" aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header bg-primary text-white">
                    <h5 class="modal-title"><asp:Literal ID="litModalTitulo" runat="server" /></h5>
                    <button type="button" class="btn-close btn-close-white" data-bs-dismiss="modal"></button>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="mb-3">
                                <label>Aprendiz *</label>
                                <asp:DropDownList ID="ddlAprendiz" runat="server" CssClass="form-select" />
                                <asp:RequiredFieldValidator ID="rfvAprendiz" runat="server" ControlToValidate="ddlAprendiz" InitialValue="" ErrorMessage="Seleccione un aprendiz." CssClass="text-danger" ValidationGroup="Plan" />
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="mb-3">
                                <label>Instructor *</label>
                                <asp:DropDownList ID="ddlInstructor" runat="server" CssClass="form-select" />
                                <asp:RequiredFieldValidator ID="rfvInstructor" runat="server" ControlToValidate="ddlInstructor" InitialValue="" ErrorMessage="Seleccione un instructor." CssClass="text-danger" ValidationGroup="Plan" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            <div class="mb-3">
                                <label>Tipo *</label>
                                <asp:DropDownList ID="ddlTipoPlan" runat="server" CssClass="form-select">
                                    <asp:ListItem Text="Interno" Value="Interno" />
                                    <asp:ListItem Text="Comité" Value="Comité" />
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="mb-3">
                                <label>Fecha asignación *</label>
                                <asp:TextBox ID="txtFechaAsignacion" runat="server" CssClass="form-control" TextMode="Date" />
                                <asp:RequiredFieldValidator ID="rfvFechaAsignacion" runat="server" ControlToValidate="txtFechaAsignacion" ErrorMessage="Obligatoria." CssClass="text-danger" ValidationGroup="Plan" />
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="mb-3">
                                <label>Fecha entrega *</label>
                                <asp:TextBox ID="txtFechaEntrega" runat="server" CssClass="form-control" TextMode="Date" />
                                <asp:RequiredFieldValidator ID="rfvFechaEntrega" runat="server" ControlToValidate="txtFechaEntrega" ErrorMessage="Obligatoria." CssClass="text-danger" ValidationGroup="Plan" />
                            </div>
                        </div>
                    </div>
                    <div class="mb-3">
                        <label>Actividades propuestas *</label>
                        <asp:TextBox ID="txtActividades" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="4" />
                        <asp:RequiredFieldValidator ID="rfvActividades" runat="server" ControlToValidate="txtActividades" ErrorMessage="Obligatorias." CssClass="text-danger" ValidationGroup="Plan" />
                    </div>
                    <div class="mb-3">
                        <label>Observaciones</label>
                        <asp:TextBox ID="txtObservaciones" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" />
                    </div>
                    <div class="mb-3">
                        <label>Estado *</label>
                        <asp:DropDownList ID="ddlEstadoPlan" runat="server" CssClass="form-select">
                            <asp:ListItem Text="Pendiente" Value="Pendiente" />
                            <asp:ListItem Text="Aprobado" Value="Aprobado" />
                            <asp:ListItem Text="No Aprobado" Value="No Aprobado" />
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                    <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-primary" ValidationGroup="Plan" OnClick="btnGuardar_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="server">
    <script>
        function obtenerModalPlan() {
            return bootstrap.Modal.getOrCreateInstance(document.getElementById('modalPlan'));
        }

        function mostrarModalPlan() {
            obtenerModalPlan().show();
        }

        function ocultarModalPlan() {
            obtenerModalPlan().hide();
        }
    </script>
</asp:Content>
