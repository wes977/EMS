<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EmployeeView.ascx.cs" Inherits="EMS.Web.EmployeeView" Debug="True" CompilationMode="Always" %>

<table class="table">
    <tr style="border-top: 0;">
        <td><strong>Employee Type</strong></td>
        <td id="type" runat="server"></td>
        <td>
            <button type="button" id="btn1" class="button-hidden text-warning" runat="server" onserverclick="btn1_ServerClick">
                <i class="fa fa-edit"></i>
            </button>
        </td>
    </tr>
    <tr>
        <td><strong>First Name</strong></td>
        <td id="fname" runat="server"></td>
        <td>
            <button type="button" id="btn2" class="button-hidden text-warning" runat="server" onserverclick="btn1_ServerClick">
                <i class="fa fa-edit"></i>
            </button>
        </td>
    </tr>
    <tr>
        <td><strong>Last Name</strong></td>
        <td id="lname" runat="server"></td>
        <td>
            <button type="button" id="btn3" class="button-hidden text-warning" runat="server" onserverclick="btn1_ServerClick">
                <i class="fa fa-edit"></i>
            </button>
        </td>
    </tr>
    <tr>
        <td><strong>Social Insurance Number</strong></td>
        <td id="sin" runat="server"></td>
        <td>
            <button type="button" id="btn4" class="button-hidden text-warning" runat="server" onserverclick="btn1_ServerClick">
                <i class="fa fa-edit"></i>
            </button>
        </td>
    </tr>
    <tr>
        <td><strong>Date of Birth</strong></td>
        <td id="dob" runat="server"></td>
        <td>
            <button type="button" id="btn5" class="button-hidden text-warning" runat="server" onserverclick="btn1_ServerClick">
                <i class="fa fa-edit"></i>
            </button>
        </td>
    </tr>
    <tr>
        <td><strong>Employeed With Company</strong></td>
        <td id="empWithCompany" runat="server"></td>
        <td>
            <button type="button" id="btn6" class="button-hidden text-warning" runat="server" onserverclick="btn1_ServerClick">
                <i class="fa fa-edit"></i>
            </button>
        </td>
    </tr>
    <tr>
        <td><strong>Company</strong></td>
        <td id="company" runat="server"></td>
        <td>
            <button type="button" id="btn7" class="button-hidden text-warning" runat="server" onserverclick="btn1_ServerClick">
                <i class="fa fa-edit"></i>
            </button>
        </td>
    </tr>
    <% if (Employee is EMS.Models.Employees.FulltimeEmployee || Employee is EMS.Models.Employees.ParttimeEmployee)
        { %>
    <tr>
        <td><strong>Date of Hire</strong></td>
        <td id="doh" runat="server"></td>
        <td>
            <button type="button" id="btn8" class="button-hidden text-warning" runat="server" onserverclick="btn1_ServerClick">
                <i class="fa fa-edit"></i>
            </button>
        </td>
    </tr>

    <% if (UserViewLevel == EMS.Web.UserViewLevel.AdminUser)
        { %>
    <tr>
        <td><strong>Date of Termination</strong></td>
        <td id="dot" runat="server"></td>
        <td>
            <button type="button" id="btn9" class="button-hidden text-warning" runat="server" onserverclick="btn1_ServerClick">
                <i class="fa fa-edit"></i>
            </button>
        </td>
    </tr>
    <% } %>
    <% } %>
    <% if (Employee is EMS.Models.Employees.ParttimeEmployee && UserViewLevel == EMS.Web.UserViewLevel.AdminUser)
        { %>
    <tr>
        <td><strong>Hourly Rate</strong></td>
        <td id="hourlyRate" runat="server"></td>
        <td>
            <button type="button" id="btn10" class="button-hidden text-warning" runat="server" onserverclick="btn1_ServerClick">
                <i class="fa fa-edit"></i>
            </button>
        </td>
    </tr>
    <% } %>
    <% if (Employee is EMS.Models.Employees.FulltimeEmployee && UserViewLevel == EMS.Web.UserViewLevel.AdminUser)
        { %>
    <tr>
        <td><strong>Salary</strong></td>
        <td id="salary" runat="server"></td>
        <td>
            <button type="button" id="btn11" class="button-hidden text-warning" runat="server" onserverclick="btn1_ServerClick">
                <i class="fa fa-edit"></i>
            </button>
        </td>
    </tr>
    <%} %>
    <%if (Employee is EMS.Models.Employees.SeasonalEmployee)
        { %>
    <tr>
        <td><strong>Season</strong></td>
        <td id="season" runat="server"></td>
        <td>
            <button type="button" id="btn12" class="button-hidden text-warning" runat="server" onserverclick="btn1_ServerClick">
                <i class="fa fa-edit"></i>
            </button>
        </td>
    </tr>
    <tr>
        <td><strong>Season Year</strong></td>
        <td id="seasonYear" runat="server"></td>
        <td>
            <button type="button" id="btn13" class="button-hidden text-warning" runat="server" onserverclick="btn1_ServerClick">
                <i class="fa fa-edit"></i>
            </button>
        </td>
    </tr>
    <%if (UserViewLevel == EMS.Web.UserViewLevel.AdminUser)
        { %>
    <tr>
        <td><strong>Piece Pay</strong></td>
        <td id="piecePay" runat="server"></td>
        <td>
            <button type="button" id="btn14" class="button-hidden text-warning" runat="server" onserverclick="btn1_ServerClick">
                <i class="fa fa-edit"></i>
            </button>
        </td>
    </tr>
    <% } %>
    <% } %>
    <% if (Employee is EMS.Models.Employees.ContractEmployee)
        { %>
    <tr>
        <td><strong>Start Date</strong></td>
        <td id="csd" runat="server"></td>
        <td>
            <button type="button" id="btn15" class="button-hidden text-warning" runat="server" onserverclick="btn1_ServerClick">
                <i class="fa fa-edit"></i>
            </button>
        </td>
    </tr>
    <tr>
        <td><strong>End Date</strong></td>
        <td id="ced" runat="server"></td>
        <td>
            <button type="button" id="btn16" class="button-hidden text-warning" runat="server" onserverclick="btn1_ServerClick">
                <i class="fa fa-edit"></i>
            </button>
        </td>
    </tr>
    <tr>
        <td><strong>Fixed Contract Amount</strong></td>
        <td id="fca" runat="server"></td>
        <td>
            <button type="button" id="btn17" class="button-hidden text-warning" runat="server" onserverclick="btn1_ServerClick">
                <i class="fa fa-edit"></i>
            </button>
        </td>
    </tr>
    <% } %>
    <tr>
        <td><strong>Reason For Leaving</strong></td>
        <td id="rfl" runat="server"></td>
        <td>
            <button type="button" id="btn18" class="button-hidden text-warning" runat="server" onserverclick="btn1_ServerClick">
                <i class="fa fa-edit"></i>
            </button>
        </td>
    </tr>
</table>
