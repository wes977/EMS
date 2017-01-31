<%@ Page Title="Search - EMS" Language="C#" MasterPageFile="~/EMS_PSS.Master" AutoEventWireup="true" CodeBehind="search.aspx.cs" Inherits="EMS.Web.ems.search" %>

<asp:Content ContentPlaceHolderID="headScripts" runat="server">
    <script src="<%= ResolveUrl("~/Scripts/query.js") %>"></script>
</asp:Content>

<asp:Content ContentPlaceHolderID="main" runat="server">
    <form runat="server" role="search">
        <div class="input-group">
            <div class="input-group-btn search-panel">
                <button type="button" class="btn btn-default dropdown-toggle" data-toggle="dropdown">
                    <span id="search_concept">Filter by</span> <span class="caret"></span>
                </button>
                <ul class="dropdown-menu" role="menu">
                    <li><a href="#fname">First Name</a></li>
                    <li><a href="#lname">Last Name</a></li>
                    <li><a href="#sin">SIN</a></li>
                    <li class="divider"></li>
                    <li><a href="#all">Anything</a></li>
                </ul>
            </div>
            <input type="text" placeholder="Search Employees" class="form-control" id="tbSearch" runat="server" />
            <div class="input-group-btn">
                <button type="button" class="btn btn-primary" runat="server" id="btnSearch" onserverclick="btnSearch_ServerClick">
                    <i class="fa fa-search"></i>
                    <span>Search</span>
                </button>
            </div>
        </div>
        <input runat="server" type="hidden" name="search_param" value="all" id="search_param" />
        <br />
        <br />
        <div runat="server" id="searchArea" visible="false">
            <div class="page-header">
                <h3 class="text-muted">Search Results for: <%= tbSearch.Value %></h3>
            </div>
            <div class="panel-group">
                <asp:Repeater runat="server" ID="searchRepeater" OnItemDataBound="searchRepeater_ItemDataBound">
                    <ItemTemplate>
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                <h4 class="panel-title">
                                    <a data-toggle="collapse" href="#<%#Eval("ID")%>"><%#Eval("FirstName")%>&nbsp<%#Eval("LastName")%></a>
                                </h4>
                            </div>
                            <div id="<%#Eval("ID")%>" class="panel-collapse collapse">
                                <div class="panel-body">
                                    <ems:EmployeeViewer runat="server" ID="employee" />
                                    <%if (Session["Clearance"].ToString() != "1")
                                        { %>
                                    <a type="button" id="btn" href="<%# ResolveUrl("~/ems/Timecard.aspx?employeeid=") + Eval("ID") %>" class="btn btn-primary pull-right">
                                        <i class="fa fa-clock-o">Time Card</i>
                                    </a>
                                    <% } %>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </form>
</asp:Content>
