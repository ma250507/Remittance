<%@ page language="VB" autoeventwireup="false" inherits="ETrx, App_Web_uiurv8lk" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>NCR Moneyfer Portal-Expired Transactions Report</title>
        <link href="style.css" rel="stylesheet" type="text/css" media="screen" />

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
    <div style="text-align: center">
    
        <asp:Label ID="Label1" runat="server" Font-Size="Large" 
            style="font-weight: 700" Text="Expired Transactions"></asp:Label>
        <br />
        <b>From: </b>
        <asp:Label ID="Label2" runat="server"></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <b>To:</b>
        <asp:Label ID="Label3" runat="server"></asp:Label>
    
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <b>ATM:</b>
        <asp:Label ID="Label6" runat="server"></asp:Label>
&nbsp;<asp:Button ID="Btn_Print" runat="server" style="font-weight: 700" Text="Print" />
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:NCRMoneyFerConnection %>" 
            
            SelectCommand="select TransactionCode,AtmId,DepositorMobile,BeneficiaryMobile,Amount 
 from transactions 
 where atmid like @atm and depositdatetime  between @From and @To and depositstatus ='EXPIRED' and withdrawalstatus='EXPIRED' and cancelstatus is null ">
            <SelectParameters>
                <asp:Parameter Name="atm" />
                <asp:Parameter Name="From" />
                <asp:Parameter Name="To" />
            </SelectParameters>
        </asp:SqlDataSource>
        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
            AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="transactioncode" 
            DataSourceID="SqlDataSource1" style="margin-left: 0px">
            <Columns>
                <asp:CommandField ShowSelectButton="True" />
                <asp:BoundField DataField="transactioncode" HeaderText="TransactionCode" 
                    ReadOnly="True" SortExpression="transactioncode" />
                <asp:BoundField DataField="atmid" HeaderText="AtmId" SortExpression="atmid" />
                <asp:BoundField DataField="depositormobile" HeaderText="DepositorMobile" 
                    SortExpression="depositormobile" />
                <asp:BoundField DataField="beneficiarymobile" HeaderText="BeneficiaryMobile" 
                    SortExpression="beneficiarymobile" />
                <asp:BoundField DataField="amount" HeaderText="Amount" 
                    SortExpression="amount" />
            </Columns>
            <RowStyle CssClass="RowStyle" />
    <PagerStyle CssClass="PagerStyle" />
    <SelectedRowStyle CssClass="SelectedRowStyle" />
    <HeaderStyle CssClass="HeaderStyle" />
    <EditRowStyle CssClass="EditRowStyle" />
    <AlternatingRowStyle CssClass="AltRowStyle" />
        </asp:GridView>
&nbsp;<asp:Label ID="Lbl_Actions" runat="server" Font-Bold="True" Text="Actions :" 
            Visible="False"></asp:Label>
        <asp:GridView ID="GridView2" runat="server" AllowPaging="True" 
            AllowSorting="True" DataSourceID="SqlDataSource2" 
            style="margin-left: 4px">
            <RowStyle CssClass="RowStyle" />
    <PagerStyle CssClass="PagerStyle" />
    <SelectedRowStyle CssClass="SelectedRowStyle" />
    <HeaderStyle CssClass="HeaderStyle" />
    <EditRowStyle CssClass="EditRowStyle" />
    <AlternatingRowStyle CssClass="AltRowStyle" />
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
            ConnectionString="<%$ ConnectionStrings:NCRMoneyFerConnection %>" SelectCommand="select TransactionCode,RequestTypeDescription as ActionType,ActionDateTime,ActionReason,ActionStatus,DispensedNotes
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
