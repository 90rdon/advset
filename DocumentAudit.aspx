<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DocumentAudit.aspx.cs" Inherits="DocumentAudit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
        <title>Abacus Settlements, LLC - Document Audit</title>
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
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:GridView ID="GridViewAudit" runat="server" AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="d4_investorid" />
                <asp:BoundField DataField="d4_document" HeaderText="Document" />
                <asp:BoundField DataField="d4_shipdate" HeaderText="Ship Date" />
            </Columns>
             <HeaderStyle CssClass="ms-crm-List-Sortable" Height="40px" />
            <RowStyle CssClass="ms-crm-List-DataArea" Height="30px" />
        </asp:GridView>
    
    </div>
    </form>
</body>
</html>
