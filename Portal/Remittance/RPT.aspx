<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RPT.aspx.vb" Inherits="RPT" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="Lbl_Status" runat="server" Font-Bold="True" Font-Size="Medium" 
            ForeColor="Red" style="text-align: center" Visible="False"></asp:Label>
        <asp:Button ID="BTN_Back" runat="server" Text="Back" />
        <br />
        <CR:CrystalReportViewer ID="CrystalReportViewer2" runat="server" 
            AutoDataBind="true" Height="50px" Width="350px" HasCrystalLogo="False" 
            ToolPanelView="None" />
        <br />
    </div>
    <div>
    
        <%--<CR:CrystalReportViewer ID="CrystalReportViewer2" runat="server" 
            AutoDataBind="true" EnableParameterPrompt="False" 
            EnableTheming="False" EnableToolTips="False" HasCrystalLogo="False" 
            HasDrillUpButton="False" 
            HasZoomFactorList="False" Height="50px" PrintMode="ActiveX" 
            style="text-align: center" Width="350px" BestFitPage="False" 
            HasToggleGroupTreeButton="False" />--%>
    
    </div>
    </form>
</body>
</html>
