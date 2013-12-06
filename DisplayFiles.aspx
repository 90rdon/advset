<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DisplayFiles.aspx.cs" Inherits="DisplayFiles" %>

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
            width: 873px;
        }
        .auto-style2 {
            width: 873px;
            height: 15px;
        }
        .auto-style3 {
            height: 15px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table class="ms-crm-MaxWidth">
            <tr>
                <td class="auto-style1">
                    <asp:Label ID="LabelError" runat="server" Visible="False"></asp:Label>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style1">
                    <asp:HyperLink ID="hlReturnToPolicies" runat="server">Return to Policies</asp:HyperLink>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style1">
      <asp:GridView ID="GridViewDocumentDetails" runat="server" AutoGenerateColumns="False" DataKeyNames="DocumentName" OnSelectedIndexChanged="GridViewDocumentDetails_SelectedIndexChanged" Width="547px">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:CheckBox ID="SelectColumn" runat="server" Width="50px" />
                        </ItemTemplate>
                    </asp:TemplateField>                  
                    <asp:BoundField DataField="DocumentName" HeaderText="Document Name" />
                    <asp:TemplateField Visible="False">
                        <ItemTemplate>
                            <asp:HyperLink ID="ArchiveLink" runat="server" Width="75px" Text="Archive"></asp:HyperLink>
                        </ItemTemplate>

                    </asp:TemplateField>
                </Columns>
                 <RowStyle CssClass="ms-crm-List-DataArea" />
            </asp:GridView>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style2">
                    <asp:LinkButton ID="lbDownloadSelected" runat="server" OnClick="LinkButton2_Click">Download Selected</asp:LinkButton>
&nbsp;&nbsp;
                    <asp:LinkButton ID="lbDownloadZip" runat="server" OnClick="LinkButton2_Click" Visible="False">Download Selected (Zip)</asp:LinkButton>
&nbsp;&nbsp;
                    <asp:LinkButton ID="ArchiveSelected" runat="server" Visible="False">Archive Selected</asp:LinkButton>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:LinkButton ID="lbSelectAll" runat="server" OnClick="lbSelectAll_Click">Select All</asp:LinkButton>
&nbsp;
                    <asp:LinkButton ID="lbClearAll" runat="server" OnClick="lbClearAll_Click">Clear All</asp:LinkButton>
                </td>
                <td class="auto-style3"></td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
