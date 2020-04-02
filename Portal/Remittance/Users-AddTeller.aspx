<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Users-AddTeller.aspx.vb" Inherits="Users_AddTeller" title="Untitled Page" %>

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
    <div style="width:99.5%;  overflow:scroll;">
             <asp:GridView ID="GridView3" runat="server" AllowPaging="True" 
                            AutoGenerateColumns="False" BackColor="#F1F1F1" BorderStyle="Outset" 
                            DataKeyNames="UserId" DataSourceID="SqlDataSource1" GridLines="None" 
                            ShowFooter="True" AllowSorting="True" Width="1040px">
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
                                            CommandName="New" OnClick="ADD_Click" Text="ADD"></asp:LinkButton>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="FName/LName" SortExpression="UserId">
                                    <FooterTemplate>
                                        <asp:TextBox ID="Txt_UserID" runat="server" ></asp:TextBox>
                                    </FooterTemplate>
                                    <EditItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("UserId") %>'></asp:Label>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("UserId") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="UserName" SortExpression="UserName">
                                    <FooterTemplate>
                                        <asp:TextBox ID="Txt_UserName" runat="server"></asp:TextBox>
                                    </FooterTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("UserName") %>'></asp:TextBox>
                                    </EditItemTemplate>
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
                                        <asp:Label ID="Label3" runat="server" Text='<%# Eval("Password") %>' 
                                            Visible="False"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Group" SortExpression="Group_ID">
                                    <EditItemTemplate>
                                        <asp:Label ID="Label13" runat="server" Text='<%# Bind("Group") %>'></asp:Label>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("Group") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Branch" SortExpression="Branch">
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="drpd_BR" runat="server" DataSourceID="TellerBRDS" 
                                            DataTextField="BranchName" DataValueField="BranchName" 
                                            SelectedValue='<%# Bind("Branch") %>'>
                                        </asp:DropDownList>
                                        <asp:SqlDataSource ID="TellerBRDS" runat="server" 
                                            ConnectionString="<%$ ConnectionStrings:NCRMoneyFerConnection %>" 
                                            SelectCommand="SELECT [BranchName] FROM [Branches]"></asp:SqlDataSource>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:DropDownList ID="drpd_BR" runat="server" DataSourceID="TellerBRDS" 
                                            DataTextField="BranchName" DataValueField="BranchName">
                                        </asp:DropDownList>
                                        <asp:SqlDataSource ID="TellerBRDS" runat="server" 
                                            ConnectionString="<%$ ConnectionStrings:NCRMoneyFerConnection %>" 
                                            SelectCommand="SELECT [BranchName] FROM [Branches]"></asp:SqlDataSource>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label8" runat="server" Text='<%# Bind("Branch") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Teller ID" SortExpression="ATM_ID">
                                    <FooterTemplate>
                                        <asp:TextBox ID="Txt_ATMID" runat="server"></asp:TextBox>
                                    </FooterTemplate>
                                    <EditItemTemplate>
                                        <asp:Label ID="Label12" runat="server" Text='<%# Eval("ATM_ID") %>'></asp:Label>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label5" runat="server" Text='<%# Bind("ATM_ID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="CountryCode" SortExpression="CountryCode">
                                    <FooterTemplate>
                                        <asp:DropDownList ID="drpd_CountryCode" runat="server" 
                                            DataSourceID="CountryCodeDS" DataTextField="CountryName" 
                                            DataValueField="CountryCode">
                                        </asp:DropDownList>
                                        <asp:SqlDataSource ID="CountryCodeDS" runat="server" 
                                            ConnectionString="<%$ ConnectionStrings:NCRMoneyFerConnection %>" 
                                            SelectCommand="SELECT [CountryCode], [CountryName] FROM [Country]">
                                        </asp:SqlDataSource>
                                    </FooterTemplate>
                                    <EditItemTemplate>
                                        <asp:Label ID="Label9" runat="server" Text='<%# Eval("CountryCode") %>'></asp:Label>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label6" runat="server" Text='<%# Bind("CountryCode") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="BankCode" SortExpression="BankCode">
                                    <FooterTemplate>
                                        <asp:DropDownList ID="drpd_BankCode" runat="server" DataSourceID="BankCodeDS" 
                                            DataTextField="BankName" DataValueField="BankCode">
                                        </asp:DropDownList>
                                        <asp:SqlDataSource ID="BankCodeDS" runat="server" 
                                            ConnectionString="<%$ ConnectionStrings:NCRMoneyFerConnection %>" 
                                            SelectCommand="SELECT [BankName], [BankCode] FROM [Bank]">
                                        </asp:SqlDataSource>
                                    </FooterTemplate>
                                    <EditItemTemplate>
                                        <asp:Label ID="Label10" runat="server" Text='<%# Eval("BankCode") %>'></asp:Label>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label7" runat="server" Text='<%# Bind("BankCode") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="IsTeller" SortExpression="IsTeller">
                                    <FooterTemplate>
                                        <asp:CheckBox ID="CB_IsTeller" runat="server" Checked="True" Enabled="False" />
                                    </FooterTemplate>
                                    <EditItemTemplate>
                                        <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# Bind("IsTeller") %>' 
                                            Enabled="False" />
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# Bind("IsTeller") %>' 
                                            Enabled="False" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="IP Address" 
                                    SortExpression="TellerIPAddress">
                                    <FooterTemplate>
                                        <asp:TextBox ID="TXT_ADD_TellerIPAddress" runat="server"></asp:TextBox>
                                    </FooterTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TXT_EDT_TellerIPAddress" runat="server" 
                                            Text='<%# Bind("TellerIPAddress") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label9" runat="server" Text='<%# Bind("TellerIPAddress") %>'></asp:Label>
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
                                        <asp:Label ID="Label23" runat="server" Text='<%# Bind("AllATMs") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle BackColor="White" HorizontalAlign="Center" 
                                VerticalAlign="Middle" />
                            <EmptyDataTemplate>
                                <table style="width: 100%">
                                    <tr>
                                        <td style="width: 107px">
                                            <asp:Label ID="Label14" runat="server" Text="FName/LName"></asp:Label>
                                        </td>
                                        <td style="width: 90px">
                                            <asp:Label ID="Label15" runat="server" Text="UserName"></asp:Label>
                                        </td>
                                        <td style="width: 79px">
                                            <asp:Label ID="Label16" runat="server" Text="Password"></asp:Label>
                                        </td>
                                        <td style="width: 57px">
                                            <asp:Label ID="Label17" runat="server" Text="Branch"></asp:Label>
                                        </td>
                                        <td style="width: 82px">
                                            <asp:Label ID="Label18" runat="server" Text="Teller ID"></asp:Label>
                                        </td>
                                        <td style="width: 96px">
                                            <asp:Label ID="Label19" runat="server" Text="CountryCode"></asp:Label>
                                        </td>
                                        <td style="width: 79px">
                                            <asp:Label ID="Label20" runat="server" Text="Bank Code"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label21" runat="server" Text="IsTeller"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label22" runat="server" Text="IP Address"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label11" runat="server" Text="AllATMs"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 107px">
                                            <asp:TextBox ID="TXT_ID" runat="server"></asp:TextBox>
                                        </td>
                                        <td style="width: 90px">
                                            <asp:TextBox ID="TXT_Name" runat="server"></asp:TextBox>
                                        </td>
                                        <td style="width: 79px">
                                            <asp:TextBox ID="TXT_PWD" runat="server" TextMode="Password"></asp:TextBox>
                                        </td>
                                        <td style="width: 57px">
                                            <asp:DropDownList ID="drpd_Br" runat="server" DataSourceID="EBranchDS" 
                                                DataTextField="BranchName" DataValueField="BranchName">
                                            </asp:DropDownList>
                                            <asp:SqlDataSource ID="EBranchDS" runat="server" 
                                                ConnectionString="<%$ ConnectionStrings:NCRMoneyFerConnection %>" 
                                                SelectCommand="SELECT [BranchName] FROM [Branches]"></asp:SqlDataSource>
                                        </td>
                                        <td style="width: 82px">
                                            <asp:TextBox ID="TXT_ATMID" runat="server"></asp:TextBox>
                                        </td>
                                        <td style="width: 96px">
                                            <asp:DropDownList ID="drpd_CC" runat="server" DataSourceID="ECountryCodeDS" 
                                                DataTextField="CountryName" DataValueField="CountryCode">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 79px">
                                            <asp:DropDownList ID="drpd_BC" runat="server" DataSourceID="EBankCodeDS" 
                                                DataTextField="BankName" DataValueField="BankCode">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="CB_Tel" runat="server" Checked="True" Enabled="False" />
                                            &nbsp;&nbsp;
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TXT_IPAddress" runat="server"></asp:TextBox>
                                            
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="CHB_AllATMs_eADD" runat="server" />
                                            <asp:Button ID="BTN_ADD" runat="server" CommandName="EmptyNew" Text="ADD" />
                                        </td>
                                    </tr>
                                </table>
                                <asp:SqlDataSource ID="ECountryCodeDS" runat="server" 
                                    ConnectionString="<%$ ConnectionStrings:NCRMoneyFerConnection %>" 
                                    SelectCommand="SELECT [CountryCode], [CountryName] FROM [Country]">
                                </asp:SqlDataSource>
                                <asp:SqlDataSource ID="EBankCodeDS" runat="server" 
                                    ConnectionString="<%$ ConnectionStrings:NCRMoneyFerConnection %>" 
                                    SelectCommand="SELECT [BankName], [BankCode] FROM [Bank]">
                                </asp:SqlDataSource>
                            </EmptyDataTemplate>
                            <HeaderStyle BackColor="#0083C1" ForeColor="White" HorizontalAlign="Center" 
                                VerticalAlign="Middle" />
                            <AlternatingRowStyle BackColor="White" />
                        </asp:GridView>
                        </div>
              <br />
                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:NCRMoneyFerConnection %>" 
                            OldValuesParameterFormatString="original_{0}" 
                            SelectCommand="SELECT     u.UserId, u.UserName, u.Password,
                          (SELECT     name
                             FROM         groups
                             WHERE     id = u.group_ID) AS [Group], u.ATM_ID, u.CountryCode, u.BankCode, a.IsTeller,u.Branch,u.TellerIPAddress,u.AllATMs
FROM         dbo.Users u INNER JOIN
                      dbo.ATM a ON u.ATM_ID = a.ATMId AND u.BankCode = a.BankCode AND u.CountryCode = a.CountryCode

where  u.group_ID=@GroupId">
                            <SelectParameters>
                                <asp:SessionParameter Name="GroupId" SessionField="GroupID" />
                            </SelectParameters>
                        </asp:SqlDataSource>
    <br />
    </asp:Content>

