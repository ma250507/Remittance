<%@ page language="VB" autoeventwireup="false" inherits="spare, App_Web_gvt1jr7a" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <style type="text/css">

.Result
{
	font-family: Arial, Helvetica, sans-serif;
	table-layout: auto;
	position: relative;
            top: 0px;
            left: 0px;
        }

        .style8
        {
            width: 201px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
                <table class="Result">
                    <tr>
                        <td class="style8">
    <asp:Label ID="Label25" runat="server" style="font-weight: 700" 
        Text="Transaction Code:"></asp:Label>
                        </td>
                        <td>
    <asp:Label ID="Label1" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style8">
    <asp:Label ID="Label26" runat="server" style="font-weight: 700" 
        Text="Country Code:"></asp:Label>
                        </td>
                        <td>
    <asp:Label ID="Label2" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style8">
    <asp:Label ID="Label27" runat="server" style="font-weight: 700" 
        Text="Bank Code: "></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label3" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style8">
    <asp:Label ID="Label28" runat="server" style="font-weight: 700" 
        Text="ATMId:&nbsp;"></asp:Label>
                        </td>
                        <td>
    <asp:Label ID="Label4" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style8">
    <asp:Label ID="Label29" runat="server" style="font-weight: 700" 
        Text="Request Type: "></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label5" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style8">
    <asp:Label ID="Label30" runat="server" style="font-weight: 700" 
        Text="ATM Date: "></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label6" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style8">
    <b>
    <asp:Label ID="Label31" runat="server" Text="ATM Time:&nbsp;"></asp:Label>
                            </b>
                        </td>
                        <td>
    <asp:Label ID="Label7" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style8">
    <asp:Label ID="Label32" runat="server" style="font-weight: 700" 
        Text="ATM Trx Sequence: "></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label8" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style8">
    <asp:Label ID="Label33" runat="server" style="font-weight: 700" 
        Text="Depositor Mobile: &nbsp;"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label9" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style8">
    <asp:Label ID="Label35" runat="server" style="font-weight: 700" 
        Text="Beneficiary Mobile: "></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label11" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style8">
    <asp:Label ID="Label37" runat="server" style="font-weight: 700" 
        Text="Amount:&nbsp;"></asp:Label>
                        </td>
                        <td>
    <asp:Label ID="Label13" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style8">
    <asp:Label ID="Label38" runat="server" style="font-weight: 700" 
        Text="Currency Code:&nbsp;"></asp:Label>
                        </td>
                        <td>
    <asp:Label ID="Label14" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style8">
    <asp:Label ID="Label39" runat="server" style="font-weight: 700" 
        Text="Deposit Date Time: "></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label15" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style8">
    <asp:Label ID="Label40" runat="server" style="font-weight: 700" 
        Text="Deposit Action Reason:&nbsp;"></asp:Label>
                        </td>
                        <td>
    <asp:Label ID="Label16" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style8">
    <asp:Label ID="Label41" runat="server" style="font-weight: 700" 
        Text="Deposit Status:&nbsp;"></asp:Label>
                        </td>
                        <td>
    <asp:Label ID="Label17" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style8">
    <asp:Label ID="Label42" runat="server" style="font-weight: 700" 
        Text="Withdrawal Status:&nbsp;"></asp:Label>
                        </td>
                        <td>
    <asp:Label ID="Label18" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style8">
    <asp:Label ID="Label43" runat="server" style="font-weight: 700" 
        Text="Withdrawal Date Time:&nbsp;"></asp:Label>
                        </td>
                        <td>
    <asp:Label ID="Label19" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style8">
    <asp:Label ID="Label44" runat="server" style="font-weight: 700" 
        Text="Cancel Status:&nbsp;"></asp:Label>
                        </td>
                        <td>
    <asp:Label ID="Label20" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style8">
    <asp:Label ID="Label45" runat="server" style="font-weight: 700" 
        Text="Cancel Date Time:&nbsp;"></asp:Label>
                        </td>
                        <td>
    <asp:Label ID="Label21" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style8">
    <asp:Label ID="Label46" runat="server" style="font-weight: 700" 
        Text="Overall Status: "></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Lbl_OS" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style8">
    <asp:Label ID="Label47" runat="server" style="font-weight: 700" 
        Text="SMS Sending Status:&nbsp;"></asp:Label>
                        </td>
                        <td>
    <asp:Label ID="Lbl_SMSSS" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style8">
    <asp:Label ID="Label48" runat="server" style="font-weight: 700" 
        Text="SMS Sent Date Time:&nbsp;"></asp:Label>
                        </td>
                        <td>
    <asp:Label ID="Lbl_SMSDT" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
    
    </div>
    </form>
</body>
</html>
