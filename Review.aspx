<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Review.aspx.cs" Inherits="Review" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Abacus Settlements, LLC - Review Cases</title>
    <!--<link href="Scripts/_common/styles/dialogs.css" rel="stylesheet" />
    <link href="_common/styles/dialogs.css" rel="stylesheet" />
    <link href="Scripts/_common/styles/fonts.css" rel="stylesheet" />
    <link href="Scripts/_common/styles/global.css" rel="stylesheet" />
    <link href="Scripts/_common/styles/select.css" rel="stylesheet" />
    <link href="Scripts/_common/styles/theme.css" rel="stylesheet" />
    <link href="Scripts/_forms/controls/controls.css" rel="stylesheet" />
    <link href="Scripts/_forms/controls/form.css" rel="stylesheet" />
    <link href="Scripts/_nav/nav.css" rel="stylesheet" />
    <link href="Scripts/_nav/tabs.css" rel="stylesheet" />-->
    <script type="text/javascript" src="scripts/jquery-1.8.2.min.js"></script>
    <script src="//netdna.bootstrapcdn.com/bootstrap/3.0.3/js/bootstrap.min.js"></script>
    <link href="//netdna.bootstrapcdn.com/bootstrap/3.0.2/css/bootstrap.min.css" rel="stylesheet" />
    <link href="//netdna.bootstrapcdn.com/bootstrap/3.0.0-rc2/css/bootstrap-glyphicons.css" rel="stylesheet" />
    <link href="//netdna.bootstrapcdn.com/bootstrap/3.0.3/css/bootstrap-theme.min.css" rel="stylesheet" />
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

        .header {
            padding-top:5px;
        }
    </style>

</head>
<body>
    <div class="container">
        <div class="header">
            <img width="254" height="44" title="" alt="" src="data:image/jpeg;base64,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" />
            <hr />
        </div>
    </div>
    <div class="container">
        <form id="form1" runat="server" class="form-signin" role="form">
            <div class="row">
                <div class="col-md-4">
                    <asp:Panel ID="AuthenticationPanel" runat="server">
                        <div class="form-group">
                            <label for="TextBoxUserName" class="control-label">User Name</label>
                            <asp:TextBox ID="TextBoxUserName" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label for="TextBoxPassword" class="control-label">Password</label>
                            <asp:TextBox ID="TextBoxPassword" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <asp:LinkButton ID="lblLogin" runat="server" OnClick="lblLogin_Click" CssClass="btn btn-default btn-success">Login</asp:LinkButton>
                        </div>
                    </asp:Panel><br />
                    <table>
                        <tr>
                            <td>
                                <span>
                                    <button type="button" id="configoptionsButton" class="btn btn-default" data-container="body" data-toggle="popover" data-original-title="" title="">
                                      Settings
                                    </button>
                                </span>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>

            <div class="col-md-9">
                <div id="InfoMessage">
                    <asp:Label ID="lblInvalidAuthentication" runat="server" Visible="False"></asp:Label>
                </div>

                <div id="Greeting">
                    <asp:Label ID="lblGreeting" runat="server" Text="Hello:" CssClass="ms-crm-FormSection" Font-Bold="True"></asp:Label>
                </div>

                <div id="Spacer"></div>
                    
                <div>
                    <asp:GridView ID="GridViewPolicies" runat="server" AutoGenerateColumns="False" AllowSorting="True" OnSorted="GridViewPolicies_Sorted" OnSorting="GridViewPolicies_Sorting" AllowPaging="True" OnPageIndexChanging="GridViewPolicies_PageIndexChanging1" PageSize="10" CssClass="table table-striped">
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
                    </asp:GridView>
                </div>
            </div>
            <div id="configoptions" style="display:none;">
                <span>
                    <asp:Panel ID="Panel1" runat="server">
                        <div class="form-group">
                            <label for="tbLookbackPeriod" class="col-md-6 control-label" style="padding:0px">Lookback Period:</label>
                            <asp:TextBox ID="tbLookbackPeriod" runat="server" Width="50px" CssClass="form-control" ToolTip="The number of days to lookback in determining which policies to include. A value of 20 for instance, will result in Policies modified in the last 20 days to appear."></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label for="tbPageSize" class="col-md-6 control-label" style="padding:0px">Page Size:</label>
                            <asp:TextBox ID="tbPageSize" runat="server" Width="50px" CssClass="form-control" ToolTip="The number of records that should appear in the grid when populated. For instance, a value of 20 will result in up to 20 records in a grid at a time. "></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <asp:LinkButton ID="lbRefresh" runat="server" CssClass="btn btn-primary" OnClick="lblRefresh_Click">Refresh</asp:LinkButton>
                        </div>
                        <asp:RangeValidator ID="validatorLookback" runat="server" ControlToValidate="tbLookbackPeriod" ErrorMessage="Lookback period can not exceed 30 days." MaximumValue="30" MinimumValue="1" Type="Integer"></asp:RangeValidator>
                    </asp:Panel>
                </span>
            </div>
            <script>
                $(document).ready(function () {
                    $('#configoptionsButton').popover({
                        html: true,
                        content: function () {
                            return $('#configoptions').html();
                        }
                    });
                });
            </script>
        </form>
    </div>      
</body>
</html>
