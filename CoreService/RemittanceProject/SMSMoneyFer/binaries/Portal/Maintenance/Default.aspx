<%@ page language="VB" autoeventwireup="false" inherits="_Default, App_Web_gvt1jr7a" %>

<%@ Register assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>



<%@ Register assembly="MetaBuilders.WebControls" namespace="MetaBuilders.WebControls" tagprefix="mb" %>



<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Activation Page</title>
    <script type="text/javascript" src="datepickercontrol.js"></script> 
      <script type="text/javascript">
        
       if (navigator.platform.toString().toLowerCase().indexOf("linux") != -1){
	 	document.write('<link type="text/css" rel="stylesheet" href="datepickercontrol_lnx.css">');
	 }
	 else{
	 	document.write('<link type="text/css" rel="stylesheet" href="datepickercontrol.css">');
	 }
	 
    </script>
    <link href="style.css" rel="stylesheet" type="text/css" media="screen" />
    <link type="text/css" rel="stylesheet" href="content.css"/> 
    <style type="text/css">
        #form1
        {
            height: 826px;
            margin-bottom: 0px;
        }
        .style1
        {
            width: 94%;
            height: 14px;
        }
        .style2
        {
            width: 32px;
            margin-left: 40px;
        }
        .style3
        {
            width: 84px;
        }
        .style4
        {
            width: 93px;
        }
        .style6
        {
            width: 328px;
        }
        .style8
        {
            width: 115px;
        }
        .style9
        {
            height: 23px;
        }
        .style10
        {
            width: 328px;
            height: 23px;
        }
        .style11
        {
            width: 293px;
        }
        </style>
</head>
<body>
    <form id="form1" runat="server" dir="ltr">
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
	    <div id="menu" runat="server" >
		<ul id = "Main" runat="server">
            <li><a href="http://localhost/Reports/Default.aspx">Reports</a></li>
			<li><a class ="current_page_item" href="http://localhost/Maintenance/Default.aspx" >Maintenance</a></li>
		</ul>
	</div>
    </div>
    <div id="page">
     <div id="search" >
						<div  style="height: 168px; text-align: left; width: auto ;">
                        &nbsp;&nbsp;
                        <fieldset style="border:thin solid White ; color:White;width:220px; height: 100px;" >
                        <legend >Transaction Source </legend>
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
                        <fieldset style="border:thin solid White; color:White;width:243px; height: 100px;" >
                        <legend >Transaction Status </legend>
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
                        <fieldset style="border:thin solid White ; color:White; height: 100px;width:440px;" >
                        <legend >Searching</legend>
                        <table style="width: 430px" >
                        <tbody  >
                        <tr>
                            <td class="style8">Transaction Code:</td>
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
                        
                        
                        </tbody>
                        </table>
                        </fieldset>&nbsp;
                            <br />
                                <table class="style1">
                                <tr>
                                    <td class="style2">
                            <asp:Label ID="Label51" runat="server" Font-Bold="True" ForeColor="White" 
                                Text="From:"></asp:Label>
                                    </td>
                                    <td class="style4">
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
       
				            <br />
                        <asp:Label ID="Lbl_Status" runat="server" Font-Bold="True" Font-Size="Medium" 
                            ForeColor="Red" Visible="False"></asp:Label>
    
	                        <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:GridView ID="GridView1" runat="server" DataSourceID="SqlDataSource1" 
                    Width="227px" DataKeyNames="TransactionCode">
                    <RowStyle CssClass="RowStyle" />
                    <PagerStyle CssClass="PagerStyle" />
                    <SelectedRowStyle CssClass="SelectedRowStyle" />
                    <HeaderStyle CssClass="HeaderStyle" />
                    <EditRowStyle CssClass="EditRowStyle" />
                    <AlternatingRowStyle CssClass="AltRowStyle" />
                    <Columns>
                        <asp:CommandField ShowSelectButton="True" ButtonType="Image" 
                            SelectImageUrl="~/Images/TextEdit.png" >
                            <ControlStyle Font-Underline="True" />
                        </asp:CommandField>
                    </Columns>
                    <SelectedRowStyle BackColor="#00CC00" />
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server"></asp:SqlDataSource>
    
			</div>
                            
	</div>
	</div>
				
	</div> 
    </div>
    </form>
      
    
	 
    </body>
   
</html>
