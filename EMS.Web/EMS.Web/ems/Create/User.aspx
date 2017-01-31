<%@ Page Title="Create User - EMS" Language="C#" MasterPageFile="~/EMS_PSS.Master" AutoEventWireup="true" CodeBehind="User.aspx.cs" Inherits="EMS.Web.ems.Create.User" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headScripts" runat="server">

    <style>
        .top-buffer {
            margin-top: 20px;
        }
    </style>
</asp:Content>



<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">

    <form runat="server">

        <!-- Page Heading -->
        <div class="page-heading">
            <h1>Enter New User </h1>
        </div>

        <!-- User type -->
        <div class="row">
            <div class="col-lg-4">
                <div class="form-group">
                    <label for="lstUserType">User Type </label>
                    <asp:DropDownList runat="server" ID="lstUserType" CssClass="form-control">
                        <asp:ListItem> None </asp:ListItem>
                        <asp:ListItem> Administrator </asp:ListItem>
                        <asp:ListItem> General User </asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="lstUserType" InitialValue="None" ErrorMessage="* Required" CssClass="text-danger"></asp:RequiredFieldValidator>
                </div>
            </div>
        </div>

        <!-- First Name Box -->
        <div class="row">
            <div class="col-lg-4">
                <div class="form-group">
                    <label for="firstNameBox">First Name: </label>
                    <asp:TextBox runat="server" ID="firstNameBox" CssClass="form-control" placeholder="Enter Firstname"> </asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="firstNameBox" ErrorMessage="* Required" CssClass="text-danger"></asp:RequiredFieldValidator>
                </div>
            </div>
        </div>

        <!-- Last Name Box -->
        <div class="row">
            <div class="col-lg-4">
                <div class="form-group">
                    <label for="lastNameBox">Last Name: </label>
                    <asp:TextBox runat="server" ID="lastNameBox" CssClass="form-control" placeholder="Enter Lastname"> </asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="lastNameBox" ErrorMessage="* Required" CssClass="text-danger"></asp:RequiredFieldValidator>
                </div>
            </div>
        </div>



        <!-- Username Box -->
        <div class="row">
            <div class="col-lg-4">
                <div class="form-group">
                    <label for="userBox" class="control-label">Username </label>
                    <asp:TextBox runat="server" ID="userBox" CssClass="form-control" placeholder="Enter Username"> </asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="userBox" ErrorMessage="* Required" CssClass="text-danger"></asp:RequiredFieldValidator>
                </div>
            </div>
        </div>

        <!-- Password Box -->
        <div class="row">
            <div class="col-lg-4">
                <div class="form-group">
                    <label for="pswdBox">Password: </label>
                    <asp:TextBox runat="server" ID="pswdBox" CssClass="form-control" TextMode="Password" placeholder="Enter Password"> </asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="pswdBox" ErrorMessage="* Required" CssClass="text-danger"></asp:RequiredFieldValidator>
                </div>
            </div>
        </div>

        <!-- Verify Password Box -->
        <div class="row">
            <div class="col-lg-4">
                <div class="form-group">
                    <label for="renterPswdBox">Verify Password: </label>
                    <asp:TextBox runat="server" ID="renterPswdBox" CssClass="form-control" TextMode="Password" placeholder="Verify Password"> </asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="renterPswdBox" ErrorMessage="* Required" CssClass="text-danger"></asp:RequiredFieldValidator>
                </div>
            </div>
        </div>


        <asp:CustomValidator runat="server" ControlToValidate="renterPswdBox" ID="matchPswd" OnServerValidate="matchPswd_ServerValidate" CssClass="text-danger" ErrorMessage="The passwords do not match"></asp:CustomValidator>
        <br />

        <asp:CustomValidator runat="server" ControlToValidate="firstNameBox" ID="fnameLength" OnServerValidate="Length_Validate" CssClass="text-danger" ErrorMessage="The first name is too long."></asp:CustomValidator>
        <br />

        <asp:CustomValidator runat="server" ControlToValidate="lastNameBox" ID="lnameLength" OnServerValidate="Length_Validate" CssClass="text-danger" ErrorMessage="The last name is too long."></asp:CustomValidator>
        <br />
        
        <asp:CustomValidator runat="server" ControlToValidate="userBox" ID="userBoxLength" OnServerValidate="Length_Validate" CssClass="text-danger" ErrorMessage="The username is too long."></asp:CustomValidator>
        <br />
        
        <asp:CustomValidator runat="server" ControlToValidate="pswdBox" ID="pswdBoxLength" OnServerValidate="Length_Validate" CssClass="text-danger" ErrorMessage="The password is too long."></asp:CustomValidator>
        <br />


        <!-- Submit Button -->
        <div class="row">
            <div class="col-lg-4">
                <asp:Button runat="server" ID="btnSubmit" CssClass="btn btn-primary btn-block" Text="Submit" OnClick="btnSubmit_Click" />
            </div>
        </div>

        <br />

        <asp:Label runat="server" CssClass="text-danger" ID="errLbl"></asp:Label>

    </form>

</asp:Content>
