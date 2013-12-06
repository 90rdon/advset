﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShipPage.aspx.cs" Inherits="ShipPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Abacus Settlements, LLC - Ship File</title>
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
            height: 10px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div style="width: 100%; height: 100%; overflow: auto">

            <table style="width: 100%; height: 100%;" cellspacing="0" cellpadding="0">
                <tr>
                    <td colspan="2" class="ms-crm-Dialog-Header">
                        <div class="ms-crm-Dialog-Header-Title" id="DlgHdTitle">
                            Look Up Record Form
                        </div>
                        <div class="ms-crm-Dialog-Header-Desc" id="DlgHdDesc">
                            Select Investors...
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="height: 100%;">
                        <div class="ms-crm-Dialog-Main">
                            <div id="divWarning" style="height: 99%;">
                                <table cellspacing="0" cellpadding="0" width="100%" height="100%">
                                    <tr>
                                        <td class="ms-crm-Dialog-Lookup-SearchArea">
                                            <table width="100%" cellpadding="0" cellspacing="0">
                                                <col>
                                                <col width="50%">
                                                <col width="16px">
                                                <col width="16px">
                                                <col width="50%">
                                                <tr height="25px">
                                                    <td nowrap style="text-align: right">
                                                        <span class="ms-crm-Bold-Header">
                                                            <label id="selObjectsText" for="selObjects">
                                                                Look for:
                                                            </label>
                                                        </span>
                                                    </td>
                                                    <td class="ms-crm-Dialog-Lookup-Types">
                                                        <select id="selObjects" name="selObjects" defaultselected="8" class="ms-crm-SelectBox "
                                                            tabindex="1" sort="ascending">
                                                            <option value="8" selected='selected' guid="&#123;E88CA999-0B16-4AE9-B6A9-9EDC840D42D8&#125;">Investors</option>
                                                        </select>
                                                    </td>
                                                    <td width="16px" style="font-size: 1px; border-right: 1px solid #CCCCCC;">&nbsp;
                                                    </td>
                                                    <td width="16px" style="font-size: 1px; border-left: 1px solid #FFFFFF;">&nbsp;
                                                    </td>
                                                    <td>
                   
                                                </tr>
                                                <tr>
                                                    <%-- <td nowrap style="text-align: right">
                                                    <span class="ms-crm-Bold-Header">Search&#58;</span>
                                                </td>
                                                <td class="ms-crm-Dialog-Lookup-Types">
                                                    <table title="Use asterisk (&#42;) wildcard character to search on partial text"
                                                        width="100%" cellspacing="0" cellpadding="0">
                                                       <%-- <tr class="quickFindControl" id="crmGrid_quickFindContainer" gridid="crmGrid">
                                                            <td width="100%">
                                                                <label for="crmGrid_findCriteria" id="crmGrid_findHintText" class="ms-crm-Dialog-Lookup-QuickFindHint">
                                                                    Search for records</label><input id="crmGrid_findCriteria" type="text" class="ms-crm-Dialog-Lookup-QuickFind"
                                                                        maxlength="100" tabindex="3" value="" />
                                                            </td>
                                                            <td nowrap class="AppQuickFind_Render_td">
                                                                <a id="crmGrid_findCriteriaButton" class="ms-crm-FindButton" href="#" target="_self"
                                                                    tabindex="3">
                                                                    <img id="crmGrid_findCriteriaImg" src="_imgs/imagestrips/transparent_spacer.gif"
                                                                        class="ms-crm-ImageStrip-search" imgbase="_imgs/search" alt="Start search" />
                                                                </a>
                                                            </td>
                                                        </tr>--%>
                                            </table>
                                        </td>
                                        <td width="16px" style="font-size: 1px; border-right: 1px solid #CCCCCC;">&nbsp;
                                        </td>
                                        <td width="16px" style="font-size: 1px; border-left: 1px solid #FFFFFF;">&nbsp;
                                        </td>
                                        --%>
                                    </tr>
                                </table>
                                <table width="100%" cellpadding="2" cellspacing="0">
                                    <tr>
                                        <td style="font-size: 1px; border-bottom: 1px solid #CCCCCC;" class="auto-style1">&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 1px; font-size: 1px; border-top: 1px solid #FFFFFF;">&nbsp;
                                        </td>
                                    </tr>
                                </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table height="100%" width="100%" id="tblFind" cellpadding="0" cellspacing="0">
                            <tr>
                                <td colspan="3" style="padding-top: 5px">
                                    <table cellspacing="0" cellpadding="0" id="crmGrid" class="ms-crm-ListControl">
                                        <tr>
                                            <td>
                                                <div style="height: 99.9%;">
                                                    <table cellspacing="0" cellpadding="0" class="ms-crm-ListArea">
                                                        <tr>
                                                            <td>
                                                                <div id="fixedrow" style="width: 100%; overflow-x: hidden;">
                                                                    <table cellspacing="0" cellpadding="0" id="crmGrid_gridBar" class="ms-crm-List-Header"
                                                                        summary="From each column header, you can sort and filter the records in that column."
                                                                        role="grid">
                                                                        <colgroup id="crmGrid_gridBarCols">
                                                                            <col width="32" class="ms-crm-List-NonDataColumnHeader">
                                                                            <col width="2">
                                                                            <col class="ms-crm-List-DataColumnHeader" width="300">
                                                                            <col width="2">
                                                                            <col class="ms-crm-List-DataColumnHeader" width="100">
                                                                            <col width="2">
                                                                            <col class="ms-crm-List-DataColumnHeader" width="150">
                                                                            <col width="2">
                                                                            <col class="ms-crm-List-DataColumnHeader" width="150">
                                                                            <col width="2">
                                                                            <col class="ms-crm-List-DataColumnHeader" width="100">
                                                                            <col width="2">
                                                                            <col class="ms-crm-List-DataColumnHeader" width="200">
                                                                            <col width="2">
                                                                            <col width="0" style="display: none;">
                                                                            <col>
                                                                        </colgroup>
                                                                        <tr height="20">
                                                                            <td>&nbsp;
                                                                            </td>
                                                                            <td>
                                                                                <img id="bar_line" src="_imgs/imagestrips/transparent_spacer.gif" class="ms-crm-ImageStrip-bar_line"
                                                                                    alt="">
                                                                            </td>
                                                                            <th field="fullname" fieldname="fullname" entityname="systemuser" renderertype="Crm.PrimaryField"
                                                                                class="ms-crm-List-Sortable" displaylabel="Full Name">
                                                                                <table cellpadding="0px" cellspacing="0px" class="ms-crm-List-Sortable">
                                                                                    <tr>
                                                                                        <td class="ms-crm-List-Sortable">
                                                                                            <a href="#" target="_self" style="cursor: hand" title="Sort by Full Name" tabindex="6">
                                                                                                <nobr><label class = "ms-crm-List-Sortable" >Full Name</label><img src="_imgs/imagestrips/transparent_spacer.gif" /> <class="ms-crm-List-Sortable ms-crm-ImageStrip-bar_up" alt=""></nobr>
                                                                                            </a>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </th>
                                                                            <td>
                                                                                <img alt="" crmgrid="crmGrid" class="ms-crm-List-ResizeBar ms-crm-ImageStrip-resize"
                                                                                    src="_imgs/imagestrips/transparent_spacer.gif">
                                                                            </td>
                                                                            <th field="address1_telephone1" fieldname="address1_telephone1" entityname="systemuser"
                                                                                renderertype="nvarchar" class="ms-crm-List-Sortable" displaylabel="Main Phone">
                                                                                <table cellpadding="0px" cellspacing="0px" class="ms-crm-List-Sortable">
                                                                                    <tr>
                                                                                        <td class="ms-crm-List-Sortable">
                                                                                            <a href="#" target="_self" style="cursor: hand" title="Sort by Main Phone" tabindex="6">
                                                                                                <nobr><label class = "ms-crm-List-Sortable" >Email Address</label><img src="_imgs/imagestrips/transparent_spacer.gif" class="ms-crm-List-Sortable ms-crm-ImageStrip-bar_up" alt=""></nobr>
                                                                                            </a>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </th>
                                                                            <td>
                                                                                <img alt="" crmgrid="crmGrid" class="ms-crm-List-ResizeBar ms-crm-ImageStrip-resize"
                                                                                    src="_imgs/imagestrips/transparent_spacer.gif">
                                                                            </td>
                                                                            <%--                                          <th field="businessunitid" fieldname="businessunitid" entityname="systemuser" renderertype="lookup"
                                                                                                class="ms-crm-List-Sortable" displaylabel="Business Unit">
                                                                                                <table cellpadding="0px" cellspacing="0px" class="ms-crm-List-Sortable">
                                                                                                    <tr>
                                                                                                        <td class="ms-crm-List-Sortable">
                                                                                                            <a href="#" target="_self" style="cursor: hand" title="Sort by Business Unit" tabindex="6">
                                                                                                                <nobr><label class = "ms-crm-List-Sortable" >Business Unit</label><img src="_imgs/imagestrips/transparent_spacer.gif" class="ms-crm-List-Sortable ms-crm-ImageStrip-bar_up" alt=""></nobr>
                                                                                                            </a>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </th>
                                                                                            <td>
                                                                                                <img alt="" crmgrid="crmGrid" class="ms-crm-List-ResizeBar ms-crm-ImageStrip-resize"
                                                                                                    src="_imgs/imagestrips/transparent_spacer.gif">
                                                                                            </td>
                                                                                            <th field="siteid" fieldname="siteid" entityname="systemuser" renderertype="lookup"
                                                                                                class="ms-crm-List-Sortable" displaylabel="Site">
                                                                                                <table cellpadding="0px" cellspacing="0px" class="ms-crm-List-Sortable">
                                                                                                    <tr>
                                                                                                        <td class="ms-crm-List-Sortable">
                                                                                                            <a href="#" target="_self" style="cursor: hand" title="Sort by Site" tabindex="6">
                                                                                                                <nobr><label class = "ms-crm-List-Sortable" >Site</label><img src="_imgs/imagestrips/transparent_spacer.gif" class="ms-crm-List-Sortable ms-crm-ImageStrip-bar_up" alt=""></nobr>
                                                                                                            </a>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </th>
                                                                                            <td>
                                                                                                <img alt="" crmgrid="crmGrid" class="ms-crm-List-ResizeBar ms-crm-ImageStrip-resize"
                                                                                                    src="_imgs/imagestrips/transparent_spacer.gif">
                                                                                            </td>
                                                                                            <th field="title" fieldname="title" entityname="systemuser" renderertype="nvarchar"
                                                                                                class="ms-crm-List-Sortable" displaylabel="Title">
                                                                                                <table cellpadding="0px" cellspacing="0px" class="ms-crm-List-Sortable">
                                                                                                    <tr>
                                                                                                        <td class="ms-crm-List-Sortable">
                                                                                                            <a href="#" target="_self" style="cursor: hand" title="Sort by Title" tabindex="6">
                                                                                                                <nobr><label class = "ms-crm-List-Sortable" >Title</label><img src="_imgs/imagestrips/transparent_spacer.gif" class="ms-crm-List-Sortable ms-crm-ImageStrip-bar_up" alt=""></nobr>
                                                                                                            </a>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </th>
                                                                                            <td>
                                                                                                <img alt="" crmgrid="crmGrid" class="ms-crm-List-ResizeBar ms-crm-ImageStrip-resize"
                                                                                                    src="_imgs/imagestrips/transparent_spacer.gif">
                                                                                            </td>
                                                                                            <th field="internalemailaddress" fieldname="internalemailaddress" entityname="systemuser"
                                                                                                renderertype="nvarchar" class="ms-crm-List-Sortable" displaylabel="Primary E-mail">
                                                                                                <table cellpadding="0px" cellspacing="0px" class="ms-crm-List-Sortable">
                                                                                                    <tr>
                                                                                                        <td class="ms-crm-List-Sortable">
                                                                                                            <a href="#" target="_self" style="cursor: hand" title="Sort by Primary E-mail" tabindex="6">
                                                                                                                <nobr><label class = "ms-crm-List-Sortable" >Primary E-mail</label><img src="_imgs/imagestrips/transparent_spacer.gif" class="ms-crm-List-Sortable ms-crm-ImageStrip-bar_up" alt=""></nobr>
                                                                                                            </a>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </th>
                                                                                            <td>
                                                                                                <img alt="" crmgrid="crmGrid" class="ms-crm-List-ResizeBar ms-crm-ImageStrip-resize"
                                                                                                    src="_imgs/imagestrips/transparent_spacer.gif">
                                                                                            </td>
                                                                                            <td ishidden="1" field="address1_fax" fieldname="address1_fax">
                                                                                            </td>
                                                                                            <td>
                                                                                            </td>--%>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr height="100%" valign="top">
                                                            <td>
                                                                <div id="crmGrid_divDataBody" class="ms-crm-List-DataBody">
                                                                    <table cellspacing="0" cellpadding="0" class="ms-crm-ListArea">
                                                                        <tr>
                                                                            <td id="tdDataArea" valign="top">
                                                                                <div id="crmGrid_divDataArea" expandable="0" class="ms-crm-List-DataArea">
                                                                                    <div>
                                                                                        <asp:Table ID="PrimaryTable" runat="server" Width="100%" class="ms-crm-List-Data" >
                                                                                            <asp:TableHeaderRow>
                                                                                                <asp:TableHeaderCell CssClass="ms-crm-List-CheckBoxColumn"></asp:TableHeaderCell>
                                                                                                <asp:TableHeaderCell CssClass="ms-crm-Hidden-List" Width="300">Investor Name</asp:TableHeaderCell>
                                                                                                <asp:TableHeaderCell CssClass="ms-crm-Hidden-List" Width="105">Email Address</asp:TableHeaderCell>
                                                                                            </asp:TableHeaderRow>

                                                                                        </asp:Table>
                                                                                        <%--<table class="ms-crm-List-Data" cellspacing="0" cellpadding="1" rules="rows" morerecords="0"
                                                                                            totalrecordcount="3" allrecordscounted="1" oname="8" numrecords="3" tabindex="6"
                                                                                            primaryfieldname="fullname" summary="The list contains 3 records of the type User."
                                                                                            border="1" id="gridBodyTable" style="border-style: None; border-collapse: collapse;">
                                                                                            <col width="18" class="ms-crm-List-CheckBoxColumn">
                                                                                            <col width="16" class="ms-crm-List-RowIconColumn">
                                                                                            <col width="300" name="fullname" class="ms-crm-List-DataColumn">
                                                                                            <col width="102" name="address1_telephone1" class="ms-crm-List-DataColumn">


                                                                                            <col>
                                                                                            <col style="display: none" name="address1_fax">
                                                                                            <thead>
                                                                                                <tr class="ms-crm-Hidden-List">
                                                                                                    <th class="ms-crm-Hidden-List">
                                                                                                        <th class="ms-crm-Hidden-List">
                                                                                                            <th class="ms-crm-Hidden-List">Full Name
                                                                                                            </th>
                                                                                                            <th class="ms-crm-Hidden-List">Email Address
                                                                                                            </th>
                                                                                                </tr>
                                                                                            </thead>
                                                                                            <tbody>
                                                                                                <tr class="ms-crm-List-Row" oid="&#123;3CC4280A-C9DB-DF11-8BFC-0800278260AF&#125;"
                                                                                                    otype="8" otypename="systemuser">
                                                                                                    <td class="ms-crm-List-DataCell" align="center">
                                                                                                        <input type="checkbox" class="ms-crm-RowCheckBox" id="checkBox_&#123;3CC4280A-C9DB-DF11-8BFC-0800278260AF&#125;"
                                                                                                            tabindex="6" title="The record is not selected." style="position: absolute; top: -9000px;">
                                                                                                    </td>
                                                                                                    <td class="ms-crm-List-DataCell" align="center">
                                                                                                        <img src="_imgs/ico_16_8.gif" alt="" />
                                                                                                    </td>
                                                                                                    <td class="ms-crm-List-DataCell">Advance SLM</td>

                                                                                                </tr>
                                                                                            </tbody>
                                                                                        </table>--%>
                                                                                    </div>
                                                                                </div>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr class="ms-crm-List-StatusBar">
                                                            <td>
                                                                <table cellspacing="0" cellpadding="0" id="gridStatusBar" class="ms-crm-List-StatusBar">
                                                                    <tr>
                                                                        <td nowrap class="ms-crm-List-StatusBar-Label" id="crmGrid_RecordSelectInfo">
                                                                            <span id="crmGrid_FirstItem">0</span> - <span id="crmGrid_LastItem">0</span><span
                                                                                id="crmGrid_TotalCountInfo">&nbsp;of <span id="crmGrid_ItemsTotal">0</span></span><span
                                                                                    id="crmGrid_ItemsSelectedInfo">&nbsp;(<span id="crmGrid_ItemsSelected">0</span>
                                                                                    selected)</span><span id="crmGrid_RecordSelectEndMarker" style="visibility: hidden;">M</span>
                                                                        </td>
                                                                        <td nowrap id="crmGrid_PageInfo" class="ms-crm-List-Paging">
                                                                            <!--Insert Paging Code-->
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr style="padding-top: 10px;" height="40">
                          
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>

        <div id='divFillBg' style='display: none; position: absolute; top: 80px; left: 20px; height: 23px; width: 355px; background-color: #ffffff;'>
            &nbsp;
        </div>
        <div id='divFill' style='display: none; position: absolute; top: 80px; left: 20px; height: 23px; width: 0px; filter: progid:DXImageTransform.Microsoft.Gradient(GradientType=0, StartColorStr=#00ff00, EndColorStr=#00cc00);'>
            &nbsp;
        </div>
        <div id='divStatus' style='display: none; position: absolute; top: 80px; left: 19px; height: 23px; width: 357px;'>
            <img alt='' src='_imgs/statusbar.gif' height='23' width='357'>
        </div>
        <%--        </td>
            </tr>--%>
        <tr>
            <td class="ms-crm-Dialog-Footer ms-crm-Dialog-Footer-Left">&nbsp;
            </td>
            <td class="ms-crm-Dialog-Footer ms-crm-Dialog-Footer-Right">
                <button id="butBegin" class="ms-crm-Button" onmouseover="Mscrm.ButtonUtils.hoverOn(this);"
                    onmouseout="Mscrm.ButtonUtils.hoverOff(this);">
                    OK</button>&nbsp;
                    <button id="cmdDialogCancel" class="ms-crm-Button" onmouseover="Mscrm.ButtonUtils.hoverOn(this);"
                        onmouseout="Mscrm.ButtonUtils.hoverOff(this);">
                        Cancel</button>&nbsp;
                 
            </td>
        </tr>
        </table>
    </div>
    </form>
</body>
</html>