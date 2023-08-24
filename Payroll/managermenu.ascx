<%@ Control Language="C#" AutoEventWireup="true" CodeFile="managermenu.ascx.cs" Inherits="managermenu" %>
<asp:Menu ID="Menu1" runat="server" BackColor="blue" style="position:absolute;left:8px; top:89px; height: 18px; width: 373px;"
    DynamicHorizontalOffset="2" Font-Names="Tahoma" Font-Size="12px" 
    ForeColor="white" Orientation="Horizontal" StaticSubMenuIndent="10px">
    <StaticSelectedStyle BackColor="blue" />
    <StaticMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
    <DynamicHoverStyle BackColor="blue" ForeColor="White" />
    <DynamicMenuStyle BackColor="blue" />
    <DynamicSelectedStyle BackColor="#FFCC66" />
    <DynamicMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
    <StaticHoverStyle BackColor="skyblue" ForeColor="White" />
    <Items>
        <asp:MenuItem NavigateUrl="managerwelcome.aspx" Text="Home" Value="Home">
        </asp:MenuItem>
        <asp:MenuItem NavigateUrl="manageremployees.aspx" Text="Employees" Value="Employees">
        </asp:MenuItem>
        <asp:MenuItem NavigateUrl="managermessages.aspx" Text="Messages" Value="Messages">
        </asp:MenuItem>
        <asp:MenuItem NavigateUrl="payslips.aspx" Text="Payslips" Value="Payslips">
        </asp:MenuItem>
        <asp:MenuItem NavigateUrl="logout.aspx" Text="Log Out" Value="Log Out">
        </asp:MenuItem>
    </Items>
</asp:Menu>
<asp:Label ID="lblLoggedinAs" runat="server" Style="position:absolute;top:33px; left:526px;" Font-Bold="True" 
        ForeColor="Green" Font-Names="Tahoma" Font-Size="12px"></asp:Label>