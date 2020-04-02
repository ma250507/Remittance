<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Admin.aspx.vb" Inherits="Admin" title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ImageButton ID="AdminConBTN" runat="server" 
    ImageUrl="~/Images/AdminConfirm.jpg" Visible="False" />
<br />
<asp:Label ID="Lbl_Status" runat="server" Font-Bold="True" Font-Size="Medium" 
    ForeColor="Red" Visible="False"></asp:Label>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder4" Runat="Server">

    <script type="text/javascript">

/* Optional: Temporarily hide the "tabber" class so it does not "flash"
   on the page as plain HTML. After tabber runs, the class is changed
   to "tabberlive" and it will appear. */

document.write('<style type="text/css">.tabber{display:none;}<\/style>');

/*==================================================
  Set the tabber options (must do this before including tabber.js)
  ==================================================*/
var tabberOptions = {

  'cookie':"tabber", /* Name to use for the cookie */

  'onLoad': function(argsObj)
  {
    var t = argsObj.tabber;
    var i;

    /* Optional: Add the id of the tabber to the cookie name to allow
       for multiple tabber interfaces on the site.  If you have
       multiple tabber interfaces (even on different pages) I suggest
       setting a unique id on each one, to avoid having the cookie set
       the wrong tab.
    */
    if (t.id) {
      t.cookie = t.id + t.cookie;
    }

    /* If a cookie was previously set, restore the active tab */
    i = parseInt(getCookie(t.cookie));
    if (isNaN(i)) { return; }
    t.tabShow(i);
    /*alert('getCookie(' + t.cookie + ') = ' + i);*/
  },

  'onClick':function(argsObj)
  {
    var c = argsObj.tabber.cookie;
    var i = argsObj.index;
    /*alert('setCookie(' + c + ',' + i + ')');*/
    setCookie(c, i);
  }
};

/*==================================================
  Cookie functions
  ==================================================*/
function setCookie(name, value, expires, path, domain, secure) {
    document.cookie= name + "=" + escape(value) +
        ((expires) ? "; expires=" + expires.toGMTString() : "") +
        ((path) ? "; path=" + path : "") +
        ((domain) ? "; domain=" + domain : "") +
        ((secure) ? "; secure" : "");
}

function getCookie(name) {
    var dc = document.cookie;
    var prefix = name + "=";
    var begin = dc.indexOf("; " + prefix);
    if (begin == -1) {
        begin = dc.indexOf(prefix);
        if (begin != 0) return null;
    } else {
        begin += 2;
    }
    var end = document.cookie.indexOf(";", begin);
    if (end == -1) {
        end = dc.length;
    }
    return unescape(dc.substring(begin + prefix.length, end));
}
function deleteCookie(name, path, domain) {
    if (getCookie(name)) {
        document.cookie = name + "=" +
            ((path) ? "; path=" + path : "") +
            ((domain) ? "; domain=" + domain : "") +
            "; expires=Thu, 01-Jan-70 00:00:01 GMT";
    }
}

</script>
<script type="text/javascript" src="scripts/tabber.js"></script>
<div class="tabber">
            <div class="tabbertab">
                <h2>
                    DataBase</h2>
                        <table>
                            <tr>
                                <td>
                                    IP:
                                </td>
                                <td class="style1">
                                    <asp:TextBox ID="txt_IP" runat="server"></asp:TextBox>
                                    </td>
                            </tr>
                            <tr>
                                <td>
                                    Data Base Name:</td>
                                <td class="style1">
                                    <asp:TextBox ID="txt_DB" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    User Name:</td>
                                <td class="style1">
                                    <asp:TextBox ID="txt_UN" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Password:</td>
                                <td class="style1">
                                    <asp:TextBox ID="txt_PWD" runat="server"></asp:TextBox>
                                </td>
                                <td class="style2">
                                    <asp:Button ID="btn_Save" runat="server" Height="24px" style="width: 42px" 
                                        Text="Save" Width="38px" />
                                </td> 
                            </tr>
                            
                        </table>
                    </div>
           <%--  <div class="tabbertab">
             <h2>Groups</h2>
             <asp:GridView ID="GridView2" runat="server" AllowPaging="True" 
                            AutoGenerateColumns="False" BackColor="#F1F1F1" BorderStyle="Outset" 
                            DataKeyNames="ID" DataSourceID="SqlDataSource2" GridLines="None" 
                            ShowFooter="True" AllowSorting="True" Width="1000px">
                            <RowStyle BackColor="Gainsboro" HorizontalAlign="Center" 
                                VerticalAlign="Middle" />
                            <Columns>
                                <asp:CommandField ButtonType="Image" CancelImageUrl="~/Images/cancel_icon.png" 
                                    DeleteImageUrl="~/Images/delete_icon.gif" EditImageUrl="~/Images/EditIcon.png" 
                                    ShowDeleteButton="True" ShowEditButton="True" 
                                    UpdateImageUrl="~/Images/confirmation_icon.png" />
                                <asp:TemplateField HeaderText="ID" SortExpression="ID">
                                    <FooterTemplate>
                                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" 
                                            CommandName="New" OnClick="GroupADD_Click" Text="ADD"></asp:LinkButton>
                                        <asp:TextBox ID="TXT_GroupId" runat="server"></asp:TextBox>
                                    </FooterTemplate>
                                    <EditItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Name" SortExpression="Name">
                                    <FooterTemplate>
                                        <asp:TextBox ID="TXT_GroupName" runat="server"></asp:TextBox>
                                    </FooterTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Name") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Reports" SortExpression="Reports">
                                    <FooterTemplate>
                                        <asp:CheckBox ID="CB_Reports" runat="server" />
                                    </FooterTemplate>
                                    <EditItemTemplate>
                                        <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# Bind("Reports") %>' />
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# Bind("Reports") %>' 
                                            Enabled="false" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Maintenance" SortExpression="Maintenance">
                                    <FooterTemplate>
                                        <asp:CheckBox ID="CB_Maintenance" runat="server" />
                                    </FooterTemplate>
                                    <EditItemTemplate>
                                        <asp:CheckBox ID="CheckBox2" runat="server" 
                                            Checked='<%# Bind("Maintenance") %>' />
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckBox2" runat="server" 
                                            Checked='<%# Bind("Maintenance") %>' Enabled="false" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Administration" SortExpression="Administration">
                                    <FooterTemplate>
                                        <asp:CheckBox ID="CB_Admin" runat="server" />
                                    </FooterTemplate>
                                    <EditItemTemplate>
                                        <asp:CheckBox ID="CheckBox3" runat="server" 
                                            Checked='<%# Bind("Administration") %>' />
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckBox3" runat="server" 
                                            Checked='<%# Bind("Administration") %>' Enabled="false" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Users" SortExpression="Users">
                                    <FooterTemplate>
                                        <asp:CheckBox ID="CB_Users" runat="server" />
                                    </FooterTemplate>
                                    <EditItemTemplate>
                                        <asp:CheckBox ID="CheckBox4" runat="server" Checked='<%# Bind("Users") %>' />
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckBox4" runat="server" Checked='<%# Bind("Users") %>' 
                                            Enabled="false" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Teller" SortExpression="Teller">
                                    <EditItemTemplate>
                                        <asp:CheckBox ID="CheckBox6" runat="server" Checked='<%# Bind("Teller") %>' />
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:CheckBox ID="CB_Teller" runat="server" />
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckBox5" runat="server" Checked='<%# Bind("Teller") %>' 
                                            Enabled="False" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Registeration" SortExpression="Registeration">
                                    <FooterTemplate>
                                        <asp:CheckBox ID="CB_Registeration" runat="server" />
                                    </FooterTemplate>
                                    <EditItemTemplate>
                                        <asp:CheckBox ID="CheckBox8" runat="server" 
                                            Checked='<%# Bind("Registeration") %>' />
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckBox7" runat="server" 
                                            Checked='<%# Bind("Registeration") %>' Enabled="False" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle BackColor="White" HorizontalAlign="Center" 
                                VerticalAlign="Middle" />
                            <HeaderStyle BackColor="#0083C1" ForeColor="White" HorizontalAlign="Center" 
                                VerticalAlign="Middle" />
                            <AlternatingRowStyle BackColor="White" />
                        </asp:GridView>
                            <br />
                            <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                                ConnectionString="<%$ ConnectionStrings:NCRMoneyFerConnection %>" 
                                DeleteCommand="DELETE FROM [Groups] WHERE [ID] = @original_ID" 
                                InsertCommand="INSERT INTO [Groups] ([ID], [Name], [Reports], [Maintenance], [Administration], [Users], [Teller], [Registeration] ) VALUES (@ID, @Name, @Reports, @Maintenance, @Administration, @Users, @Teller,@Registeration)" 
                                OldValuesParameterFormatString="original_{0}" 
                                SelectCommand="SELECT ID, Name, Reports, Maintenance, Administration, Users, Teller, Registeration FROM dbo.Groups" 
                                
                                UpdateCommand="UPDATE [Groups] SET [Name] = @Name, [Reports] = @Reports, [Maintenance] = @Maintenance, [Administration] = @Administration, [Users] = @Users, [Teller] = @Teller , [Registeration] = @Registeration
WHERE [ID] = @original_ID">
                                <DeleteParameters>
                                    <asp:Parameter Name="original_ID" Type="String" />
                                </DeleteParameters>
                                <UpdateParameters>
                                    <asp:Parameter Name="Name" Type="String" />
                                    <asp:Parameter Name="Reports" Type="Boolean" />
                                    <asp:Parameter Name="Maintenance" Type="Boolean" />
                                    <asp:Parameter Name="Administration" Type="Boolean" />
                                    <asp:Parameter Name="Users" Type="Boolean" />
                                    <asp:Parameter Name="Teller" Type="Boolean" />
                                    <asp:Parameter Name="Registeration" />
                                    <asp:Parameter Name="original_ID" Type="String" />
                                </UpdateParameters>
                                <InsertParameters>
                                    <asp:Parameter Name="ID" Type="String" />
                                    <asp:Parameter Name="Name" Type="String" />
                                    <asp:Parameter Name="Reports" Type="Boolean" />
                                    <asp:Parameter Name="Maintenance" Type="Boolean" />
                                    <asp:Parameter Name="Administration" Type="Boolean" />
                                    <asp:Parameter Name="Users" Type="Boolean" />
                                    <asp:Parameter Name="Teller" Type="Boolean" />
                                    <asp:Parameter Name="Registeration" />
                                </InsertParameters>
                            </asp:SqlDataSource>
             </div>--%> <%-- Groups are hidden and visible under users now --%>
          <div class="tabbertab ">
          <h2>Terminal</h2>
             

              <table style="width: 30%">
                  <tr>
                      <td>
                           <asp:Label ID="Label13" runat="server" Text="Search"></asp:Label>
                           </td>
                      <td>
                          <asp:TextBox ID="TXT_ATM_Search" runat="server" Width="166px"></asp:TextBox>
                          </td>
                      <td>
                          <asp:Button ID="Btn_ATM_Search" runat="server" Text="Search" Width="64px" />
                      </td>
                  </tr>
              </table>
             

             <asp:GridView ID="GridView3" runat="server" AllowPaging="True" 
                            AutoGenerateColumns="False" BackColor="#F1F1F1" BorderStyle="Outset" 
                            DataKeyNames="ATMId,CountryCode,BankCode" DataSourceID="SqlDataSource1" GridLines="None" 
                            ShowFooter="True" AllowSorting="True" Width="1050px">
                            <RowStyle BackColor="Gainsboro" HorizontalAlign="Center" 
                                VerticalAlign="Middle" />
                            <Columns>
                                <asp:CommandField ButtonType="Image" CancelImageUrl="~/Images/cancel_icon.png" 
                                    DeleteImageUrl="~/Images/delete_icon.gif" EditImageUrl="~/Images/EditIcon.png" 
                                    InsertImageUrl="~/Images/add.png" ShowEditButton="True" 
                                    UpdateImageUrl="~/Images/confirmation_icon.png" />
                                <asp:TemplateField>
                                    <FooterTemplate>
                                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" 
                                            CommandName="New" OnClick="ATMADD_Click" Text="ADD" ValidationGroup="ATM"></asp:LinkButton>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Terminal ID" SortExpression="ATMId">
                                    <FooterTemplate>
                                        <asp:TextBox ID="Txt_ATMID" runat="server"></asp:TextBox>
                                    </FooterTemplate>
                                    <EditItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("ATMId") %>'></asp:Label>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("ATMId") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Terminal Name" SortExpression="ATMName">
                                    <FooterTemplate>
                                        <asp:TextBox ID="TXT_ATMName" runat="server"></asp:TextBox>
                                    </FooterTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox7" runat="server" Text='<%# Bind("ATMName") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label10" runat="server" Text='<%# Bind("ATMName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Location" SortExpression="ATMLocation">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="drpd_EATMBr" runat="server" Text='<%# CheckNULL(Eval("ATMLocation")) %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="drpd_AATMBr" runat="server" 
                                            Text='<%# CheckNULL(Eval("ATMLocation")) %>'></asp:TextBox>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("ATMLocation") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="CountryCode" SortExpression="CountryCode">
                                    <FooterTemplate>
                                        <asp:DropDownList ID="drpd_CountryCode" runat="server" 
                                            DataSourceID="CountryCode_DS" DataTextField="CountryName" 
                                            DataValueField="CountryCode">
                                        </asp:DropDownList>
                                        <asp:SqlDataSource ID="CountryCode_DS" runat="server" 
                                            ConnectionString="<%$ ConnectionStrings:NCRMoneyFerConnection %>" 
                                            SelectCommand="SELECT [CountryCode], [CountryName] FROM [Country]">
                                        </asp:SqlDataSource>
                                    </FooterTemplate>
                                    <EditItemTemplate>
                                        <asp:Label ID="Label2" runat="server" Text='<%# Eval("CountryCode") %>'></asp:Label>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("CountryCode") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="BankCode" SortExpression="BankCode">
                                    <FooterTemplate>
                                        <asp:DropDownList ID="drpd_BankCode" runat="server" DataSourceID="BankCode_DS" 
                                            DataTextField="BankName" DataValueField="BankCode">
                                        </asp:DropDownList>
                                        <asp:SqlDataSource ID="BankCode_DS" runat="server" 
                                            ConnectionString="<%$ ConnectionStrings:NCRMoneyFerConnection %>" 
                                            SelectCommand="SELECT [BankName], [BankCode] FROM [Bank]">
                                        </asp:SqlDataSource>
                                    </FooterTemplate>
                                    <EditItemTemplate>
                                        <asp:Label ID="Label3" runat="server" Text='<%# Eval("BankCode") %>'></asp:Label>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("BankCode") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cassitte1Value" SortExpression="Cassitte1Value">
                                    <FooterTemplate>
                                        <asp:TextBox ID="Txt_Cassitte1" runat="server" Width="100px"></asp:TextBox>
                                    </FooterTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("Cassitte1Value") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label5" runat="server" Text='<%# Bind("Cassitte1Value") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cassitte2Value" SortExpression="Cassitte2Value">
                                    <FooterTemplate>
                                        <asp:TextBox ID="Txt_Cassitte2" runat="server" Width="100px"></asp:TextBox>
                                    </FooterTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("Cassitte2Value") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label6" runat="server" Text='<%# Bind("Cassitte2Value") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cassitte3Value" SortExpression="Cassitte3Value">
                                    <FooterTemplate>
                                        <asp:TextBox ID="Txt_Cassitte3" runat="server" Width="100px"></asp:TextBox>
                                    </FooterTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("Cassitte3Value") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label7" runat="server" Text='<%# Bind("Cassitte3Value") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cassitte4Value" SortExpression="Cassitte4Value">
                                    <FooterTemplate>
                                        <asp:TextBox ID="Txt_Cassitte4" runat="server" Width="100px"></asp:TextBox>
                                    </FooterTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox5" runat="server" Text='<%# Bind("Cassitte4Value") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label8" runat="server" Text='<%# Bind("Cassitte4Value") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Terminal IP Address" 
                                    SortExpression="ATMIPAddress">
                                    <FooterTemplate>
                                        <asp:TextBox ID="Txt_ATMIPAddress" runat="server" Width="100px"></asp:TextBox>
                                    </FooterTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox6" runat="server" Text='<%# Bind("ATMIPAddress") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label9" runat="server" Text='<%# Bind("ATMIPAddress") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="IsTeller" SortExpression="IsTeller">
                                    <FooterTemplate>
                                        <asp:CheckBox ID="CB_IsTeller" runat="server" />
                                    </FooterTemplate>
                                    <EditItemTemplate>
                                        <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# Bind("IsTeller") %>' />
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# Bind("IsTeller") %>' 
                                            Enabled="false" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Unit Number" SortExpression="TerminalID">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TXT_EDT_TermID" runat="server" 
                                            Text='<%# Bind("TerminalID") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="TXT_ADD_TermID" runat="server" 
                                            Text='<%# Bind("TerminalID") %>'></asp:TextBox>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label11" runat="server" Text='<%# Bind("TerminalID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="MerchantID" SortExpression="MerchantID">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TXT_EDT_MerchantID" runat="server" 
                                            Text='<%# Bind("MerchantID") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="TXT_ADD_MerchantID" runat="server" 
                                            Text='<%# Bind("MerchantID") %>'></asp:TextBox>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label12" runat="server" Text='<%# Bind("MerchantID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle BackColor="White" HorizontalAlign="Center" 
                                VerticalAlign="Middle" />
                            <HeaderStyle BackColor="#0083C1" ForeColor="White" HorizontalAlign="Center" 
                                VerticalAlign="Middle" />
                            <AlternatingRowStyle BackColor="White" />
                        </asp:GridView>
              <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                                ConnectionString="<%$ ConnectionStrings:NCRMoneyFerConnection %>" 
                                DeleteCommand="DELETE FROM [ATM] WHERE [ATMId] = @original_ATMId AND [CountryCode] = @original_CountryCode AND [BankCode] = @original_BankCode" 
                                InsertCommand="INSERT INTO [ATM] ([ATMId], [ATMLocation], [CountryCode], [BankCode], [Cassitte1Value], [Cassitte2Value], [Cassitte3Value], [Cassitte4Value], [ATMIPAddress], [IsTeller], [ATMName],[TerminalID],[MerchantID]) VALUES (@ATMId, @ATMLocation, @CountryCode, @BankCode, @Cassitte1Value, @Cassitte2Value, @Cassitte3Value, @Cassitte4Value, @ATMIPAddress, @IsTeller, @ATMName,@TerminalID,@MerchantID)" 
                                OldValuesParameterFormatString="original_{0}" 
                                SelectCommand="SELECT * FROM [ATM]" 
                                
                                
                                
                                UpdateCommand="UPDATE [ATM] SET [ATMLocation] = @ATMLocation, [Cassitte1Value] = @Cassitte1Value, [Cassitte2Value] = @Cassitte2Value, [Cassitte3Value] = @Cassitte3Value, [Cassitte4Value] = @Cassitte4Value, [ATMIPAddress] = @ATMIPAddress, [IsTeller] = @IsTeller ,[ATMName]=@ATMName ,[TerminalID]=@TerminalID ,[MerchantID]=@MerchantID WHERE [ATMId] = @original_ATMId AND [CountryCode] = @original_CountryCode AND [BankCode] = @original_BankCode">
                  <DeleteParameters>
                      <asp:Parameter Name="original_ATMId" Type="String" />
                      <asp:Parameter Name="original_CountryCode" Type="String" />
                      <asp:Parameter Name="original_BankCode" Type="String" />
                  </DeleteParameters>
                  <UpdateParameters>
                      <asp:Parameter Name="ATMLocation" Type="String" />
                      <asp:Parameter Name="Cassitte1Value" Type="Byte" />
                      <asp:Parameter Name="Cassitte2Value" Type="Byte" />
                      <asp:Parameter Name="Cassitte3Value" Type="Byte" />
                      <asp:Parameter Name="Cassitte4Value" Type="Byte" />
                      <asp:Parameter Name="ATMIPAddress" Type="String" />
                      <asp:Parameter Name="IsTeller" Type="Boolean" />
                      <asp:Parameter Name="ATMName" />
                      <asp:Parameter Name="original_ATMId" Type="String" />
                      <asp:Parameter Name="original_CountryCode" Type="String" />
                      <asp:Parameter Name="original_BankCode" Type="String" />
                      <asp:Parameter Name="TerminalID" Type="String" />
                      <asp:Parameter Name="MerchantID" Type="String" />
                  </UpdateParameters>
                  <InsertParameters>
                      <asp:Parameter Name="ATMId" Type="String" />
                      <asp:Parameter Name="ATMLocation" Type="String" />
                      <asp:Parameter Name="CountryCode" Type="String" />
                      <asp:Parameter Name="BankCode" Type="String" />
                      <asp:Parameter Name="Cassitte1Value" Type="Byte" />
                      <asp:Parameter Name="Cassitte2Value" Type="Byte" />
                      <asp:Parameter Name="Cassitte3Value" Type="Byte" />
                      <asp:Parameter Name="Cassitte4Value" Type="Byte" />
                      <asp:Parameter Name="ATMIPAddress" Type="String" />
                      <asp:Parameter Name="IsTeller" Type="Boolean" />
                      <asp:Parameter Name="ATMName" />
                      <asp:Parameter Name="TerminalID" Type="String" />
                      <asp:Parameter Name="MerchantID" Type="String" />
                  </InsertParameters>
                            </asp:SqlDataSource>
          </div>
         
            <div class="tabbertab">
            <h2>Bank</h2>               
            <table>
            <tr>
                <td>
                    Bank:
                </td>
                <td>
                        <asp:DropDownList ID="drpd_country" runat="server" Height="22px" Width="300px" 
                        AutoPostBack="True">
                    </asp:DropDownList>
                  
                    <asp:DropDownList ID="drpd_Bank" runat="server" Height="22px" Width="300px" 
                        AutoPostBack="True">
                    </asp:DropDownList>
              </td>
            </tr>
            <tr>
                <td>
                    Maximum Notes Count:
                </td>
                <td>
                    <asp:TextBox ID="txt_MXNC" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Maximum Amount Per Transaction:
                </td>
                <td>
                    <asp:TextBox ID="txt_MXA" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Minimum Amount Per Transaction:
                </td>
                <td>
                    <asp:TextBox ID="txt_MNA" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Receipt Line1:                </td>
                <td>
                    <asp:TextBox ID="txt_RCPTL1" runat="server" TextMode="MultiLine" Width="425px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Receipt Line2:
                </td>
                <td>
                    <asp:TextBox ID="txt_RCPTL2" runat="server" TextMode="MultiLine" Width="425px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Receipt Line3:
                </td>
                <td>
                    <asp:TextBox ID="txt_RCPTL3" runat="server" TextMode="MultiLine" Width="425px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Start Amount1:
                </td>
                <td>
                    <asp:TextBox ID="txt_SA1" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    End Amount1:
                </td>
                <td>
                    <asp:TextBox ID="txt_EA1" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Commission Amount1:
                </td>
                <td>
                    <asp:TextBox ID="txt_CA1" runat="server"></asp:TextBox>
                </td>
            </tr>
            
            <tr>
                <td>
                    Start Amount2:
                </td>
                <td>
                    <asp:TextBox ID="txt_SA2" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    End Amount2:
                </td>
                <td>
                    <asp:TextBox ID="txt_EA2" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Commission Amount2:
                </td>
                <td>
                    <asp:TextBox ID="txt_CA2" runat="server"></asp:TextBox>
                </td>
            </tr>
             <tr>
                <td>
                    Maximum Daily Amount:
                </td>
                <td>
                    <asp:TextBox ID="txt_MXDA" runat="server"></asp:TextBox>
                </td>
            </tr>
             <tr>
                <td>
                    Maintenance ATM:</td>
                <td>
                    <asp:TextBox ID="txt_MATM" runat="server"></asp:TextBox>
                </td>
            </tr>
             <tr>
                <td>
                    Remittance Service Port:</td>
                <td>
                    <asp:TextBox ID="txt_RSP" runat="server"></asp:TextBox>
                </td>
            </tr>
             <tr>
                <td>
                    Remittance Service IP Address:</td>
                <td>
                    <asp:TextBox ID="txt_RSIP" runat="server"></asp:TextBox>
                </td>
            </tr>
             <tr>
                <td>
                    Maximum key Trials:</td>
                <td>
                    <asp:TextBox ID="txt_MXKeyTR" runat="server"></asp:TextBox>
                </td>
            </tr>
             <tr>
                <td>
                    Maximum Reactivate Times:</td>
                <td>
                    <asp:TextBox ID="txt_MXReactivateTimes" runat="server"></asp:TextBox>
                </td>
            </tr>
             <tr>
                <td>
                    Deposit Transaction Expiration Days: </td>
                <td>
                    <asp:TextBox ID="txt_DTExpirationDays" runat="server"></asp:TextBox>
                </td>
            </tr>
             <tr>
                <td>
                    Maximum Monthly Amount:</td>
                <td>
                    <asp:TextBox ID="txt_MXMMAMT" runat="server"></asp:TextBox>
                </td>
            </tr>
             <tr>
                <td>
                    Maximum Daily Count:</td>
                <td>
                    <asp:TextBox ID="txt_DailyMAXCount" runat="server"></asp:TextBox>
                    <asp:Button ID="btn_Bsave" runat="server" Font-Bold="True" Text="Save" 
                        Height="21px" />
                </td>
            </tr>
            </table>
            </div>
           <div class="tabbertab">
            <h2>Branches</h2>
             <asp:GridView ID="GridView4" runat="server" AllowPaging="True" 
                            AutoGenerateColumns="False" BackColor="#F1F1F1" BorderStyle="Outset" 
                            DataKeyNames="BankCode,CountryCode,BranchName" 
                   DataSourceID="SqlDataSource3" GridLines="None" 
                            ShowFooter="True" AllowSorting="True" Width="1000px">
                            <RowStyle BackColor="Gainsboro" HorizontalAlign="Center" 
                                VerticalAlign="Middle" />
                            <Columns>
                                <asp:CommandField ButtonType="Image" CancelImageUrl="~/Images/cancel_icon.png" 
                                    DeleteImageUrl="~/Images/delete_icon.gif" EditImageUrl="~/Images/EditIcon.png" 
                                    ShowDeleteButton="True" ShowEditButton="True" 
                                    UpdateImageUrl="~/Images/confirmation_icon.png" />
                                <asp:TemplateField>
                                    <FooterTemplate>
                                        <asp:LinkButton ID="LNK_BRADD" runat="server" CommandName="BRNew" 
                                            onclick="LNK_BRADD_Click">ADD</asp:LinkButton>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="BankCode" SortExpression="BankCode">
                                    <EditItemTemplate>
                                        <asp:Label ID="Label3" runat="server" Text='<%# Eval("BankCode") %>'></asp:Label>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:DropDownList ID="drpd_BRbc" runat="server" DataSourceID="BRBankCodeDS" 
                                            DataTextField="BankName" DataValueField="BankCode">
                                        </asp:DropDownList>
                                        <asp:SqlDataSource ID="BRBankCodeDS" runat="server" 
                                            ConnectionString="<%$ ConnectionStrings:NCRMoneyFerConnection %>" 
                                            SelectCommand="SELECT [BankName], [BankCode] FROM [Bank]">
                                        </asp:SqlDataSource>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("BankCode") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="CountryCode" SortExpression="CountryCode">
                                    <EditItemTemplate>
                                        <asp:Label ID="Label2" runat="server" Text='<%# Eval("CountryCode") %>'></asp:Label>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:DropDownList ID="drpd_BRcc" runat="server" DataSourceID="BRCountryCodeDS" 
                                            DataTextField="CountryName" DataValueField="CountryCode" >
                                        </asp:DropDownList>
                                        <asp:SqlDataSource ID="BRCountryCodeDS" runat="server" 
                                            ConnectionString="<%$ ConnectionStrings:NCRMoneyFerConnection %>" 
                                            SelectCommand="SELECT [CountryCode], [CountryName] FROM [Country]">
                                        </asp:SqlDataSource>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("CountryCode") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="BranchName" SortExpression="BranchName">
                                    <EditItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("BranchName") %>'></asp:Label>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="TXT_BranchName" runat="server"></asp:TextBox>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("BranchName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Address" SortExpression="Address">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Address") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="TXT_BranchAddress" runat="server"></asp:TextBox>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("Address") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle BackColor="White" HorizontalAlign="Center" 
                                VerticalAlign="Middle" />
                            <EmptyDataTemplate>
                                <table style="width: 100%">
                                    <tr>
                                        <td>
                                            Bank Code</td>
                                        <td>
                                            CountryCode</td>
                                        <td>
                                            Branch Name</td>
                                        <td>
                                            Address</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:DropDownList ID="drpd_EBankCode" runat="server" DataSourceID="EBankCodeDS" 
                                                DataTextField="BankName" DataValueField="BankCode">
                                            </asp:DropDownList>
                                            <asp:SqlDataSource ID="EBankCodeDS" runat="server" 
                                                ConnectionString="<%$ ConnectionStrings:NCRMoneyFerConnection %>" 
                                                SelectCommand="SELECT [BankName], [BankCode] FROM [Bank]">
                                            </asp:SqlDataSource>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="drpd_ECountryCode" runat="server" 
                                                DataSourceID="ECountryCodeDS" DataTextField="CountryName" 
                                                DataValueField="CountryCode">
                                            </asp:DropDownList>
                                            <asp:SqlDataSource ID="ECountryCodeDS" runat="server" 
                                                ConnectionString="<%$ ConnectionStrings:NCRMoneyFerConnection %>" 
                                                SelectCommand="SELECT [CountryCode], [CountryName] FROM [Country]">
                                            </asp:SqlDataSource>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TXT_BranchName" runat="server"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TXT_Address" runat="server"></asp:TextBox>
                                            <asp:Button ID="BTN_EmptyADD" runat="server" CommandName="EmptyBranch"  Text="ADD" />
                                        </td>
                                    </tr>
                                </table>
                            </EmptyDataTemplate>
                            <HeaderStyle BackColor="#0083C1" ForeColor="White" HorizontalAlign="Center" 
                                VerticalAlign="Middle" />
                            <AlternatingRowStyle BackColor="White" />
                        </asp:GridView>
                            <br />
                            <asp:SqlDataSource ID="SqlDataSource3" runat="server" 
                                ConnectionString="<%$ ConnectionStrings:NCRMoneyFerConnection %>" 
                                DeleteCommand="DELETE FROM [Branches] WHERE [BankCode] = @original_BankCode AND [CountryCode] = @original_CountryCode AND [BranchName] = @original_BranchName" 
                                InsertCommand="INSERT INTO [Branches] ([BankCode], [CountryCode], [BranchName], [Address]) VALUES (@BankCode, @CountryCode, @BranchName, @Address)" 
                                OldValuesParameterFormatString="original_{0}" 
                                SelectCommand="SELECT * FROM [Branches]" 
                                
                                
                   UpdateCommand="UPDATE [Branches] SET [Address] = @Address WHERE [BankCode] = @original_BankCode AND [CountryCode] = @original_CountryCode AND [BranchName] = @original_BranchName">
                                <DeleteParameters>
                                    <asp:Parameter Name="original_BankCode" Type="String" />
                                    <asp:Parameter Name="original_CountryCode" Type="String" />
                                    <asp:Parameter Name="original_BranchName" Type="String" />
                                </DeleteParameters>
                                <UpdateParameters>
                                    <asp:Parameter Name="Address" Type="String" />
                                    <asp:Parameter Name="original_BankCode" Type="String" />
                                    <asp:Parameter Name="original_CountryCode" Type="String" />
                                    <asp:Parameter Name="original_BranchName" Type="String" />
                                </UpdateParameters>
                                <InsertParameters>
                                    <asp:Parameter Name="BankCode" Type="String" />
                                    <asp:Parameter Name="CountryCode" Type="String" />
                                    <asp:Parameter Name="BranchName" Type="String" />
                                    <asp:Parameter Name="Address" Type="String" />
                                </InsertParameters>
                            </asp:SqlDataSource>
            
            </div> 
        </div>
</asp:Content>

