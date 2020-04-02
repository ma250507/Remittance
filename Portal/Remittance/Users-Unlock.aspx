<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Users-Unlock.aspx.vb" Inherits="Users_Unlock" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder4" Runat="Server">
    <asp:GridView ID="GridView3" runat="server" AllowPaging="True" 
                            AutoGenerateColumns="False" BackColor="#F1F1F1" 
        BorderStyle="Outset" DataSourceID="SqlDataSource1" GridLines="None" 
                            ShowFooter="True" AllowSorting="True" Width="534px" 
        DataKeyNames="UserId">
        <PagerSettings Mode="NumericFirstLast" />
        <RowStyle BackColor="Gainsboro" HorizontalAlign="Center" 
                                VerticalAlign="Middle" />
        <Columns>
            <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/delete_icon.gif" 
                ShowDeleteButton="True" />
            <asp:BoundField DataField="UserId" HeaderText="UserId" 
                SortExpression="UserId" ReadOnly="True" />
            <asp:BoundField DataField="UserName" HeaderText="UserName" 
                SortExpression="UserName" />
        </Columns>
        <FooterStyle BackColor="White" HorizontalAlign="Center" 
                                VerticalAlign="Middle" />
        <PagerStyle HorizontalAlign="Center" />
        <HeaderStyle BackColor="#0083C1" ForeColor="White" HorizontalAlign="Center" 
                                VerticalAlign="Middle" />
        <AlternatingRowStyle BackColor="White" />
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:NCRMoneyFerConnection %>" 
        SelectCommand="SELECT [UserId], [UserName] FROM [AllUsers] WHERE ([Locked] = @Locked)"
        DeleteCommand ="Update users set locked= 'False' where userid=@userid">
        <SelectParameters>
            <asp:Parameter DefaultValue="True" Name="Locked" Type="Boolean" />
        </SelectParameters>
        <DeleteParameters>
            <asp:Parameter DefaultValue="" Name="UserId" Type="String" />
        </DeleteParameters>
    </asp:SqlDataSource>
</asp:Content>

