<%@ Page Title="Reports - EMS" Language="C#" MasterPageFile="~/EMS_PSS.Master" AutoEventWireup="true" CodeBehind="Reports.aspx.cs" Inherits="EMS.Web.ems.Reports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headScripts" runat="server">
    <script src="<%= ResolveUrl("~/Scripts/moment.min.js") %>"></script>
    <script src="<%=ResolveUrl("~/Scripts/bootstrap-datetimepicker.min.js") %>"></script>
    <script src="<%= ResolveUrl("~/Scripts/Time-picker.js") %>"></script>
    <script src="<%= ResolveUrl("~/Scripts/date-picker.js") %>"></script>
    <script src="<%= ResolveUrl("~/Scripts/bootstrap-multiselect.js") %>"></script>
    <script src="<%= ResolveUrl("~/Scripts/scripts.js") %>"></script>
    <script src="<%= ResolveUrl("~/Scripts/CreateEmployeeScripts.js") %>"></script>
    <link href="<%= ResolveUrl("~/Content/bootstrap-datetimepicker.min.css") %>" rel="stylesheet" />
    <link href="<%= ResolveUrl("~/Content/bootstrap-multiselect.css") %>" rel="stylesheet" />
    <link href="<%= ResolveUrl("~/Content/bootstrap-responsive.min.css") %>" rel="stylesheet" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">
    <form runat="server" role="form">
        <asp:ScriptManager runat="server"></asp:ScriptManager>
        <div class="page-header">
            <h3 id="reportType" runat="server"></h3>
        </div>
        <div class="jumbotron">

            <div class="form-group">
                <label for="companyDropDown" class="control-label"></label>
                <asp:DropDownList runat="server" ID="companyDropDown" CssClass="form-control"></asp:DropDownList>
            </div>
            <asp:MultiView runat="server" ActiveViewIndex="0" ID="WeekPickerMV" Visible="false">
                <asp:View ID="WeekPicker" runat="server">
                    <div class="form-group">
                        <label for="WeekReport" class="control-label">Week of report</label>
                        <div class='input-group date' id="TimeCardDWeek234">
                            <input runat="server" id="WeekReport" type='text'  class="form-control" readonly="true" />
                            <span class="input-group-addon">
                                <span class="fa fa-calendar"></span>
                            </span>
                        </div>
                    </div>
                </asp:View>
            </asp:MultiView>
            <div class="form-group">
                <asp:UpdatePanel ID="btnGenerateUpdate" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <button id="btnGenerate" runat="server" onserverclick="btnGenerate_ServerClick" class="btn btn-primary">
                            <i class="fa fa-check"></i>Generate Report
                        </button>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnGenerate" EventName="ServerClick" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>

            <asp:UpdatePanel ID="reportUpdatePanel" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                    <asp:MultiView runat="server" ActiveViewIndex="0" ID="ReportViews" Visible="false">

                        <!-- Seniority Report -->
                        <asp:View ID="SeniorReportView" runat="server">

                            <div class="panel panel-defualt">
                                <div class="panel-heading">
                                    <h3 class="text-center panel-header"></h3>
                                </div>
                                <div class="panel-body">
                                    <asp:GridView ID="SRGrid" class="table table-striped" runat="server" Width="100%">
                                    </asp:GridView>

                                </div>
                            </div>

                        </asp:View>

                        <!-- Weekly Hours worked Report -->
                        <asp:View ID="WeeklyHoursReportView" runat="server">

                            <div class="panel panel-danger">
                                <div class="panel-heading">
                                    <h3 class="text-center panel-header">FullTime</h3>
                                </div>
                                <div class="panel-body">
                                    <asp:GridView ID="WHRGridFT" class="table table-striped" runat="server" Width="100%">
                                    </asp:GridView>
                                </div>
                            </div>

                            <div class="panel panel-success">
                                <div class="panel-heading">
                                    <h3 class="text-center panel-header">PartTime</h3>
                                </div>
                                <div class="panel-body">

                                    <asp:GridView ID="WHRGridPT" class="table table-striped" runat="server" Width="100%">
                                    </asp:GridView>

                                </div>
                            </div>

                            <div class="panel panel-warning">
                                <div class="panel-heading">
                                    <h3 class="text-center panel-header">Seasonal</h3>
                                </div>
                                <div class="panel-body">
                                    <asp:GridView ID="WHRGridSN" class="table table-striped" runat="server" Width="100%">
                                    </asp:GridView>
                                </div>
                            </div>
                            <asp:Label ID="WHRReportMes" runat="server"></asp:Label>
                        </asp:View>
                        <!--Payroll Report -->
                        <asp:View ID="PayrollView" runat="server">

                            <div class="panel panel-danger">
                                <div class="panel-heading">
                                    <h3 class="text-center panel-header">FullTime</h3>
                                </div>
                                <div class="panel-body">
                                    <asp:GridView ID="PRGridFT" class="table table-striped" runat="server" Width="100%"></asp:GridView>
                                </div>
                            </div>

                            <div class="panel panel-success">
                                <div class="panel-heading">
                                    <h3 class="text-center panel-header">PartTime</h3>
                                </div>
                                <div class="panel-body">
                                    <asp:GridView ID="PRGridPT" class="table table-striped" runat="server" Width="100%"></asp:GridView>
                                </div>
                            </div>

                            <div class="panel panel-warning">
                                <div class="panel-heading">
                                    <h3 class="text-center panel-header">Seasonal</h3>
                                </div>
                                <div class="panel-body">
                                    <asp:GridView ID="PRGridSN" class="table table-striped" runat="server" Width="100%"></asp:GridView>
                                </div>
                            </div>
                            <div class="panel panel-primary">
                                <div class="panel-heading">
                                    <h3 class="text-center panel-header">Contract</h3>
                                </div>
                                <div class="panel-body">
                                    <asp:GridView ID="PRGridCT" class="table table-striped" runat="server" Width="100%"></asp:GridView>
                                </div>
                            </div>
                            <asp:Label ID="PRReportMes" runat="server"></asp:Label>
                        </asp:View>
                        <!-- Active employment -->
                        <asp:View ID="ActiveEmploymentView" runat="server">
                            <div class="panel panel-danger">
                                <div class="panel-heading">
                                    <h3 class="text-center panel-header">FullTime</h3>
                                </div>
                                <div class="panel-body">
                                    <asp:GridView ID="AERGridFT" class="table table-striped" runat="server" Width="100%"></asp:GridView>
                                </div>
                            </div>

                            <div class="panel panel-success">
                                <div class="panel-heading">
                                    <h3 class="text-center panel-header">PartTime</h3>
                                </div>
                                <div class="panel-body">
                                    <asp:GridView ID="AERGridPT" class="table table-striped" runat="server" Width="100%"></asp:GridView>
                                </div>
                            </div>

                            <div class="panel panel-warning">
                                <div class="panel-heading">
                                    <h3 class="text-center panel-header">Seasonal</h3>
                                </div>
                                <div class="panel-body">
                                    <asp:GridView ID="AERGridSN" class="table table-striped" runat="server" Width="100%"></asp:GridView>
                                </div>
                            </div>
                            <div class="panel panel-primary">
                                <div class="panel-heading">
                                    <h3 class="text-center panel-header">Contract</h3>
                                </div>
                                <div class="panel-body">
                                    <asp:GridView ID="AERGridCT" class="table table-striped" runat="server" Width="100%"></asp:GridView>
                                    
                                </div>
                            </div>
                            
                        </asp:View>

                        <!-- Inactive -->
                        <asp:View ID="InactiveEmploymentView" runat="server">
                            <div class="panel panel-defualt">
                                <div class="panel-heading">
                                    <h3 class="text-center panel-header"></h3>
                                </div>
                                <div class="panel-body">
                                    <asp:GridView ID="IERGrid" class="table table-striped" runat="server" Width="100%"></asp:GridView>
                                </div>
                            </div>
                        </asp:View>
                    </asp:MultiView>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </form>
</asp:Content>
