<%@ Page Title="Manage User - EMS" Language="C#" MasterPageFile="~/EMS_PSS.Master" AutoEventWireup="true" CodeBehind="Users.aspx.cs" Inherits="EMS.Web.ems.Manage.Users" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headScripts" runat="server">

    <script src="<%= ResolveUrl("~/Scripts/userBtnHandlers.js") %>"></script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">
    <form runat="server" role="form">
        <!-- create new users -->
        <div class="pull-right">
            <button runat="server" class="btn btn-sm btn-success" onserverclick="btnUser_Click"><i class="fa fa-plus"></i>Create New</button>
        </div>
        <br />
        <br />
        <br />
        <!-- display created users -->
        <div class="panel panel-default">
            <div class="panel-heading">
                <h2 class="panel-title"><i class="fa fa-users"></i>Users</h2>
            </div>
            <div class="panel-body">
                <!-- users table -->
                <asp:Table runat="server" ID="usersTable" CssClass="table table-bordered table-hover"></asp:Table>
            </div>



        </div>


    </form>

    <p id="errorLabel" class="text-danger"></p>

</asp:Content>
