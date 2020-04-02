<%@ page language="VB" autoeventwireup="false" inherits="Resend, App_Web_gvt1jr7a" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:RadioButton ID="RD_Both" runat="server" Font-Bold="True" GroupName="V" 
            Text="Both" Checked="True" />
        <br />
        <asp:RadioButton ID="RD_Beneficiary" runat="server" Font-Bold="True" 
            GroupName="V" Text="Beneficiary" />
        <br />
        <asp:RadioButton ID="RD_Depositor" runat="server" Font-Bold="True" 
            GroupName="V" Text="Depositor" />
        <br />
                        <asp:Label ID="Lbl_Status" runat="server" Font-Bold="True" Font-Size="Medium" 
                            ForeColor="Red" Visible="False"></asp:Label>
    
	                        <br />
        <asp:Button ID="btn_submit" runat="server" Font-Bold="True" Text="Submit" />
    
    </div>
    </form>
</body>
</html>
