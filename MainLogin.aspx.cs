using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MainLogin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Unnamed6_Click(object sender, EventArgs e)
    {
        CrmApi ApiInstance = new CrmApi();
        Guid Id = Guid.Empty;
        try
        {
            Id = ApiInstance.GetUserId(UserName.Text, Password.Text);
        }
        catch (Exception ex)
        {
            String ErrMsg = String.Format(CultureInfo.CurrentCulture, "Error encountered on MainLogin-Unnamed6_click: {0}", ex.ToString());
            ex.LogException(ErrMsg);
            //FailureText. = ex.ToString();
        }
        if (Id != Guid.Empty)
        {
            Session[ProjectConstants.UserName] = UserName.Text;
            Session[ProjectConstants.Password] = Password.Text;
            Session[ProjectConstants.UserGuid] = Id;
            FormsAuthentication.RedirectFromLoginPage(UserName.Text, RememberMe.Checked);
        }
        else
        {

        }

    }
}