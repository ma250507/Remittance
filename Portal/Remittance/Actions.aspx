<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Actions.aspx.vb" Inherits="Actions" title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <div class="collapsibleContainer" title="Show/Hide">
    <div >
                        &nbsp;&nbsp;
                        <fieldset style="border:thin solid White ; color:White;width:330px; height: 190px;" >
                        <legend style="color: #FFFFFF" >Alerting </legend>
                        <table style="height: auto; width: 330px;" >
                        <tbody  >
                        <tr>
                            <td >
                                <asp:Label ID="Label58" runat="server" Font-Bold="False" ForeColor="White" 
                                Text="Transaction Alert Status:"></asp:Label>
                            </td>
                            <td >
                                <asp:TextBox ID="Lbl_TAS" runat="server" ReadOnly="True" CssClass="TxtBox"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>    
                            <td >Transaction Alert DateTime:</td>
                            <td >
                                <asp:TextBox ID="Lbl_ADT" runat="server" ReadOnly="True" CssClass="TxtBox"></asp:TextBox>
                            </td>
                            </tr>
                         <tr>
                            <td style="height: 22px" >
                                Beneficiary Alert Status:
                             </td>
                            <td style="height: 22px" >
                                <asp:TextBox ID="Lbl_BAS" runat="server" ReadOnly="True" CssClass="TxtBox"></asp:TextBox>
                             </td>
                        </tr>
                         <tr>
                            
                            <td style="height: 22px" >
                                Beneficiary Alert DateTime:</td>
                            <td style="height: 22px" >
                                <asp:TextBox ID="Lbl_BADT" runat="server" ReadOnly="True" CssClass="TxtBox"></asp:TextBox>
                             </td>
                        </tr>
                         <tr>
                            
                            <td style="height: 22px" >
                                Resend Alert Status:</td>
                            <td style="height: 22px" >
                                <asp:TextBox ID="Lbl_RAS" runat="server" ReadOnly="True" CssClass="TxtBox"></asp:TextBox>
                             </td>
                        </tr>
                         <tr>
                            
                            <td style="height: 22px" >
                                Resend Alert DateTime:</td>
                            <td >
                                <asp:TextBox ID="Lbl_RADT" runat="server" ReadOnly="True" CssClass="TxtBox"></asp:TextBox>
                             </td>
                        </tr>
                        </tbody>
                        </table>
                        </fieldset>&nbsp;
                         <fieldset style="border:thin solid White; color:White; height: 220px;width:274px;" >
                        <legend style="color: #FFFFFF" >Transaction Data</legend>
                        <table style="width: 274px; height: auto;" >
                        <tbody  >
                        <tr>
                            <td  style="width: 134px">Transaction Code:</td>
                            <td >
                                <asp:TextBox ID="Lbl_TC" runat="server" ReadOnly="True" CssClass="TxtBox"></asp:TextBox>
                                                </td>
                        </tr>
                        <tr>    
                            <td  style="width: 134px">Beneficiary Mobile:</td>
                            <td >
                                <asp:TextBox ID="Lbl_BM" runat="server" ReadOnly="True" CssClass="TxtBox"></asp:TextBox>
                            </td>
                            </tr>
                         <tr>
                            
                            <td class="style13" style="width: 134px">Depositor Mobile:</td>
                            <td class="style14">
                                <asp:TextBox ID="Lbl_DM" runat="server" ReadOnly="True" CssClass="TxtBox"></asp:TextBox>
                             </td>
                        </tr>
                        
                        
                         <tr>
                            
                            <td class="style15" style="width: 134px">Transaction Sequence:</td>
                            <td class="style16">
                                <asp:TextBox ID="Lbl_TRXSeq" runat="server" ReadOnly="True" CssClass="TxtBox"></asp:TextBox>
                             </td>
                        </tr>
                        
                        
                         <tr>
                            
                            <td class="style9" style="width: 134px">Amount:</td>
                            <td class="style10">
                                <asp:TextBox ID="Lbl_AMT" runat="server" ReadOnly="True" CssClass="TxtBox"></asp:TextBox>
                             </td>
                        </tr>
                        
                        
                         <tr>
                            
                            <td class="style9" style="width: 134px">ReActivation Counter:</td>
                            <td class="style10">
                                <asp:TextBox ID="Lbl_ReActivateC" runat="server" ReadOnly="True" 
                                    CssClass="TxtBox"></asp:TextBox>
                             </td>
                        </tr>
                        
                        
                         <tr>
                            
                            <td class="style9" style="width: 134px">Invalid Withdrawal Trials:</td>
                            <td class="style10">
                                <asp:TextBox ID="Lbl_WithdrawalTrials" runat="server" ReadOnly="True" 
                                    CssClass="TxtBox"></asp:TextBox>
                             </td>
                        </tr>
                        
                        
                        </tbody>
                        </table>
                        </fieldset>&nbsp;
                        <fieldset style="border:thin solid White; color:White;width:263px; height: 190px;" >
                        <legend style="color: #FFFFFF" >Transaction Status </legend>
                        <table style="height: auto; width: 253px;" >
                        <tbody  >
                        <tr>
                            <td class="style19" style="width: 133px"><asp:Label ID="Label56" runat="server" 
                                    Font-Bold="False" ForeColor="White" 
                                Text="DepositStatus:"></asp:Label></td>
                            <td>
                                <asp:TextBox ID="Lbl_DS" runat="server" ReadOnly="True" CssClass="TxtBox" 
                                    Width="110px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>    
                            <td class="style19" style="width: 133px"><asp:Label ID="Label59" runat="server" 
                                    Font-Bold="False" ForeColor="White" 
                                Text="Withdrawal Status:"></asp:Label></td>
                            <td>
                                <asp:TextBox ID="Lbl_WS" runat="server" ReadOnly="True" CssClass="TxtBox" 
                                    Width="110px"></asp:TextBox>
                                                </td>
                        </tr>
                        <tr>
                            <td class="style21" style="width: 133px">Deposit DateTime:</td>
                            <td>
                                <asp:TextBox ID="Lbl_DDT" runat="server" ReadOnly="True" CssClass="TxtBox" 
                                    Width="110px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="style21" style="width: 133px">Withdrawal DateTime:</td>
                            <td>
                                <asp:TextBox ID="Lbl_WDT" runat="server" ReadOnly="True" CssClass="TxtBox" 
                                    Width="110px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="style21" style="width: 133px">Redemption Status:</td>
                            <td>
                                <asp:TextBox ID="Lbl_RS" runat="server" ReadOnly="True" CssClass="TxtBox" 
                                    Width="110px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="style21" style="width: 133px">Redemption DateTime:</td>
                            <td>
                                <asp:TextBox ID="Lbl_RDT" runat="server" ReadOnly="True" CssClass="TxtBox" 
                                    Width="110px"></asp:TextBox>
                            </td>
                        </tr>
                        </tbody>
                        </table>
                        </fieldset>&nbsp;
                       
                         
                        <fieldset style="border:thin solid White ; color:White;width:166px; height: 190px ;" >
                        <legend style="color: #FFFFFF" >Transaction Source </legend>
                        <table style="height: auto " >
                        <tbody  >
                        <tr>
                            <td class="style11">
                                <asp:Label ID="Label53" runat="server" Font-Bold="False" ForeColor="White" 
                                Text="Country:"></asp:Label></td>
                            <td class="style11">
                                <asp:TextBox ID="Lbl_CC" runat="server" ReadOnly="True" CssClass="TxtBox" 
                                    Width="100px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>    
                            <td class="style12">
                                <asp:Label ID="Label54" runat="server" Font-Bold="False" ForeColor="White" 
                                Text="Bank:"></asp:Label></td>
                            <td class="style12">
                                <asp:TextBox ID="Lbl_BC" runat="server" ReadOnly="True" CssClass="TxtBox" 
                                    Width="100px"></asp:TextBox>
                            </td>
                            </tr>
                         <tr>
                            
                            <td><asp:Label ID="Label55" runat="server" Font-Bold="True" ForeColor="White" 
                                Text="ATM:"></asp:Label>
                                <br />
                             </td>
                            <td>
                                <asp:TextBox ID="Lbl_AId" runat="server" ReadOnly="True" CssClass="TxtBox" 
                                    Width="100px"></asp:TextBox>
                                <br />
                             </td>
                        </tr>
                        </tbody>
                        </table>
                        </fieldset>&nbsp;
                        
                        
                                <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            </div>
    
</div> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Button ID="btn_Activate" runat="server" Height="22px" 
                    style="font-weight: 700" Text="Activate" Width="74px" />
                <asp:Button ID="btn_Hold" runat="server" Height="22px" style="font-weight: 700" 
                    Text="Hold" />
                <asp:Button ID="btn_Unhold" runat="server" Height="22px" 
                    style="font-weight: 700" Text="Unhold" Width="57px" />
                <input id="Btn_Unblock" type="button" value="UnBlock" runat="server" 
             onclick="document.getElementById('ctl00$ContentPlaceHolder1$drpd_Unblock').style.display = 'inline'; document.getElementById('ctl00$ContentPlaceHolder1$Btn_UnblockConfirm').style.display = 'inline';" 
                 style="font-weight: bold; height: 22px;"/><asp:DropDownList ID="drpd_Unblock" runat="server" CssClass="ResendCB">
                 <asp:ListItem>--------</asp:ListItem>
                 <asp:ListItem Value="09">Release</asp:ListItem>
                 <asp:ListItem Value="19">Mark transaction as withdrawn</asp:ListItem>
             </asp:DropDownList>
             
                <asp:Button ID="Btn_UnblockConfirm" runat="server" CssClass="ResendCB" 
                 Font-Bold="True" Height="22px" Text="Confirm" />
             
                <input id="btn_Resend0" type="button" value="Resend" runat="server" 
             onclick="document.getElementById('ctl00$ContentPlaceHolder1$drpd_Resend').style.display = 'inline'; document.getElementById('ctl00$ContentPlaceHolder1$Btn_ResendConfirm').style.display = 'inline';" 
                 style="font-weight: bold; height: 22px;"/> &nbsp;<asp:DropDownList ID="drpd_Resend" runat="server" CssClass="ResendCB" >
                    <asp:ListItem>-----</asp:ListItem>
                    <asp:ListItem Value="06">Both(Depositor and Beneficiary)</asp:ListItem>
                    <asp:ListItem Value="16">Depositor SMS</asp:ListItem>
                    <asp:ListItem Value="26">Beneficiary SMS</asp:ListItem>
                    <asp:ListItem Value="36">Redemption PIN</asp:ListItem>
                    
                </asp:DropDownList>
    
                <asp:Button ID="Btn_ResendConfirm" runat="server" CssClass="ResendCB" 
                 Font-Bold="True" Height="23px" Text="Confirm" />
                 
                 <asp:Button ID="Btn_ResetKT" runat="server" Text="Reset Key Trials" Font-Bold="True" Height="22px" Width="118px" />
                 <asp:Button ID="Btn_Expire" runat="server" 
    Text="Expire Transaction" Font-Bold="True" Height="22px" Width="141px" 
    Visible="False" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder4" Runat="Server">
    <asp:Label ID="Lbl_TH" runat="server" Font-Bold="True" Font-Size="Medium" 
                            ForeColor="Black" Visible="False">Transaction 
History:</asp:Label>
    <br />
    <br />
    <asp:ImageButton ID="BTNConfirm" runat="server" ImageUrl="~/Images/Confirm.png" 
        Visible="False" />
    <br />
    <br />
    <asp:Label ID="Lbl_Status" runat="server" Font-Bold="True" Font-Size="Medium" 
                            ForeColor="Red" Visible="False"></asp:Label>
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                                DataSourceID="SqlDataSource2" AllowPaging="True" 
        Width="100%">
                                <RowStyle CssClass="RowStyle" />
                                <Columns>
                                    <asp:BoundField DataField="Action" HeaderText="Action" ReadOnly="True" 
                                        SortExpression="Action" />
                                    <asp:BoundField DataField="ActionDateTime" HeaderText="ActionDateTime" 
                                        SortExpression="ActionDateTime" />
                                    <asp:BoundField DataField="ActionReason" HeaderText="ActionReason" 
                                        SortExpression="ActionReason" />
                                    <asp:BoundField DataField="ActionStatus" HeaderText="ActionStatus" 
                                        SortExpression="ActionStatus" />
                                    <asp:BoundField DataField="DispensedNotes" HeaderText="DispensedNotes" 
                                        SortExpression="DispensedNotes" />
                                    <asp:BoundField DataField="DispensedAmount" HeaderText="Dispensed                                            Amount" 
                                        SortExpression="DispensedAmount" />
                                    <asp:BoundField DataField="Cassette1" HeaderText="Cassette1" 
                                        SortExpression="Cassette1" />
                                    <asp:BoundField DataField="Cassette2" HeaderText="Cassette2" 
                                        SortExpression="Cassette2" />
                                    <asp:BoundField DataField="Cassette3" HeaderText="Cassette3" 
                                        SortExpression="Cassette3" />
                                    <asp:BoundField DataField="Cassette4" HeaderText="Cassette4" 
                                        SortExpression="Cassette4" />
                                    <asp:BoundField DataField="CommissionAmount" HeaderText="Commission" 
                                        SortExpression="CommissionAmount" />
                                    <asp:BoundField DataField="ATMId" HeaderText="ATMId" SortExpression="ATMId" />
                                </Columns>
                    <PagerStyle CssClass="PagerStyle" />
                    <SelectedRowStyle CssClass="SelectedRowStyle" />
                    <HeaderStyle CssClass="HeaderStyle" />
                    <EditRowStyle CssClass="EditRowStyle" />
                    <AlternatingRowStyle CssClass="AltRowStyle" />
                            </asp:GridView>
                            <br />
                            <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
        ConnectionString="<%$ ConnectionStrings:NCRMoneyFerConnection %>" 
        
        SelectCommand="SELECT (SELECT RequestTypeDescription FROM dbo.RequestType WHERE (RequestTypeCode = dbo.TransactionNestedActions.Action)) AS Action, ActionDateTime, ActionReason, ActionStatus, DispensedNotes, DispensedAmount, Cassette1, Cassette2, Cassette3, Cassette4, CommissionAmount,ATMId FROM dbo.TransactionNestedActions WHERE (TransactionCode = @TransactionCode) ORDER BY ActionDateTime">
                                <SelectParameters>
                                    <asp:SessionParameter Name="TransactionCode" SessionField="Trx" />
                                </SelectParameters>
    </asp:SqlDataSource>
    </asp:Content>

