<%@ Page Title="" Language="C#" MasterPageFile="~/vista/Instructor/InstructorMaster.Master" AutoEventWireup="true" CodeBehind="MisAprendices.aspx.cs" Inherits="sistemaGestionPlanesDeMejoramiento.vista.Instructor.MisAprendices" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="d-flex justify-content-between mb-3">
        <h2><i class="fas fa-users"></i>Mis Aprendices</h2>
    </div>
    <div class="card shadow">
        <div class="card-body">
            <asp:GridView ID="gvAprendices" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-hover"
                DataKeyNames="idAprendiz" OnRowCommand="gvAprendices_RowCommand"
                AllowPaging="True" PageSize="15" OnPageIndexChanging="gvAprendices_PageIndexChanging">
                <Columns>
                    <asp:BoundField DataField="nombres" HeaderText="Nombres" />
                    <asp:BoundField DataField="apellidos" HeaderText="Apellidos" />
                    <asp:BoundField DataField="correo" HeaderText="Correo" />
                    <asp:BoundField DataField="codigoFicha" HeaderText="Ficha" />
                    <asp:BoundField DataField="estado" HeaderText="Estado" />
                    <asp:TemplateField HeaderText="Acciones">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnCrearPlan" runat="server" CommandName="CrearPlan" CommandArgument='<%# Eval("idAprendiz") %>'
                                CssClass="btn btn-sm btn-primary" CausesValidation="false"
                                Enabled='<%# !EsEstadoCancelado(Eval("estado")) %>'>
                                Crear Plan</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate>
                    <div class="alert alert-info">No tiene aprendices asignados.</div>
                </EmptyDataTemplate>
            </asp:GridView>
        </div>
    </div>

    <!-- Modal para crear plan-->
    <div class="modal fade" id="modalCrearPlan" tabindex="-1">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header bg-primary text-white">
                    <h5 class="modal-title">Crear Plan de Mejoramiento</h5>
                    <button type="button" class="btn-close btn-close-white" data-bs-dismiss="modal"></button>
                </div>
                <div class="modal-body">
                    <asp:HiddenField ID="hfIdAprendiz" runat="server" />
                    <div class="mb-3">
                        <label>Resultados incumplidos *</label>
                        <asp:CheckBoxList ID="cblResultados" runat="server" CssClass="list-unstyled" DataTextField="descripcion" DataValueField="idResultado" />
                    </div>
                    <div class="mb-3">
                        <label>Actividades propuestas *</label>
                        <asp:TextBox ID="txtActividades" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="4" />
                    </div>
                    <div class="mb-3">
                        <label>Observaciones</label>
                        <asp:TextBox ID="txtObservaciones" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" />
                    </div>
                    <div class="mb-3">
                        <label>Fecha límite *</label>
                        <asp:TextBox ID="txtFechaEntrega" runat="server" CssClass="form-control" TextMode="Date" />
                    </div>
                    <asp:Label ID="lblError" runat="server" CssClass="alert alert-danger field-alert" />
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                    <asp:Button ID="btnGuardarPlan" runat="server" Text="Guardar Plan" CssClass="btn btn-primary" OnClick="btnGuardarPlan_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="server">
    <script>
        function obtenerModalCrearPlan() {
            return bootstrap.Modal.getOrCreateInstance(document.getElementById('modalCrearPlan'));
        }

        function mostrarModal() {
            var modal = obtenerModalCrearPlan();
            modal.show();
        }
        function ocultarModal() {
            var modal = bootstrap.Modal.getInstance(document.getElementById('modalCrearPlan'));
            if (modal) modal.hide();
        }
    </script>
</asp:Content>
