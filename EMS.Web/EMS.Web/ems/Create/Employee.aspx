<%@ Page Title="Create Employee - EMS" Language="C#" MasterPageFile="~/EMS_PSS.Master" AutoEventWireup="true" CodeBehind="Employee.aspx.cs" Inherits="EMS.Web.ems.Create.Employee" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headScripts" runat="server">
    <script src="<%= ResolveUrl("~/Scripts/moment.min.js") %>"></script>
    <script src="<%=ResolveUrl("~/Scripts/bootstrap-datetimepicker.min.js") %>"></script>
    <script src="<%= ResolveUrl("~/Scripts/date-picker.js") %>"></script>
    <script src="<%= ResolveUrl("~/Scripts/bootstrap-multiselect.js") %>"></script>
    <script src="<%= ResolveUrl("~/Scripts/scripts.js") %>"></script>
    <script src="<%= ResolveUrl("~/Scripts/CreateEmployeeScripts.js") %>"></script>
    <link href="<%= ResolveUrl("~/Content/bootstrap-datetimepicker.min.css") %>" rel="stylesheet" />
    <link href="<%= ResolveUrl("~/Content/bootstrap-multiselect.css") %>" rel="stylesheet" />
    <link href="<%= ResolveUrl("~/Content/bootstrap-responsive.min.css") %>" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">
    <form runat="server" role="form" class="form-horizontal">
        <div class="page-header">
            <h3 id="employeeType" runat="server"></h3>
        </div>
        <div class="jumbotron">

            <!-- First name field -->
            <div class="form-group">
                <label for="firstName" class="control-label">First Name</label>
                <asp:TextBox ID="firstName" runat="server" CssClass="form-control" placeHolder="John"></asp:TextBox>
            </div>

            <!-- Last name field -->
            <div class="form-group">
                <label for="lastName" class="control-label">Last Name</label>
                <asp:TextBox ID="lastName" runat="server" CssClass="form-control" placeHolder="Smith"></asp:TextBox>
            </div>

            <!-- Social insurance number field -->
            <div class="form-group">
                <label runat="server" id="lblSinBn" for="SIN" class="control-label">Social Insurance Number</label>
                <asp:TextBox ID="SIN" runat="server" CssClass="form-control" placeHolder="XXX XXX XXX"></asp:TextBox>
            </div>

            <!-- Holds names of companies in database -->
            <div class="form-group">
                <label for="lstCompanies" class="control-label">Employed With</label>
                <asp:DropDownList runat="server" ID="lstCompanies" CssClass="form-control">
                </asp:DropDownList>
            </div>

            <!-- Money field -->
            <div class="form-group">
                <label for="txtMoney" id="lblMoney" runat="server" class="control-label">Salary</label>
                <asp:TextBox ID="txtMoney" runat="server" CssClass="form-control" placeHolder="$0.00"></asp:TextBox>
            </div>

            <!-- Date of birth field -->
            <div class="row">
                <div class='col-sm-6'>
                    <div class="form-group">
                        <label for="dateOfBirth" class="control-label">Date of Birth</label>
                        <div class='input-group date' id="dateTimeDOB">
                            <input runat="server" id="dateOfBirth" type='text' class="form-control" />
                            <span class="input-group-addon">
                                <span class="fa fa-calendar"></span>
                            </span>
                        </div>
                    </div>
                </div>
            </div>

            <!-- Employee specific views -->
            <asp:MultiView runat="server" ActiveViewIndex="0" ID="employeeViews">

                <!-- FULL TIME EMPLOYEE VIEW -->
                <asp:View ID="fullTimeView" runat="server">
                    <!-- Date of Hire -->
                    <div class="row">
                        <div class='col-sm-6'>
                            <div class="form-group">
                                <label for="ftHireDate" class="control-label">Date of Hire</label>
                                <div class='input-group date' id="ftDateTimeHire">
                                    <input runat="server" id="ftHireDate" type='text' class="form-control" />
                                    <span class="text-success input-group-addon">
                                        <span class="fa fa-calendar"></span>
                                    </span>
                                </div>
                            </div>
                        </div>
                    </div>

                    <!-- Date of termination -->
                    <div class="row">
                        <div class='col-sm-6'>
                            <div class="form-group">
                                <label for="ftDateOfTerm" class="control-label">Date of Termination</label>
                                <div class='input-group date' id="ftDateTimeTerm">
                                    <input runat="server" id="ftDateOfTerm" type='text' class="form-control" />
                                    <span class="input-group-addon">
                                        <span class="fa fa-calendar"></span>
                                    </span>
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:View>

                <!-- PART TIME EMPLOYEE VIEW -->
                <asp:View ID="partTimeView" runat="server">
                    <!-- Date of Hire -->
                    <div class="row">
                        <div class='col-sm-6'>
                            <div class="form-group">
                                <label for="ptHireDate" class="control-label">Date of Hire</label>
                                <div class='input-group date' id="ptDateTimeHire">
                                    <input runat="server" id="ptHireDate" type='text' class="form-control" />
                                    <span class="input-group-addon">
                                        <span class="fa fa-calendar"></span>
                                    </span>
                                </div>
                            </div>
                        </div>
                    </div>

                    <!-- Date of termination -->
                    <div class="row">
                        <div class='col-sm-6'>
                            <div class="form-group">
                                <label for="ptDateOfTerm" class="control-label">Date of Termination</label>
                                <div class='input-group date' id="ptDateTimeTerm">
                                    <input runat="server" id="ptDateOfTerm" type='text' class="form-control" />
                                    <span class="input-group-addon">
                                        <span class="fa fa-calendar"></span>
                                    </span>
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:View>

                <!-- SEASONAL EMPLOYEE VIEW -->
                <asp:View ID="seasonalView" runat="server">
                    <!-- Season Selection -->
                    <div class="row">
                        <div class='col-sm-6'>
                            <div class="form-group">
                                <label for="seasonYear" class="control-label">Season Year</label>
                                <div class='input-group date' id="seasonYearDateTime">
                                    <input runat="server" id="seasonYear" type='text' class="form-control" />
                                    <span class="input-group-addon">
                                        <span class="fa fa-calendar"></span>
                                    </span>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!-- Season Selection -->
                    <div class="form-group">
                        <label for="season" class="control-label">Season</label>
                        <asp:DropDownList runat="server" ID="season" CssClass="form-control">
                            <asp:ListItem Value="0">Spring</asp:ListItem>
                            <asp:ListItem Value="1">Summer</asp:ListItem>
                            <asp:ListItem Value="2">Fall</asp:ListItem>
                            <asp:ListItem Value="3">Winter</asp:ListItem>
                        </asp:DropDownList>
                    </div>

                </asp:View>

                <!-- CONTRACT EMPLOYEE VIEW -->
                <asp:View ID="contractView" runat="server">

                    <!-- Contract Start Date -->
                    <div class="row">
                        <div class='col-sm-6'>
                            <div class="form-group">
                                <label for="conStartDate" class="control-label">Contract Start Date</label>
                                <div class='input-group date' id="conStartDateTime">
                                    <input runat="server" id="conStartDate" type='text' class="form-control" />
                                    <span class="input-group-addon">
                                        <span class="fa fa-calendar"></span>
                                    </span>
                                </div>
                            </div>
                        </div>
                    </div>

                    <!-- Contract End Date -->
                    <div class="row">
                        <div class='col-sm-6'>
                            <div class="form-group">
                                <label for="conEndDate" class="control-label">Contract End Date</label>
                                <div class='input-group date' id="conEndDateTime">
                                    <input runat="server" id="conEndDate" type='text' class="form-control" />
                                    <span class="input-group-addon">
                                        <span class="fa fa-calendar"></span>
                                    </span>
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:View>
            </asp:MultiView>

            <!-- Reason for Leaving field -->
            <asp:Panel ID="panelReasonForLeaving" runat="server">
                <div class="form-group">
                    <label for="reasonForLeaving" class="control-label">Reason for Leaving</label>
                    <asp:DropDownList runat="server" Enabled="false" ID="reasonForLeaving" CssClass="form-control">
                        <asp:ListItem>Terminated</asp:ListItem>
                        <asp:ListItem>Retired</asp:ListItem>
                        <asp:ListItem>Quit</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </asp:Panel>

            <!-- Display any employee creation errors here -->
            <asp:Panel ID="errorPanel" runat="server" Visible="false" EnableViewState="false">
                <div class="form-group" style="text-align: center">
                    <label for="lstErrors" class="control-label">The following errors have occurred</label>
                    <br />
                    <asp:ListBox runat="server" ID="lstErrors" ForeColor="DarkRed" EnableViewState="false"></asp:ListBox>
                </div>
            </asp:Panel>

            <div class="form-group" style="text-align:center">
                <asp:label id="lblStatus" runat="server" EnableViewState="false"></asp:label>
            </div>

            <!-- Submit button -->
            <button id="btnSubmit" class="btn btn-info"><i class="fa fa-check"></i>&nbsp; Save</button>
        </div>
    </form>
</asp:Content>
