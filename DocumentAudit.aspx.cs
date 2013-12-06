using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DocumentAudit : System.Web.UI.Page
{

    private const String PolicyIdConstant = "id";
    private const String QueryStringId = "QueryStringId";
    private const String DocumentId = "DocumentId";
    private const String SelectColumn = "SelectColumn";
    private const String SharePointBaseConfig = "SharePointBaseUrl";
    private const String SamplePolicyIdConfig = "SamplePolicyId";
    private const String FromUserId = "FromUserId";
    private const String D4PolicyName = "d4_policyname";
    private const String D4ShipDate = "d4_shipdate";
    private const String D4Document = "d4_document";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            String PolicyIdQueryString = String.Empty;
            if (!String.IsNullOrWhiteSpace(Request[PolicyIdConstant]))
            {
              //  LabelErrorMessage.Visible = false;
                PolicyIdQueryString = Request[PolicyIdConstant];
                ViewState[QueryStringId] = PolicyIdQueryString;
            }
        }
    }
}