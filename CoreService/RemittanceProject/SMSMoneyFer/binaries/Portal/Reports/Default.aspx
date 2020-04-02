<%@ page language="VB" autoeventwireup="false" inherits="_Default, App_Web_uiurv8lk" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register assembly="MetaBuilders.WebControls" namespace="MetaBuilders.WebControls" tagprefix="mb" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Reports</title>
    <script type="text/javascript" src="datepickercontrol.js"></script> 
    <link href="style.css" rel="stylesheet" type="text/css" media="screen" />
    <style type="text/css">
        .style2
        {
            width: 44px;
            font-weight: bold;
        }
        .style3
        {
            font-weight: bold;
            width: 146px;
        }
        .style8
        {
            width: 147px;
            font-weight: bold;
            height: 19px;
        }
        .style9
        {
            width: 147px;
            font-weight: bold;
        }
        .style11
        {
            width: 248px;
        }
        .style12
        {
            width: 294px;
        }
        .style13
        {
            width: 127px;
            font-weight: bold;
        }
        .style14
        {
            width: 66px;
        }
        .style15
        {
            width: 294px;
            font-weight: bold;
            height: 19px;
        }
        </style>
    <script type="text/javascript">
        
       if (navigator.platform.toString().toLowerCase().indexOf("linux") != -1){
	 	document.write('<link type="text/css" rel="stylesheet" href="datepickercontrol_lnx.css">');
	 }
	 else{
	 	document.write('<link type="text/css" rel="stylesheet" href="datepickercontrol.css">');
	 }
	 
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="wrapper">
    <div id="header">
    
        <div id="logo">
	    </div>
	    <br />
        <br />
        <br />
        <br />
        <br />
        <br />
	    <div id="menu">
		<ul>
            <li><a class ="current_page_item" href="http://localhost/Reports/Default.aspx">Reports</a></li>
			<li><a href="http://localhost/Maintenance/Default.aspx" >Maintenance</a></li>
		</ul>
	</div>
    </div>
    <div id="page">
    <div id="search" >
						<div  style="height: 196px; text-align: left; width: 1245px;">
                        &nbsp;&nbsp;
                        <fieldset style="border:thin solid White; color:White;width:700px; height: 150px;" >
                        <legend style="color: #FFFFFF" >Customed Reports</legend>
                        <table >
                        <tbody  >
                        
                         <tr>
                            <td>
                             
                             <fieldset style="border:thin solid White; color:White;width:243px; height: 100px;" >
                        <legend style="color: #FFFFFF" >Transaction Status </legend>
                        <table >
                        <tbody  >
                        <tr>
                            <td><asp:Label ID="Label56" runat="server" Font-Bold="True" ForeColor="White" 
                                Text="DepositStatus:"></asp:Label></td>
                            <td><asp:DropDownList ID="drpdl_Dstatus" runat="server" Height="22px" Width="86px">
                                <asp:ListItem>Authorized</asp:ListItem>
                                <asp:ListItem>Confirmed</asp:ListItem>
                                <asp:ListItem>Canceled</asp:ListItem>
                                <asp:ListItem>Expired</asp:ListItem>
                                <asp:ListItem>Holded</asp:ListItem>
                                <asp:ListItem Selected="True">ALL</asp:ListItem>
                                <asp:ListItem>Don`t Care</asp:ListItem>
                            </asp:DropDownList></td>
                        </tr>
                        <tr>    
                            <td>&nbsp;<asp:Label ID="Label59" runat="server" Font-Bold="True" ForeColor="White" 
                                Text="Withdrawal Status:"></asp:Label></td>
                            <td><asp:DropDownList ID="drpdl_Wstatus0" runat="server" Height="20px" 
                                Width="86px">
                                <asp:ListItem>Authorized</asp:ListItem>
                                <asp:ListItem>Confirmed</asp:ListItem>
                                <asp:ListItem>Canceled</asp:ListItem>
                                <asp:ListItem>Expired</asp:ListItem>
                                <asp:ListItem>Holded</asp:ListItem>
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
                        </fieldset>&nbsp;
                             <fieldset style="border:thin solid White; color:White; height: 100px;width:422px;" >
                        <legend style="color: #FFFFFF" >Searching</legend>
                        <table style="width: 415px; margin-right: 1px;" >
                        <tbody  >
                        <tr>
                            <td class="style8">Transaction Code:</td>
                            <td class="style15">
                                <asp:TextBox ID="txt_TRX_Code" runat="server" Width="125px"></asp:TextBox>
                                                <asp:DropDownList ID="drpd_TRXcode" runat="server" 
                                    Width="100px">
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
                            <td class="style15">
                                <asp:TextBox ID="txt_BM" runat="server" Width="125px" style="margin-left: 0px"></asp:TextBox>
                                <asp:DropDownList ID="drpd_BM" runat="server" Height="22px" Width="100px" 
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
                            <td class="style12">
                                <asp:TextBox ID="txt_DM" runat="server" Width="125px"></asp:TextBox>
                                <asp:DropDownList ID="drpd_DM" runat="server" Width="100px">
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
                        </fieldset>&nbsp;
							</td>
						 </tr> 
                        </tbody>
                        </table>
                        </fieldset>  
                        
                        <fieldset style="border:thin solid White ; color:White;width:220px; height: 150px;" >
                        <legend style="color: #FFFFFF" >Transaction Source</legend>
                        <table>
                        <tbody>
                         <tr>
                            <td >
                                                            <asp:RadioButton ID="RB_CR" runat="server" Font-Bold="True" 
                                    Text="Customed Reports" ValidationGroup="MainFlag" GroupName="MainFlag" />
                            </td> 
                         </tr>
                         <tr>
                        <td>
                            <asp:RadioButton ID="RB_RM" runat="server" Font-Bold="True" 
                                Text="Ready Made Reports" ValidationGroup="MainFlag" 
                                GroupName="MainFlag" />
                         </td> 
                        
                         </tr>
                        </tbody>
                        </table>
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
                            
                            <td><asp:Label ID="Label55" runat="server" Font-Bold="True" ForeColor="White" 
                                Text="ATM:"></asp:Label></td>
                            <td>
                                <mb:ComboBox ID="drpd_ATM" runat="server" Width="122px">
                                </mb:ComboBox>
                             </td>
                        </tr>
                        </tbody>
                        </table>
                        </fieldset>&nbsp;
                        <fieldset style="border:thin solid White; color:White;width:290px; height: 150px;" >
                        <legend style="color: #FFFFFF" >Ready Reports</legend>
                        <table >
                        <tbody  >
                        
                         <tr>
                            <td>
                             <fieldset style="border:thin solid White ; color:White;width:280px; height: 50px;" >
                        <legend style="color: #FFFFFF" >Report Types </legend>
                        <table >
                        <tbody  >
                        <tr>
                            <td class="style14">
                                <asp:Label ID="Label1" runat="server" Font-Bold="True" ForeColor="White" 
                                Text="Report Type:"></asp:Label></td>
                            <td>
                <b>
                <asp:DropDownList ID="drpd_RPT_Type" runat="server" Height="22px" Width="220px">
                    <asp:ListItem Value="00">Total Cancelled TRX</asp:ListItem>
                    <asp:ListItem Value="1">Total Confirmed Withdrawal TRX</asp:ListItem>
                    <asp:ListItem Value="6">Total Confirmed Deposit TRX</asp:ListItem>
                    <asp:ListItem Value="2">Deposited and Withdrawaled Authorized TRX</asp:ListItem>
                    <asp:ListItem Value="4">Expired TRX</asp:ListItem>
                
               
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
                            <br />
                                <table class="style1">
                                <tr>
                                    <td class="style2">
                            <asp:Label ID="Label51" runat="server" Font-Bold="True" ForeColor="White" 
                                Text="From:"></asp:Label>
                                    </td>
                                    <td class="style13">
                                        <input type="text" id="DPC_date1" size="14" runat="server" readonly="readonly"    /> </td>
                                    <td class="style11">
                                        <b>HH:</b><asp:DropDownList ID="drpd_FRM_HH" 
                    runat="server">
                </asp:DropDownList>
                                        <b>MM:<asp:DropDownList ID="drpd_FRM_MM" runat="server">
                </asp:DropDownList>
                                        </b>
                                    </td>
                                    <td>
                            <asp:Label ID="Label52" runat="server" Font-Bold="True" ForeColor="White" 
                                Text="To:"></asp:Label>
                                    </td>
                                    <td class="style3">
                                        <input type="text" id="DPC_date2"  size="14" runat="server" readonly="readonly" /></td>
                                    <td>
                <b>HH:<asp:DropDownList ID="drpd_TO_HH" 
                    runat="server">
                </asp:DropDownList>
&nbsp;MM:<asp:DropDownList ID="drpd_TO_MM" runat="server">
                </asp:DropDownList>
                </b>
                                    </td>
                                    <td>
                <b>
                            <asp:Button ID="btn_Search" runat="server" Height="22px" 
                    style="font-weight: 700; text-align: left;" Text="Search" Width="77px" BorderWidth="2px" />
                </b>
                                    </td>
                                </tr>
                                </table>
                            <br />
                            
                            <br />
                            <br />
                            </div>
					</div>
    <div id="page-bgtop">
	<div id="page-bgbtm">
	    <div id="content">
                        <asp:Label ID="Lbl_Status" runat="server" Font-Bold="True" Font-Size="Medium" 
                            ForeColor="Red" Visible="False"></asp:Label>
    
                <asp:SqlDataSource ID="SqlDataSource1" runat="server"></asp:SqlDataSource>
    
			<br />
    </div>
    </div>
    </div>
    </div>
    </div>
    </form>
    <div id="footer">
	    Copyright (c) NCR.com. All Rights Reserved.l Rights Reserved.</div>
</body>
</html>
