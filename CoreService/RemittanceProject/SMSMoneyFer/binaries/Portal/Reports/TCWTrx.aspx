<%@ page language="VB" autoeventwireup="false" inherits="TCWTrx, App_Web_uiurv8lk" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>NCR Moneyfer Portal-Confirmed Withdrawaled Transactions Report</title>
    <link href="style.css" rel="stylesheet" type="text/css" media="screen" />

    <style type="text/css">
        .style1
        {
            text-align: center;
        }
        #form1
        {
            text-align: left;
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
    <div class="style1">
    
    &nbsp;<asp:Label ID="Label1" runat="server" Font-Size="Large" 
            style="font-weight: 700" Text="Confirmed Withdrawaled Transactions"></asp:Label>
        <br />
        <b>From: </b>
        <asp:Label ID="Label2" runat="server"></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <b>To:</b>
        <asp:Label ID="Label3" runat="server"></asp:Label>
    
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <b>ATM:</b>
        <asp:Label ID="Label6" runat="server"></asp:Label>
&nbsp;<asp:Button ID="Btn_Print" runat="server" style="font-weight: 700" Text="Print" />
        </div>
    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
        AllowSorting="True" DataSourceID="SqlDataSource1" 
        style="text-align: center; margin-left: 313px;" 
        DataMember="DefaultView" Height="111px" DataKeyNames="TransactionCode" 
                AutoGenerateColumns="False">
        <Columns>
            <asp:CommandField ShowSelectButton="True" />
            <asp:BoundField DataField="TransactionCode" HeaderText="TransactionCode" 
                ReadOnly="True" SortExpression="TransactionCode" />
            <asp:BoundField DataField="AtmId" HeaderText="AtmId" SortExpression="AtmId" />
            <asp:BoundField DataField="DepositorMobile" HeaderText="DepositorMobile" 
                SortExpression="DepositorMobile" />
            <asp:BoundField DataField="BeneficiaryMobile" HeaderText="BeneficiaryMobile" 
                SortExpression="BeneficiaryMobile" />
            <asp:BoundField DataField="Amount" HeaderText="Amount" 
                SortExpression="Amount" />
            <asp:BoundField DataField="SMSSendingStatus" HeaderText="SMSSendingStatus" 
                SortExpression="SMSSendingStatus" />
            <asp:BoundField DataField="SMSSentDateTime" HeaderText="SMSSentDateTime" 
                SortExpression="SMSSentDateTime" />
        </Columns>
        <RowStyle CssClass="RowStyle" />
    <PagerStyle CssClass="PagerStyle" />
    <SelectedRowStyle CssClass="SelectedRowStyle" />
    <HeaderStyle CssClass="HeaderStyle" />
    <EditRowStyle CssClass="EditRowStyle" />
    <AlternatingRowStyle CssClass="AltRowStyle" />
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:NCRMoneyFerConnection %>" 
        
        SelectCommand="select TransactionCode,AtmId,DepositorMobile,BeneficiaryMobile,Amount,SMSSendingStatus,SMSSentDateTime 
from transactions 
where atmid like @atm and withdrawalstatus ='CONFIRMED' and depositdatetime between @From and @To">
        <SelectParameters>
            <asp:Parameter Name="atm" />
            <asp:Parameter Name="From" />
            <asp:Parameter Name="To" />
        </SelectParameters>
    </asp:SqlDataSource>
    &nbsp;&nbsp;
    <div style="height: 15px; width: 56px; margin-left: 487px">
        <asp:Label ID="Lbl_Actions" runat="server" Font-Bold="True" Text="Actions :" 
        Visible="False" style="text-align: center"></asp:Label>
    </div>
&nbsp;<asp:GridView ID="GridView2" runat="server" AllowPaging="True" 
        AllowSorting="True" DataSourceID="SqlDataSource2" 
        style="text-align: center; margin-left: 302px;" AutoGenerateColumns="False">
        <RowStyle CssClass="RowStyle" />
                <Columns>
                    <asp:BoundField DataField="TransactionCode" HeaderText="TransactionCode" 
                        SortExpression="TransactionCode" />
                    <asp:BoundField DataField="ActionType" HeaderText="ActionType" 
                        SortExpression="ActionType" />
                    <asp:BoundField DataField="ActionDateTime" HeaderText="ActionDateTime" 
                        SortExpression="ActionDateTime" />
                    <asp:BoundField DataField="ActionReason" HeaderText="ActionReason" 
                        SortExpression="ActionReason" />
                    <asp:BoundField DataField="ActionStatus" HeaderText="ActionStatus" 
                        SortExpression="ActionStatus" />
                    <asp:BoundField DataField="DispensedNotes" HeaderText="DispensedNotes" 
                        SortExpression="DispensedNotes" />
                </Columns>
    <PagerStyle CssClass="PagerStyle" />
    <SelectedRowStyle CssClass="SelectedRowStyle" />
    <HeaderStyle CssClass="HeaderStyle" />
    <EditRowStyle CssClass="EditRowStyle" />
    <AlternatingRowStyle CssClass="AltRowStyle" />
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
        ConnectionString="<%$ ConnectionStrings:NCRMoneyFerConnection %>" SelectCommand="
select TransactionCode,RequestTypeDescription as ActionType,ActionDateTime,ActionReason,ActionStatus,DispensedNotes
from transactionnestedactions inner join requesttype on transactionnestedactions.[action]=requesttype.requesttypecode
where transactioncode=@TrxCode and requesttypecode=[Action]">
        <SelectParameters>
            <asp:Parameter Name="TrxCode" />
        </SelectParameters>
    </asp:SqlDataSource>
    </div>
    </form>
</body>
</html>
