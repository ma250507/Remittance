<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Maintenance.aspx.vb" Inherits="Maintenance" title="Untitled Page" %>

<%@ Register assembly="MetaBuilders.WebControls" namespace="MetaBuilders.WebControls" tagprefix="mb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">

    <script type="text/javascript">
        
       if (navigator.platform.toString().toLowerCase().indexOf("linux") != -1){
	 	document.write('<link type="text/css" rel="stylesheet" href="datepickercontrol_lnx.css">');
	 }
	 else{
	 	document.write('<link type="text/css" rel="stylesheet" href="datepickercontrol.css">');
	 }
	 
    </script>
    <div class="collapsibleContainer" title="Show/Hide">
<div  >
                        
                        <table style="width: 100%">
                            <tr>
                                <td>
                                    <fieldset style="border:thin solid White ; color:White;width:95%; height: 130px;" >
                        <legend style="color: #FFFFFF" >Transaction Source </legend>
                        <table >
                        <tbody  >
                        <tr>
                            <td><asp:Label ID="Label53" runat="server" Font-Bold="True" ForeColor="White" 
                                Text="Country:"></asp:Label></td>
                            <td><asp:DropDownList ID="drpd_Country" runat="server" AutoPostBack="True">
                            </asp:DropDownList></td>
                        </tr>
                        <tr>    
                            <td><asp:Label ID="Label54" runat="server" Font-Bold="True" ForeColor="White" 
                                Text="Bank:"></asp:Label></td>
                            <td><asp:DropDownList ID="drpd_Bank" runat="server" AutoPostBack="True">
                            </asp:DropDownList></td>
                            </tr>
                        <tr>    
                            <td><b>Is Teller:</b></td>
                            <td>
                                <asp:CheckBox ID="CB_IsTeller" runat="server" AutoPostBack="True" />
                                            </td>
                            </tr>
                         <tr>
                            
                            <td><asp:Label ID="Label55" runat="server" Font-Bold="True" ForeColor="White" 
                                Text="Terminal:"></asp:Label></td>
                            <td>
                                <mb:ComboBox ID="drpd_ATM" runat="server" Width="122px">
                                </mb:ComboBox>
                             </td>
                        </tr>
                        </tbody>
                        </table>
                        </fieldset>
                                </td>
                                <td>
                                    <fieldset style="border:thin solid White; color:White;width:95%; height: 130px;" >
                        <legend style="color: #FFFFFF" >Transaction Status </legend>
                        <table >
                        <tbody  >
                        <tr>
                            <td><asp:Label ID="Label56" runat="server" Font-Bold="True" ForeColor="White" 
                                Text="Deposit Status:"></asp:Label></td>
                            <td><asp:DropDownList ID="drpdl_Dstatus" runat="server" Height="22px" Width="86px">
                                <asp:ListItem>Authorized</asp:ListItem>
                                <asp:ListItem>Confirmed</asp:ListItem>
                                <asp:ListItem>Canceled</asp:ListItem>
                                <asp:ListItem>Expired</asp:ListItem>
                                <asp:ListItem>Held</asp:ListItem>
                                <asp:ListItem Selected="True">ALL</asp:ListItem>
                                <asp:ListItem>Don`t Care</asp:ListItem>
                            </asp:DropDownList></td>
                        </tr>
                        <tr>    
                            <td><asp:Label ID="Label59" runat="server" Font-Bold="True" ForeColor="White" 
                                Text="Withdrawal Status:"></asp:Label></td>
                            <td><asp:DropDownList ID="drpdl_Wstatus0" runat="server" Height="20px" 
                                Width="86px">
                                <asp:ListItem>Authorized</asp:ListItem>
                                <asp:ListItem>Confirmed</asp:ListItem>
                                <asp:ListItem>Canceled</asp:ListItem>
                                <asp:ListItem>Expired</asp:ListItem>
                                <asp:ListItem>Held</asp:ListItem>
                                <asp:ListItem Selected="True">ALL</asp:ListItem>
                                <asp:ListItem>Don`t Care</asp:ListItem>
                            </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td><asp:Label ID="Label58" runat="server" Font-Bold="True" ForeColor="White" 
                                Text="Transaction Alert Status:"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="drpdl_smsstatus" runat="server">
                                    <asp:ListItem Selected="True">Don`t Care</asp:ListItem>
                                    <asp:ListItem>Sent</asp:ListItem>
                                    <asp:ListItem>UnSent</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        </tbody>
                        </table>
                        </fieldset>
                                </td>
                                <td>
                                    <fieldset style="border:thin solid White ; color:White; height: 130px;width:95%;" >
                        <legend style="color: #FFFFFF" >Searching</legend>
                        <table style="width: 430px" >
                        <tbody  >
                        <tr>
                            <td class="style13">Transaction Code:</td>
                            <td class="style6">
                                <asp:TextBox ID="txt_TRX_Code" runat="server" Width="125px"></asp:TextBox>
                                                <asp:DropDownList ID="drpd_TRXcode" runat="server" 
                                    Height="22px" Width="159px">
                                                    <asp:ListItem>Start With</asp:ListItem>
                                                    <asp:ListItem>Part Of</asp:ListItem>
                                                    <asp:ListItem>Ends With</asp:ListItem>
                                                    <asp:ListItem>Exact</asp:ListItem>
                                                    <asp:ListItem Selected="True">Don`t Care</asp:ListItem>
                                </asp:DropDownList>
                                                </td>
                        </tr>
                        <tr>    
                            <td class="style8">Beneficiary Mobile:</td>
                            <td class="style6">
                                <asp:TextBox ID="txt_BM" runat="server" Width="125px"></asp:TextBox>
                                <asp:DropDownList ID="drpd_BM" runat="server" Height="22px" Width="159px" 
                                    style="margin-right: 0px">
                                    <asp:ListItem>Start With</asp:ListItem>
                                    <asp:ListItem>Part Of</asp:ListItem>
                                    <asp:ListItem>Ends With</asp:ListItem>
                                    <asp:ListItem>Exact</asp:ListItem>
                                    <asp:ListItem Selected="True">Don`t Care</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            </tr>
                         <tr>
                            
                            <td class="style9">Depositor Mobile:</td>
                            <td class="style10">
                                <asp:TextBox ID="txt_DM" runat="server" Width="125px"></asp:TextBox>
                                <asp:DropDownList ID="drpd_DM" runat="server" Height="22px" Width="159px">
                                    <asp:ListItem>Start With</asp:ListItem>
                                    <asp:ListItem>Part Of</asp:ListItem>
                                    <asp:ListItem>Ends With</asp:ListItem>
                                    <asp:ListItem>Exact</asp:ListItem>
                                    <asp:ListItem Selected="True">Don`t Care</asp:ListItem>
                                </asp:DropDownList>
                             </td>
                        </tr>
                        
                        
                         <tr>
                            
                            <td >Amount:</td>
                            <td >
                                <asp:TextBox ID="TXT_AMT" runat="server" Width="125px"></asp:TextBox>
                                <asp:DropDownList ID="Drpd_Amount" runat="server" Height="22px" Width="159px">
                                    <asp:ListItem>Don`t Care</asp:ListItem>
                                    <asp:ListItem>Equal</asp:ListItem>
                                    <asp:ListItem>Less Than</asp:ListItem>
                                    <asp:ListItem>Larger Than</asp:ListItem>
                                </asp:DropDownList>
                             </td>
                        </tr>
                        
                        
                         <tr>
                            
                            <td >&nbsp;</td>
                            <td >
                <b>
    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                    ControlToValidate="TXT_AMT" Display="Dynamic" 
                    ErrorMessage="Amount Must Be Numeric" 
                    ValidationExpression="\d+" ValidationGroup="AMT"></asp:RegularExpressionValidator>
                                        </b>
                             </td>
                        </tr>
                        
                        
                        </tbody>
                        </table>
                        </fieldset>
                                </td>
                            </tr>
                        </table>

                            <br />
                                <table class="style1">
                                <tr>
                                    <td class="style2">
                            <asp:Label ID="Label51" runat="server" Font-Bold="True" ForeColor="White" 
                                Text="From:"></asp:Label>
                                    </td>
                                    <td class="style4" style="width: 195px">
                                        <input type="text" id="DPC_date1" size="14" runat="server" readonly="readonly"  datepicker="true"  /> </td>
                                    <td class="style11" style="width: 247px">
                                        <span style="color: #FFFFFF">
                                        <b>HH:</b></span><asp:DropDownList ID="drpd_FRM_HH" 
                    runat="server">
                </asp:DropDownList>
                                        <b><span style="color: #FFFFFF">MM:</span><asp:DropDownList ID="drpd_FRM_MM" runat="server">
                </asp:DropDownList>
                                        </b>
                                    </td>
                                    <td>
                            <asp:Label ID="Label52" runat="server" Font-Bold="True" ForeColor="White" 
                                Text="To:"></asp:Label>
                                    </td>
                                    <td class="style3" style="width: 119px">
                                        <input type="text" id="DPC_date2"  size="14" runat="server" readonly="readonly"  datepicker="true"/></td>
                                    <td class="style12">
                <b><span style="color: #FFFFFF">HH:</span><asp:DropDownList ID="drpd_TO_HH" 
                    runat="server">
                </asp:DropDownList>
                                        &nbsp;<span style="color: #FFFFFF">MM:</span><asp:DropDownList ID="drpd_TO_MM" runat="server">
                </asp:DropDownList>
                </b>
                                    </td>
                                    <td style="width: 90px">
                <b>
                            <asp:Button ID="btn_Search" runat="server" Height="22px" 
                    style="font-weight: 700; text-align: left;" Text="Search" Width="77px" BorderWidth="2px" 
                                            ValidationGroup="AMT" />
                                        &nbsp;&nbsp;&nbsp;
                                        </b>
                                    </td>
                                </tr>
                                </table>
                            <br />
                            <br />
                            <br />
                            </div>
 </div> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Label ID="Lbl_Status" runat="server" Font-Bold="True" Font-Size="Medium" 
        ForeColor="Red" Visible="False"></asp:Label>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder4" Runat="Server">
    <asp:GridView ID="GridView1" runat="server" DataKeyNames="TransactionCode" 
        DataSourceID="SqlDataSource1" Width="227px">
        <RowStyle CssClass="RowStyle" />
        <PagerStyle CssClass="PagerStyle" />
        <SelectedRowStyle CssClass="SelectedRowStyle" />
        <HeaderStyle CssClass="HeaderStyle" />
        <EditRowStyle CssClass="EditRowStyle" />
        <AlternatingRowStyle CssClass="AltRowStyle" />
        <Columns>
            <asp:CommandField ButtonType="Image" SelectImageUrl="~/Images/TextEdit.png" 
                ShowSelectButton="True">
                <ControlStyle Font-Underline="True" />
            </asp:CommandField>
        </Columns>
        <SelectedRowStyle BackColor="#00CC00" />
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server"></asp:SqlDataSource>
</asp:Content>

