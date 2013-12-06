using Microsoft.Xrm.Client;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ShipPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            CrmApi Instance = new CrmApi();
            EntityCollection Investors = Instance.GetInvestorList();
            if (null != Investors && Investors.Entities.Count > 0)
            {
                foreach (Entity Investor in Investors.Entities)
                {
                    TableRow InvestorRow = new TableRow();
                    TableCell NameCell = new TableCell();
                    NameCell.CssClass = "ms-crm-List-DataCell";
                    NameCell.Width = new Unit("300px");
                    if (Investor.Attributes.Contains(InvestorConstants.Name))
                    {
                        NameCell.Text = Investor[InvestorConstants.Name].ToString();
                    }
                    else
                    {
                        NameCell.Text = String.Empty;
                    }
                    InvestorRow.Cells.Add(NameCell);


                    TableCell EmailCell = new TableCell();
                    EmailCell.CssClass = "ms-crm-List-DataCell";
                    if (Investor.Attributes.Contains(InvestorConstants.EmailAddress))
                    {
                        EmailCell.Text = Investor[InvestorConstants.EmailAddress].ToString();
                    }
                    else
                    {
                        EmailCell.Text = String.Empty;
                    }
                    InvestorRow.Cells.Add(EmailCell);
                    PrimaryTable.Rows.Add(InvestorRow);
                }
            }
        }
    }
}