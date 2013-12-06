<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MainLogin.aspx.cs" Inherits="MainLogin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Abacus Settlements, LLC - Login</title>
<%--    <link href="Scripts/_common/styles/dialogs.css" rel="stylesheet" />--%>
<%--    <link href="_common/styles/dialogs.css" rel="stylesheet" />--%>
    <link href="Scripts/_common/styles/fonts.css" rel="stylesheet" />
    <link href="Scripts/_common/styles/global.css" rel="stylesheet" />
<%--    <link href="Scripts/_common/styles/select.css" rel="stylesheet" />--%>
    <link href="Scripts/_common/styles/theme.css" rel="stylesheet" />
<%--    <link href="Scripts/_forms/controls/controls.css" rel="stylesheet" />--%>
    <link href="Scripts/_forms/controls/form.css" rel="stylesheet" />
    <link href="Scripts/_nav/nav.css" rel="stylesheet" />
    <link href="Scripts/_nav/tabs.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div id="Login">
              <ol>
                            <li>
                                <asp:Label ID="Label1" runat="server" AssociatedControlID="UserName">User name</asp:Label>
                                <asp:TextBox runat="server" ID="UserName" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="UserName" CssClass="field-validation-error" ErrorMessage="The user name field is required." />
                            </li>
                            <li>
                                <asp:Label ID="Label2" runat="server" AssociatedControlID="Password">Password</asp:Label>
                                <asp:TextBox runat="server" ID="Password" TextMode="Password" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="Password" CssClass="field-validation-error" ErrorMessage="The password field is required." />
                            </li>
                            <li>
                                <asp:CheckBox runat="server" ID="RememberMe" />
                                <asp:Label ID="Label3" runat="server" AssociatedControlID="RememberMe" CssClass="checkbox">Remember me?</asp:Label>
                            </li>
                        </ol>
                        <asp:Button ID="Button1" runat="server" CommandName="Login" Text="Log in" OnClick="Unnamed6_Click" />
        </div>
       <%-- <div>
            <br />
            <asp:Login ID="LoginPrimary" runat="server" ViewStateMode="Disabled" RenderOuterTable="false">
                <LayoutTemplate>
                    <p class="validation-summary-errors">
                        <asp:Literal runat="server" ID="FailureText" />
                    </p>
                    <fieldset>
                        <legend>Log in Form</legend>
                        <ol>
                            <li>
                                <asp:Label ID="Label1" runat="server" AssociatedControlID="UserName">User name</asp:Label>
                                <asp:TextBox runat="server" ID="UserName" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="UserName" CssClass="field-validation-error" ErrorMessage="The user name field is required." />
                            </li>
                            <li>
                                <asp:Label ID="Label2" runat="server" AssociatedControlID="Password">Password</asp:Label>
                                <asp:TextBox runat="server" ID="Password" TextMode="Password" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="Password" CssClass="field-validation-error" ErrorMessage="The password field is required." />
                            </li>
                            <li>
                                <asp:CheckBox runat="server" ID="RememberMe" />
                                <asp:Label ID="Label3" runat="server" AssociatedControlID="RememberMe" CssClass="checkbox">Remember me?</asp:Label>
                            </li>
                        </ol>
                        <asp:Button ID="Button1" runat="server" CommandName="Login" Text="Log in" OnClick="Unnamed6_Click" />
                    </fieldset>
                </LayoutTemplate>
            </asp:Login>
        </div>--%>
    </form>
</body>
</html>
