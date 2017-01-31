<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EmployeeView.ascx.cs" Inherits="EMS.Web.EmployeeView" %>

<table class="table">
    <tr>
        <td>Employee Type: </td>
        <td id="type" runat="server"></td>
    </tr>
    <tr>
        <td>First Name: </td>
        <td id="fname" runat="server"></td>
    </tr>
    <tr>
        <td>Last Name: </td>
        <td id="lname" runat="server"></td>
    </tr>
    <tr>
        <td>Social Insurance Number: </td>
        <td id="sin" runat="server"></td>
    </tr>
    <tr>
        <td>Date of Birth: </td>
        <td id="dob" runat="server"></td>
    </tr>
    <tr>
        <td>Employeed With Company: </td>
        <td id="empWithCompany" runat="server"></td>
    </tr>
    <tr>
        <td>Company: </td>
        <td id="company" runat="server"></td>
    </tr>
    <% if(Employee is EMS.Models.Employees.FulltimeEmployee || Employee is EMS.Models.Employees.ParttimeEmployee) { %>
        <tr>
            <td>Date of Hire: </td>
            <td id="doh" runat="server"></td>
        </tr>
    <% } %>
    <% if(UserViewLevel == EMS.Web.UserViewLevel.AdminUser) { %>
        <tr>
            <td>Date of Termination: </td>
            <td id="dot" runat="server"></td>
        </tr>
    <% } %>
    <% if(Employee is EMS.Models.Employees.ParttimeEmployee && UserViewLevel == EMS.Web.UserViewLevel.AdminUser) { %>
        <tr>
            <td>Hourly Rate: </td>
            <td id="hourlyRate" runat="server"></td>
        </tr>
    <% } %>
    <% if(Employee is EMS.Models.Employees.FulltimeEmployee && UserViewLevel == EMS.Web.UserViewLevel.AdminUser) { %>
    <tr>
        <td>Salary: </td>
        <td id="salary" runat="server"></td>
    </tr>
    <%} %>
    <%if(Employee is EMS.Models.Employees.SeasonalEmployee) { %>
    <tr>
        <td>Season: </td>
        <td id="season" runat="server"></td>
    </tr>
    <tr>
        <td>Season Year: </td>
        <td id="seasonYear" runat="server"></td>
    </tr>
    <%if(UserViewLevel == EMS.Web.UserViewLevel.AdminUser) { %>
    <tr>
        <td>Piece Pay: </td>
        <td id="piecePay" runat="server"></td>
    </tr>
    <% } %>
    <% } %>
    <% if(Employee is EMS.Models.Employees.ContractEmployee) { %>
    <tr>
        <td>Contract Start Date: </td>
        <td id="csd" runat="server"></td>
    </tr>
    <tr>
        <td>Contract End Date: </td>
        <td id="ced" runat="server"></td>
    </tr>
    <tr>
        <td>Fixed Contract Amount: </td>
        <td id="fca" runat="server"></td>
    </tr>
    <% } %>
    <tr>
        <td>Reason For Leaving: </td>
        <td id="rfl" runat="server"></td>
    </tr>
</table>