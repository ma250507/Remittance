<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Registeration.aspx.vb" Inherits="Registeration" title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width: 100%">
    <tr>
        <td colspan="2">
    <asp:ImageButton ID="REGConBTN" runat="server" 
    ImageUrl="~/Images/AdminConfirm.jpg" Visible="False" />
        </td>
    </tr>
    <tr>
        <td style="width: 284px">
            <asp:TextBox ID="TXT_Reg_MobileNum" runat="server" Width="320px"></asp:TextBox>
        </td>
        <td>
            <asp:Button ID="Btn_Reg_Search" runat="server" Text="Search" />
        </td>
    </tr>
    <tr>
        <td colspan="2">
<asp:Label ID="Lbl_Status" runat="server" ForeColor="Red"></asp:Label>
        </td>
    </tr>
</table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder4" Runat="Server">
    <asp:GridView ID="GridView2" runat="server" AllowPaging="True" 
                            AutoGenerateColumns="False" BackColor="#F1F1F1" BorderStyle="Outset" 
                            DataKeyNames="MobileNumber" GridLines="None" 
                            ShowFooter="True" AllowSorting="True" 
    Width="1000px">
        <RowStyle BackColor="Gainsboro" HorizontalAlign="Center" 
                                VerticalAlign="Middle" />
        <Columns>
            <asp:TemplateField ShowHeader="False">
                <FooterTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" CommandName="ADD" 
                        onclick="LinkButton1_Click" ValidationGroup="REG">ADD</asp:LinkButton>
                </FooterTemplate>
                <EditItemTemplate>
                    <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="True" 
                        CommandName="Update" ImageUrl="~/Images/confirmation_icon.png" Text="Update" />
                    &nbsp;<asp:ImageButton ID="ImageButton2" runat="server" CausesValidation="False" 
                        CommandName="Cancel" ImageUrl="~/Images/cancel_icon.png" Text="Cancel" />
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" 
                        CommandName="Edit" ImageUrl="~/Images/EditIcon.png" Text="Edit" />
                    &nbsp;
                    <asp:ImageButton ID="ImageButton3" runat="server" CommandName="Delete" 
                        ImageUrl="~/Images/delete_icon.gif" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="MobileNumber" SortExpression="MobileNumber">
                <FooterTemplate>
                    <asp:TextBox ID="TXT_ADD_Mobile" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ControlToValidate="TXT_ADD_Mobile" ErrorMessage="*" ValidationGroup="REG"></asp:RequiredFieldValidator>
                    <br />
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                        ControlToValidate="TXT_ADD_Mobile" ErrorMessage="Mobile must be 11 digit" 
                        ValidationExpression="^[0-9]{11}$" ValidationGroup="REG"></asp:RegularExpressionValidator>
                </FooterTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="TXT_MobileNumber" runat="server" 
                        Text='<%# bind("MobileNumber") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label7" runat="server" Text='<%# Eval("MobileNumber") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Name" SortExpression="Name">
                <EditItemTemplate>
                    <asp:Label ID="TXT_EDIT_Name" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="TXT_ADD_Name" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                        ControlToValidate="TXT_ADD_Name" ErrorMessage="*" ValidationGroup="REG"></asp:RequiredFieldValidator>
                    <br />
                    <br />
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="RegisteringDate" 
                SortExpression="RegisteringDate">
                <EditItemTemplate>
                    <asp:Label ID="Label6" runat="server" 
                        Text='<%# Eval("RegisteringDate", "{0:G}") %>'></asp:Label>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("RegisteringDate") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ID" SortExpression="ID">
                <FooterTemplate>
                    <asp:TextBox ID="TXT_ADD_ID" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                        ControlToValidate="TXT_ADD_ID" ErrorMessage="*" ValidationGroup="REG"></asp:RequiredFieldValidator>
                    <br />
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
                        ControlToValidate="TXT_ADD_ID" ErrorMessage="ID Must be 14 or 20" 
                        ValidationExpression="^[0-9]{14,20}$" ValidationGroup="REG"></asp:RegularExpressionValidator>
                </FooterTemplate>
                <EditItemTemplate>
                    <asp:Label ID="TXT_EDIT_ID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Address" SortExpression="Address">
                <FooterTemplate>
                    <asp:TextBox ID="TXT_ADD_ADDRESS" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                        ControlToValidate="TXT_ADD_ADDRESS" ErrorMessage="*" ValidationGroup="REG"></asp:RequiredFieldValidator>
                    <br />
                    <br />
                </FooterTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="TXT_EDIT_ADDRESS" runat="server" Text='<%# Bind("Address") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label5" runat="server" Text='<%# Bind("Address") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Staff">
                <EditItemTemplate>
                    <asp:CheckBox ID="CHK_EStaff" runat="server" Checked='<%# Bind("Staff") %>' />
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:CheckBox ID="CHK_AStaff" runat="server" />
                </FooterTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="LBL_Staff" runat="server" Checked='<%# Bind("Staff") %>' 
                        Enabled="False" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="BankCustomer">
                <EditItemTemplate>
                    <asp:CheckBox ID="CHK_BankCustomer_E" runat="server" Checked="<%# Bind('BankCustomer') %>" />
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:CheckBox ID="CHK_BankCustomer_A" runat="server" />
                </FooterTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="CHK_BankCustomer" runat="server" 
                        Checked="<%# Bind('BankCustomer') %>" Enabled="False" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <FooterStyle BackColor="White" HorizontalAlign="Center" 
                                VerticalAlign="Middle" />
        <HeaderStyle BackColor="#0083C1" ForeColor="White" HorizontalAlign="Center" 
                                VerticalAlign="Middle" />
        <AlternatingRowStyle BackColor="White" />
    </asp:GridView>
    </asp:Content>

