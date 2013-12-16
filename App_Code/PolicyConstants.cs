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
    public static String ClientDOB
    {
        get
        {
            return "d4_dob";
        }
    }
    public static String ClientGender
    {
        get
        {
            return "d4_gender";
        }
    }
    public static String ClientAge
    {
        get
        {
            return "d4_age";
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
    public static Dictionary<int, string> States
    {
        get
        {
            Dictionary<int, string> states = new Dictionary<int, string>
            {
                {100000001, "Alabama"},
                {100000000, "Alaska"},
                {100000002, "Arizona"},
                {100000003, "Arkansas"},
                {100000004, "California"},
                {100000005, "Colorado"},
                {100000006, "Connecticut"},
                {100000007, "Delaware"},
                {100000008, "Florida"},
                {100000009, "Georgia"},
                {100000010, "Hawaii"},
                {100000011, "Idaho"},
                {100000012, "Illinois"},
                {100000013, "Indiana"},
                {100000014, "Iowa"},
                {100000015, "Kansas"},
                {100000016, "Kentucky"},
                {100000017, "Louisiana"},
                {100000018, "Maine"},
                {100000019, "Maryland"},
                {100000020, "Massachusetts"},
                {100000021, "Michigan"},
                {100000022, "Minnesota"},
                {100000023, "Mississippi"},
                {100000024, "Missouris"},
                {100000025, "Montana"},
                {100000026, "Nebraska"},
                {100000027, "Nevada"},
                {100000028, "New Hampshire"},
                {100000029, "New Jersey"},
                {100000030, "New Mexico"},
                {100000031, "New York"},
                {100000032, "North Carolina"},
                {100000033, "North Dakota"},
                {100000034, "Ohio"},
                {100000035, "Oklahoma"},
                {100000036, "Oregon"},
                {100000037, "Pennsylvania"},
                {100000039, "South Carolina"},
                {100000040, "South Dakota"},
                {100000041, "Tennessee"},
                {100000042, "Texas"},
                {100000043, "Utah"},
                {100000044, "Vermont"},
                {100000045, "Virginia"},
                {100000046, "Washington"},
                {100000047, "West Virginia"},
                {100000048, "Wisconsin"},
                {100000049, "Wyoming"},
                
            };

            return states;
        }
    }
    #endregion
}