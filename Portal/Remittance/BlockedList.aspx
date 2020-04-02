<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="BlockedList.aspx.vb" Inherits="BlockedList" title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ImageButton ID="BlockConBTN" runat="server" 
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
                ShowEditButton="True" UpdateImageUrl="~/Images/confirmation_icon.png" 
                ValidationGroup="Block" />
            <asp:TemplateField>
                <FooterTemplate>
                    <asp:LinkButton ID="ADD_UnBlock" runat="server" onclick="ADD_UnBlock_Click" 
                        ValidationGroup="Block">ADD</asp:LinkButton>
                </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ID" InsertVisible="False" SortExpression="ID">
                <EditItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label6" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Mobile Number" SortExpression="MobileNumber">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("MobileNumber") %>'></asp:TextBox>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                        ControlToValidate="TXT_MobileNumber" ErrorMessage="Numbers Only" 
                        ValidationExpression="\d+" ValidationGroup="Block"></asp:RegularExpressionValidator>
                    <br />
                    <asp:TextBox ID="TXT_MobileNumber" runat="server"></asp:TextBox>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("MobileNumber") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Block DateTime" SortExpression="BlockDateTime">
                <EditItemTemplate>
                    <asp:Label ID="Label6" runat="server" Text='<%# Eval("BlockDateTime") %>'></asp:Label>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("BlockDateTime") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="DepositorOrBeneficiary" 
                SortExpression="DepositorOrBeneficiary">
                <EditItemTemplate>
                    <asp:DropDownList ID="drpd_DepOrBen" runat="server" 
                        SelectedValue='<%# Convert.ToInt32(Eval("DepositorOrBeneficiary")) %>'>
                        <asp:ListItem Value="0">Depositor</asp:ListItem>
                        <asp:ListItem Value="1">Beneficiary</asp:ListItem>
                    </asp:DropDownList>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:DropDownList ID="drpd_DepOrBen" runat="server" >
                        <asp:ListItem Value="0">Depositor</asp:ListItem>
                        <asp:ListItem Value="1">Beneficiary</asp:ListItem>
                    </asp:DropDownList>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:DropDownList ID="drpd_ItemDepOrBen" runat="server" 
                        SelectedValue='<%# Convert.ToInt32(Eval("DepositorOrBeneficiary")) %>' 
                        Enabled="False">
                        <asp:ListItem Value="0">Depositor</asp:ListItem>
                        <asp:ListItem Value="1">Beneficiary</asp:ListItem>
                    </asp:DropDownList>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Block Reason" SortExpression="BlockReason">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("BlockReason") %>' 
                        TextMode="MultiLine"></asp:TextBox>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="TXT_BlockReason" runat="server" TextMode="MultiLine"></asp:TextBox>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("BlockReason") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="UnBlock" SortExpression="UnBlocked">
                <EditItemTemplate>
                    <asp:CheckBox ID="CheckBox2" runat="server" 
                        Checked='<%# Bind("UnBlocked") %>' />
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="CheckBox2" runat="server" Checked='<%# Bind("UnBlocked") %>' 
                        Enabled="false" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="UnBlock DateTime" 
                SortExpression="UnBlockDateTime">
                <EditItemTemplate>
                    <asp:Label ID="Label8" runat="server" Text='<%# Eval("UnBlockDateTime") %>'></asp:Label>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("UnBlockDateTime") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Added By" SortExpression="UserId">
                <EditItemTemplate>
                    <asp:Label ID="Label7" runat="server" Text='<%# Eval("UserId") %>'></asp:Label>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label5" runat="server" Text='<%# Bind("UserId") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <FooterStyle BackColor="White" HorizontalAlign="Center" 
                                VerticalAlign="Middle" />
        <EmptyDataTemplate>
            <table style="width: 100%">
                <tr>
                    <td>
                        Mobile Number</td>
                    <td>
                        Depositor Or Beneficiary</td>
                    <td>
                        Block Reason</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="TXT_EMobileNumber" runat="server" ValidationGroup="EBlock"></asp:TextBox>
                        &nbsp;<asp:RegularExpressionValidator ID="RegularExpressionValidator2" 
                            runat="server" ControlToValidate="TXT_EMobileNumber" 
                            ErrorMessage="Numbers Only" ValidationExpression="\d+" ValidationGroup="EBlock"></asp:RegularExpressionValidator>
                    </td>
                    <td>
                        <asp:DropDownList ID="drpd_EDepOrBen" runat="server" 
                            SelectedValue='<%# Convert.ToInt32(Eval("DepositorOrBeneficiary")) %>'>
                            <asp:ListItem Value="0">Depositor</asp:ListItem>
                            <asp:ListItem Value="1">Beneficiary</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:TextBox ID="TXT_EBlockReason" runat="server" TextMode="MultiLine"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Button ID="BTN_EBlockAdd" runat="server" CommandName="EBlockADD" 
                            Text="ADD" ValidationGroup="EBlock" />
                    </td>
                </tr>
            </table>
        </EmptyDataTemplate>
        <HeaderStyle BackColor="#0083C1" ForeColor="White" HorizontalAlign="Center" 
                                VerticalAlign="Middle" />
        <AlternatingRowStyle BackColor="White" />
    </asp:GridView>
    <br />
    <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                                ConnectionString="<%$ ConnectionStrings:NCRMoneyFerConnection %>" 
                                DeleteCommand="DELETE FROM [BlockedCustomers] WHERE [ID] = @original_ID" 
                                InsertCommand="INSERT INTO [BlockedCustomers] ([MobileNumber], [DepositorOrBeneficiary], [BlockDateTime], [BlockReason], [UnBlocked], [UnBlockDateTime], [UserId]) VALUES (@MobileNumber, @DepositorOrBeneficiary, @BlockDateTime, @BlockReason, @UnBlocked, @UnBlockDateTime, @UserId)" 
                                OldValuesParameterFormatString="original_{0}" 
                                SelectCommand="SELECT * FROM [BlockedCustomers]" 
                                
                                
        UpdateCommand="UPDATE [BlockedCustomers] SET [MobileNumber] = @MobileNumber, [DepositorOrBeneficiary] = @DepositorOrBeneficiary, [BlockReason] = @BlockReason, [UnBlocked] = @UnBlocked, [UnBlockDateTime] = @UnBlockDateTime, [UserId] = @UserId WHERE [ID] = @original_ID">
        <DeleteParameters>
            <asp:Parameter Name="original_ID" Type="Int64" />
        </DeleteParameters>
        <UpdateParameters>
            <asp:Parameter Name="MobileNumber" Type="String" />
            <asp:Parameter Name="DepositorOrBeneficiary" Type="Boolean" />
            <asp:Parameter Name="BlockDateTime" Type="DateTime" />
            <asp:Parameter Name="BlockReason" Type="String" />
            <asp:Parameter Name="UnBlocked" Type="Boolean" />
            <asp:Parameter Name="UnBlockDateTime" Type="DateTime" />
            <asp:Parameter Name="UserId" Type="String" />
            <asp:Parameter Name="original_ID" Type="Int64" />
        </UpdateParameters>
        <InsertParameters>
            <asp:Parameter Name="MobileNumber" Type="String" />
            <asp:Parameter Name="DepositorOrBeneficiary" Type="Boolean" />
            <asp:Parameter Name="BlockDateTime" Type="DateTime" />
            <asp:Parameter Name="BlockReason" Type="String" />
            <asp:Parameter Name="UnBlocked" Type="Boolean" />
            <asp:Parameter Name="UnBlockDateTime" Type="DateTime" />
            <asp:Parameter Name="UserId" Type="String" />
        </InsertParameters>
    </asp:SqlDataSource>
</asp:Content>

