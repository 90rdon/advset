using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public static class DocumentsDataTableConstants
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

public static class InvestorDataTableConstants
{
    #region properties
    /// <summary>
    /// String literal for the value 'Investors'
    /// </summary>
    public static String TableName
    {
        get
        {
            return "Investors";
        }
    }
    /// <summary>
    /// String literal for value "accountid"
    /// </summary>
    public static String AccountId
    {
        get
        {
            return "accountid";
        }
    }

    /// <summary>
    /// String literal for value "name"
    /// </summary>
    public static String Name
    {
        get
        {
            return "name";
        }
    }

    /// <summary>
    /// String literal for value "emailaddress1"
    /// </summary>
    public static String EmailAddress1
    {
        get
        {
            return "emailaddress1";
        }
    }


    #endregion
}
public partial class FileShipping : System.Web.UI.Page
{

    private const String SamplePolicyIdConfig = "SamplePolicyId";

    private DataTable GetDataTable()
    {
        DataTable Investors = new DataTable(InvestorDataTableConstants.TableName);
        DataColumn AccountId = new DataColumn(InvestorDataTableConstants.AccountId, typeof(Guid));
        DataColumn Name = new DataColumn(InvestorDataTableConstants.Name, typeof(String));
        DataColumn EmailAddress = new DataColumn(InvestorDataTableConstants.EmailAddress1, typeof(String));
        Investors.Columns.Add(AccountId);
        Investors.Columns.Add(Name);
        Investors.Columns.Add(EmailAddress);
        return Investors;
    }

    private String SamplePolicyId
    {
        get
        {
            return ConfigurationManager.AppSettings[SamplePolicyIdConfig].ToString();
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            String PolicyIdQueryString = String.Empty;
            if (!String.IsNullOrWhiteSpace(Request[ProjectConstants.PolicyIdConstant]))
            {
                LabelErrorMessage.Visible = false;
                PolicyIdQueryString = Request[ProjectConstants.PolicyIdConstant];
                ViewState[ProjectConstants.QueryStringId] = PolicyIdQueryString;
            }
            else
            {
                PolicyIdQueryString = SamplePolicyId;
                ViewState[ProjectConstants.QueryStringId] = PolicyIdQueryString;
                LabelErrorMessage.Visible = true;
                String ErrorMessage = String.Format(CultureInfo.CurrentCulture,
                    ProjectConstants.QueryStringError, PolicyIdQueryString);
                LabelErrorMessage.Text = ErrorMessage;
            }
            CrmApi Instance = new CrmApi();
            EntityCollection Docs = Instance.GetDocumentList(PolicyIdQueryString);
            if (null != Docs && Docs.Entities.Count > 0)
            {
                ViewState[ProjectConstants.DocumentId] = Docs[0][DocumentsDataTableConstants.RelativeUrl].ToString();
            }
            EntityCollection Investors = Instance.GetInvestorList();

            DataTable InvestorTable = GetDataTable();
            if (null != Investors && Investors.Entities.Count > 0)
            {
                foreach (Entity Investor in Investors.Entities)
                {

                    DataRow InvestorRow = InvestorTable.NewRow();
                    if (Investor.Attributes.Contains(InvestorDataTableConstants.AccountId))
                    {
                        InvestorRow[0] = new Guid(Investor[InvestorDataTableConstants.AccountId].ToString());
                    }
                    else
                    {
                        InvestorRow[0] = Guid.Empty;
                    }
                    if (Investor.Attributes.Contains(InvestorConstants.Name))
                    {
                        InvestorRow[1] = Investor[InvestorConstants.Name].ToString();
                    }
                    else
                    {
                        InvestorRow[1] = String.Empty;
                    }

                    if (Investor.Attributes.Contains(InvestorConstants.EmailAddress))
                    {
                        InvestorRow[2] = Investor[InvestorConstants.EmailAddress].ToString();
                    }
                    else
                    {
                        InvestorRow[2] = String.Empty;
                    }
                    InvestorTable.Rows.Add(InvestorRow);
                    GridViewInvestors.DataSource = InvestorTable;
                    GridViewInvestors.DataBind();
                }
                String[] DocumentLists = Instance.GetDocumentsByPolicy(ViewState[ProjectConstants.DocumentId].ToString());
                DataTable DocumentTable = new DataTable(DocumentsDataTableConstants.TableName);
                DocumentTable.Columns.Add(DocumentsDataTableConstants.DocumentName, typeof(String));
                if (null != DocumentLists)
                {
                    foreach (String doc in DocumentLists)
                    {
                        DataRow NewDocRow = DocumentTable.NewRow();
                        NewDocRow[0] = doc;
                        DocumentTable.Rows.Add(NewDocRow);
                    }
                }
                GridViewDocumentDetails.DataSource = DocumentTable;
                GridViewDocumentDetails.DataBind();
            }
        }
    }

    private String SharePointBaseUri
    {
        get
        {
            return ConfigurationManager.AppSettings[ProjectConstants.SharePointBaseUrl].ToString();
        }
    }
    protected void butBegin_Click(object sender, EventArgs e)
    {
        List<Guid> Accounts = new List<Guid>();

        foreach (GridViewRow currentRow in GridViewInvestors.Rows)
        {
            CheckBox selected = (CheckBox)currentRow.FindControl(ProjectConstants.SelectColumn);
            if (selected != null && selected.Checked)
            {
                Guid Acctid = new Guid(GridViewInvestors.DataKeys[currentRow.RowIndex].Value.ToString());
                Accounts.Add(Acctid);
            }
        }
        CrmApi Instance = new CrmApi();
        List<String> LibraryLocation = new List<String>();
        String Library = String.Format(SharePointBaseUri, ViewState[ProjectConstants.DocumentId]);
        LibraryLocation.Add(Library);
        foreach (GridViewRow currentDocRow in GridViewDocumentDetails.Rows)
        {
            CheckBox selected = (CheckBox)currentDocRow.FindControl(ProjectConstants.SelectColumn);
            if (selected != null && selected.Checked)
            {
                String DocName = GridViewDocumentDetails.DataKeys[currentDocRow.RowIndex].Value.ToString();
                LibraryLocation.Add(DocName);
            }
        }
        Guid FromId;
        if (Guid.TryParse(ConfigurationManager.AppSettings[ProjectConstants.FromUserId].ToString(), out FromId))
        {
          //  Instance.CreateEmailActivity(FromId, Accounts, LibraryLocation, new Guid(ViewState[ProjectConstants.QueryStringId].ToString()), null, null);
        }
        String PolicyNumber = null;
        try
        {
            PolicyNumber = Instance.GetPolicyNumberFromId(new Guid(ViewState[ProjectConstants.QueryStringId].ToString()));
        }
        catch (Exception)
        {
            LabelErrorMessage.Text = "Policy # could not be retrieved.";
            LabelErrorMessage.Visible = true;
            return;
        }
        EntityCollection Clients = null;
        if (!String.IsNullOrWhiteSpace(PolicyNumber))
        {
            try
            {
                Clients = Instance.GetClientFromPolicyNumber(PolicyNumber);
            }
            catch (Exception)
            {
                LabelErrorMessage.Text = "Client Information could not be retrieved.";
                LabelErrorMessage.Visible = true;
                return;
            }
        }
        String SubjectName = null;
        if (null != Clients && Clients.Entities.Count > 0)
        {
            if (Clients[0].Attributes.Contains(PolicyConstants.ClientName))
            {
                SubjectName = Clients[0][PolicyConstants.ClientName].ToString();
            }
            else
            {
                SubjectName = ProjectConstants.NotFound;
            }


        }
        else
        {
            SubjectName = ProjectConstants.NotFound;
        }
        String DBAmount = null;
        try
        {
            DBAmount = Instance.GetPolicyAmountFromId(new Guid(ViewState[ProjectConstants.QueryStringId].ToString()));
        }
        catch (Exception)
        {
            LabelErrorMessage.Text = "An error occured and processing did not complete, please try again.";
            LabelErrorMessage.Visible = true;
        }
        if (!String.IsNullOrWhiteSpace(DBAmount))
        {
            SubjectName = String.Format(CultureInfo.CurrentCulture, "{0} - {1}", SubjectName, DBAmount);
        }
        try
        {
            Instance.CreateEmailActivity(FromId, Accounts, LibraryLocation, new Guid(ViewState[ProjectConstants.QueryStringId].ToString()), SubjectName, PolicyNumber, TextBoxNotes.Text);
            Instance.LogActivity(Accounts, LibraryLocation, PolicyNumber);
            Instance.CreateInvestor(Accounts, new Guid(ViewState[ProjectConstants.QueryStringId].ToString()));
            ClientScript.RegisterClientScriptBlock(this.GetType(), "validation", "alert('File successfully shipped');", true);
        }
        catch (Exception)
        {
            LabelErrorMessage.Text = "An error occured and processing did not complete, please try again.";
            LabelErrorMessage.Visible = true;
            ClientScript.RegisterClientScriptBlock(this.GetType(), "validation", "alert('An error occurred, processing did not complete. Please try again.');", true);
        }
       
    }
    protected void GridViewInvestors_RowCreated(object sender, GridViewRowEventArgs e)
    {
        //e.Row.Cells[0].Visible = false;
    }
    protected void GridViewDocumentDetails_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}

// try
// {
//
//     //SharePointItems.SharepointApi SharePointAPI = new SharePointItems.SharepointApi();
// SPUtils.SharePointUtility Util = new SPUtils.SharePointUtility();
//// SPUtils Utils = new SPUtils();

//     DataTable DocumentDetails = Util.GetDocList();
//     GridViewDocumentDetails.DataSource = DocumentDetails;
//     GridViewDocumentDetails.DataBind();
//}
//catch (Exception ex)
//{
//   LabelErrorMessage.Visible = true;
//   LabelErrorMessage.Text = ex.ToString();
//   throw;
//}
// TableCell NameCell = new TableCell();
//NameCell.CssClass = "ms-crm-List-DataCell";
//NameCell.Width = new Unit("300px");

//InvestorRow.Cells.Add(NameCell);


//TableCell EmailCell = new TableCell();
//EmailCell.CssClass = "ms-crm-List-DataCell";
//if (Investor.Attributes.Contains(InvestorConstants.EmailAddress))
//{
//    EmailCell.Text = Investor[InvestorConstants.EmailAddress].ToString();
//}
//else
//{
//    EmailCell.Text = String.Empty;
//}
//InvestorRow.Cells.Add(EmailCell);
//PrimaryTable.Rows.Add(InvestorRow);