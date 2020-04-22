<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="BulkTransactionReports.aspx.vb" Inherits="BulkTransactionReports" Title="Untitled Page" %>

<%@ Register Assembly="MetaBuilders.WebControls" Namespace="MetaBuilders.WebControls" TagPrefix="mb" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">


    <script type="text/javascript">

        if (navigator.platform.toString().toLowerCase().indexOf("linux") != -1) {
            document.write('<link type="text/css" rel="stylesheet" href="css/datepickercontrol_lnx.css">');
        }
        else {
            document.write('<link type="text/css" rel="stylesheet" href="css/datepickercontrol.css">');
        }

    </script>
    <div class="collapsibleContainer" title="Show/Hide">

        <table style="width: 100%">
            <tr>
                <td style="width: 67%">

                    <fieldset style="border: thin solid White; color: White; width: 50%;">
                        <legend style="color: #FFFFFF">Transaction Status </legend>
                        <table>
                            <tr>
                                <td>
                                    <asp:Label ID="Label53" runat="server" Font-Bold="True" ForeColor="White"
                                        Text="Country:"></asp:Label></td>
                                <td style="width: 130px">
                                    <asp:DropDownList ID="drpd_Country" Width="247px" runat="server" AutoPostBack="True">
                                    </asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label54" runat="server" Font-Bold="True" ForeColor="White"
                                        Text="Bank:"></asp:Label></td>
                                <td style="width: 130px">
                                    <asp:DropDownList ID="drpd_Bank" Width="247px" runat="server" AutoPostBack="True">
                                    </asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td><b>Is Teller:</b></td>
                                <td style="width: 130px">
                                    <asp:CheckBox ID="CB_IsTeller" runat="server" AutoPostBack="True" />
                                </td>
                            </tr>
                                <tr>

                                    <td>
                                        <asp:Label ID="Label55" runat="server" Font-Bold="True" ForeColor="White"
                                            Text="Terminal:"></asp:Label></td>
                                    <td style="width: 130px">
                                        <asp:DropDownList ID="drpd_ATM" runat="server" Width="247px">
                                        </asp:DropDownList>
                                        <%--<asp:DropDownList ID="drpd_ATM" runat="server" >
                            </asp:DropDownList>--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 34px;">Report:</td>
                                    <td>
                                        <b>
                                            <asp:DropDownList ID="drpd_RPT_Type" runat="server" Height="22px" Width="247px">

                                                <asp:ListItem Value="1">Bulk Transactions Reports</asp:ListItem>

                                            </asp:DropDownList>
                                        </b>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 125px">
                                        <asp:Label ID="Label59" runat="server" Font-Bold="True" ForeColor="White"
                                            Text="Withdrawal Status:"></asp:Label></td>
                                    <td>
                                        <asp:DropDownList ID="drpdl_Wstatus" runat="server" Height="20px" AutoPostBack="true" 
                                            Width="247px">
                                            <asp:ListItem Selected="True">All</asp:ListItem>
                                            <asp:ListItem>New</asp:ListItem>
                                            <asp:ListItem>Authorized</asp:ListItem>
                                            <asp:ListItem>Confirmed</asp:ListItem>
                                            <asp:ListItem>Canceled</asp:ListItem>
                                            <asp:ListItem>Expired</asp:ListItem>


                                        </asp:DropDownList></td>
                                </tr>

                                <tr>
                                    <td>Transaction Code:</td>
                                    <td>
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
                                    <td>National ID:</td>
                                    <td>
                                        <asp:TextBox ID="txt_NationalID" runat="server" Width="125px" Style="margin-left: 0px"></asp:TextBox>
                                        <asp:DropDownList ID="drpd_NationalID" runat="server" Height="22px" Width="90px"
                                            Style="margin-right: 0px">
                                            <asp:ListItem>Start With</asp:ListItem>
                                            <asp:ListItem>Part Of</asp:ListItem>
                                            <asp:ListItem>Ends With</asp:ListItem>
                                            <asp:ListItem>Exact</asp:ListItem>
                                            <asp:ListItem Selected="True">Don`t Care</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>

                                    <td>Remitter:</td>
                                    <td>

                                        <asp:DropDownList ID="drpd_Remitters" runat="server" Width="247px" Height="22px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>


                                <tr>

                                    <td>Sequence Number:</td>
                                    <td>
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

                                    <td>Amount:</td>
                                    <td>
                                       
                                        <asp:TextBox ID="TXT_AMT" runat="server" Width="125px"></asp:TextBox>
                                        <asp:DropDownList ID="Drpd_Amount" runat="server" Height="22px" Width="90px">
                                            <asp:ListItem>Don`t Care</asp:ListItem>
                                            <asp:ListItem>Equal</asp:ListItem>
                                            <asp:ListItem>Less Than</asp:ListItem>
                                            <asp:ListItem>Larger Than</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr runat="server"  id="trFrom">
                                    <td>
                                        <asp:Label ID="Label51" runat="server" Font-Bold="True" ForeColor="White"
                                            Text="From:"></asp:Label>
                                    </td>
                                    <td style="width: 121px">
                                        <input type="text" id="DPC_date1" style="width: 125px;" size="14" runat="server" readonly="readonly" datepicker="true" />
                                    </td>
                                    <td>
                                        <b style="color: #FFFFFF">HH:</b><asp:DropDownList ID="drpd_FRM_HH"
                                            runat="server">
                                        </asp:DropDownList>
                                        <b style="color: #FFFFFF">MM:<asp:DropDownList ID="drpd_FRM_MM" runat="server">
                                        </asp:DropDownList>
                                        </b>
                                    </td>
                                </tr>

                                <tr runat="server"  id="trTo">
                                    <td>
                                        <asp:Label ID="Label52" runat="server" Font-Bold="True" ForeColor="White"
                                            Text="To:"></asp:Label>
                                    </td>
                                    <td style="width: 117px">
                                        <input type="text" id="DPC_date2" style="width: 125px;" size="14" runat="server" readonly="readonly" datepicker="true" /></td>
                                    <td>
                                        <b style="color: #FFFFFF">HH:<asp:DropDownList ID="drpd_TO_HH"
                                            runat="server">
                                        </asp:DropDownList>
                                            &nbsp;MM:<asp:DropDownList ID="drpd_TO_MM" runat="server">
                                            </asp:DropDownList>
                                        </b>
                                    </td>
                                </tr>
                        </table>
                    </fieldset>






                </td>

            </tr>
        </table>

        <div>





            <br />
            <table>
                <tr>


                    <td>
                        <b>
                            <asp:Button ID="btn_Search" runat="server" Height="22px"
                                Style="font-weight: 700; text-align: left;" Text="Search" Width="77px" BorderWidth="2px"
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
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder4" runat="Server">
</asp:Content>

