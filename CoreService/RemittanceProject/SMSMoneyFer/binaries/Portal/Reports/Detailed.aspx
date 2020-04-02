<%@ page language="VB" autoeventwireup="false" inherits="Detailed, App_Web_uiurv8lk" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>NCR Moneyfer Portal-Detailed Transactions Report</title>
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
            style="font-weight: 700" Text="Detailed Transactions"></asp:Label>
        <br />
        <b>From: </b>
        <asp:Label ID="Label2" runat="server"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <b>To:</b>
        <asp:Label ID="Label3" runat="server"></asp:Label>
    
        </div>
    
        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
            AllowSorting="True" AutoGenerateColumns="False" 
            AutoGenerateSelectButton="True" DataKeyNames="transactioncode" 
            DataSourceID="SqlDataSource1">
            <Columns>
                <asp:BoundField DataField="transactioncode" HeaderText="TransactionCode" 
                    ReadOnly="True" SortExpression="transactioncode" />
                <asp:BoundField DataField="countrycode" HeaderText="CountryCode" 
                    SortExpression="countrycode" />
                <asp:BoundField DataField="bankcode" HeaderText="BankCode" 
                    SortExpression="bankcode" />
                <asp:BoundField DataField="atmid" HeaderText="AtmId" SortExpression="atmid" />
                <asp:BoundField DataField="requesttype" HeaderText="RequestType" 
                    SortExpression="requesttype" />
                <asp:BoundField DataField="atmdate" HeaderText="AtmDate" 
                    SortExpression="atmdate" />
                <asp:BoundField DataField="atmtime" HeaderText="AtmTime" 
                    SortExpression="atmtime" />
                <asp:BoundField DataField="atmtrxsequence" HeaderText="AtmTrxSequence" 
                    SortExpression="atmtrxsequence" />
                <asp:BoundField DataField="depositormobile" HeaderText="DepositorMobile" 
                    SortExpression="depositormobile" />
                <asp:BoundField DataField="beneficiarymobile" HeaderText="BeneficiaryMobile" 
                    SortExpression="beneficiarymobile" />
                <asp:BoundField DataField="amount" HeaderText="Amount" 
                    SortExpression="amount" />
                <asp:BoundField DataField="currencycode" HeaderText="CurrencyCode" 
                    SortExpression="currencycode" />
                <asp:BoundField DataField="depositdatetime" HeaderText="DepositDateTime" 
                    SortExpression="depositdatetime" />
                <asp:BoundField DataField="depositactionreason" 
                    HeaderText="DepositActionReason" SortExpression="depositactionreason" />
                <asp:BoundField DataField="depositstatus" HeaderText="DepositStatus" 
                    SortExpression="depositstatus" />
                <asp:BoundField DataField="withdrawalstatus" HeaderText="WithdrawalStatus" 
                    SortExpression="withdrawalstatus" />
                <asp:BoundField DataField="withdrawaldatetime" HeaderText="WithdrawalDateTime" 
                    SortExpression="withdrawaldatetime" />
                <asp:BoundField DataField="cancelstatus" HeaderText="CancelStatus" 
                    SortExpression="cancelstatus" />
                <asp:BoundField DataField="canceldatetime" HeaderText="CancelDateTime" 
                    SortExpression="canceldatetime" />
                <asp:BoundField DataField="overallstatus" HeaderText="OverallStatus" 
                    SortExpression="overallstatus" />
                <asp:BoundField DataField="smssendingstatus" HeaderText="SMSSendingStatus" 
                    SortExpression="smssendingstatus" />
                <asp:BoundField DataField="smssentdatetime" HeaderText="SMSSentDateTime" 
                    SortExpression="smssentdatetime" />
            </Columns>
             <RowStyle CssClass="RowStyle" />
    <EmptyDataRowStyle CssClass="EmptyRowStyle" />
    <PagerStyle CssClass="PagerStyle" />
    <SelectedRowStyle CssClass="SelectedRowStyle" />
    <HeaderStyle CssClass="HeaderStyle" />
    <EditRowStyle CssClass="EditRowStyle" />
    <AlternatingRowStyle CssClass="AltRowStyle" />
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:NCRMoneyFerConnection %>" 
            SelectCommand="Detailed" SelectCommandType="StoredProcedure">
            <SelectParameters>
                <asp:SessionParameter Name="From" SessionField="DFrom" Size="8" 
                    Type="DateTime" />
                <asp:SessionParameter DefaultValue="" Name="To" SessionField="DTo" Size="8" 
                    Type="DateTime" />
                <asp:SessionParameter DefaultValue="" Name="Atmid" SessionField="atm" 
                    Type="String" />
            </SelectParameters>
        </asp:SqlDataSource>
    
    </div>
    </div>
    </form>
</body>
</html>
