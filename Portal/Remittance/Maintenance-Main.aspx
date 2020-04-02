<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Maintenance-Main.aspx.vb" Inherits="Maintenance_Main" title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    Please Select a Function:<br />
    <asp:RadioButton ID="RD_Maintenance" runat="server" GroupName="Function" 
            Text="Maintain a Transaction." ValidationGroup="Function" 
        Checked="True" />
    <br />
    <asp:RadioButton ID="RD_Blocked" runat="server" GroupName="Function" 
            Text="Blocked List." />
    <br />
    <asp:Button ID="Btn_Next" runat="server" Text="Next" Font-Bold="True" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder4" Runat="Server">
</asp:Content>

