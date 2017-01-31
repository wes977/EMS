<%@ Page Title="Manage Companies - EMS" Language="C#" MasterPageFile="~/EMS_PSS.Master" AutoEventWireup="true" CodeBehind="Companies.aspx.cs" Inherits="EMS.Web.ems.Manage.Companies" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headScripts" runat="server">

    <script src="<%= ResolveUrl("~/Scripts/companyBtnHandler.js")%>"> </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">

    <form runat="server" role="form">
        <!-- create new users -->

        <div class="pull-right">
            <button runat="server" id="createNewCompany" class="btn btn-sm btn-success" onserverclick="createNewCompany_ServerClick"><i class="fa fa-plus"></i>Create New</button>
        </div>

        <br />
        <br />
        <br />

        <!-- display created users -->
        <div class="panel panel-default">
            <div class="panel-heading">
                <h2 class="panel-title"><i class="fa fa-users"></i>Companies  </h2>
            </div>
            <div class="panel-body">
                <!-- users table -->
                <asp:Table runat="server" ID="companiesTable" CssClass="table table-bordered table-hover"></asp:Table>
            </div>
        </div>


    </form>

    <p id="errorLabel" class="text-danger"></p>

</asp:Content>
