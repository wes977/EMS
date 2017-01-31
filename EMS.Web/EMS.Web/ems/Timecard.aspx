<%@ Page Title="TimeCard - EMS" Language="C#" MasterPageFile="~/EMS_PSS.Master" AutoEventWireup="true" CodeBehind="Timecard.aspx.cs" Inherits="EMS.Web.Timecard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headScripts" runat="server">
    <script src="<%= ResolveUrl("~/Scripts/moment.min.js") %>"></script>
    <script src="<%=ResolveUrl("~/Scripts/bootstrap-datetimepicker.min.js") %>"></script>
    <script src="<%= ResolveUrl("~/Scripts/date-picker.js") %>"></script>
    <script src="<%= ResolveUrl("~/Scripts/Time-picker.js") %>"></script>
    <script src="<%= ResolveUrl("~/Scripts/bootstrap-multiselect.js") %>"></script>
    <script src="<%= ResolveUrl("~/Scripts/scripts.js") %>"></script>
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


            
            <!-- Week of timecard -->
            <div class="row">
                <div class='col-sm-6'>
                    <div class="form-group">
                        <label for="WeekTimecard" class="control-label">Week of timecard</label>
                        <div class='input-group date' id="TimeCardDWeek">
                            <input runat="server" id="WeekTimecard" type='text'  class="form-control" readonly="true" />
                            <span class="input-group-addon">
                                <span class="fa fa-calendar"></span>
                            </span>
                        </div>
                    </div>
                </div>
            </div>

            <!-- Date of timecard -->
            <div class="row">
                <div class='col-sm-6'>
                    <div class="form-group">
                        <label for="DateTimecard" class="control-label">Date of timecard</label>
                        <div class='input-group date' id="TimeCardDate">
                            <input runat="server" id="DateTimecard" type='text' 6 class="form-control" readonly="true" />
                            <span class="input-group-addon">
                                <span class="fa fa-calendar"></span>
                            </span>
                        </div>
                    </div>
                </div>
            </div>
            <asp:MultiView runat="server" ActiveViewIndex="0" ID="employeeViews">
                <asp:View ID="View1" runat="server">
            <!-- Start time of timecard -->
            <div class="row">
                <div class='col-sm-6'>
                    <div class="form-group">
                        <label for="TimeCardStart" class="control-label">Start time</label>
                        <div class='input-group date' id="TimeCardStart">
                            <input runat="server" id="StartTimecard" type='text' class="form-control" readonly="true"  />
                            <span class="input-group-addon">
                                <span class="glyphicon glyphicon-time"></span>
                            </span>
                        </div>
                    </div>
                </div>
            </div>
            <!-- End time of timecard -->
            <div class="row">
                <div class='col-sm-6'>
                    <div class="form-group">
                        <label for="EndTimecard" class="control-label">End time</label>
                        <div class='input-group date' id="TimeCardEnd">
                            <input runat="server" id="EndTimecard" type='text' class="form-control" readonly="true" />
                            <span class="input-group-addon">
                                <span class="glyphicon glyphicon-time"></span>
                            </span>
                        </div>
                    </div>
                </div>
            </div>
            <!-- Time worked Label -->
            <div class="row">
                <div class='col-sm-6'>
                    <div class="form-group">
                        <label id="HoursWorked"></label>
                    </div>
                </div>
            </div>
                    </asp:View>
            <!-- seasonal specific view -->
            
                <asp:View ID="seasonalView" runat="server">
                    <div class="row">
                        <div class='col-sm-6'>
                            <div class="form-group">
                                <div class="form-group">
                                    <label for="PiecePay" class="control-label">Number of pieces</label>
                                    <asp:TextBox ID="PiecePay" runat="server" CssClass="form-control" placeHolder="0"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:View>
            </asp:MultiView>


            <!-- Submit Button -->
            <div class="row">
                <div class="col-lg-4">
                    <asp:Button runat="server" ID="btnSubmit" CssClass="btn btn-primary btn-block" Text="Submit" OnClick="btnSubmit_Click" />
                </div>
            </div>
                        <!-- Time worked Label -->
            <div class="row">
                <div class='col-sm-6'>
                    <div class="form-group">
                        <label runat="server" id="ERROR"></label>
                    </div>
                </div>
            </div>
        </div>
    </form>
</asp:Content>
