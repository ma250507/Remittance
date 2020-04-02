<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="_Default" title="Untitled Page" %>

<%@ Register assembly="MetaBuilders.WebControls" namespace="MetaBuilders.WebControls" tagprefix="mb" %>

<asp:Content ID="ContentPlaceHolder2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">

    </asp:Content>
<asp:Content ID="ContentPlaceHolder1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <p>
        Welcome,
    <asp:Label ID="Lbl_UserName" runat="server" Font-Bold="True"></asp:Label>
        &nbsp;Please select a function from the menu above.</p>

    </asp:Content>


<asp:Content ID="Content1" runat="server" 
    contentplaceholderid="ContentPlaceHolder4">

</asp:Content>




