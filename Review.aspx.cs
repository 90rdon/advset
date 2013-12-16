using Microsoft.Practices.EnterpriseLibrary.Logging;
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

public partial class Review : System.Web.UI.Page
{
    #region constants
    // TODO - we need to add exception handling, we also need null checks
    // TODO - Many shortcuts were taken authorized by David R to save time
    public const String D4PolicyId = "d4_policyid";
    public const String D4ClientPolicyId = "d4_clientpolicyid";
    public const String D4PolicyNumber = "d4_policynumber";
    public const String D4InsuranceCompany = "d4_insurancecompany";
    public const String D4UserName = "d4_username";
    public const String D4Password = "d4_password";
    public const String D4DbAmount = "d4_dbamount";
    public const String D4Age = "d4_age";
    public const String CreatedOn = "createdon";
    public const String ModifiedDate = "modifiedon";
    public const Int32 LookbackConstant = 20;
    public const Int32 PageSizeConstant = 20;
    public const String LastShippedDate = "new_lastshippeddate";
    #endregion
    #region private members
    private DataTable BuildTable()
    {
        DataTable dt = new DataTable(ProjectConstants.Policies);
        dt.Columns.Add(new DataColumn(D4PolicyId, typeof(Guid)));
        dt.Columns.Add(new DataColumn(D4ClientPolicyId, typeof(String)));
        dt.Columns.Add(new DataColumn(D4PolicyNumber, typeof(String)));
        dt.Columns.Add(new DataColumn(D4InsuranceCompany, typeof(String)));
        dt.Columns.Add(new DataColumn(D4DbAmount, typeof(String)));
        dt.Columns.Add(new DataColumn(D4Age, typeof(Int32)));
        dt.Columns.Add(new DataColumn(CreatedOn, typeof(DateTime)));
        dt.Columns.Add(new DataColumn(ModifiedDate, typeof(DateTime)));
        dt.Columns.Add(new DataColumn(LastShippedDate, typeof(DateTime)));
        return dt;
    }

    private void LoadPage()
    {
        if (!Page.IsPostBack)
        {
            CrmApi ApiInstance = new CrmApi();

            Guid Id = new Guid(Session[ProjectConstants.UserGuid].ToString());
            if (Guid.Empty != Id)
            {
                lblInvalidAuthentication.Visible = false;

                lblGreeting.Text = String.Format("Greetings: <b>{0}, the following policies are ready for review.</b>", HttpUtility.HtmlEncode(Session[ProjectConstants.UserName].ToString()));
                // EntityCollection Policies = ApiInstance.GetPolicies(Id);
                Int32 ReviewPeriod = Int32.Parse(Session["LookbackPeriod"].ToString());
                EntityCollection PolicyDetails = ApiInstance.GetRelatedPolicies(Id, ReviewPeriod);
                DataTable dt = HydrateTable(PolicyDetails);
                Session["dt"] = dt;
                if (Session["TempPageSize"] != null)
                {
                    Int32 PageCt = Int32.Parse(Session["TempPageSize"].ToString());
                }
                DataView PolicyView = dt.DefaultView;
                PolicyView.Sort = String.Format(CultureInfo.CurrentCulture,  "{0} DESC", LastShippedDate);
                GridViewPolicies.DataSource = PolicyView;
                GridViewPolicies.DataBind();
            }
            else
            {
                lblInvalidAuthentication.Visible = true;
                lblInvalidAuthentication.Text = "Invalid UserName and Password combination.";
            }
        }
    }
    private void BuildGridView(Int32 i)
    {
        if (Session["dt"] != null)
        {
            DataTable dt = (Session["dt"] as DataTable);
            DataView PolicyView = dt.DefaultView;
            PolicyView.Sort = String.Format(CultureInfo.CurrentCulture, "{0} DESC", LastShippedDate);
            GridViewPolicies.DataSource = PolicyView;
            GridViewPolicies.PageIndex = i;
            GridViewPolicies.DataBind();
        }
    }
    private DataTable HydrateTable(EntityCollection policies)
    {
        DataTable dt = BuildTable();
        if (null != policies)
        {
            foreach (Entity policy in policies.Entities)
            {
                DataRow NewRow = dt.NewRow();
                if (policy.Attributes.Contains(D4PolicyId))
                {
                    NewRow[D4PolicyId] = new Guid(policy[D4PolicyId].ToString());
                }
                if (policy.Attributes.Contains(D4ClientPolicyId))
                {
                    EntityReference Client = policy[D4ClientPolicyId] as EntityReference;
                    NewRow[D4ClientPolicyId] = Client.Name;
                }
                if (policy.Attributes.Contains(D4PolicyNumber))
                {
                    NewRow[D4PolicyNumber] = policy[D4PolicyNumber].ToString();
                }
                if (policy.Attributes.Contains(D4InsuranceCompany))
                {
                    EntityReference Client = policy[D4InsuranceCompany] as EntityReference;
                    NewRow[D4InsuranceCompany] = Client.Name;
                }
                if (policy.Attributes.Contains(D4DbAmount))
                {
                    Money DBAmount = (policy[D4DbAmount] as Money);
                    NewRow[D4DbAmount] = String.Format(CultureInfo.CurrentCulture, "{0:C}", DBAmount.Value);
                }
                if (policy.Attributes.Contains(D4Age))
                {
                    NewRow[D4Age] = policy[D4Age].ToString();
                }
                if (policy.Attributes.Contains(CreatedOn))
                {
                    NewRow[CreatedOn] = Convert.ToDateTime(policy[CreatedOn]);
                }
                if (policy.Attributes.Contains(ModifiedDate))
                {
                    NewRow[ModifiedDate] = Convert.ToDateTime(policy[ModifiedDate]);
                }
                if (policy.Attributes.Contains(LastShippedDate))
                {
                    NewRow[LastShippedDate] = Convert.ToDateTime(policy[LastShippedDate]);
                }
                dt.Rows.Add(NewRow);
            }
        }

        return dt;
    }


    #endregion

    public string GetUrl(Object policyId)
    {

        String url = String.Format(CultureInfo.CurrentCulture, ConfigurationManager.AppSettings[ProjectConstants.ShipPageUri], policyId);
        return url;
    }
    #region page events
    protected void Page_Load(object sender, EventArgs e)
    {

        Int32 LookbackPeriod = LookbackConstant;
        Int32 PageSize = PageSizeConstant;


        if (!Page.IsPostBack)
        {
            if (ConfigurationManager.AppSettings["Lookback"] != null)
            {
                LookbackPeriod = Int32.Parse(ConfigurationManager.AppSettings["Lookback"].ToString());
                tbLookbackPeriod.Text = LookbackPeriod.ToString();
            }

            if (ConfigurationManager.AppSettings["PageSize"] != null)
            {
                PageSize = Int32.Parse(ConfigurationManager.AppSettings["PageSize"].ToString());
                tbPageSize.Text = PageSize.ToString();
            }
            lblGreeting.Visible = false;
            if (Session[ProjectConstants.UserName] != null && Session[ProjectConstants.Password] != null)
            {
                AuthenticationPanel.Visible = false;
                GridViewPolicies.PageSize = PageSizeConstant;
                LoadPage();
            }
        }
        else
        {
            Int32 TempLookback;
            Int32 TempPageSize;
            if (Int32.TryParse(Request["tbLookbackPeriod"], out TempLookback))
            {
                LookbackPeriod = TempLookback;
                Session["LookbackPeriod"] = LookbackPeriod;
            }
            else
            {
                LookbackPeriod = LookbackConstant;
                Session["LookbackPeriod"] = LookbackPeriod;
            }
            if (!Int32.TryParse(tbPageSize.Text, out TempPageSize))
            {
                PageSize = TempPageSize;
                Session["TempPageSize"] = PageSize;
            }
            else
            {
                PageSize = TempPageSize;
                Session["TempPageSize"] = PageSize;
            }
        }
    }
    protected void GridViewPolicies_SelectedIndexChanged(object sender, EventArgs e)
    {


    }
    protected void lblLogin_Click(object sender, EventArgs e)
    {
        if (!String.IsNullOrWhiteSpace(TextBoxUserName.Text) && !String.IsNullOrWhiteSpace(TextBoxPassword.Text))
        {
            Session[ProjectConstants.UserName] = TextBoxUserName.Text;
            Session[ProjectConstants.Password] = TextBoxPassword.Text;
            lblGreeting.Visible = true;
            CrmApi ApiInstance = new CrmApi();

            Guid Id = Guid.Empty;
            try
            {
                Id = ApiInstance.GetUserId(TextBoxUserName.Text, TextBoxPassword.Text);
                Session["UserId"] = Id;
                Session[ProjectConstants.UserGuid] = Id;
            }
            catch (Exception ex)
            {
                String ExceptionMsg = String.Format(CultureInfo.CurrentCulture, "An error occurred on Review.aspx-lblLogin_Click: {0}", ex.ToString());
                ex.LogException(ExceptionMsg);
                lblInvalidAuthentication.Visible = true;
                lblInvalidAuthentication.Text = "An error occurred trying to retrieve policy information, please try again.";
                // TODO clean up exceptions, don't handle System.Exception, add logging
            }
            if (Guid.Empty != Id)
            {
                AuthenticationPanel.Visible = false;
                lblInvalidAuthentication.Visible = false;
                EntityCollection PolicyDetails = null;
                try
                {
                    Int32 ReviewPeriod = Int32.Parse(Session["LookbackPeriod"].ToString());
                    PolicyDetails = ApiInstance.GetRelatedPolicies(Id, ReviewPeriod);
                }
                catch (Exception ex)
                {
                    String ExceptionMsg = String.Format(CultureInfo.CurrentCulture, "An error occurred on Review.aspx-lblLogin_Click: {0}", ex.ToString());
                    ex.LogException(ExceptionMsg);
                    lblGreeting.Text = "An Error Occurred trying to retrieve files, please try again.";
                }
                if (PolicyDetails != null && PolicyDetails.Entities.Count > 0)
                {
                    lblGreeting.Visible = true;
                    lblGreeting.Text = String.Format("Greetings: <b>{0}, the following policies are ready for review.</b>", HttpUtility.HtmlEncode(TextBoxUserName.Text));
                    // EntityCollection Policies = ApiInstance.GetPolicies(Id);
                    DataTable dt = HydrateTable(PolicyDetails);
                    Session["dt"] = dt;
                    DataView PolicyView = dt.DefaultView;
                    PolicyView.Sort = String.Format(CultureInfo.CurrentCulture, "{0} DESC", LastShippedDate);
                    Session["SortedView"] = PolicyView;
                    GridViewPolicies.DataSource = PolicyView;
                    GridViewPolicies.DataBind();
                    ViewState["Direction"] = "Descending";
                }
                else
                {
                    lblGreeting.Visible = true;
                    lblGreeting.Text = String.Format("Greetings: <b>{0}, there are currently no files to review</b>.", HttpUtility.HtmlEncode(TextBoxUserName.Text));
                }

            }
            else
            {
                AuthenticationPanel.Visible = true;
                String AuthMsg = String.Format(CultureInfo.CurrentCulture, "Invalid Authentication Attempt. Attempted UserName: {0}, Attempted Password: {1}", TextBoxUserName.Text, TextBoxPassword.Text);
                AuthMsg.LogMessage();
                lblGreeting.Visible = false;
                lblInvalidAuthentication.Visible = true;
                lblInvalidAuthentication.Text = "Invalid UserName and Password combination.";
            }
        }

    }
    protected void hlSelect_Click(object sender, EventArgs e)
    {
        String IdValue = GridViewPolicies.DataKeys[0].Value.ToString();

    }
    protected void GridViewPolicies_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //  e.Row.Cells[0].Visible = false;

    }
    protected void GridViewPolicies_PageIndexChanged(object sender, EventArgs e)
    {

    }
    protected void GridViewPolicies_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        BuildGridView(e.NewPageIndex);
    }
    protected void GridViewPolicies_Sorted(object sender, EventArgs e)
    {

    }
    protected void GridViewPolicies_Sorting(object sender, GridViewSortEventArgs e)
    {
        String SortOrder = String.Empty;
        String CurrentSortField = e.SortExpression;
        if (ViewState["Direction"] != null)
        {
            String CurrentSortDirection = ViewState["Direction"].ToString();
            if (CurrentSortDirection == "Ascending")
            {
                SortOrder = String.Format("{0} {1}", CurrentSortField, "DESC");
                ViewState["Direction"] = "Descending";
            }
            if (CurrentSortDirection == "Descending")
            {
                SortOrder = String.Format("{0} {1}", CurrentSortField, "ASC");
                ViewState["Direction"] = "Ascending";
            }
        }

        if (Session["dt"] != null)
        {

            DataTable dt = Session["dt"] as DataTable;
            DataView PolicyView = dt.DefaultView;

            PolicyView.Sort = SortOrder;


            Session["SortedView"] = PolicyView;
            GridViewPolicies.DataSource = PolicyView;
            GridViewPolicies.DataBind();
        }
    }
    protected void GridViewPolicies_PageIndexChanging1(object sender, GridViewPageEventArgs e)
    {
        Int32 CurrentPageIndex = e.NewPageIndex;
        if (Session["dt"] != null)
        {
            DataTable dt = (Session["dt"] as DataTable);
            DataView PolicyView = dt.DefaultView;
            //PolicyView.Sort = "createdon DESC";
            GridViewPolicies.DataSource = PolicyView;
            GridViewPolicies.PageIndex = CurrentPageIndex;
            GridViewPolicies.DataBind();
        }
    }
    protected void lblRefresh_Click(object sender, EventArgs e)
    {
        CrmApi ApiInstance = new CrmApi();
        Guid Id = Guid.Empty;
        if (!String.IsNullOrWhiteSpace(tbPageSize.Text))
        {
            Int32 GridPaging;
            if (Int32.TryParse(tbPageSize.Text, out GridPaging))
            {
                Session["PageSize"] = GridPaging;
            }
        }
        Int32 GridViewPage;

        Id = new Guid(Session["UserId"].ToString());

        if (Guid.Empty != Id)
        {
            EntityCollection PolicyDetails = null;
            try
            {
                Int32 Temp;
                if (Int32.TryParse(Request["tbLookbackPeriod"], out Temp))
                {
                    PolicyDetails = ApiInstance.GetRelatedPolicies(Id, Temp);
                }
                else
                {
                    PolicyDetails = ApiInstance.GetRelatedPolicies(Id, 20);
                }


            }
            catch (Exception ex)
            {
                String ExceptionMsg = String.Format(CultureInfo.CurrentCulture, "An error occurred on Review.aspx-lblRefresh_Click: {0}", ex.ToString());
                ex.LogException(ExceptionMsg);
                lblGreeting.Text = "An Error Occurred trying to retrieve files, please try again.";
            }
            if (PolicyDetails != null && PolicyDetails.Entities.Count > 0)
            {
                GridViewPolicies.PageSize = Int32.Parse(Session["PageSize"].ToString());
                DataTable dt = HydrateTable(PolicyDetails);
                Session["dt"] = dt;
                DataView PolicyView = dt.DefaultView;
                PolicyView.Sort = String.Format(CultureInfo.CurrentCulture, "{0} DESC", LastShippedDate);
                Session["SortedView"] = PolicyView;
                GridViewPolicies.DataSource = PolicyView;
                GridViewPolicies.DataBind();
                ViewState["Direction"] = "Descending";
            }
        }
    }
    #endregion
}