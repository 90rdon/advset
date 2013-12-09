using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for PolicyConstants
/// </summary>
public static class PolicyConstants
{
    #region properties


    /// <summary>
    /// Gets the ClientEntityName property - 'd4_clientname'.
    /// </summary>
    public static String ClientName
    {
        get
        {
            return "d4_clientname";
        }
    }
    /// <summary>
    /// Gets the ClientEntityName property - 'd4_client'.
    /// </summary>
    public static String ClientEntityName
    {
        get
        {
            return "d4_client";
        }
    }
    public static String ClientPolicyId
    {
        get
        {
            return "d4_clientpolicyid";
        }
    }
    public static String ClientId
    {
        get
        {
            return "d4_clientid";
        }
    }


    /// <summary>
    /// Gets the DBAmount property - d4_dbamount.
    /// </summary>
    public static String DBAmount
    {
        get
        {
            return "d4_dbamount";
        }
    }
    /// <summary>
    /// Gets the PolicyEntityName property - 'd4_policy'.
    /// </summary>
    public static String PolicyEntityName
    {
        get
        {
            return "d4_policy";
        }
    }
    /// <summary>
    /// Gets the PolicyName property = "d4_policyname"
    /// </summary>
    public static String PolicyName
    {

        get
        {
            return "d4_policyname";
        }
    }

    /// <summary>
    /// Gets the PolicyEntityName property - 'd4_policyid'.
    /// </summary>
    public static String PolicyId
    {
        get
        {
            return "d4_policyid";
        }
    }
    /// <summary>
    /// Gets the PolicyNumber property - 'd4_policynumber'.
    /// </summary>
    public static String PolicyNumber
    {
        get
        {
            return "d4_policynumber";
        }
    }
    public static String DateofIssue
    {
        get
        {
            return "d4_dateofissue";
        }
    }
    public static String PremiumFinance
    {
        get
        {
            return "d4_premiumfinance";
        }
    }
    public static String InsuranceCompany
    {
        get
        {
            return "d4_insurancecompany";
        }
    }
    public static String Premium
    {
        get
        {
            return "d4_premium";
        }
    }
    public static String TwentyFirstFifty
    {
        get
        {
            return "new_21st_50";
        }
    }
    public static String AVS
    {
        get
        {
            return "new_avs";
        }
    }
    public static String OtherLE
    {
        get
        {
            return "new_other_le";
        }
    }
    public static String OwnerState
    {
        get
        {
            return "new_owner_state";
        }
    }
    #endregion
}