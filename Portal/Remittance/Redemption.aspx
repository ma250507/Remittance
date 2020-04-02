<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Redemption.aspx.vb" Inherits="Redemption" title="Untitled Page" %>

<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <fieldset style="border:thin solid White; color:White;width:350px; height: 210px;">
        <legend style="color: #FFFFFF"><b>Redemption</b></legend>
        <table>
            <tr>
                <td style="width: 200px">
                    ID:<asp:RadioButton ID="RD_NationalID" runat="server" GroupName="IDType" 
                        Text="NationalID" ValidationGroup="IDType" AutoPostBack="True" 
                        Checked="True" />
                    <br />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:RadioButton ID="RD_Passport" runat="server" GroupName="IDType" 
                        Text="Passport" ValidationGroup="IDType" AutoPostBack="True" />
                </td>
                <td style="width: 173px">
                    <asp:TextBox ID="DepositorIdTXT" runat="server" style="margin-bottom: 0px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ControlToValidate="DepositorIdTXT" ErrorMessage="ID is Required." 
                        ValidationGroup="Redemption" EnableClientScript="False">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td style="width: 200px">
                    <asp:Label ID="Label10" runat="server" style="font-weight: 700" 
                        Text="Redemption Key:"></asp:Label>
                </td>
                <td style="width: 173px">
                    <asp:TextBox ID="txt_RedemptionKey" runat="server" TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                        ControlToValidate="txt_RedemptionKey" 
                        ErrorMessage="Redemption key is Required." ValidationGroup="Redemption" 
                        EnableClientScript="False">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td style="width: 200px">
                    <asp:Label ID="Label8" runat="server" style="font-weight: 700" 
                        Text="Depositor Mobile:"></asp:Label>
                </td>
                <td style="width: 173px">
                    <asp:TextBox ID="txt_DepMobile" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                        ControlToValidate="txt_DepMobile" ErrorMessage="Depositor Mobile is Required." 
                        ValidationGroup="Redemption" EnableClientScript="False">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td style="width: 200px">
                    <asp:Label ID="Label11" runat="server" style="font-weight: 700" 
                        Text="Beneficiary Mobile:"></asp:Label>
                </td>
                <td style="width: 173px">
                    <asp:TextBox ID="txt_BenMobile" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                        ControlToValidate="txt_BenMobile" 
                        ErrorMessage="Beneficiary Mobile is Required." 
                        ValidationGroup="Redemption" EnableClientScript="False">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td style="width: 200px">
                    <asp:Label ID="Label2" runat="server" style="font-weight: 700" 
                        Text="Transaction Code:"></asp:Label>
                </td>
                <td style="width: 173px">
                    <asp:TextBox ID="txt_Trxode" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                        ControlToValidate="txt_Trxode" ErrorMessage="Transaction Code is Required." 
                        ValidationGroup="Redemption" EnableClientScript="False">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2">
                    <asp:Button ID="Btn_Submit" runat="server" Font-Bold="True" Text="Submit" 
                        ValidationGroup="Redemption" OnClientClick="document.getElementById('ctl00_ContentPlaceHolder2_Btn_Submit').style.display = 'none';"/>
                    <br />
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                        ControlToValidate="txt_RedemptionKey" Display="Dynamic" 
                        ErrorMessage="Key must be 4 digits" ValidationExpression="^[0-9]{4}$" 
                        ValidationGroup="Deposit"></asp:RegularExpressionValidator>
                    <br />
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" 
                        ControlToValidate="txt_Trxode" Display="Dynamic" 
                        ErrorMessage="Transaction Code must be 12 digits" 
                        ValidationExpression="^[0-9]{12}$" ValidationGroup="Deposit"></asp:RegularExpressionValidator>
    
                <br />
    <asp:RegularExpressionValidator ID="NationalIdValidator" runat="server" Display="Dynamic" 
                    ErrorMessage="National Id must be 14 digits." 
                    ValidationExpression="^[0-9]{14}$" ValidationGroup="Deposit" 
                        ControlToValidate="DepositorIdTXT"></asp:RegularExpressionValidator>
    
                <br />
    <asp:RegularExpressionValidator ID="PassportIdValidator" runat="server" Display="Dynamic" 
                    ErrorMessage="Passport ID must be 9 digits." 
                    ValidationExpression="^[0-9]{9}$" ValidationGroup="Deposit" 
                        ControlToValidate="DepositorIdTXT"></asp:RegularExpressionValidator>
    
                </td>
            </tr>
        </table>
    </fieldset>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:ImageButton ID="TConBTN" runat="server" ImageUrl="~/Images/Confirm.png" 
    Visible="False" />
    <asp:Label ID="Lbl_Status" runat="server" Font-Bold="True" Font-Size="Medium" 
    ForeColor="Red" Visible="False"></asp:Label>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder4" Runat="Server">
    <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" 
    AutoDataBind="true"  EnableDrillDown="False" 
    HasCrystalLogo="False" HasDrillUpButton="False" HasGotoPageButton="False" 
    HasPageNavigationButtons="False" HasSearchButton="False" 
    HasToggleGroupTreeButton="False"  HasZoomFactorList="False" 
    PrintMode="ActiveX" Visible="False">
    </CR:CrystalReportViewer>
</asp:Content>

