<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Reports.aspx.vb" Inherits="Reports" title="Untitled Page" %>

<%@ Register assembly="MetaBuilders.WebControls" namespace="MetaBuilders.WebControls" tagprefix="mb" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">


    <script type="text/javascript">
        
       if (navigator.platform.toString().toLowerCase().indexOf("linux") != -1){
	 	document.write('<link type="text/css" rel="stylesheet" href="css/datepickercontrol_lnx.css">');
	 }
	 else{
	 	document.write('<link type="text/css" rel="stylesheet" href="css/datepickercontrol.css">');
	 }
	 
    </script>
    <div class="collapsibleContainer" title="Show/Hide">

        <table style="width: 100%">
            <tr>
                <td style = "width: 100%">
                                <fieldset style="border:thin solid White; color:White;width:99%; height: 200px;" >
                        <legend style="color: #FFFFFF" >Customed Reports</legend>
                                    <%--<asp:DropDownList ID="drpd_ATM" runat="server" >
                            </asp:DropDownList>--%>
                             
                             <fieldset style="border:thin solid White; color:White;width:35%; height: 180px;" >
                        <legend style="color: #FFFFFF" >Transaction Status </legend>
                        <table >
                        <tbody  >
                        <tr>
                            <td style="width: 125px"><asp:Label ID="Label56" runat="server" Font-Bold="True" ForeColor="White" 
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
                            <td style="width: 125px"><asp:Label ID="Label59" runat="server" Font-Bold="True" ForeColor="White" 
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
                            <td style="width: 125px"><asp:Label ID="Label58" runat="server" Font-Bold="True" ForeColor="White" 
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

                        <fieldset style="border:thin solid White; color:White; height: 180px;width:62%;" >
                        <legend style="color: #FFFFFF" >Searching</legend>
                        <table style="width: 100%; margin-right: 1px;" >
                        <tbody  >
                        <tr>
                            <td  >Transaction Code:</td>
                            <td  >
                                <asp:TextBox ID="txt_TRX_Code" runat="server" Width="125px"></asp:TextBox>
                                                <asp:DropDownList ID="drpd_TRXcode" runat="server" 
                                    Width="90px" Height="22px">
                                                    <asp:ListItem>Start With</asp:ListItem>
                                                    <asp:ListItem>Part Of</asp:ListItem>
                                                    <asp:ListItem>Ends With</asp:ListItem>
                                                    <asp:ListItem>Exact</asp:ListItem>
                                                    <asp:ListItem Selected="True">Don`t Care</asp:ListItem>
                                </asp:DropDownList>
                                                </td>
                        </tr>
                        <tr>    
                            <td  >Beneficiary Mobile:</td>
                            <td  >
                                <asp:TextBox ID="txt_BM" runat="server" Width="125px" style="margin-left: 0px"></asp:TextBox>
                                <asp:DropDownList ID="drpd_BM" runat="server" Height="22px" Width="90px" 
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
                            
                            <td  >Depositor Mobile:</td>
                            <td >
                                <asp:TextBox ID="txt_DM" runat="server" Width="125px"></asp:TextBox>
                                <asp:DropDownList ID="drpd_DM" runat="server" Width="90px" Height="22px">
                                    <asp:ListItem>Start With</asp:ListItem>
                                    <asp:ListItem>Part Of</asp:ListItem>
                                    <asp:ListItem>Ends With</asp:ListItem>
                                    <asp:ListItem>Exact</asp:ListItem>
                                    <asp:ListItem Selected="True">Don`t Care</asp:ListItem>
                                </asp:DropDownList>
                             </td>
                        </tr>
                        
                        
                         <tr>
                            
                            <td  >Sequence Number:</td>
                            <td >
                                <asp:TextBox ID="txt_TRXSeq" runat="server" Width="125px"></asp:TextBox>
                                <asp:DropDownList ID="drpd_TRXSeq" runat="server" Width="90px" Height="22px">
                                    <asp:ListItem>Start With</asp:ListItem>
                                    <asp:ListItem>Part Of</asp:ListItem>
                                    <asp:ListItem>Ends With</asp:ListItem>
                                    <asp:ListItem>Exact</asp:ListItem>
                                    <asp:ListItem Selected="True">Don`t Care</asp:ListItem>
                                </asp:DropDownList>
                             </td>
                        </tr>
                        
                        
                         <tr>
                            
                            <td  >Amount:</td>
                            <td >
                                <asp:TextBox ID="TXT_AMT" runat="server" Width="125px"></asp:TextBox>
                                <asp:DropDownList ID="Drpd_Amount" runat="server" Height="22px" Width="90px">
                                    <asp:ListItem>Don`t Care</asp:ListItem>
                                    <asp:ListItem>Equal</asp:ListItem>
                                    <asp:ListItem>Less Than</asp:ListItem>
                                    <asp:ListItem>Larger Than</asp:ListItem>
                                </asp:DropDownList>
                             </td>
                        </tr>
                        
                        
                         <tr>
                            
                            <td  >
                                <asp:Label ID="Label60" runat="server" Font-Bold="True" ForeColor="White" 
                                Text="ID:"></asp:Label>
                             </td>
                            <td >
                                <asp:TextBox ID="TXT_ID" runat="server" Width="125px"></asp:TextBox>
                                                <asp:DropDownList ID="Drpd_ID" runat="server" 
                                    Width="90px" Height="22px">
                                                    <asp:ListItem>Start With</asp:ListItem>
                                                    <asp:ListItem>Part Of</asp:ListItem>
                                                    <asp:ListItem>Ends With</asp:ListItem>
                                                    <asp:ListItem>Exact</asp:ListItem>
                                                    <asp:ListItem Selected="True">Don`t Care</asp:ListItem>
                                </asp:DropDownList>
                             </td>
                        </tr>
                        
                        
                        </tbody>
                        </table>
                        </fieldset>
                        </fieldset>

                                

                                    
                
                </td>
                <td>
                    <fieldset style="border:thin solid White ; color:White;width:200px; height: 200px;" >
                        <legend style="color: #FFFFFF" >Transaction Source</legend>
                        <table>
                        <tbody>
                         <tr>
                            <td style="width: 190px"  >
                                                            <asp:RadioButton ID="RB_CR" runat="server" Font-Bold="True" 
                                    Text="Customed Reports" ValidationGroup="MainFlag" GroupName="MainFlag" />
                            </td> 
                         </tr>
                         <tr>
                        <td style="width: 190px" >
                            <asp:RadioButton ID="RB_RM" runat="server" Font-Bold="True" 
                                Text="Ready Made Reports" ValidationGroup="MainFlag" 
                                GroupName="MainFlag" style="text-align: left" />
                         </td> 
                        
                         </tr>
                        </tbody>
                        </table>
                        <table >
                        <tbody  >
                        
                        <tr>
                            <td><asp:Label ID="Label53" runat="server" Font-Bold="True" ForeColor="White" 
                                Text="Country:"></asp:Label></td>
                            <td style="width: 130px"><asp:DropDownList ID="drpd_Country" runat="server" AutoPostBack="True">
                            </asp:DropDownList></td>
                        </tr>
                        <tr>    
                            <td><asp:Label ID="Label54" runat="server" Font-Bold="True" ForeColor="White" 
                                Text="Bank:"></asp:Label></td>
                            <td style="width: 130px"><asp:DropDownList ID="drpd_Bank" runat="server" AutoPostBack="True">
                            </asp:DropDownList></td>
                            </tr>
                        <tr>    
                            <td><b>Is Teller:</b></td>
                            <td style="width: 130px">
                                <asp:CheckBox ID="CB_IsTeller" runat="server" AutoPostBack="True" />
                                            </td>
                            </tr>
                         <tr>
                            
                            <td><asp:Label ID="Label55" runat="server" Font-Bold="True" ForeColor="White" 
                                Text="Terminal:"></asp:Label></td>
                            <td style="width: 130px">
                                <mb:ComboBox ID="drpd_ATM" runat="server" Width="77px">
                                </mb:ComboBox>
                                <%--<asp:DropDownList ID="drpd_ATM" runat="server" >
                            </asp:DropDownList>--%>
                             </td>
                        </tr>
                        </tbody>
                        </table>
                        </fieldset></td>
                <td>
                    <fieldset style="border:thin solid White; color:White;width:280px; height: 200px;" >
                        <legend style="color: #FFFFFF" >Ready Reports</legend>
                        <table >
                        <tbody  >
                        
                         <tr>
                            <td>
                             <fieldset style="border:thin solid White ; color:White;width:270px; height: 100px;" >
                        <legend style="color: #FFFFFF" >Report Types </legend>
                        <table style="width: 261px" >
                        <tbody  >
                        <tr>
                            <td class="style14" style="height: 19px;">
                                <asp:Label ID="Label1" runat="server" Font-Bold="True" ForeColor="White" 
                                Text="Report Type:"></asp:Label></td>
                            <td style="height: 19px">
                            </td>
                        </tr>
                        <tr>
                            <td class="style14" style="height: 34px;" colspan="2">
                <b>
                <asp:DropDownList ID="drpd_RPT_Type" runat="server" Height="22px" Width="247px">
                
               
                    <asp:ListItem Value="23">ATMTotals</asp:ListItem>
                
               
                    <asp:ListItem Value="6">Confirmed Deposits</asp:ListItem>
                    <asp:ListItem Value="1">Confirmed Withdrawals</asp:ListItem>
                    <asp:ListItem Value="2">Confirmed Redemptions</asp:ListItem>
                    <asp:ListItem Value="4">Expired Transactions</asp:ListItem>
                    <asp:ListItem Value="5">Blocked Transactions</asp:ListItem>
                    <asp:ListItem Value="7">Audit Report</asp:ListItem>
                    <asp:ListItem Value="11">Depositor Transactions &gt;30000</asp:ListItem>
                    <asp:ListItem Value="10">Beneficiary Transactions &gt;30000</asp:ListItem>
                    <asp:ListItem Value="12">Blocked Depositors</asp:ListItem>
                    <asp:ListItem Value="13">Blocked Beneficiaries</asp:ListItem>
                    <asp:ListItem Value="14">Blocking History</asp:ListItem>
                    <asp:ListItem Value="15">Transactions with Invalid Trials</asp:ListItem>
                    <asp:ListItem Value="18">Cash Remittance Users</asp:ListItem>
                    <asp:ListItem Value="19">Cash Remittance Terminals</asp:ListItem>
                
               
                    <asp:ListItem Value="20">Statistical</asp:ListItem>
                
               
                    <asp:ListItem Value="21">Registered Customers</asp:ListItem>
                
               
                    <asp:ListItem Value="22">Detailed</asp:ListItem>
                
               
                    <asp:ListItem Value="24">UnDispensed Trx</asp:ListItem>
                
               
                    <asp:ListItem Value="25">All Dispensed Trx</asp:ListItem>
                    <asp:ListItem Value="26">Transaction Deposit Balance Stat.</asp:ListItem>
                
               
                </asp:DropDownList>
                </b>
                            </td>
                        </tr>
                        </tbody>
                        </table>
                        </fieldset>&nbsp;
							</td>
                        </tr>
                        </tbody>
                        </table>
                        </fieldset>
                </td>
            </tr>
        </table>

    <div  >
                        
                          
                        
                        
                        
                            <br />
                                <table >
                                <tr>
                                    <td>
                            <asp:Label ID="Label51" runat="server" Font-Bold="True" ForeColor="White" 
                                Text="From:"></asp:Label>
                                    </td>
                                    <td style="width: 121px" >
                                        <input type="text" id="DPC_date1" size="14" runat="server" readonly="readonly"   datepicker="true" /> </td>
                                    <td >
                                        <b style="color: #FFFFFF">HH:</b><asp:DropDownList ID="drpd_FRM_HH" 
                    runat="server">
                </asp:DropDownList>
                                        <b style="color: #FFFFFF">MM:<asp:DropDownList ID="drpd_FRM_MM" runat="server">
                </asp:DropDownList>
                                        </b>
                                    </td>
                                    <td>
                            <asp:Label ID="Label52" runat="server" Font-Bold="True" ForeColor="White" 
                                Text="To:"></asp:Label>
                                    </td>
                                    <td style="width: 117px" >
                                        <input type="text" id="DPC_date2"  size="14" runat="server" readonly="readonly"  datepicker="true"/></td>
                                    <td>
                <b style="color: #FFFFFF">HH:<asp:DropDownList ID="drpd_TO_HH" 
                    runat="server">
                </asp:DropDownList>
                                        &nbsp;MM:<asp:DropDownList ID="drpd_TO_MM" runat="server">
                </asp:DropDownList>
                </b>
                                    </td>
                                    <td>
                <b>
                            <asp:Button ID="btn_Search" runat="server" Height="22px" 
                    style="font-weight: 700; text-align: left;" Text="Search" Width="77px" BorderWidth="2px" 
                                            ValidationGroup="AMT" />
                                        &nbsp;<asp:Label ID="Lbl_Status" runat="server" Font-Bold="True" Font-Size="Medium" ForeColor="Red" 
                                            Visible="False"></asp:Label>
                                        <br />
    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                    ControlToValidate="TXT_AMT" Display="Dynamic" 
                    ErrorMessage="Amount Must Be Numeric" 
                    ValidationExpression="\d+" ValidationGroup="AMT"></asp:RegularExpressionValidator>
                                        &nbsp;
                                        </b>
                                    </td>
                                </tr>
                                </table>
                            <br />
                            
                            <br />
                            </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder4" Runat="Server">
</asp:Content>

