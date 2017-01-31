<%@ Page Title="Incomplete Employees - EMS" Language="C#" MasterPageFile="~/EMS_PSS.Master" AutoEventWireup="true" CodeBehind="Incomplete.aspx.cs" Inherits="EMS.Web.ems.View.Incomplete" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headScripts" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">
    <div class="page-header">
        <h3 class="text-muted" id="pageHead" runat="server"></h3>
    </div>
    <asp:Repeater runat="server" ID="incompleteRepeater" OnItemDataBound="incompleteRepeater_ItemDataBound">
        <ItemTemplate>
            <div class="panel-group">
                <div class="panel panel-warning">
                    <div class="panel-heading">
                        <h4 class="panel-title">
                            <a data-toggle="collapse" href="#<%#Eval("ID")%>"><%#Eval("FirstName")%>&nbsp<%#Eval("LastName")%></a>
                            <i class="fa fa-warning pull-right"></i>
                        </h4>
                    </div>
                    <div id="<%#Eval("ID")%>" class="panel-collapse collapse">
                        <div class="panel-body">
                            <ems:EmployeeViewer runat="server" ID="employee" />
                        </div>
                    </div>
                </div>
            </div>
        </ItemTemplate>
    </asp:Repeater>
</asp:Content>
