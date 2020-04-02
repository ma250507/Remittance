<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TransBalanceRPTView.aspx.vb" Inherits="TransBalanceRPTView" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:Label ID="Lbl_Status" runat="server" Font-Bold="True" Font-Size="Medium" 
            ForeColor="Red" style="text-align: center" Visible="False"></asp:Label>
        <asp:Button ID="BTN_Back" runat="server" Text="Back" />
        <br />
        <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" 
            AutoDataBind="True" Height="940px" Width="1210px" HasCrystalLogo="False" 
            ToolPanelView="None" GroupTreeImagesFolderUrl="" 
        ToolbarImagesFolderUrl="" 
        ToolPanelWidth="200px"  />
        <br />
    </form>
</body>
</html>
