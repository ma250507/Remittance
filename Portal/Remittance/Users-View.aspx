<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Users-View.aspx.vb" Inherits="Users_View" title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder4" Runat="Server">
    <br />
    <asp:GridView ID="GridView3" runat="server" AllowPaging="True" 
                            AutoGenerateColumns="False" BackColor="#F1F1F1" 
        BorderStyle="Outset" DataSourceID="SqlDataSource1" GridLines="None" 
                            ShowFooter="True" AllowSorting="True" Width="1050px">
        <PagerSettings Mode="NumericFirstLast" />
        <RowStyle BackColor="Gainsboro" HorizontalAlign="Center" 
                                VerticalAlign="Middle" />
        <Columns>
            <asp:BoundField DataField="UserId" HeaderText="FName/LName" 
                SortExpression="UserId" />
            <asp:BoundField DataField="UserName" HeaderText="UserName" 
                SortExpression="UserName" />
            <asp:BoundField DataField="Group" HeaderText="Group" 
                SortExpression="Group" />
            <asp:BoundField DataField="Branch" HeaderText="Branch" 
                SortExpression="Branch" />
            <asp:BoundField DataField="ATM_ID" HeaderText="Terminal ID" 
                SortExpression="ATM_ID" />
            <asp:BoundField DataField="CountryCode" HeaderText="CountryCode" 
                SortExpression="CountryCode" />
            <asp:BoundField DataField="BankCode" HeaderText="BankCode" 
                SortExpression="BankCode" />
            <asp:CheckBoxField DataField="IsTeller" HeaderText="IsTeller" 
                SortExpression="IsTeller" />
        </Columns>
        <FooterStyle BackColor="White" HorizontalAlign="Center" 
                                VerticalAlign="Middle" />
        <PagerStyle HorizontalAlign="Center" />
        <HeaderStyle BackColor="#0083C1" ForeColor="White" HorizontalAlign="Center" 
                                VerticalAlign="Middle" />
        <AlternatingRowStyle BackColor="White" />
    </asp:GridView>
    <br />
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:NCRMoneyFerConnection %>" 
                            OldValuesParameterFormatString="original_{0}" 
                            
        SelectCommand="SELECT * FROM [AllUsers]">
    </asp:SqlDataSource>
</asp:Content>

