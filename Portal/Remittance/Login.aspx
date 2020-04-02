<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Login.aspx.vb" Inherits="Login" title="Login" %>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="page">
	
	    &nbsp;<span class="style5">*Please Enter Your User Name and Password</span><table class="style1">
            <tr>
                <td class="style3">
                    User Name:</td>
                <td>
                    <asp:TextBox ID="txt_UsrName" runat="server"  Width="225px" CssClass="date" 
                        AutoCompleteType="Disabled"></asp:TextBox>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style3">
                    Password:</td>
                <td>
                    <asp:TextBox ID="txt_Password" runat="server" Width="225px" TextMode="Password"></asp:TextBox>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style2">
                    </td>
                <td class="style4">
                    <asp:Button ID="Btn_Login" runat="server" CssClass="Btn" Text="Login" 
                        style="height: 26px; width: 47px;" />
                </td>
                <td class="style4">
                    <asp:Label ID="lbl_Error" runat="server" Font-Bold="True" Font-Size="Medium" 
                        ForeColor="Red" Visible="False"></asp:Label>
                </td>
            </tr>
        </table>
	
	</div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder4" Runat="Server">
</asp:Content>




