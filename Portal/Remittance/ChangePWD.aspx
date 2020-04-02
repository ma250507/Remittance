<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="ChangePWD.aspx.vb" Inherits="ChangePWD" title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width: 100%">
    <tr>
        <td colspan="2" style="text-align: center">
            Change Your Password</td>
    </tr>
    <tr>
        <td style="width: 252px">
            <asp:Label ID="CurrentPasswordLabel" runat="server" 
                                    AssociatedControlID="CurrentPassword">Current Password:</asp:Label>
        </td>
        <td>
            <asp:TextBox ID="CurrentPassword" runat="server" TextMode="Password"></asp:TextBox>
            <asp:RequiredFieldValidator ID="CurrentPasswordRequired" runat="server" 
                                    ControlToValidate="CurrentPassword" ErrorMessage="Password is required." 
                                    ToolTip="Password is required." 
                ValidationGroup="CHPWD" EnableClientScript="False">*</asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td style="width: 252px">
            <asp:Label ID="NewPasswordLabel" runat="server" 
                                    AssociatedControlID="NewPassword" 
                style="text-align: left">New Password:</asp:Label>
        </td>
        <td>
            <asp:TextBox ID="NewPassword" runat="server" TextMode="Password"></asp:TextBox>
            <asp:RequiredFieldValidator ID="NewPasswordRequired" runat="server" 
                                    ControlToValidate="NewPassword" ErrorMessage="New Password is required." 
                                    ToolTip="New Password is required." 
                ValidationGroup="CHPWD" EnableClientScript="False">*</asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td style="width: 252px">
            <asp:Label ID="ConfirmNewPasswordLabel" runat="server" 
                                    AssociatedControlID="ConfirmNewPassword">Confirm New 
            Password:</asp:Label>
        </td>
        <td>
            <asp:TextBox ID="ConfirmNewPassword" runat="server" TextMode="Password"></asp:TextBox>
            <asp:RequiredFieldValidator ID="ConfirmNewPasswordRequired" runat="server" 
                                    ControlToValidate="ConfirmNewPassword" 
                                    ErrorMessage="Confirm New Password is required." 
                                    ToolTip="Confirm New Password is required." 
                                    ValidationGroup="CHPWD" EnableClientScript="False">*</asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td colspan="2" style="text-align: center">
            <asp:CompareValidator ID="NewPasswordCompare" runat="server" 
                                    ControlToCompare="NewPassword" ControlToValidate="ConfirmNewPassword" 
                                    Display="Dynamic" 
                                    ErrorMessage="The Confirm New Password must match the New Password entry." 
                                    ValidationGroup="CHPWD"></asp:CompareValidator>
            <br />
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                                    ControlToValidate="NewPassword" Display="Dynamic" 
                                    ErrorMessage="The passowrd must be 5 or more alphanumeric no-special characters." 
                                    style="text-align: left" 
                                    ValidationExpression="(?!^[0-9]*$)(?!^[a-zA-Z]*$)^([a-zA-Z0-9]{5,10})$" 
                                    ValidationGroup="CHPWD"></asp:RegularExpressionValidator>
            <br />
            <asp:Label ID="FailureText" runat="server" Font-Bold="True" Font-Size="Small" 
                ForeColor="Red"></asp:Label>
            <br />
            <asp:ImageButton ID="ImageButton1" runat="server" 
                ImageUrl="~/Images/AdminConfirm.jpg" Visible="False" />
        </td>
    </tr>
    <tr>
        <td style="width: 252px">
            <asp:Button ID="ChPWD" runat="server" Text="Change Password" 
                ValidationGroup="CHPWD" />
        </td>
        <td>
            <asp:Button ID="CancelPushButton" runat="server" CausesValidation="False" 
                                    CommandName="Cancel" Text="Cancel" 
                onclick="CancelPushButton_Click" />
        </td>
    </tr>
    <tr>
        <td style="width: 252px">
            &nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>
</table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder4" Runat="Server">
</asp:Content>

