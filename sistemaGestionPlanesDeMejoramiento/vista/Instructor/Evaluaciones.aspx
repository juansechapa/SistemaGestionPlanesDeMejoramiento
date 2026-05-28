<%@ Page Title="" Language="C#" MasterPageFile="~/vista/Instructor/InstructorMaster.Master" AutoEventWireup="true" CodeBehind="Evaluaciones.aspx.cs" Inherits="sistemaGestionPlanesDeMejoramiento.vista.Instructor.Evaluaciones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel ID="pnlListado" runat="server">
        <div class="d-flex justify-content-between mb-3">
            <h2><i class="fas fa-check-circle"></i>Evaluaciones</h2>
        </div>
        <div class="card shadow">
            <div class="card-body">
                <asp:GridView ID="gvPlanesEvaluacion" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-hover"
                    OnRowCommand="gvPlanesEvaluacion_RowCommand"
                    AllowPaging="True" PageSize="15" OnPageIndexChanging="gvPlanesEvaluacion_PageIndexChanging">
                    <Columns>
                        <asp:BoundField DataField="idPlanMejoramiento" HeaderText="ID" />
                        <asp:BoundField DataField="NombreAprendiz" HeaderText="Aprendiz" />
                        <asp:BoundField DataField="TipoPlan" HeaderText="Tipo" />
                        <asp:BoundField DataField="actividadesPropuestas" HeaderText="Actividades" />
                        <asp:BoundField DataField="fechaEntrega" HeaderText="Entrega" DataFormatString="{0:dd/MM/yyyy}" />
                        <asp:BoundField DataField="estadoPlan" HeaderText="Estado" />
                        <asp:TemplateField HeaderText="Acciones">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnEvaluar" runat="server" CommandName="Evaluar" CommandArgument='<%# Eval("idPlanMejoramiento") %>'
                                    CssClass="btn btn-sm btn-warning" CausesValidation="false" Visible='<%# Eval("estadoPlan").ToString() == "Pendiente" %>'>
                                    Evaluar
                                </asp:LinkButton>
                                <asp:LinkButton ID="btnVerEvidencias" runat="server" CommandName="VerEvidencias" CommandArgument='<%# Eval("idPlanMejoramiento") %>'
                                    CssClass="btn btn-sm btn-info" CausesValidation="false">
                                    Evidencias
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        <div class="alert alert-info">No hay planes para evaluar.</div>
                    </EmptyDataTemplate>
                </asp:GridView>
            </div>
        </div>
    </asp:Panel>

    <asp:Panel ID="pnlDetalle" runat="server" Visible="false">
        <div class="d-flex justify-content-between mb-3">
            <h2><i class="fas fa-check-double"></i>Evaluar Plan de Mejoramiento</h2>
            <asp:HyperLink ID="btnVolver" runat="server" NavigateUrl="~/vista/Instructor/Evaluaciones.aspx" CssClass="btn btn-secondary">Volver</asp:HyperLink>
        </div>
        <div class="card shadow mb-3">
            <div class="card-body">
                <asp:HiddenField ID="hfIdPlan" runat="server" />
                <div class="row">
                    <div class="col-md-6 mb-3">
                        <label>Aprendiz</label>
                        <asp:Label ID="lblAprendiz" runat="server" CssClass="form-control-plaintext" />
                    </div>
                    <div class="col-md-3 mb-3">
                        <label>Estado</label>
                        <asp:Label ID="lblEstadoPlan" runat="server" CssClass="form-control-plaintext" />
                    </div>
                    <div class="col-md-3 mb-3">
                        <label>Fecha entrega</label>
                        <asp:Label ID="lblFechaEntrega" runat="server" CssClass="form-control-plaintext" />
                    </div>
                </div>
                <div class="mb-3">
                    <label>Actividades propuestas</label>
                    <asp:Label ID="lblActividades" runat="server" CssClass="form-control-plaintext" />
                </div>
                <div class="mb-3">
                    <label>Observaciones del plan</label>
                    <asp:Label ID="lblObservaciones" runat="server" CssClass="form-control-plaintext" />
                </div>
                <hr />
                <h5>Criterios de evaluacion</h5>
                <div class="row">
                    <div class="col-md-4 mb-3">
                        <label>Producto</label>
                        <asp:DropDownList ID="ddlProducto" runat="server" CssClass="form-select">
                            <asp:ListItem Value="Aprueba">Aprueba</asp:ListItem>
                            <asp:ListItem Value="No Aprueba">No Aprueba</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-4 mb-3">
                        <label>Conocimiento</label>
                        <asp:DropDownList ID="ddlConocimiento" runat="server" CssClass="form-select">
                            <asp:ListItem Value="Aprueba">Aprueba</asp:ListItem>
                            <asp:ListItem Value="No Aprueba">No Aprueba</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-4 mb-3">
                        <label>Desempeno</label>
                        <asp:DropDownList ID="ddlDesempeno" runat="server" CssClass="form-select">
                            <asp:ListItem Value="Aprueba">Aprueba</asp:ListItem>
                            <asp:ListItem Value="No Aprueba">No Aprueba</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="mb-3">
                    <label>Observaciones de la evaluacion</label>
                    <asp:TextBox ID="txtObservacionesEval" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" />
                </div>
                <asp:Button ID="btnGuardar" runat="server" Text="Guardar Evaluacion" CssClass="btn btn-primary" OnClick="btnGuardar_Click" />
                <asp:Label ID="lblMensaje" runat="server" CssClass="message-label mt-3" />
            </div>
        </div>

        <div class="card shadow">
            <div class="card-header">
                <h5 class="mb-0"><i class="fas fa-paperclip"></i>Evidencias del aprendiz</h5>
            </div>
            <div class="card-body">
                <asp:Label ID="lblMensajeEvidencias" runat="server" CssClass="alert alert-danger message-label" />
                <asp:GridView ID="gvEvidencias" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-hover"
                    OnRowCommand="gvEvidencias_RowCommand"
                    AllowPaging="True" PageSize="10" OnPageIndexChanging="gvEvidencias_PageIndexChanging">
                    <Columns>
                        <asp:BoundField DataField="nombreArchivo" HeaderText="Archivo" />
                        <asp:BoundField DataField="tipoArchivo" HeaderText="Tipo" />
                        <asp:BoundField DataField="fechaSubida" HeaderText="Subida" DataFormatString="{0:dd/MM/yyyy HH:mm}" />
                        <asp:BoundField DataField="observaciones" HeaderText="Observaciones" />
                        <asp:TemplateField HeaderText="Descargar">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnDescargar" runat="server" CommandName="Descargar" CommandArgument='<%# Eval("idEvidencia") %>'
                                    CssClass="btn btn-sm btn-success" CausesValidation="false">
                                    Descargar
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        <div class="alert alert-info">Este plan todavia no tiene evidencias subidas.</div>
                    </EmptyDataTemplate>
                </asp:GridView>
            </div>
        </div>
    </asp:Panel>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>
