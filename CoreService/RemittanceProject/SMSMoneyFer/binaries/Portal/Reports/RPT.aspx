<%@ page language="VB" autoeventwireup="false" inherits="RPT, App_Web_uiurv8lk" %>

<%@ Register assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Report - NCR Money Fer Portal</title>
        <link href="style.css" rel="stylesheet" type="text/css" media="screen" />

    <style type="text/css">
        #form1
        {
            text-align: center;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
     <div id="wrapper">
    <div id="header">
    
        <div id="logo">
	    </div>
	    <br />
        <br />
        <br />
        <br />
        <br />
        <br />
	    <div id="menu">
		<ul>
            <li><a class ="current_page_item" href="http://localhost/Reports/Default.aspx">Home</a></li>
		</ul>
	</div>
    </div>
    <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" 
        AutoDataBind="true" DisplayGroupTree="False" HasCrystalLogo="False" 
        HasDrillUpButton="False" HasToggleGroupTreeButton="False" 
        HasZoomFactorList="False" PrintMode="ActiveX" EnableParameterPrompt="False" />
        </div>
    </form>
</body>
</html>
