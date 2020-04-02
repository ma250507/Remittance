<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Users-AddNormal.aspx.vb" Inherits="Users_AddNormal" title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    </asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder4" Runat="Server">
    <asp:ImageButton ID="UsersConBtn" runat="server" 
    ImageUrl="~/Images/UsersConfirm.jpg" Visible="False" />
    <br />
    <asp:Label ID="Lbl_Status" runat="server" Font-Bold="True" Font-Size="Medium" 
    ForeColor="Red" Visible="False"></asp:Label>
    <asp:Label ID="Lbl_Notification" runat="server" Font-Bold="True" Font-Size="Medium" 
    ForeColor="#0083C1" Visible="False"></asp:Label>
    <asp:GridView ID="GridView3" runat="server" AllowPaging="True" 
                            AutoGenerateColumns="False" BackColor="#F1F1F1" BorderStyle="Outset" 
                            DataKeyNames="UserId" DataSourceID="SqlDataSource1" GridLines="None" 
                            ShowFooter="True" AllowSorting="True" Width="1050px">
        <PagerSettings Mode="NumericFirstLast" />
        <RowStyle BackColor="Gainsboro" HorizontalAlign="Center" 
                                VerticalAlign="Middle" />
        <Columns>
            <asp:CommandField ButtonType="Image" CancelImageUrl="~/Images/cancel_icon.png" 
                DeleteImageUrl="~/Images/delete_icon.gif" EditImageUrl="~/Images/EditIcon.png" 
                ShowDeleteButton="True" ShowEditButton="True" 
                UpdateImageUrl="~/Images/confirmation_icon.png" />
            <asp:TemplateField>
                <FooterTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" 
                        CommandName="NonEmpty" OnClick="ADD_Click" Text="ADD"></asp:LinkButton>
                </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="FName\LName" SortExpression="UserId">
                <EditItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("UserId") %>'></asp:Label>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="Txt_UserId" runat="server"></asp:TextBox>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("UserId") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="UserName" SortExpression="UserName">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("UserName") %>'></asp:TextBox>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="Txt_UserName" runat="server"></asp:TextBox>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("UserName") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Password" SortExpression="Password">
                <FooterTemplate>
                    <asp:TextBox ID="Txt_Password" runat="server" TextMode="Password"></asp:TextBox>
                </FooterTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox3" runat="server" Text='<%# Eval("Password") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("Password") %>' 
                        Visible="False"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Group" SortExpression="Group_ID">
                <EditItemTemplate>
                    <asp:Label ID="Label6" runat="server" Text='<%# Bind("Group_ID") %>'></asp:Label>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("Group_ID") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="CountryCode" SortExpression="CountryCode">
                <FooterTemplate>
                    <asp:DropDownList ID="drpd_CountryCode" runat="server" 
                        DataSourceID="CountryCodeADS" DataTextField="CountryName" 
                        DataValueField="CountryCode">
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="CountryCodeADS" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:NCRMoneyFerConnection %>" 
                        SelectCommand="SELECT [CountryCode], [CountryName] FROM [Country]">
                    </asp:SqlDataSource>
                </FooterTemplate>
                <EditItemTemplate>
                    <asp:DropDownList ID="drpd_CountryCode" runat="server" 
                        DataSourceID="CountryCodeEDS" DataTextField="CountryName" 
                        DataValueField="CountryCode" SelectedValue='<%# Bind("CountryCode") %>'>
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="CountryCodeEDS" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:NCRMoneyFerConnection %>" 
                        SelectCommand="SELECT [CountryCode], [CountryName] FROM [Country]">
                    </asp:SqlDataSource>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label6" runat="server" Text='<%# Bind("CountryCode") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="BankCode" SortExpression="BankCode">
                <FooterTemplate>
                    <asp:DropDownList ID="drpd_BankCode" runat="server" DataSourceID="BankCodeEDS" 
                        DataTextField="BankName" DataValueField="BankCode">
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="BankCodeEDS" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:NCRMoneyFerConnection %>" 
                        SelectCommand="SELECT [BankCode], [BankName] FROM [Bank]">
                    </asp:SqlDataSource>
                </FooterTemplate>
                <EditItemTemplate>
                    <asp:DropDownList ID="drpd_BankCode" runat="server" DataSourceID="BankCodeEDS" 
                        DataTextField="BankName" DataValueField="BankCode" 
                        SelectedValue='<%# Bind("BankCode") %>'>
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="BankCodeEDS" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:NCRMoneyFerConnection %>" 
                        SelectCommand="SELECT [BankCode], [BankName] FROM [Bank]">
                    </asp:SqlDataSource>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label7" runat="server" Text='<%# Bind("BankCode") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Branch" SortExpression="Branch">
                <FooterTemplate>
                    <asp:DropDownList ID="drpd_Br" runat="server" DataSourceID="NormalBRDS" 
                        DataTextField="BranchName" DataValueField="BranchName">
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="NormalBRDS" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:NCRMoneyFerConnection %>" 
                        SelectCommand="SELECT [BranchName] FROM [Branches]"></asp:SqlDataSource>
                </FooterTemplate>
                <EditItemTemplate>
                    <asp:DropDownList ID="drpd_Br" runat="server" DataSourceID="NormalBRDS" 
                        DataTextField="BranchName" DataValueField="BranchName" 
                        SelectedValue='<%# Bind("Branch") %>'>
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="NormalBRDS" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:NCRMoneyFerConnection %>" 
                        SelectCommand="SELECT [BranchName] FROM [Branches]"></asp:SqlDataSource>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label5" runat="server" Text='<%# Bind("Branch") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="AllATMs" SortExpression="AllATMs">
                <FooterTemplate>
                    <asp:CheckBox ID="CHB_AllATMs_ADD" runat="server" />
                </FooterTemplate>
                <EditItemTemplate>
                    <asp:CheckBox ID="CHB_AllATMs_EDIT" runat="server" 
                        Checked='<%# CheckNULL(Eval("AllATMs")) %>' />
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label13" runat="server" Text='<%# Bind("AllATMs") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <FooterStyle BackColor="White" HorizontalAlign="Center" 
                                VerticalAlign="Middle" />
        <PagerStyle HorizontalAlign="Center" />
        <EmptyDataTemplate>
            <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" 
                CommandName="New" OnClick="LinkButton1_Click" Text="ADD"></asp:LinkButton>
            <br />
            <table style="width: 100%">
                <tr>
                    <td>
                        <asp:Label ID="Label7" runat="server" Text="FName\LName:"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label8" runat="server" Text="UserName:"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label9" runat="server" Text="Password"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label10" runat="server" Text="CountryCode"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label11" runat="server" Text="BankCode"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label12" runat="server" Text="Branch"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label13" runat="server" Text="AllATMs"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="TXTEUID" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="TXTEUName" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="TXTEPassword" runat="server" TextMode="Password"></asp:TextBox>
                    </td>
                    <td>
                        <asp:DropDownList ID="drpd_CountryCode" runat="server" 
                            DataSourceID="CountryCodeEDS" DataTextField="CountryName" 
                            DataValueField="CountryCode">
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="CountryCodeEDS" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:NCRMoneyFerConnection %>" 
                            SelectCommand="SELECT [CountryCode], [CountryName] FROM [Country]">
                        </asp:SqlDataSource>
                    </td>
                    <td>
                        <asp:DropDownList ID="drpd_BankCode" runat="server" DataSourceID="BankCodeEDS" 
                            DataTextField="BankName" DataValueField="BankCode">
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="BankCodeEDS" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:NCRMoneyFerConnection %>" 
                            SelectCommand="SELECT [BankCode], [BankName] FROM [Bank]">
                        </asp:SqlDataSource>
                    </td>
                    <td>
                        <asp:DropDownList ID="drpd_Br" runat="server" DataSourceID="NormalBRDS" 
                            DataTextField="BranchName" DataValueField="BranchName">
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="NormalBRDS" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:NCRMoneyFerConnection %>" 
                            SelectCommand="SELECT [BranchName] FROM [Branches]"></asp:SqlDataSource>
                    </td>
                    <td>
                        <asp:CheckBox ID="CHB_AllATMs_eADD" runat="server" />
                    </td>
                </tr>
            </table>
        </EmptyDataTemplate>
        <HeaderStyle BackColor="#0083C1" ForeColor="White" HorizontalAlign="Center" 
                                VerticalAlign="Middle" />
        <AlternatingRowStyle BackColor="White" />
    </asp:GridView>
    <br />
    
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:NCRMoneyFerConnection %>" 
                            OldValuesParameterFormatString="original_{0}" 
                            
        SelectCommand="SELECT [UserId], [UserName], [Password],  (SELECT     name
                             FROM         groups
                             WHERE     id = users.group_ID) AS [Group_ID] ,CountryCode,BankCode , Branch,AllATMs
FROM [Users]
 where group_id=@group" 
        DeleteCommand="DELETE FROM [Users] WHERE [UserId] = @original_UserId" 
        InsertCommand="INSERT INTO [Users] ([UserId], [UserName], [Password], [Group_ID],[Branch],[CountryCode],[BankCode],[AllATMs]) VALUES (@UserId, @UserName, @Password, @Group_ID,@Branch,@CountryCode,@BankCode,@AllATMs)" 
        
        
        
        
        
        
        
    UpdateCommand="UPDATE [Users] SET [UserName] = @UserName, [Password] = @Password , [Branch] = @Branch , [FirstTime]=@FirstTime ,[CountryCode] =@CountryCode ,[BankCode]=@BankCode , [AllATMs]=@AllATMs
 WHERE [UserId] = @original_UserId">
        <SelectParameters>
            <asp:SessionParameter Name="group" SessionField="GroupId" DefaultValue="" />
        </SelectParameters>
        <DeleteParameters>
            <asp:Parameter Name="original_UserId" Type="String" />
        </DeleteParameters>
        <UpdateParameters>
            <asp:Parameter Name="UserName" Type="String" />
            <asp:Parameter Name="Password" Type="String" />
            <%--<asp:Parameter Name="Group_ID" Type="String" />--%>
            <asp:Parameter Name="Branch" />
            <asp:Parameter Name="FirstTime" />
            <asp:Parameter Name="CountryCode" />
            <asp:Parameter Name="BankCode" />
            <asp:Parameter Name="AllATMs" />
            <asp:Parameter Name="original_UserId" Type="String" />
        </UpdateParameters>
        <InsertParameters>
            <asp:Parameter Name="UserId" Type="String" />
            <asp:Parameter Name="UserName" Type="String" />
            <asp:Parameter Name="Password" Type="String" />
            <asp:Parameter Name="Group_ID" Type="String" />
            <asp:Parameter Name="Branch" />
            <asp:Parameter Name="CountryCode" />
            <asp:Parameter Name="BankCode" />
            <asp:Parameter Name="AllATMs" />
        </InsertParameters>
    </asp:SqlDataSource>
</asp:Content>

