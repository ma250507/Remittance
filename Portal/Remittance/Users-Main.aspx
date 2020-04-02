<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Users-Main.aspx.vb" Inherits="Users_Main" title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <p>
        &nbsp;Please Select a Function:<br />
        <asp:RadioButton ID="RD_View" runat="server" GroupName="Function" 
            Text="View" Checked="True" /><br />
        <asp:RadioButton ID="RD_Add" runat="server" GroupName="Function" 
            Text="Add/Edit User" ValidationGroup="Function" /><br />
            <asp:RadioButton ID="RD_Group" runat="server" GroupName="Function" 
            Text="Groups" ValidationGroup="Function" /><br />
            <asp:RadioButton ID="RD_Unlock" runat="server" GroupName="Function" 
            Text="Unlock" ValidationGroup="Function" /><br />
        <asp:Button ID="Btn_Next" runat="server" Text="Next" Font-Bold="True" />
        
            
    <asp:Label ID="Lbl_Status" runat="server" Font-Bold="True" Font-Size="Medium" 
    ForeColor="Red" Visible="False"></asp:Label>
        
            
    </p>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder4" Runat="Server">
    <asp:Label ID="Lbl_GroupTitle" runat="server" 
    Text="Please Select a Group First:" Visible="False"></asp:Label>
    &nbsp;&nbsp;
<br />
<asp:DropDownList ID="drpd_Groups" runat="server" 
    DataTextField="Name" DataValueField="ID" Visible="False" 
        DataSourceID="SqlDataSource1">
</asp:DropDownList>
        <asp:Button ID="Btn_GroupNext" runat="server" Text="Next" 
    Font-Bold="True" Visible="False" />
        
            
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:NCRMoneyFerConnection %>" 
        SelectCommand="SELECT [Name], [ID] FROM [Groups]"></asp:SqlDataSource>
        
            
    </asp:Content>

