<%@ Page Title="" Language="C#" MasterPageFile="~/vista/Aprendiz/AprendizMaster.Master" AutoEventWireup="true" CodeBehind="MisPlanes.aspx.cs" Inherits="sistemaGestionPlanesDeMejoramiento.vista.Aprendiz.MisPlanes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="d-flex justify-content-between mb-3">
        <h2><i class="fas fa-clipboard-list"></i>Mis Planes de Mejoramiento</h2>
    </div>
    <div class="card shadow">
        <div class="card-body">
            <asp:GridView ID="gvPlanes" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-hover"
                DataKeyNames="idPlanMejoramiento" OnRowCommand="gvPlanes_RowCommand"
                AllowPaging="True" PageSize="15" OnPageIndexChanging="gvPlanes_PageIndexChanging">
                <Columns>
                    <asp:BoundField DataField="idPlanMejoramiento" HeaderText="ID" />
                    <asp:BoundField DataField="TipoPlan" HeaderText="Tipo" />
                    <asp:BoundField DataField="actividadesPropuestas" HeaderText="Actividades" />
                    <asp:BoundField DataField="fechaAsignacion" HeaderText="Asignación" DataFormatString="{0:dd/MM/yyyy}" />
                    <asp:BoundField DataField="fechaEntrega" HeaderText="Entrega" DataFormatString="{0:dd/MM/yyyy}" />
                    <asp:BoundField DataField="estadoPlan" HeaderText="Estado" />
                    <asp:TemplateField HeaderText="Evidencias">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnSubir" runat="server" CommandName="SubirEvidencia" CommandArgument='<%# Eval("idPlanMejoramiento") %>'
                                CssClass="btn btn-sm btn-primary" CausesValidation="false" Visible='<%# Eval("estadoPlan").ToString() == "Pendiente" %>'>Subir Evidencia</asp:LinkButton>
                            <asp:LinkButton ID="btnVer" runat="server" CommandName="VerEvidencias" CommandArgument='<%# Eval("idPlanMejoramiento") %>'
                                CssClass="btn btn-sm btn-info" CausesValidation="false">Ver Evidencias</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate>
                    <div class="alert alert-info">No tiene planes de mejoramiento asignados.</div>
                </EmptyDataTemplate>
            </asp:GridView>
        </div>
    </div>

    <!-- Modal para subir evidencia -->
    <div class="modal fade" id="modalEvidencia" tabindex="-1">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header bg-primary text-white">
                    <h5 class="modal-title">Subir Evidencia</h5>
                    <button type="button" class="btn-close btn-close-white" data-bs-dismiss="modal"></button>
                </div>
                <div class="modal-body">
                    <asp:HiddenField ID="hfIdPlan" runat="server" />
                    <div class="mb-3">
                        <label>Seleccionar archivo (PDF, DOCX, JPG, PNG, ZIP)</label>
                        <asp:FileUpload ID="fuArchivo" runat="server" CssClass="form-control" />
                        <asp:RequiredFieldValidator ID="rfvArchivo" runat="server" ControlToValidate="fuArchivo" ErrorMessage="Seleccione un archivo." CssClass="text-danger" ValidationGroup="Evidencia" />
                    </div>
                    <div class="mb-3">
                        <label>Observaciones (opcional)</label>
                        <asp:TextBox ID="txtObservaciones" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" />
                    </div>
                    <asp:Label ID="lblError" runat="server" CssClass="alert alert-danger field-alert" />
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                    <asp:Button ID="btnGuardarEvidencia" runat="server" Text="Subir" CssClass="btn btn-primary" OnClick="btnGuardarEvidencia_Click" ValidationGroup="Evidencia" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="server">
    <script>
        function mostrarModal() {
            bootstrap.Modal.getOrCreateInstance(document.getElementById('modalEvidencia')).show();
        }
        function ocultarModal() {
            bootstrap.Modal.getOrCreateInstance(document.getElementById('modalEvidencia')).hide();
        }
    </script>
</asp:Content>
