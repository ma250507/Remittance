<%@ page language="VB" autoeventwireup="false" inherits="Actions, App_Web_gvt1jr7a" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Actions</title>
     <link href="style.css" rel="stylesheet" type="text/css" media="screen" />
    <script type="text/javascript" >
    function ToggleVisibility(elementID)
{
    var element = document.getElementByID(elementID);

    if (element.style.display = 'none')
    {
        element.style.display = 'inherit';
    }
    else
    {
        element.style.display = 'none';
    }
}
    </script>
    <style type="text/css">


#search fieldset {
	margin: 0;
	padding: 0;
	border: none;
}

        .style9
        {
            height: 23px;
            width: 234px;
            font-weight: bold;
        }
        .style10
        {
            width: 328px;
            height: 23px;
        }
                
body
{
	background-position: 512px 384px;
	margin: 0;
	font-family: Arial, Helvetica, sans-serif; 
	font-size: 12px;
	background-repeat: no-repeat;
	background-attachment: scroll;
	width: auto  ;
	/*background: #BED3F4 url(images/BG.jpg);*/
}

        .style11
        {
            height: 22px;
        }
        .style12
        {
            height: 18px;
        }
        .style13
        {
            width: 234px;
            height: 6px;
            font-weight: bold;
        }
        .style14
        {
            width: 328px;
            height: 6px;
        }
        .style15
        {
            width: 234px;
            height: 11px;
            font-weight: 700;
        }
        .style16
        {
            width: 328px;
            height: 11px;
        }
        .style17
        {
            width: 234px;
            height: 3px;
            font-weight: bold;
        }
        .style18
        {
            width: 328px;
            height: 3px;
        }

        .style19
        {
            width: 191px;
        }

        .style20
        {
            height: 18px;
            font-weight: bold;
        }
        .style21
        {
            width: 191px;
            font-weight: bold;
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
            <li><a class ="current_page_item" href="http://localhost/Maintenance/Default.aspx">Home</a></li>
		</ul>
	</div>
    </div>
	 <asp:Button ID="btn_Activate" runat="server" Height="22px" 
                    style="font-weight: 700" Text="Activate" Width="74px" />
                <asp:Button ID="btn_Hold" runat="server" Height="22px" style="font-weight: 700" 
                    Text="Hold" />
                <asp:Button ID="btn_Unhold" runat="server" Height="22px" 
                    style="font-weight: 700" Text="Unhold" Width="57px" />
                <asp:Button ID="Btn_Unblock" runat="server" 
            Height="22px" style="font-weight: 700" Text="UnBlock" />
                <asp:Button ID="btn_Resend0" runat="server" Font-Bold="True" Height="22px" 
                    Text="Resend" />
                <asp:DropDownList ID="drpd_Resend" runat="server" AutoPostBack="True" 
                    Visible="False"  >
                    <asp:ListItem>-----</asp:ListItem>
                    <asp:ListItem Value="06">Both</asp:ListItem>
                    <asp:ListItem Value="16">Depositor</asp:ListItem>
                    <asp:ListItem Value="26">Beneficiary</asp:ListItem>
                    
                </asp:DropDownList>
    
                <br />
     <div id="search" style="background-color: #339933;width:1363px">
						<div  style="height: 175px; text-align: left; width: 1358px;">
                        &nbsp;&nbsp;
                        <fieldset style="border:thin solid White ; color:White;width:330px; height: auto ;" >
                        <legend style="color: #FFFFFF" >Alerting </legend>
                        <table style="height: auto; width: 330px;" >
                        <tbody  >
                        <tr>
                            <td class="style11"><asp:Label ID="Label58" runat="server" Font-Bold="True" ForeColor="White" 
                                Text="Transaction Alert Status:"></asp:Label>
                            </td>
                            <td class="style11">
                                <asp:TextBox ID="Lbl_TAS" runat="server" ReadOnly="True"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>    
                            <td class="style20">Transaction Alert DateTime:</td>
                            <td class="style12">
                                <asp:TextBox ID="Lbl_ADT" runat="server" ReadOnly="True"></asp:TextBox>
                            </td>
                            </tr>
                         <tr>
                            
                            <td>
                                <b>Beneficiary Alert Status:</b><br />
                             </td>
                            <td>
                                <asp:TextBox ID="Lbl_BAS" runat="server" ReadOnly="True"></asp:TextBox>
                                <br />
                             </td>
                        </tr>
                         <tr>
                            
                            <td>
                                <b>Beneficiary Alert DateTime:</b></td>
                            <td>
                                <asp:TextBox ID="Lbl_BADT" runat="server" ReadOnly="True"></asp:TextBox>
                             </td>
                        </tr>
                         <tr>
                            
                            <td>
                                <b>Resend Alert Status:</b></td>
                            <td>
                                <asp:TextBox ID="Lbl_RAS" runat="server" ReadOnly="True"></asp:TextBox>
                             </td>
                        </tr>
                         <tr>
                            
                            <td>
                                <b>Resend Alert DateTime:</b></td>
                            <td>
                                <asp:TextBox ID="Lbl_RADT" runat="server" ReadOnly="True"></asp:TextBox>
                             </td>
                        </tr>
                        </tbody>
                        </table>
                        </fieldset>&nbsp;
                         <fieldset style="border:thin solid White; color:White; height: auto;width:355px;" >
                        <legend style="color: #FFFFFF" >Transaction Data</legend>
                        <table style="width: 355px; height: auto;" >
                        <tbody  >
                        <tr>
                            <td class="style17">Transaction Code:</td>
                            <td class="style18">
                                <asp:TextBox ID="Lbl_TC" runat="server" ReadOnly="True"></asp:TextBox>
                                                </td>
                        </tr>
                        <tr>    
                            <td class="style15">Beneficiary Mobile:</td>
                            <td class="style16">
                                <asp:TextBox ID="Lbl_BM" runat="server" ReadOnly="True"></asp:TextBox>
                            </td>
                            </tr>
                         <tr>
                            
                            <td class="style13">Depositor Mobile:</td>
                            <td class="style14">
                                <asp:TextBox ID="Lbl_DM" runat="server" ReadOnly="True"></asp:TextBox>
                             </td>
                        </tr>
                        
                        
                         <tr>
                            
                            <td class="style15">Transaction Sequence:</td>
                            <td class="style16">
                                <asp:TextBox ID="Lbl_TRXSeq" runat="server" ReadOnly="True"></asp:TextBox>
                             </td>
                        </tr>
                        
                        
                         <tr>
                            
                            <td class="style9">Amount:</td>
                            <td class="style10">
                                <asp:TextBox ID="Lbl_AMT" runat="server" ReadOnly="True"></asp:TextBox>
                             </td>
                        </tr>
                        
                        
                        </tbody>
                        </table>
                        </fieldset>&nbsp;
                        <fieldset style="border:thin solid White; color:White;width:345px; height: auto;" >
                        <legend style="color: #FFFFFF" >Transaction Status </legend>
                        <table style="height: auto; width: 345px;" >
                        <tbody  >
                        <tr>
                            <td class="style19"><asp:Label ID="Label56" runat="server" Font-Bold="True" ForeColor="White" 
                                Text="DepositStatus:"></asp:Label></td>
                            <td>
                                <asp:TextBox ID="Lbl_DS" runat="server" ReadOnly="True"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>    
                            <td class="style19">&nbsp;<asp:Label ID="Label59" runat="server" Font-Bold="True" ForeColor="White" 
                                Text="Withdrawal Status:"></asp:Label></td>
                            <td>
                                <asp:TextBox ID="Lbl_WS" runat="server" ReadOnly="True"></asp:TextBox>
                                                </td>
                        </tr>
                        <tr>
                            <td class="style21">Deposit DateTime:</td>
                            <td>
                                <asp:TextBox ID="Lbl_DDT" runat="server" ReadOnly="True"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="style21">Withdrawal DateTime:</td>
                            <td>
                                <asp:TextBox ID="Lbl_WDT" runat="server" ReadOnly="True"></asp:TextBox>
                            </td>
                        </tr>
                        </tbody>
                        </table>
                        </fieldset>&nbsp;
                       
                         
                        <fieldset style="border:thin solid White ; color:White;width:220px; height: auto ;" >
                        <legend style="color: #FFFFFF" >Transaction Source </legend>
                        <table style="height: auto " >
                        <tbody  >
                        <tr>
                            <td class="style11"><asp:Label ID="Label53" runat="server" Font-Bold="True" ForeColor="White" 
                                Text="Country:"></asp:Label></td>
                            <td class="style11">
                                <asp:TextBox ID="Lbl_CC" runat="server" ReadOnly="True"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>    
                            <td class="style12"><asp:Label ID="Label54" runat="server" Font-Bold="True" ForeColor="White" 
                                Text="Bank:"></asp:Label></td>
                            <td class="style12">
                                <asp:TextBox ID="Lbl_BC" runat="server" ReadOnly="True"></asp:TextBox>
                            </td>
                            </tr>
                         <tr>
                            
                            <td><asp:Label ID="Label55" runat="server" Font-Bold="True" ForeColor="White" 
                                Text="ATM:"></asp:Label>
                                <br />
                             </td>
                            <td>
                                <asp:TextBox ID="Lbl_AId" runat="server" ReadOnly="True"></asp:TextBox>
                                <br />
                             </td>
                        </tr>
                        </tbody>
                        </table>
                        </fieldset>&nbsp;
                        
                        
                                <br />
                            <br />
                            <br />
                            <br />
                        <asp:Label ID="Lbl_Status" runat="server" Font-Bold="True" Font-Size="Medium" 
                            ForeColor="Red" Visible="False"></asp:Label>
                            <br />
                        <asp:Label ID="Lbl_TH" runat="server" Font-Bold="True" Font-Size="Medium" 
                            ForeColor="Black" Visible="False">Transaction History:</asp:Label>
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                                DataSourceID="SqlDataSource1">
                                <RowStyle CssClass="RowStyle" />
                                <Columns>
                                    <asp:BoundField DataField="Action" HeaderText="Action" 
                                        SortExpression="Action" >
                                        <HeaderStyle Width="150px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ActionDateTime" HeaderText="ActionDateTime" 
                                        SortExpression="ActionDateTime">
                                        <HeaderStyle Width="190px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ActionReason" HeaderText="ActionReason" 
                                        SortExpression="ActionReason" />
                                    <asp:BoundField DataField="ActionStatus" HeaderText="ActionStatus" 
                                        SortExpression="ActionStatus" />
                                    <asp:BoundField DataField="DispensedNotes" HeaderText="DispensedNotes" 
                                        SortExpression="DispensedNotes" />
                                    <asp:BoundField DataField="DispensedAmount" HeaderText="DispensedAmount" 
                                        SortExpression="DispensedAmount" />
                                    <asp:BoundField DataField="Cassette3" HeaderText="Cassette3" 
                                        SortExpression="Cassette3" />
                                    <asp:BoundField DataField="Cassette2" HeaderText="Cassette2" 
                                        SortExpression="Cassette2" />
                                    <asp:BoundField DataField="Cassette1" HeaderText="Cassette1" 
                                        SortExpression="Cassette1" />
                                    <asp:BoundField DataField="CommissionAmount" HeaderText="CommissionAmount" 
                                        SortExpression="CommissionAmount" />
                                    <asp:BoundField DataField="Cassette4" HeaderText="Cassette4" 
                                        SortExpression="Cassette4" />
                                    <asp:BoundField DataField="CommissionValue" HeaderText="CommissionValue" 
                                        SortExpression="CommissionValue" />
                                    <asp:BoundField DataField="CommissionPercent" HeaderText="CommissionPercent" 
                                        SortExpression="CommissionPercent" />
                                </Columns>
                    <PagerStyle CssClass="PagerStyle" />
                    <SelectedRowStyle CssClass="SelectedRowStyle" />
                    <HeaderStyle CssClass="HeaderStyle" />
                    <EditRowStyle CssClass="EditRowStyle" />
                    <AlternatingRowStyle CssClass="AltRowStyle" />
                            </asp:GridView>
                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                                ConnectionString="<%$ ConnectionStrings:NCRMoneyFerConnection %>" 
                                SelectCommand=" SELECT (select RequestTypeDescription from requesttype where RequestTypeCode=[Action])[Action], ActionDateTime, ActionReason, ActionStatus, DispensedNotes, DispensedAmount, Cassette3, Cassette2, Cassette1, CommissionAmount, Cassette4, CommissionValue, CommissionPercent 
FROM TransactionNestedActions 
WHERE TransactionCode = @TransactionCode
order by ActionDateTime ">
                                <SelectParameters>
                                    <asp:Parameter Name="TransactionCode" Type="String" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                            </div>
					</div>
                <br />
        </div>
    </form>
</body>
</html>
