<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Review.aspx.cs" Inherits="Review" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Abacus Settlements, LLC - Review Cases</title>
    <link href="Scripts/_common/styles/dialogs.css" rel="stylesheet" />
    <link href="_common/styles/dialogs.css" rel="stylesheet" />
    <link href="Scripts/_common/styles/fonts.css" rel="stylesheet" />
    <link href="Scripts/_common/styles/global.css" rel="stylesheet" />
    <link href="Scripts/_common/styles/select.css" rel="stylesheet" />
    <link href="Scripts/_common/styles/theme.css" rel="stylesheet" />
    <link href="Scripts/_forms/controls/controls.css" rel="stylesheet" />
    <link href="Scripts/_forms/controls/form.css" rel="stylesheet" />
    <link href="Scripts/_nav/nav.css" rel="stylesheet" />
    <link href="Scripts/_nav/tabs.css" rel="stylesheet" />
    <style type="text/css">
        .auto-style1 {
            width: 160px;
        }

        #Spacer {
            height: 44px;
        }

        #Greeting {
            height: 34px;
        }
    </style>

</head>
<body>

    <form id="form1" runat="server">
        <div id="authentiction">
            <asp:Panel ID="AuthenticationPanel" runat="server">
                <table>
                    <tr>
                        <td>UserName</td>
                        <td class="auto-style1" align="left">
                            <asp:TextBox ID="TextBoxUserName" runat="server" Width="249px"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>Password:</td>

                        <td class="auto-style1" align="left">
                            <asp:TextBox ID="TextBoxPassword" runat="server" Width="250px" TextMode="Password"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td></td>
                        <td class="auto-style1">
                            <asp:LinkButton ID="lblLogin" runat="server" OnClick="lblLogin_Click">Login</asp:LinkButton></td>
                        <td>&nbsp;</td>
                    </tr>

                </table>
            </asp:Panel>
        </div>
        <div id="configoptions">
             <asp:Panel ID="ConfigOptions" runat="server">
                <table>
                    <tr>
                        <td>Lookback Period:</td>
                        <td class="auto-style1" align="left">
                            <asp:TextBox ID="tbLookbackPeriod" runat="server" Width="50px" ToolTip="The number of days to lookback in determining which policies to include. A value of 20 for instance, will result in Policies modified in the last 20 days to appear."></asp:TextBox>
                        </td>
                        <td align="left" class="auto-style1">
                            <asp:RangeValidator ID="validatorLookback" runat="server" ControlToValidate="tbLookbackPeriod" ErrorMessage="Lookback period can not exceed 30 days." MaximumValue="30" MinimumValue="1" Type="Integer"></asp:RangeValidator>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>Page Size:</td>
                        <td class="auto-style1" align="left">
                            <asp:TextBox ID="tbPageSize" runat="server" Width="50px" ToolTip="The number of records that should appear in the grid when populated. For instance, a value of 20 will result in up to 20 records in a grid at a time. "></asp:TextBox>
                        </td>
                        <td align="left" class="auto-style1">&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td></td>
                        <td class="auto-style1">
                            <asp:LinkButton ID="lbRefresh" runat="server" OnClick="lblRefresh_Click">Refresh</asp:LinkButton></td>
                        <td class="auto-style1">&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>

                </table>
            </asp:Panel>
        </div>
        <div id="InfoMessage">
            <asp:Label ID="lblInvalidAuthentication" runat="server" Visible="False"></asp:Label>
        </div>
        <div id="Greeting">
            <asp:Label ID="lblGreeting" runat="server" Text="Hello:" CssClass="ms-crm-FormSection" Font-Bold="True"></asp:Label>
        </div>
        <div id="Spacer">
        </div>
        <div>

            <asp:GridView ID="GridViewPolicies" runat="server" AutoGenerateColumns="False" AllowSorting="True" OnSorted="GridViewPolicies_Sorted" OnSorting="GridViewPolicies_Sorting" AllowPaging="True" OnPageIndexChanging="GridViewPolicies_PageIndexChanging1" PageSize="20">
                <Columns>
                    <asp:HyperLinkField DataNavigateUrlFields="d4_policyid" DataNavigateUrlFormatString="~/DisplayFiles.aspx?id={0}" DataTextField="d4_clientpolicyid" HeaderText="Insured Name" SortExpression="d4_policyid">
                        <ControlStyle Font-Underline="True" />
                        <ItemStyle Font-Underline="True" />
                    </asp:HyperLinkField>
                    <asp:BoundField DataField="d4_policynumber" HeaderText="Policy Number" SortExpression="d4_policynumber">
                        <HeaderStyle Width="175px" />
                        <ItemStyle Width="175px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="d4_insurancecompany" HeaderText="Insurance Company" SortExpression="d4_insurancecompany">
                        <HeaderStyle Width="100px" />
                        <ItemStyle Width="100px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="d4_dbamount" HeaderText="DB Amount" />
                    <asp:BoundField DataField="d4_age" HeaderText="Age" SortExpression="d4_age">
                        <HeaderStyle Width="50px" />
                        <ItemStyle Width="50px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="createdon" HeaderText="Timestamp" SortExpression="createdon" Visible="False">
                        <HeaderStyle Width="150px" />
                        <ItemStyle Width="50px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="new_lastshippeddate" HeaderText="Last Activity Date" SortExpression="new_lastshippeddate" />
                </Columns>
                <HeaderStyle CssClass="ms-crm-List-Sortable" Height="40px" />
                <RowStyle CssClass="ms-crm-List-DataArea" Height="30px" />
            </asp:GridView>

        </div>
    </form>
</body>
</html>
