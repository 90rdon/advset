using Ionic.Zip;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public static class DocumentsTableConstants
{
    #region properties

    /// <summary>
    /// String literal for the value 'DocumentName'
    /// </summary>
    public static String DocumentName
    {
        get
        {
            return "DocumentName";
        }
    }
    /// <summary>
    /// String literal for the value 'Documents'
    /// </summary>
    public static String TableName
    {
        get
        {
            return "Documents";
        }
    }

    /// <summary>
    /// String literal for value 'relativeurl'.
    /// </summary>
    public static String RelativeUrl
    {
        get
        {
            return "relativeurl";
        }
    }
    #endregion
}
public partial class DisplayFiles : System.Web.UI.Page
{


    protected void Page_Load(object sender, EventArgs e)
    {
      
        String PolicyIdQueryString = null;
        if (Request.QueryString[ProjectConstants.IdQueryString] != null)
        {
            PolicyIdQueryString = Request.QueryString[ProjectConstants.IdQueryString];
        }
        else
        {
            // TODO - Temporary for testing - code needs a cleanup - many things need removed b/c we didn't have time to test
            // or follow up - won't interfere with operation but definitely need an overall cleanup.
            PolicyIdQueryString = "add40190-4d10-e311-93f1-001cc46bb796";
        }
        if (!Page.IsPostBack)
        {
            if (Request.UrlReferrer != null && Request.UrlReferrer.AbsoluteUri != null)
            {
                hlReturnToPolicies.NavigateUrl = Request.UrlReferrer.AbsoluteUri;
            }
            CrmApi Instance = new CrmApi();
            String userName = Session[ProjectConstants.UserName].ToString();
           // String AcctName = Instance.GetAccountNameFromId(userName);
            EntityCollection Docs = null;
            try
            {
                Docs = Instance.GetDocumentList(PolicyIdQueryString);
            }
            catch (Exception ex)
            {
                LabelError.Visible = true;
                LabelError.Text = "An error occurred trying to retrieve files, please try again.";
                Logger.Write(String.Format(CultureInfo.CurrentCulture, "Error in Export Files - GetDocumentList: {0}", ex.ToString()));
                return;
            }
            try
            {
                String[] DocumentLists = Instance.GetDocumentsByPolicy(Docs[0][DocumentsTableConstants.RelativeUrl].ToString());
                Session[ProjectConstants.UriSessionVariable] = Docs[0][DocumentsTableConstants.RelativeUrl].ToString();
                DataTable DocumentTable = new DataTable(ProjectConstants.Policies);
                DocumentTable.Columns.Add(DocumentsTableConstants.DocumentName, typeof(String));
                if (null != DocumentLists)
                {
                    foreach (String doc in DocumentLists)
                    {
                        CrmApi CurrentInstance = new CrmApi();
                        //   Boolean ShouldAdd = CurrentInstance.WereDocumentsSent(AcctName, String.Format(GetSharePointLocation(), Session[ProjectConstants.UriSessionVariable], doc));
                        Boolean ShouldAdd = CurrentInstance.WereDocumentsSent(userName,  doc);
                        if(ShouldAdd){
                        DataRow NewDocRow = DocumentTable.NewRow();
                        NewDocRow[0] = doc;
                        DocumentTable.Rows.Add(NewDocRow);
                        }
                    }
                }
                if (DocumentTable.Rows.Count > 0)
                {
                    SetLinkLabelVisibility(true);
                    GridViewDocumentDetails.DataSource = DocumentTable;
                    GridViewDocumentDetails.DataBind();
                }
                else
                {
                    SetLinkLabelVisibility(false);
                }
            }
            catch (Exception ex)
            {
                LabelError.Visible = true;
                LabelError.Text = "An error occurred trying to document list , please try again.";
                Logger.Write(String.Format(CultureInfo.CurrentCulture, "Error in DisplayFiles OnLoad: {0}", ex.ToString()));
            }
        }
    }

    private void SetLinkLabelVisibility(Boolean visible)
    {
        lbDownloadSelected.Visible = visible;
        lbClearAll.Visible = visible;
        lbSelectAll.Visible = visible;
    }
    protected void GridViewDocumentDetails_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    private String GetSharePointLocation()
    {
        return ConfigurationManager.AppSettings[ProjectConstants.SharePointBaseUrlFull];
    }
    private void ExportFiles()
    {
        List<String> FileList = new List<String>();

        foreach (GridViewRow currentDocRow in GridViewDocumentDetails.Rows)
        {
            CheckBox selected = (CheckBox)currentDocRow.FindControl(ProjectConstants.SelectColumn);
            if (selected != null && selected.Checked)
            {
                String DocName = GridViewDocumentDetails.DataKeys[currentDocRow.RowIndex].Value.ToString();
                WebClient Downloader = new WebClient();
                Downloader.UseDefaultCredentials = true;
                Downloader.DownloadFile(String.Format(GetSharePointLocation(), Session[ProjectConstants.UriSessionVariable], DocName),
                    String.Format("{0}{1}", ConfigurationManager.AppSettings[ProjectConstants.DownloadLocation], DocName));
                FileList.Add(String.Format("{0}{1}", ConfigurationManager.AppSettings[ProjectConstants.DownloadLocation], DocName));
            }
        }
        String ZipFileName = String.Format("{0}{1}", ConfigurationManager.AppSettings[ProjectConstants.DownloadLocation], DateTime.Now.Ticks.ToString());
        using (ZipFile zip = new ZipFile())
        {
            zip.AddDirectoryByName("Policies");
            foreach (String currentFile in FileList)
            {
                zip.AddFile(currentFile, "Policies");
            }
            zip.Save(@ZipFileName + ".zip");
            zip.Dispose();
        }
        foreach (String current in FileList)
        {
            FileInfo files = new FileInfo(current);
            try
            {
                files.Delete();
            }
            catch (Exception)
            {
                //just eat it.
            }
        }

        try
        {
            ClearAll();
            FileStream Input = null;
            Input = File.OpenRead(@ZipFileName + ".zip");
            Byte[] CurrentBytes = new Byte[Input.Length];
            Input.Read(CurrentBytes, 0, Convert.ToInt32(Input.Length));
            Input.Dispose();
            Response.ContentType = "application/zip";
            Response.AddHeader("Content-Disposition", "filename=PolicySummary.zip");

            Response.BinaryWrite(CurrentBytes);



        }
        catch (Exception ex)
        {
            Logger.Write(String.Format(CultureInfo.CurrentCulture, "Error in Export Files trying to steam PDF output: {0}", ex.ToString()));
        }
        finally
        {
            try
            {

                Response.End();
                Response.Clear();
            }
            catch (Exception exc)
            {
                Logger.Write(String.Format(CultureInfo.CurrentCulture, "Error in Export Files: {0}", exc.ToString()));
            }
            try
            {
                File.Delete(@ZipFileName + ".zip");
            }
            catch (Exception ex)
            {
                Logger.Write(String.Format(CultureInfo.CurrentCulture, "Error in Export Files attempting to delete files: {0}", ex.ToString()));
            }
            ClientScript.RegisterClientScriptBlock(this.GetType(), "validation", "alert('Download complete');", true);

        }

    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        ExportFiles();
    }
    protected void CheckBoxSelectAll_CheckedChanged(object sender, EventArgs e)
    {


    }
    protected void CheckBoxClear_CheckedChanged(object sender, EventArgs e)
    {

    }
    protected void lbSelectAll_Click(object sender, EventArgs e)
    {
        CheckBox chk;
        foreach (GridViewRow rowItem in GridViewDocumentDetails.Rows)
        {
            chk = (CheckBox)(rowItem.Cells[0].FindControl(ProjectConstants.SelectColumn));
            chk.Checked = true;
        }
    }
    private void ClearAll()
    {
        CheckBox chk;
        foreach (GridViewRow rowItem in GridViewDocumentDetails.Rows)
        {
            chk = (CheckBox)(rowItem.Cells[0].FindControl(ProjectConstants.SelectColumn));
            chk.Checked = false;
        }
    }
    protected void lbClearAll_Click(object sender, EventArgs e)
    {
        ClearAll();
    }
}