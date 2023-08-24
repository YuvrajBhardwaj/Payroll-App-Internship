<%@ Control Language="C#" AutoEventWireup="true" CodeFile="adminmenu.ascx.cs" Inherits="AdminMenu" %>
<asp:Menu ID="Menu1" runat="server" BackColor="#FFFBD6" style="position:absolute;left:8px; top:91px; height: 20px; width: 188px;"
    DynamicHorizontalOffset="2" Font-Names="Tahoma" Font-Size="12px" 
    ForeColor="#990000" Orientation="Horizontal" StaticSubMenuIndent="10px">
    <StaticSelectedStyle BackColor="#FFCC66" />
    <StaticMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
    <DynamicHoverStyle BackColor="#990000" ForeColor="White" />
    <DynamicMenuStyle BackColor="#FFFBD6" />
    <DynamicSelectedStyle BackColor="#FFCC66" />
    <DynamicMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
    <StaticHoverStyle BackColor="#990000" ForeColor="White" />
    <Items>
        <asp:MenuItem NavigateUrl="adminwelcome.aspx" Text="Home" Value="Home">
        </asp:MenuItem>
        <asp:MenuItem NavigateUrl="employees.aspx" Text="Employees" Value="Employees">
        </asp:MenuItem>
        <asp:MenuItem NavigateUrl="logout.aspx" Text="Log Out" Value="Log Out">
        </asp:MenuItem>
    </Items>
</asp:Menu>
<asp:Label ID="lblLoggedinAs" runat="server" Style="position:absolute;top:33px; left:526px;" Font-Bold="True" 
        ForeColor="Green" Font-Names="Tahoma" Font-Size="12px"></asp:Label>