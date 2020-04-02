<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Teller-Main.aspx.vb" Inherits="Teller_Main" title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <p>
        Please Select a Function:<br />
        <asp:RadioButton ID="RD_Deposit" runat="server" GroupName="Function" 
            Text="Deposit" ValidationGroup="Function" Checked="True" /><br />
        <asp:RadioButton ID="RD_Withdraw" runat="server" GroupName="Function" 
            Text="Withdraw" /><br />
        <asp:RadioButton ID="RD_Redemption" runat="server" GroupName="Function" 
            Text="Redemption" ValidationGroup="Function" /><br />
        <asp:Button ID="Btn_Next" runat="server" Text="Next" Font-Bold="True" />
        
            
    </p>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder4" Runat="Server">
</asp:Content>

