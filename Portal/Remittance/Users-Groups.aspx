<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Users-Groups.aspx.vb" Inherits="Users_Groups" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ImageButton ID="AdminConBTN" runat="server" 
    ImageUrl="~/Images/AdminConfirm.jpg" Visible="False" />
    <br />
    <asp:Label ID="Lbl_Status" runat="server" Font-Bold="True" Font-Size="Medium" 
    ForeColor="Red" Visible="False"></asp:Label>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder4" Runat="Server">
    <asp:GridView ID="GridView2" runat="server" AllowPaging="True" 
                            AutoGenerateColumns="False" BackColor="#F1F1F1" BorderStyle="Outset" 
                            DataKeyNames="ID" DataSourceID="SqlDataSource2" GridLines="None" 
                            ShowFooter="True" AllowSorting="True" Width="1000px">
        <RowStyle BackColor="Gainsboro" HorizontalAlign="Center" 
                                VerticalAlign="Middle" />
        <Columns>
            <asp:CommandField ButtonType="Image" CancelImageUrl="~/Images/cancel_icon.png" 
                                    DeleteImageUrl="~/Images/delete_icon.gif" EditImageUrl="~/Images/EditIcon.png" 
                                    ShowDeleteButton="True" ShowEditButton="True" 
                                    UpdateImageUrl="~/Images/confirmation_icon.png" />
            <asp:TemplateField HeaderText="ID" SortExpression="ID">
                <FooterTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" 
                                            CommandName="New" OnClick="GroupADD_Click" Text="ADD"></asp:LinkButton>
                    <asp:TextBox ID="TXT_GroupId" runat="server"></asp:TextBox>
                </FooterTemplate>
                <EditItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Name" SortExpression="Name">
                <FooterTemplate>
                    <asp:TextBox ID="TXT_GroupName" runat="server"></asp:TextBox>
                </FooterTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Name") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Reports" SortExpression="Reports">
                <FooterTemplate>
                    <asp:CheckBox ID="CB_Reports" runat="server" />
                </FooterTemplate>
                <EditItemTemplate>
                    <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# Bind("Reports") %>' />
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="CheckBox9" runat="server" Checked='<%# Bind("Reports") %>' 
                                            Enabled="false" />
                </ItemTemplate>
            </asp:TemplateField>
              <asp:TemplateField HeaderText="Bulk Transactions Reports" SortExpression="Reports">
                <FooterTemplate>
                    <asp:CheckBox ID="CB_BulkTransactionsReports" runat="server" />
                </FooterTemplate>
                <EditItemTemplate>
                    <asp:CheckBox ID="cb_BulkTransactionsReports_Edit" runat="server" Checked='<%# Bind("BulkTransactionsReports") %>' />
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="cb_BulkTransactionsReports_View" runat="server" Checked='<%# Bind("BulkTransactionsReports") %>' 
                                            Enabled="false" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Maintenance" SortExpression="Maintenance">
                <FooterTemplate>
                    <asp:CheckBox ID="CB_Maintenance" runat="server" />
                </FooterTemplate>
                <EditItemTemplate>
                    <asp:CheckBox ID="CheckBox2" runat="server" 
                                            Checked='<%# Bind("Maintenance") %>' />
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="CheckBox10" runat="server" 
                                            Checked='<%# Bind("Maintenance") %>' Enabled="false" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Administration" SortExpression="Administration">
                <FooterTemplate>
                    <asp:CheckBox ID="CB_Admin" runat="server" />
                </FooterTemplate>
                <EditItemTemplate>
                    <asp:CheckBox ID="CheckBox3" runat="server" 
                                            Checked='<%# Bind("Administration") %>' />
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="CheckBox11" runat="server" 
                                            Checked='<%# Bind("Administration") %>' 
                        Enabled="false" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Users" SortExpression="Users">
                <FooterTemplate>
                    <asp:CheckBox ID="CB_Users" runat="server" />
                </FooterTemplate>
                <EditItemTemplate>
                    <asp:CheckBox ID="CheckBox4" runat="server" Checked='<%# Bind("Users") %>' />
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="CheckBox12" runat="server" Checked='<%# Bind("Users") %>' 
                                            Enabled="false" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Teller" SortExpression="Teller">
                <EditItemTemplate>
                    <asp:CheckBox ID="CheckBox6" runat="server" Checked='<%# Bind("Teller") %>' />
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:CheckBox ID="CB_Teller" runat="server" />
                </FooterTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="CheckBox5" runat="server" Checked='<%# Bind("Teller") %>' 
                                            Enabled="False" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Registeration" SortExpression="Registeration">
                <FooterTemplate>
                    <asp:CheckBox ID="CB_Registeration" runat="server" />
                </FooterTemplate>
                <EditItemTemplate>
                    <asp:CheckBox ID="CheckBox8" runat="server" 
                                            Checked='<%# Bind("Registeration") %>' />
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="CheckBox7" runat="server" 
                                            Checked='<%# Bind("Registeration") %>' Enabled="False" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <FooterStyle BackColor="White" HorizontalAlign="Center" 
                                VerticalAlign="Middle" />
        <HeaderStyle BackColor="#0083C1" ForeColor="White" HorizontalAlign="Center" 
                                VerticalAlign="Middle" />
        <AlternatingRowStyle BackColor="White" />
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                                ConnectionString="<%$ ConnectionStrings:NCRMoneyFerConnection %>" 
                                DeleteCommand="DELETE FROM [Groups] WHERE [ID] = @original_ID" 
                                InsertCommand="INSERT INTO [Groups] ([ID], [Name], [Reports], [Maintenance], [Administration], [Users], [Teller], [Registeration],[BulkTransactionsReports] ) VALUES (@ID, @Name, @Reports, @Maintenance, @Administration, @Users, @Teller,@Registeration,@BulkTransactionsReports)" 
                                OldValuesParameterFormatString="original_{0}" 
                                SelectCommand="SELECT ID, Name, Reports, Maintenance, Administration, Users, Teller, Registeration, BulkTransactionsReports FROM dbo.Groups" 
                                
                                UpdateCommand="UPDATE [Groups] SET [Name] = @Name, [Reports] = @Reports, [Maintenance] = @Maintenance, [Administration] = @Administration, [Users] = @Users, [Teller] = @Teller , [Registeration] = @Registeration,BulkTransactionsReports = @BulkTransactionsReports
WHERE [ID] = @original_ID">
                                <DeleteParameters>
                                    <asp:Parameter Name="original_ID" Type="String" />
                                </DeleteParameters>
                                <UpdateParameters>
                                    <asp:Parameter Name="Name" Type="String" />
                                    <asp:Parameter Name="Reports" Type="Boolean" />
                                    <asp:Parameter Name="Maintenance" Type="Boolean" />
                                    <asp:Parameter Name="Administration" Type="Boolean" />
                                    <asp:Parameter Name="Users" Type="Boolean" />
                                    <asp:Parameter Name="Teller" Type="Boolean" />
                                     <asp:Parameter Name="BulkTransactionsReports" Type="Boolean" />
                                    <asp:Parameter Name="Registeration" />
                                    
                                    <asp:Parameter Name="original_ID" Type="String" />
                                </UpdateParameters>
                                <InsertParameters>
                                    <asp:Parameter Name="ID" Type="String" />
                                    <asp:Parameter Name="Name" Type="String" />
                                    <asp:Parameter Name="Reports" Type="Boolean" />
                                    <asp:Parameter Name="Maintenance" Type="Boolean" />
                                    <asp:Parameter Name="Administration" Type="Boolean" />
                                    <asp:Parameter Name="Users" Type="Boolean" />
                                    <asp:Parameter Name="Teller" Type="Boolean" />
                                    <asp:Parameter Name="BulkTransactionsReports" Type="Boolean" />
                                    
                                    <asp:Parameter Name="Registeration" />
                                </InsertParameters>
                            </asp:SqlDataSource>
</asp:Content>

