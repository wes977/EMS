<%@ Page Title="View Employee - EMS" Language="C#" MasterPageFile="~/EMS_PSS.Master" AutoEventWireup="true" CodeBehind="ViewEmployee.aspx.cs" Inherits="EMS.Web.ems.View.ViewEmployee" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headScripts" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">
    <form runat="server">
        <ems:EmployeeViewer runat="server" ID="employee" >
        </ems:EmployeeViewer>
    </form>
</asp:Content>
