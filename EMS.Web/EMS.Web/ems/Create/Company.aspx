<%@ Page Title="Create Companies - EMS" Language="C#" MasterPageFile="~/EMS_PSS.Master" AutoEventWireup="true" CodeBehind="Company.aspx.cs" Inherits="EMS.Web.ems.Create.Company" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headScripts" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">
    <form role="form" runat="server">

        <!-- Page Heading -->
        <div class="page-heading">
            <h1>Enter New Company. </h1>
        </div>

        <!-- Company Name -->
        <div class="row">
            <div class="col-lg-4">
                <div class="form-group">
                    <label for="txtCompanyName">Company Name: </label>
                    <asp:TextBox runat="server" ID="txtCompanyName" CssClass="form-control"> </asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtCompanyName" ErrorMessage="* Required" CssClass="text-danger"></asp:RequiredFieldValidator>
                </div>
            </div>
        </div>

        <!-- Company Country  -->
        <div class="row">
            <div class="col-lg-4">
                <div class="form-group">
                    <label for="txtCountry">Country: </label>
                    <asp:TextBox runat="server" ID="txtCountry" CssClass="form-control"> </asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtCountry" ErrorMessage="* Required" CssClass="text-danger"></asp:RequiredFieldValidator>
                </div>
            </div>
        </div>


        <!-- Company Street  -->
        <div class="row">
            <div class="col-lg-4">
                <div class="form-group">
                    <label for="txtCompanyStreet">Street: </label>
                    <asp:TextBox runat="server" ID="txtCompanyStreet" CssClass="form-control"> </asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtCompanyName" ErrorMessage="* Required" CssClass="text-danger"></asp:RequiredFieldValidator>
                </div>
            </div>
        </div>

        <!-- Company Postal Code -->
        <div class="row">
            <div class="col-lg-4">
                <div class="form-group">
                    <label for="txtPostalCode">Postal Code: </label>
                    <asp:TextBox runat="server" ID="txtPostalCode" CssClass="form-control"> </asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtCompanyName" ErrorMessage="* Required" CssClass="text-danger"></asp:RequiredFieldValidator>
                </div>
            </div>
        </div>

        <!-- City -->
        <div class="row">
            <div class="col-lg-4">
                <div class="form-group">
                    <label for="txtCity">City: </label>
                    <asp:TextBox runat="server" ID="txtCity" CssClass="form-control"> </asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtCompanyName" ErrorMessage="* Required" CssClass="text-danger"></asp:RequiredFieldValidator>
                </div>
            </div>
        </div>

        <!-- Phone  -->
        <div class="row">
            <div class="col-lg-4">
                <div class="form-group">
                    <label for="txtPhone">Phone Number: </label>
                    <asp:TextBox runat="server" ID="txtPhone" CssClass="form-control"> </asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtCompanyName" ErrorMessage="* Required" CssClass="text-danger"></asp:RequiredFieldValidator>
                </div>
            </div>
        </div>

        <!-- Fax -->
        <div class="row">
            <div class="col-lg-4">
                <div class="form-group">
                    <label for="txtFax"> Fax: </label>
                    <asp:TextBox runat="server" ID="txtFax" CssClass="form-control"> </asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtCompanyName" ErrorMessage="* Required" CssClass="text-danger"></asp:RequiredFieldValidator>
                </div>
            </div>
        </div>

        <!-- Year -->
        <div class="row">
            <div class="col-lg-4">
                <div class="form-group">
                    <label for="txtYear">Incorporation Year: </label>
                    <asp:TextBox runat="server" ID="txtYear" CssClass="form-control"> </asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtYear" ErrorMessage="* Required" CssClass="text-danger"></asp:RequiredFieldValidator>
                </div>
            </div>
        </div>

        <!-- Submit Button -->
        <div class="row">
            <div class="col-lg-4">
                <asp:Button runat="server" ID="btnSubmit" CssClass="btn btn-primary btn-block" Text="Submit" OnClick="btnSubmit_Click"/>
            </div>
        </div>
        
        <br />
        
        <asp:Label runat="server" CssClass="text-danger" ID="errLbl"></asp:Label>

    </form>
</asp:Content>
