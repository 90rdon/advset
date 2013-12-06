using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ProjectConstants
/// </summary>
public static class ProjectConstants
{
    #region properties

    /// <summary>
    /// Returns a string literal of 'DocumentId'.
    /// </summary>
    public static String DocumentId
    {
        get
        {
            return "DocumentId";
        }
    }
    /// <summary>
    /// Returns a string literal of 'id'.
    /// </summary>
    public static String IdQueryString
    {
        get
        {
            return "id";
        }
    }

    public static String QueryStringError
    {
        get
        {
            return "Missing value [id] in QueryString. Substituting sample value {0}.";
        }
    }
    /// <summary>
    /// Returns string literal with value 'NotFound'.
    /// </summary>
    public static String NotFound
    {
        get
        {
            return ConfigurationManager.AppSettings["NotFound"];
        }
    }
    /// <summary>
    /// Returns a string literal with a value "Policy Documents".
    /// </summary>
    public static String PolicyDocuments
    {
        get
        {
            return "Policy Documents";
        }
    }
    /// <summary>
    /// Returns a string literal of 'id'.
    /// </summary>
    public static String PolicyIdConstant
    {
        get
        {
            return "id";
        }
    }

    /// <summary>
    /// Returns a string literal of 'PortalUri'.
    /// </summary>
    public static String PortalUri
    {
        get
        {
            return "PortalUri";
        }
    }

    /// <summary>
    /// Returns a string literal of 'PortalUriFull'.
    /// </summary>
    public static String PortalUriFull
    {
        get
        {
            return "PortalUriFull";
        }
    }
    /// <summary>
    /// Returns a string literal of 'QueryStringId'.
    /// </summary>
    public static String QueryStringId
    {
        get
        {
            return "QueryStringId";
        }
    }

    /// <summary>
    /// Returns a string literal of 'SelectColumn'.
    /// </summary>
    public static String SelectColumn
    {
        get
        {
            return "SelectColumn";
        }
    }
    /// <summary>
    /// Returns a string literal of 'DownloadLocation'
    /// </summary>
    public static String DownloadLocation
    {
        get
        {
            return "DownloadLocation";
        }
    }
    /// <summary>
    /// Returns a string literal of 'SharePointBaseUrl'.
    /// </summary>
    public static String SharePointBaseUrl
    {
        get
        {
            return "SharePointBaseUrl";
        }
    }

    /// <summary>
    /// Returns a string literal of 'SharePointBaseUrlFull'
    /// </summary>
    public static String SharePointBaseUrlFull
    {
        get
        {
            return "SharePointBaseUrlFull";
        }
    }
    /// <summary>
    /// Returns a string literal of 'SamplePolicyId'.
    /// </summary>
    public static String SamplePolicyId
    {
        get
        {
            return "SamplePolicyId";
        }
    }
    /// <summary>
    /// Returns a string literal of 'FromUserId'.
    /// </summary>
    public static String FromUserId
    {
        get
        {
            return "FromUserId";
        }
    }

    /// <summary>
    /// Returns a string literal of 'Policies'.
    /// </summary>
    public static String Policies
    {
        get
        {
            return "Policies";
        }
    }

    /// <summary>
    /// Returns a string literal of 'ShipPageUri'.
    /// </summary>
    public static String ShipPageUri
    {
        get
        {
            return "ShipPageUri";
        }
    }

    /// <summary>
    /// Returns a string literal of 'UserGuid'.
    /// </summary>
    public static String UserGuid
    {
        get
        {
            return "UserGuid";
        }
    }
    /// <summary>
    /// "d4_name"
    /// </summary>
    public static String D4Name
    {
        get
        {
            return "d4_name";
        }
    }
    /// <summary>
    /// "d4_document"
    /// </summary>
    public static String D4Document
    {
        get
        {
            return "d4_document";
        }

    }
    /// <summary>
    /// "d4_shipdate"
    /// </summary>
    public static String D4ShipDate
    {
        get
        {
            return "d4_shipdate";
        }
    }
    /// <summary>
    /// Returns a string literal of 'UserName'.
    /// </summary>
    public static String UserName
    {
        get
        {
            return "UserName";
        }
    }

    /// <summary>
    /// Returns a string literal of 'Password'.
    /// </summary>
    public static String Password
    {
        get
        {
            return "Password";
        }
    }

    /// <summary>
    /// Returns a string literal of 'PolicyId'.
    /// </summary>
    public static String UriSessionVariable
    {
        get
        {
            return "PolicyId";
        }
    }
    #endregion
}