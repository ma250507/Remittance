<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Deposit.aspx.vb" Inherits="Deposit" title="Untitled Page" %>

<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <fieldset style="border:thin solid White; color:White;width:420px; height: 340px;">
    <legend style="color: #FFFFFF"><b>Deposit</b></legend>
    <table style="width: 377px">
        <tr>
            <td>
                ID:<asp:RadioButton ID="RD_NationalID" runat="server" GroupName="IDType" 
                    Text="NationalID" ValidationGroup="IDType" AutoPostBack="True" 
                    Checked="True" />
                <br />
                &nbsp;&nbsp;&nbsp;&nbsp;
                <asp:RadioButton ID="RD_Passport" runat="server" GroupName="IDType" 
                    Text="Passport" ValidationGroup="IDType" AutoPostBack="True" />
            </td>
            <td>
                <asp:TextBox ID="DepositorIdTXT" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ControlToValidate="DepositorIdTXT" ErrorMessage="ID is Required." 
                    ValidationGroup="Deposit" EnableClientScript="False">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Text="Depositor Mobile:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="DepMobileTxt" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                    ControlToValidate="DepMobileTxt" ErrorMessage="Depositor Mobile is Required." 
                    ValidationGroup="Deposit" EnableClientScript="False">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                Confirm Depositor Mobile</td>
            <td>
                <asp:TextBox ID="ConDepMobileTxt" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                    ControlToValidate="ConDepMobileTxt" 
                    ErrorMessage="Mobile Number Confirmation is Required." 
                    ValidationGroup="Deposit" EnableClientScript="False">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label3" runat="server" Text="Beneficiary Mobile:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="BenMobileTxt" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                    ControlToValidate="BenMobileTxt" ErrorMessage="Beneficiary Mobile is Required." 
                    ValidationGroup="Deposit" EnableClientScript="False">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label6" runat="server" Text="Confirm Beneficiary Mobile:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="ConBenMobileTxt" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                    ControlToValidate="ConBenMobileTxt" ErrorMessage="Confirmation is Required." 
                    ValidationGroup="Deposit" EnableClientScript="False">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label4" runat="server" Text="Amount:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="AmountTxt" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                    ControlToValidate="AmountTxt" ErrorMessage="Amount is Required." 
                    ValidationGroup="Deposit" EnableClientScript="False">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label5" runat="server" Text="Currency:"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="DrpdCurr" runat="server" Height="21px" Width="127px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                SMS Language:</td>
            <td>
                <asp:DropDownList ID="drpd_SMSLang" runat="server" 
                    DataSourceID="SqlDataSource1" DataTextField="SMSDescription" 
                    DataValueField="SMSCode">
                </asp:DropDownList>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:NCRMoneyFerConnection %>" 
                    SelectCommand="SELECT * FROM [SMSLanguages]"></asp:SqlDataSource>
            </td>
        </tr>
        <tr>
            <td align="center" colspan="2">
                <asp:Button ID="Btn_Submit" runat="server" Font-Bold="True" Text="Submit" 
                    ValidationGroup="Deposit" OnClientClick="document.getElementById('ctl00_ContentPlaceHolder2_Btn_Submit').style.display = 'none';"/>
                <asp:HiddenField ID="trxCodetxt" runat="server" />
                <asp:CompareValidator ID="CompareValidator2" runat="server" 
                    ControlToCompare="BenMobileTxt" ControlToValidate="ConBenMobileTxt" 
                    Display="Dynamic" ErrorMessage="The Beneficiary Mobile Must Match." 
                    ValidationGroup="Deposit"></asp:CompareValidator>
    <br />
    <asp:CompareValidator ID="CompareValidator1" runat="server" 
                    ControlToCompare="DepMobileTxt" ControlToValidate="ConDepMobileTxt" 
                    Display="Dynamic" ErrorMessage="The Depositor Mobile Must Match." 
                    ValidationGroup="Deposit"></asp:CompareValidator>
    <br />
    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                    ControlToValidate="DepMobileTxt" Display="Dynamic" 
                    ErrorMessage="Mobile Number must be 10 or 11 digits." 
                    ValidationExpression="^[0-9]{10,11}$" ValidationGroup="Deposit"></asp:RegularExpressionValidator>
    <br />
    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
                    ControlToValidate="BenMobileTxt" Display="Dynamic" 
                    ErrorMessage="Mobile Number must be 10 or 11 digits." 
                    ValidationExpression="^[0-9]{10,11}$" ValidationGroup="Deposit"></asp:RegularExpressionValidator>
    
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
</fieldset><br />
&nbsp;             
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <br />
<asp:ImageButton ID="TConBTN" runat="server" ImageUrl="~/Images/Confirm.png" 
    Visible="False" />
<br />
<asp:Label ID="Lbl_Status" runat="server" Font-Bold="True" Font-Size="Medium" 
    ForeColor="Red" Visible="False"></asp:Label>
    
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder4" Runat="Server">
    <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" 
    AutoDataBind="true"  EnableDrillDown="False" 
    HasCrystalLogo="False" HasDrillUpButton="False" HasGotoPageButton="False" 
    HasPageNavigationButtons="False" HasSearchButton="False" 
    HasToggleGroupTreeButton="False" HasZoomFactorList="False" 
    PrintMode="ActiveX" Visible="False">
</CR:CrystalReportViewer>
</asp:Content>

