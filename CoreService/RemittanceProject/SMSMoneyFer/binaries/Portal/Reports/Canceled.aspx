<%@ page language="VB" autoeventwireup="false" inherits="Canceled, App_Web_uiurv8lk" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>NCR Moneyfer Portal-Cancelled Transaction Report</title>
        <link href="style.css" rel="stylesheet" type="text/css" media="screen" />

    <style type="text/css">
        .style1
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
    <div class="style1">
    
        <div class="style1">
        <asp:Label ID="Label1" runat="server" Font-Size="Large" 
            style="font-weight: 700" Text="Canceled Transactions"></asp:Label>
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
            AllowSorting="True" AutoGenerateColumns="False" AutoGenerateSelectButton="True" 
            DataKeyNames="TransactionCode" DataSourceID="SqlDataSource1" 
            EmptyDataText="NOOOOOO" style="margin-left: 126px">
            <Columns>
                <asp:BoundField DataField="TransactionCode" HeaderText="TransactionCode" 
                    ReadOnly="True" SortExpression="TransactionCode" />
                <asp:BoundField DataField="ATMID" HeaderText="ATMID" SortExpression="ATMID" />
                <asp:BoundField DataField="RequestType" 
                    HeaderText="RequestType" SortExpression="RequestType" />
                <asp:BoundField DataField="DepositorMobile" HeaderText="DepositorMobile" 
                    SortExpression="DepositorMobile" />
                <asp:BoundField DataField="BeneficiaryMobile" HeaderText="BeneficiaryMobile" 
                    SortExpression="BeneficiaryMobile" />
                <asp:BoundField DataField="Amount" HeaderText="Amount" 
                    SortExpression="Amount" />
                <asp:BoundField DataField="DepositDateTime" HeaderText="DepositDateTime" 
                    SortExpression="DepositDateTime" />
            </Columns>
            <RowStyle CssClass="RowStyle" />
    <EmptyDataRowStyle CssClass="EmptyRowStyle" />
    <PagerStyle CssClass="PagerStyle" />
    <SelectedRowStyle CssClass="SelectedRowStyle" />
    <HeaderStyle CssClass="HeaderStyle" />
    <EditRowStyle CssClass="EditRowStyle" />
    <AlternatingRowStyle CssClass="AltRowStyle" />
        </asp:GridView>
        <asp:Label ID="Lbl_Actions" runat="server" Font-Bold="True" Text="Actions :" 
            Visible="False"></asp:Label>
        <asp:GridView ID="GridView2" runat="server" AllowPaging="True" 
            AllowSorting="True" DataSourceID="SqlDataSource2">
            <RowStyle CssClass="RowStyle" />
    <EmptyDataRowStyle CssClass="EmptyRowStyle" />
    <PagerStyle CssClass="PagerStyle" />
    <SelectedRowStyle CssClass="SelectedRowStyle" />
    <HeaderStyle CssClass="HeaderStyle" />
    <EditRowStyle CssClass="EditRowStyle" />
    <AlternatingRowStyle CssClass="AltRowStyle" />
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:NCRMoneyFerConnection %>" SelectCommand="select TransactionCode,ATMID,requesttypedescription as RequestType ,DepositorMobile,BeneficiaryMobile,Amount,DepositDateTime 
from transactions  ,RequestType 
where atmid like  @atm and cancelstatus='CANCELED' 
and canceldatetime between @From and @To     
and RequestTypecode=transactions.requesttype ">
            <SelectParameters>
                <asp:SessionParameter Name="atm" SessionField="atm" />
                <asp:SessionParameter DefaultValue="" Name="From" SessionField="From" />
                <asp:SessionParameter DefaultValue="" Name="To" SessionField="To" />
            </SelectParameters>
        </asp:SqlDataSource>
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
    </div>
    </form>
</body>
</html>
