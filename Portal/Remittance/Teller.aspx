<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Teller.aspx.vb" Inherits="Teller" title="Untitled Page" %>

<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <div class="collapsibleContainer" title="Teller">
    <div>
<fieldset style="border:thin solid White; color:White;width:420px; height: 210px;" >
                        <legend style="color: #FFFFFF" ><b>Deposit</b></legend>
                         <table>
        <tr>
            <td >
                <span lang="en-us">
                <asp:Label ID="Label6" runat="server" style="font-weight: 700" Text="TransactionCode:"></asp:Label>
                </span>
            </td>
            <td >
                <asp:TextBox ID="trxCodetxt" runat="server" Enabled="False"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td  >
                <asp:Label ID="Label1" runat="server" Text="Depositor Mobile:"></asp:Label>
            </td>
            <td >
                <asp:TextBox ID="DepMobileTxt" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td  >
                <asp:Label ID="Label3" runat="server" Text="Beneficiary Mobile:"></asp:Label>
            </td>
            <td >
                <asp:TextBox ID="BenMobileTxt" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td  >
                <asp:Label ID="Label4" runat="server"  Text="Amount:"></asp:Label>
            </td>
            <td >
                <asp:TextBox ID="AmountTxt" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td >
                <asp:Label ID="Label5" runat="server"  Text="Currency:"></asp:Label>
            </td>
            <td >
                <asp:DropDownList ID="DrpdCurr" runat="server" Height="21px" Width="127px">
                </asp:DropDownList>
            </td>
        </tr>
    </table>
                       
                        </fieldset> 
    <fieldset style="border:thin solid White; color:White;width:200px; height: 210px;" >
                        <legend style="color: #FFFFFF" ><b>Please select a function</b></legend>
                        <table>
                        <tbody>
                         <tr>
                            <td >
                                 <asp:RadioButton ID="RB_Deposit" runat="server"  
                                    Text="Deposit" GroupName="VOO" ValidationGroup="VOO"/>
                              
                            </td> 
                         </tr>
                         <tr>
                        <td>
                            <asp:RadioButton ID="RB_Withdraw" runat="server"  
                                Text="Withdraw" GroupName="VOO" ValidationGroup="VOO"/></td> 
                         </tr>
                         <tr>
                             <td>
                                    
                                 <asp:Button ID="Btn_Confirm" runat="server" Text="Confirm" Font-Bold="True" />
                                    
                             </td>
                         </tr>
                         <tr>
                             <td>
                                    
                                 <asp:Button ID="Btn_New" runat="server" Text="New" Font-Bold="True" 
                                     Width="74px" />
                                    
                             </td>
                         </tr>
                        </tbody>
                        </table>
                           
                        </fieldset> 
    <fieldset style="border:thin solid White; color:White;width:350px; height: 210px;">
        <legend style="color: #FFFFFF"><b>Withdraw</b></legend>
        <table>
        <tbody>
        <tr>
            <td>
                 <asp:Label ID="Label2" runat="server" style="font-weight: 700" Text="TransactionCode:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txt_Trxode" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label7" runat="server" style="font-weight: 700" Text="Depositor Mobile:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txt_DepMobile" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label8" runat="server" style="font-weight: 700" Text="Depositor Pin:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txt_DepPin" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label9" runat="server" style="font-weight: 700" Text="Beneficiary Mobile:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txt_BenMobile" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label10" runat="server" style="font-weight: 700" Text="Beneficiary Pin:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txt_BenPin" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label11" runat="server" style="font-weight: 700" Text="Amount:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txt_Amount" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label12" runat="server" style="font-weight: 700" Text="Currency:"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="DropDownList1" runat="server" Height="19px" Width="128px"></asp:DropDownList>
            </td>
        </tr>
        </tbody>
    </table>
        </fieldset>
     </div>
</div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Label ID="Lbl_Status" runat="server" Font-Bold="True" Font-Size="Medium" 
    ForeColor="Red" Visible="False"></asp:Label>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder4" Runat="Server">
    <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" 
    AutoDataBind="true" EnableDrillDown="False" 
    HasCrystalLogo="False" HasDrillUpButton="False" HasGotoPageButton="False" 
    HasPageNavigationButtons="False" HasSearchButton="False" 
    HasToggleGroupTreeButton="False" HasZoomFactorList="False" 
    PrintMode="ActiveX" Visible="False"></CR:CrystalReportViewer>
</asp:Content>

