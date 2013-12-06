<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FileShipping.aspx.cs" Inherits="FileShipping" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Abacus Settlements, LLC - Ship Files</title>
    <link href="Scripts/_common/styles/fonts.css" rel="stylesheet" type="text/css" />
    <link href="Scripts/_common/styles/global.css" rel="stylesheet" type="text/css" />
    <link href="Scripts/_common/styles/select.css" rel="stylesheet" type="text/css" />
    <link href="Scripts/_common/styles/theme.css" rel="stylesheet" type="text/css" />
    <link href="Scripts/_forms/controls/controls.css" rel="stylesheet" type="text/css" />
    <link href="Scripts/_forms/controls/form.css" rel="stylesheet" type="text/css" />
    <link href="Scripts/_nav/nav.css" rel="stylesheet" type="text/css" />
    <link href="Scripts/_nav/tabs.css" rel="stylesheet" type="text/css" />
    </head>
<body>
    <form id="form1" runat="server">
        <div id="InvestorDetails">

            <asp:GridView ID="GridViewInvestors" runat="server" CssClass="ms-crm-List-Sortable" DataKeyNames="accountid" AutoGenerateColumns="False" OnRowCreated="GridViewInvestors_RowCreated" Width="550px">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:CheckBox ID="SelectColumn" runat="server" width="50px"/>
                        </ItemTemplate>

                    </asp:TemplateField>
                  
                    <asp:BoundField DataField="name" HeaderText="Investor Name" />
                    <asp:BoundField DataField="emailaddress1" HeaderText="Email Address" />
                  
                </Columns>
                <HeaderStyle CssClass="ms-crm-List-DataColumnHeader" />
                <RowStyle CssClass="ms-crm-List-DataArea" />
            </asp:GridView>
                    <asp:TextBox ID="TextBoxNotes" runat="server" Height="58px" TextMode="MultiLine" Width="541px"></asp:TextBox>
                    <div id="seperator" >
                        <br />
                        <br />
        </div>
             <asp:GridView ID="GridViewDocumentDetails" runat="server" AutoGenerateColumns="False" DataKeyNames="DocumentName" OnSelectedIndexChanged="GridViewDocumentDetails_SelectedIndexChanged" Width="550px">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:CheckBox ID="SelectColumn" runat="server" Width="50px" />
                        </ItemTemplate>
                    </asp:TemplateField>                  
                    <asp:BoundField DataField="DocumentName" HeaderText="Document Name" />
                </Columns>
                 <RowStyle CssClass="ms-crm-List-DataArea" />
            </asp:GridView>

        </div>
        <div id="DocumentDetails">
        </div>

        <div id="ErrorMessage">

            <asp:Label ID="LabelErrorMessage" runat="server" ForeColor="#FF3300"></asp:Label>

        </div>
        <div id="ButtonPanel" "ms-crm-Dialog-Footer ms-crm-Dialog-Footer-Right">
            <%--   onmouseout="Mscrm.ButtonUtils.hoverOff(this);" onmouseover="Mscrm.ButtonUtils.hoverOn(this);"  onmouseout="Mscrm.ButtonUtils.hoverOff(this);" onmouseover="Mscrm.ButtonUtils.hoverOn(this);" --%>
            <table class="ms-crm-MaxWidth">
                <tr>
                    <td class="ms-crm-Dialog-Footer ms-crm-Dialog-Footer-Left" style="width: 411px">
                        <asp:Button ID="butBegin" runat="server" class="ms-crm-Button"  Text="Ok" Width="100px" OnClick="butBegin_Click" CssClass="ms-crm-Button"/>
                        &nbsp; &nbsp;&nbsp;<asp:Button ID="cmdDialogCancel" runat="server" class="ms-crm-Button" Text="Cancel" Width="100px" CssClass="ms-crm-Button"/>
                    </td>
                    <td class="ms-crm-Dialog-Footer ms-crm-Dialog-Footer-Right">
                        <%--           <asp:Button ID="butBegin" runat="server" class="ms-crm-Button" Text="Ok" Width="100px" OnClick="butBegin_Click"/>--%>
                    </td>
                </tr>
            </table>
           
        </div>
    </form>
</body>
</html>
