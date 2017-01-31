<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="EMS.Web.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login - Employee Managment System</title>
    <script src="Scripts/jquery-1.9.1.min.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <link href="Content/font-awesome.min.css" rel="stylesheet" />
</head>
<body>
    <div class="container">
        <div class="page-header">
            <h1 class="text-muted">Employee Management System</h1>
        </div>
        <br />
        <br />
        <form id="form1" runat="server" role="form" class="form-horizontal">
            <div class="form-group">
                <label for="tbUserId" class="control-label col-sm-2">
                    <i class="fa fa-user"></i>
                    Username
                </label>
                <div class="col-xs-5">
                    <asp:TextBox ID="tbUserId" runat="server"  CssClass="form-control" placeHolder="Enter Username" CausesValidation="true"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" CssClass="text-danger" ErrorMessage="Username is Required" ControlToValidate="tbUserId"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="form-group">
                <label for="tbPassword" class="control-label col-sm-2">
                    <i class="fa fa-eye"></i>
                    Password
                </label>  
                <div class="col-xs-5">
                    <asp:TextBox ID="tbPassword" TextMode="Password" runat="server" CssClass="form-control" placeHolder="Enter Password" CausesValidation="true"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" CssClass="text-danger" ErrorMessage="Password is required" ControlToValidate="tbPassword"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="form-group">
                <div class="col-sm-offset-2 col-sm-10">
                    <asp:Label runat="server" ID="errLabel" CssClass="text-danger" Enabled="false" Text=""></asp:Label>
                </div>
            </div>
            <div class="form-group">
                <div class="col-sm-offset-2 col-sm-10">
                    <asp:Button ID="btnLogin" runat="server" OnClick="btnLogin_Click" Text="Login" CssClass="btn btn-lg btn-primary"/>
                </div>
            </div>     
        </form>
    </div>
</body>
</html>
