<%@ page language="VB" autoeventwireup="false" inherits="Login, App_Web_uiurv8lk" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
<link href="style.css" rel="stylesheet" type="text/css" media="screen" />
        <style type="text/css">
            #form1
            {
                width: 997px;
            }
            .style5
            {
                color: #FF3300;
                font-weight: bold;
            }
            .style1
            {
                width: 78%;
                height: 87px;
            }
            .style3
            {
                font-family: Arial, Helvetica, sans-serif;
                font-size: small;
                font-weight: bold;
                width: 83px;
            }
            .style2
            {
                width: 83px;
                height: 31px;
            }
            .style4
            {
                height: 31px;
            }
            .Btn
            {
                height: 26px;
            }
            </style>
</head>
<body>
    <form id="form1" runat="server" style="width:auto ">
<div id="wrapper">

	<div id="header">
	    <div id="logo">
	    </div>

        <br />
        <br />
	    

    
	</div>

	<div id="page">
	
	    &nbsp;<span class="style5">*Please Enter Your User Name and Password</span><table class="style1">
            <tr>
                <td class="style3">
                    User Name:</td>
                <td>
                    <asp:TextBox ID="txt_UsrName" runat="server"  Width="225px" CssClass="date" 
                        AutoCompleteType="Disabled"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style3">
                    Password:</td>
                <td>
                    <asp:TextBox ID="txt_Password" runat="server" Width="225px" TextMode="Password"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style2">
                    </td>
                <td class="style4">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="Btn_Login" runat="server" CssClass="Btn" Text="Login" />
                &nbsp;<asp:Label ID="lbl_Error" runat="server" Font-Bold="True" Font-Size="Medium" 
                        ForeColor="Red" Visible="False"></asp:Label>
                </td>
            </tr>
        </table>
	
	</div>

	<div id="footer">
	    Copyright (c) NCR.com. All Rights Reserved.</div>
	    </div>
	</form>
</body>
</html>