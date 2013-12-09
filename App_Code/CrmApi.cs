using Microsoft.Crm.Sdk.Messages;
using Microsoft.Practices.EnterpriseLibrary.Logging;
//using Microsoft.SharePoint;
using Microsoft.Xrm.Client;
using Microsoft.Xrm.Client.Services;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Web;
using System.IO;

/// <summary>
/// Handles all interaction with Microsoft Dynamics CRM 2011
/// </summary>
public class CrmApi
{

    // TODO - we need to add exception handling, we also need null checks
    // TODO - Many shortcuts were taken authorized by David R to save time
    #region constants

    /// <summary>
    /// "d4_documentaudit"
    /// </summary>
    public const String D4DocumentAudit = "d4_documentaudit";
    /// <summary>
    /// d4_investordetails
    /// </summary>
    public const String D4InvestorDetails = "d4_investordetails";
    /// <summary>
    /// d4_investor
    /// </summary>
    public const String D4Investor = "d4_investor";
    /// <summary>
    /// d4_policy
    /// </summary>
    public const String D4Policy = "d4_policy";
    /// <summary>
    /// d4_policyid
    /// </summary>
    public const String D4PolicyId = "d4_policyid";
    /// <summary>
    /// d4_policyaudit
    /// </summary>
    public const String D4PolicyAudit = "d4_policyaudit";
    /// <summary>
    /// d4_investorid
    /// </summary>
    public const String D4InvestorId = "d4_investorid";
    /// <summary>
    /// d4_policy
    /// </summary>
    public const String Policy = "d4_policy";

    /// <summary>
    /// "d4_username"
    /// </summary>
    public const String D4UserName = "d4_username";
    /// <summary>
    /// sharepointdocumentlocation
    /// </summary>
    public const String SharePointDocumentLocation = "sharepointdocumentlocation";

    /// <summary>
    /// "regardingobjectid"
    /// </summary>
    public const String RegardingObjectId = "regardingobjectid";

    /// <summary>
    /// modifiedon
    /// </summary>
    public const String ModifiedOnDate = "modifiedon";
    ///<summar>
    ///"Lookback"
    ///</summary>
    public const String Lookback = "Lookback";

    /// <summary>
    /// new_lastshippeddate
    /// </summary>
    public const String LastShippedDate = "new_lastshippeddate";
    #endregion
    #region methods
    public EntityCollection GetAuditBy(String investorName)
    {
        EntityCollection Audits = null;
        ConditionExpression AuditCondition = new ConditionExpression(investorName, ConditionOperator.Equal, investorName);
        FilterExpression AuditFilter = new FilterExpression();
        QueryExpression AuditDetails = new QueryExpression(D4PolicyAudit)
        {
            ColumnSet = new ColumnSet(true),
            Criteria = AuditFilter
        };
        Audits = OrgServiceInstance.RetrieveMultiple(AuditDetails);
        return Audits;
    }
    public EntityCollection GetPoliciesByAccount(List<Guid> ids)
    {
        Int32 LookbackPeriod = 0;
        if (ConfigurationManager.AppSettings[Lookback] == null)
        {
            LookbackPeriod = 20;
        }
        LookbackPeriod =  Int32.Parse(ConfigurationManager.AppSettings[Lookback].ToString());
        return GetPoliciesByAccount(ids, LookbackPeriod);
    }
    public EntityCollection GetPoliciesByAccount(List<Guid> ids, Int32 lookBackDays)
    {
        // TODO - we need to add exception handling, we also need null checks
        // TODO - Many shortcuts were taken authorized by David R to save time
        EntityCollection PolicyCollection = new EntityCollection();
        foreach (Guid id in ids)
        {
            EntityCollection AllPolicies = null;
            ConditionExpression Policies = new ConditionExpression(D4PolicyId, ConditionOperator.Equal, id);
            DateTime LookbackPeriod = DateTime.Now.AddDays((Convert.ToDouble(lookBackDays * -1)));
            ConditionExpression ModifiedCondition = new ConditionExpression(LastShippedDate, ConditionOperator.GreaterThan, LookbackPeriod);

            FilterExpression PolicyFilter = new FilterExpression();
            PolicyFilter.AddCondition(Policies);
            PolicyFilter.AddCondition(ModifiedCondition);
            OrderExpression SortFilter = new OrderExpression();
            SortFilter.AttributeName = LastShippedDate;
            SortFilter.OrderType = OrderType.Descending;
            QueryExpression PolicyDetails = new QueryExpression(D4Policy)
            {
                ColumnSet = new ColumnSet(true),
                Criteria = PolicyFilter
            };
            PolicyDetails.Orders.Add(SortFilter);
            AllPolicies = OrgServiceInstance.RetrieveMultiple(PolicyDetails);
            if (null != AllPolicies && AllPolicies.Entities.Count > 0)
            {
                PolicyCollection.Entities.Add(AllPolicies[0]);
            }
        }
        return PolicyCollection;
    }
    public EntityCollection GetRelatedPolicies(Guid accountId, Int32 lookbackPeriod)
    {
        EntityCollection RelatedPolicies = null;
        ConditionExpression Investors = new ConditionExpression(D4Investor, ConditionOperator.Equal, accountId);
        FilterExpression InvestorFilter = new FilterExpression();
        InvestorFilter.AddCondition(Investors);
        QueryExpression InvestorDetails = new QueryExpression(D4InvestorDetails)
        {
            ColumnSet = new ColumnSet(true),
            Criteria = InvestorFilter
        };
        EntityCollection InvestorDetailsCollection = OrgServiceInstance.RetrieveMultiple(InvestorDetails);
        if (null != InvestorDetailsCollection && InvestorDetailsCollection.Entities.Count > 0)
        {
            List<Guid> PolicyIdCollection = new List<Guid>();
            foreach (Entity investorDetails in InvestorDetailsCollection.Entities)
            {
                EntityReference AccountPolicy = investorDetails[D4Policy] as EntityReference;
                if (AccountPolicy != null)
                {
                    Entity ConvertedPolicy = AccountPolicy.ToEntity(OrgServiceInstance);
                    if (ConvertedPolicy.Attributes.Contains(D4PolicyId))
                    {
                        Guid CurrentPolicyId = new Guid(ConvertedPolicy[D4PolicyId].ToString());
                        PolicyIdCollection.Add(CurrentPolicyId);
                    }
                }
            }
            RelatedPolicies = GetPoliciesByAccount(PolicyIdCollection, lookbackPeriod);


        }
        return RelatedPolicies;
    }

    public Guid GetUserId(String userName, String password)
    {
        #region prevalidation
        // TODO Remove this when it goes to production
        if (String.IsNullOrWhiteSpace(userName))
        {
            userName = "Dynamics4";
        }
        if (String.IsNullOrWhiteSpace(password))
        {
            password = "Dynamics4";
        }
        #endregion
        Guid UserId = Guid.Empty;
        EntityCollection AccountInfo = null;
        ConditionExpression AccountUserName = new ConditionExpression("d4_username", ConditionOperator.Equal, userName);
        ConditionExpression AccountPassword = new ConditionExpression("d4_password", ConditionOperator.Equal, password);
        FilterExpression AccountFilter = new FilterExpression();
        AccountFilter.AddCondition(AccountUserName);
        AccountFilter.AddCondition(AccountPassword);
        QueryExpression AccountQuery = new QueryExpression("account")
        {
            ColumnSet = new ColumnSet(true),
            Criteria = AccountFilter
        };
        AccountInfo = OrgServiceInstance.RetrieveMultiple(AccountQuery);
        if (null != AccountInfo && AccountInfo.Entities.Count > 0)
        {
            UserId = new Guid(AccountInfo[0]["accountid"].ToString());
        }
        return UserId;
    }
    public Guid WhoAmIUserId(String userName, String password)
    {
        Guid UserId = Guid.Empty;
        if (!String.IsNullOrWhiteSpace(userName) && !String.IsNullOrWhiteSpace(password))
        {
            ConnectionInstance.ClientCredentials = new System.ServiceModel.Description.ClientCredentials();
            ConnectionInstance.ClientCredentials.UserName.UserName = userName;
            ConnectionInstance.ClientCredentials.UserName.Password = password;
        }
        WhoAmIRequest WhoAmIId = new WhoAmIRequest();
        WhoAmIResponse WhoAmIValue = OrgServiceInstance.Execute(WhoAmIId) as WhoAmIResponse;
        UserId = WhoAmIValue.UserId;
        return UserId;

    }
    public EntityCollection GetPolicies(Guid userId)
    {
        EntityCollection Policies = null;
        ConditionExpression PolicyCondition = new ConditionExpression("ownerid", ConditionOperator.Equal, userId);
        FilterExpression PolicyFilter = new FilterExpression();
        PolicyFilter.AddCondition(PolicyCondition);
        QueryExpression PolicyQuery = new QueryExpression("d4_policy")
        {
            ColumnSet = new ColumnSet(true),
            Criteria = PolicyFilter
        };
        Policies = OrgServiceInstance.RetrieveMultiple(PolicyQuery);
        return Policies;

    }
    #endregion
    #region properties
    private CrmConnection connectionInstance;
    private IOrganizationService orgServiceInstance;
    /// <summary>
    /// Gets the <see cref="CrmConnection"/> property.
    /// </summary>
    /// <remarks>
    /// Checks if the backing variable is initialized, if it is not, it instantiates it.</remarks>
    public CrmConnection ConnectionInstance
    {
        get
        {
            if (null == connectionInstance)
            {
                connectionInstance = new CrmConnection("Crm");
            }
            return connectionInstance;
        }
    }

    /// <summary>
    /// Gets or sets the <see cref="IOrganizationService"/> property.
    /// </summary>
    public IOrganizationService OrgServiceInstance
    {
        get
        {
            if (null == orgServiceInstance)
            {
                orgServiceInstance = new OrganizationService(ConnectionInstance);
            }
            return orgServiceInstance;
        }
    }
    #endregion
    /// <summary>
    /// Creates a new instance of the CrmApi class.
    /// </summary>
    /// <remarks>
    /// CrmUri=dyncrm</remarks>
    [DebuggerStepThrough()]
    public CrmApi() { }

    /// <summary>
    /// Returns a list of documents based on their parent library identifier
    /// </summary>
    /// <param name="libraryId"></param>
    /// <returns></returns>
    public String[] GetDocumentsByPolicy(String libraryId)
    {
        #region validation
        if (String.IsNullOrWhiteSpace(libraryId))
        {
            throw new ArgumentNullException("libraryId", "Value can not be null");
        }
        #endregion
        AbacusService.Service SPServiceInastance = new AbacusService.Service();
        String[] Documents = null;
        try
        {
            Documents = SPServiceInastance.GetDocList(libraryId);
        }
        catch (Exception ex)
        {
            Logger.Write(String.Format(CultureInfo.CurrentCulture, "Error in CrmApi.GetDocumentsByPolicy: {0}", ex.ToString()));
        }
        return Documents;
    }
    /// <summary>
    /// Returns a list of Items based on the library id.
    /// </summary>
    /// <param name="libraryId"></param>
    /// <returns></returns>
    public EntityCollection GetDocumentList(String libraryId)
    {
        #region validation
        if (String.IsNullOrWhiteSpace(libraryId))
        {
            throw new ArgumentNullException("libraryId", "Value can not be null");
        }
        #endregion
        EntityCollection SharePointSites = new EntityCollection();
        QueryExpression ShPSites = new QueryExpression(SharePointDocumentLocation);
        ConditionExpression RegardingCondition = new ConditionExpression();
        RegardingCondition.AttributeName = RegardingObjectId;
        RegardingCondition.Operator = ConditionOperator.Equal;
        RegardingCondition.Values.Add(new Guid(libraryId));
        ShPSites.ColumnSet = new ColumnSet(true);
        ShPSites.Criteria.AddCondition(RegardingCondition);
        try
        {
            SharePointSites = OrgServiceInstance.RetrieveMultiple(ShPSites);
        }
        catch (Exception ex)
        {
            Logger.Write(String.Format(CultureInfo.CurrentCulture, "Error in CrmApi.GetDocumentList: {0}", ex.ToString()));
        }
        return SharePointSites;
    }

    /// <summary>
    /// Creates a new investor
    /// </summary>
    /// <param name="investorIds"></param>
    /// <param name="policyNumber"></param>
    public void CreateInvestor(List<Guid> investorIds, Guid policyNumber)
    {
        #region validation
        if (null == investorIds)
        {
            throw new ArgumentNullException("investorIds", "Value can not be null.");
        }
        #endregion
        // Instance.LogActivity(Accounts, LibraryLocation, PolicyNumber);
        foreach (Guid investorId in investorIds)
        {
            ConditionExpression AccountCondition = new ConditionExpression(D4Investor, ConditionOperator.Equal, investorId);
            ConditionExpression PolicyCondition = new ConditionExpression(D4Policy, ConditionOperator.Equal, policyNumber);
            FilterExpression PolicyFilter = new FilterExpression();
            PolicyFilter.AddCondition(AccountCondition);
            PolicyFilter.AddCondition(PolicyCondition);


            QueryExpression InvestorDetailsQuery = new QueryExpression(D4InvestorDetails);
            InvestorDetailsQuery.ColumnSet = new ColumnSet(true);

            InvestorDetailsQuery.Criteria.AddFilter(PolicyFilter);
            EntityCollection DetailsCollection = null;
            try
            {
                DetailsCollection = OrgServiceInstance.RetrieveMultiple(InvestorDetailsQuery);
            }
            catch (Exception ex)
            {
                Logger.Write(String.Format(CultureInfo.CurrentCulture, "Error in CrmApi.CreateInvestor: {0}", ex.ToString()));
            }
            if (null == DetailsCollection || DetailsCollection.Entities.Count < 1)
            {
                Entity PolicyDetails = new Entity(D4InvestorDetails);
                PolicyDetails[D4Investor] = new EntityReference(EmailConstants.AccountEntityName, investorId);
                PolicyDetails[D4Policy] = new EntityReference(D4Policy, policyNumber);
                try
                {
                    OrgServiceInstance.Create(PolicyDetails);
                }
                catch (Exception ex)
                {
                    Logger.Write(String.Format(CultureInfo.CurrentCulture, "Error in CrmApi.CreateInvestor: {0}", ex.ToString()));
                }
            }
        }
    }

    /// <summary>
    /// Logs a Ship Document activity.
    /// </summary>
    /// <param name="accountIds"></param>
    /// <param name="sharepointDocuments"></param>
    /// <param name="policyNumber"></param>
    public void LogActivity(List<Guid> accountIds, List<String> sharepointDocuments, String policyNumber)
    {
        #region validation
        if (null == accountIds)
        {
            throw new ArgumentNullException("accountIds", "Value can not be null.");
        }
        if (null == sharepointDocuments)
        {
            throw new ArgumentNullException("sharepointDocuments", "Value can not be null");
        }
        #endregion
        DateTime CurrentShipDate = DateTime.Now;
        foreach (Guid accountId in accountIds)
        {
            Entity CurrentAcct = GetAccountById(accountId);
            String D4Name = String.Empty;
            if (CurrentAcct.Attributes.Contains(D4UserName))
            {
                D4Name = CurrentAcct[D4UserName].ToString();
            }
            foreach (String sharePointDoc in sharepointDocuments)
            {
                Entity NewAudit = new Entity(D4DocumentAudit);
                NewAudit[ProjectConstants.D4Document] = sharePointDoc;
                NewAudit[ProjectConstants.D4ShipDate] = CurrentShipDate;
                NewAudit[PolicyConstants.PolicyName] = policyNumber; //"d4_policyname"
                NewAudit[ProjectConstants.D4Name] = policyNumber;
                NewAudit[InvestorConstants.D4InvestorId] = D4Name;
                try
                {
                    OrgServiceInstance.Create(NewAudit);
                }
                catch (Exception ex)
                {
                    Logger.Write(String.Format(CultureInfo.CurrentCulture, "Error in CrmApi.LogActivity: {0}", ex.ToString()));
                }
            }

        }
    }
    /// <summary>
    /// Creates a new Email activity and issues a send on it.
    /// </summary>
    /// <param name="from"></param>
    /// <param name="accountIds"></param>
    /// <param name="sharepointDocuments"></param>
    /// <param name="regardingObjectId"></param>
    /// <param name="clientName"></param>
    /// <param name="policyNumber"></param>
    //public void CreateEmailActivity(Guid from, List<Guid> accountIds, List<String> sharepointDocuments, Guid regardingObjectId, String clientName, String policyNumber, String textNote)
    //{

    //    if (Guid.Empty == from)
    //    {
    //        from = new Guid(ConfigurationManager.AppSettings[ProjectConstants.FromUserId].ToString());
    //    }
    //    #region validation
    //    if (null == accountIds)
    //    {
    //        throw new ArgumentNullException("accountIds", "Value can not be null.");
    //    }
    //    if (Guid.Empty == regardingObjectId)
    //    {
    //        throw new ArgumentOutOfRangeException("regardingObjectId", "Guid.Empty is not a valid");
    //    }
    //    if (String.IsNullOrWhiteSpace(clientName))
    //    {
    //        clientName = "Case Number: Not given.";
    //    }
    //    if (String.IsNullOrWhiteSpace(policyNumber))
    //    {
    //        policyNumber = "Policy Number: Not given.";
    //    }
    //    #endregion
    //    foreach (Guid accountId in accountIds)
    //    {
    //        //Create the Email entity
    //        Entity NewEmail = new Entity(EmailConstants.EmailEntityName);

    //        //Create the From component of the Email
    //        EntityReference FromReference = new EntityReference(EmailConstants.SystemUserEntityName, from);
    //        Entity FromEntity = new Entity(EmailConstants.ActivityParty);
    //        FromEntity[EmailConstants.PartyId] = FromReference;
    //        EntityCollection FromGroup = new EntityCollection();
    //        FromGroup.EntityName = EmailConstants.SystemUserEntityName;
    //        FromGroup.Entities.Add(FromEntity);

    //        //Create the To component of the email.
    //        EntityReference ToReference = new EntityReference(EmailConstants.AccountEntityName, accountId);
    //        Entity ToEntity = new Entity(EmailConstants.ActivityParty);
    //        ToEntity[EmailConstants.PartyId] = ToReference;
    //        EntityCollection ToGroup = new EntityCollection();
    //        ToGroup.EntityName = EmailConstants.AccountEntityName;
    //        ToGroup.Entities.Add(ToEntity);
    //        //Set the RegardingObjectId - It is retrieved from the QueryString and maps to the Policy
    //        EntityReference RegardingObjectId = new EntityReference(PolicyConstants.PolicyEntityName, regardingObjectId);

    //        NewEmail[EmailConstants.From] = FromGroup;
    //        NewEmail[EmailConstants.To] = ToGroup;
    //        NewEmail[EmailConstants.Subject] = clientName;
    //        NewEmail[EmailConstants.Description] = BuildMessageBody(accountId, sharepointDocuments, policyNumber, clientName, textNote);
    //        NewEmail[EmailConstants.RegardingObjectId] = RegardingObjectId;
    //        //Guid NewEmailId = Guid.Empty;
    //        //try
    //        //{
    //        //    NewEmailId = OrgServiceInstance.Create(NewEmail);
    //        //}
    //        //catch (FaultException<OrganizationServiceFault> CreateError)
    //        //{
    //        //    StringBuilder CreateMessage = new StringBuilder();
    //        //    CreateMessage.AppendFormat(CultureInfo.CurrentCulture, "An Error occurred attempting to create Email for Policy: {0} and Case: {1}. \r\n Details: {3}",
    //        //                            policyNumber, clientName, CreateError.ToString());
    //        //    Logger.Write(String.Format(CultureInfo.CurrentCulture, "Error in CrmApi.CreateEmailActivity: {0}", CreateError.ToString()));

    //        //}
    //        //if (Guid.Empty != NewEmailId)
    //        //{
    //        //    SendEmailRequest MailRequest = new SendEmailRequest();
    //        //    MailRequest.EmailId = NewEmailId;
    //        //    MailRequest.IssueSend = true;
    //        //    MailRequest.TrackingToken = String.Empty;
    //        //    try
    //        //    {
    //        //        SendEmailResponse SendResponse = (OrgServiceInstance.Execute(MailRequest) as SendEmailResponse);
    //        //    }
    //        //    catch (FaultException<OrganizationServiceFault> SendError)
    //        //    {
    //        //        StringBuilder CreateMessage = new StringBuilder();
    //        //        CreateMessage.AppendFormat(CultureInfo.CurrentCulture, "An Error occurred attempting to Send Email for Policy: {0} and Case: {1}. \r\n Details: {3}. "
    //        //            + "The Email message was created but it has not been sent. Please reivew the Activities section of this record for further details.",
    //        //                                policyNumber, clientName, SendError.ToString());
    //        //        Logger.Write(String.Format(CultureInfo.CurrentCulture, "Error in CrmApi.CreateEmailActivity.Send: {0}", SendError.ToString()));
    //        //    }
    //        //}
    //    }
    //}
    /// <summary>
    /// Indicates whether or not documents were sent alread.
    /// </summary>
    /// <param name="acctName">The name of the Account</param>
    /// <param name="uri">Document URI location</param>
    /// <returns>True or false</returns>
    public Boolean WereDocumentsSent(String acctName, String uri)
    {
        #region validation
        if (String.IsNullOrWhiteSpace(acctName))
        {
            throw new ArgumentNullException("acctName", "Value can not be null.");
        }
        if (String.IsNullOrWhiteSpace(uri))
        {
            throw new ArgumentNullException("uri", "Value can not be null.");
        }
        #endregion
        Boolean WasSent = false;
        ConditionExpression AccountNameCondition = new ConditionExpression();
        AccountNameCondition.AttributeName = D4InvestorId;
        AccountNameCondition.Operator = ConditionOperator.Equal;
        AccountNameCondition.Values.Add(acctName);
        ConditionExpression DocumentCondition = new ConditionExpression();
        DocumentCondition.AttributeName = ProjectConstants.D4Document;
        DocumentCondition.Operator = ConditionOperator.Equal;
        DocumentCondition.Values.Add(uri);
        QueryExpression NameQuery = new QueryExpression(D4DocumentAudit);
        NameQuery.ColumnSet = new ColumnSet(true);
        NameQuery.Criteria.AddCondition(AccountNameCondition);
        NameQuery.Criteria.AddCondition(DocumentCondition);
        EntityCollection NameValue = null;
        try
        {
            NameValue = OrgServiceInstance.RetrieveMultiple(NameQuery);
        }
        catch (Exception ex)
        {
            Logger.Write(String.Format(CultureInfo.CurrentCulture, "Error in CrmApi.WereDocumentsSent: {0}", ex.ToString()));
        }
        if (null != NameValue && NameValue.Entities.Count > 0)
        {
            WasSent = true;
        }
        return WasSent;
    }

    /// <summary>
    /// Retrieves an Account Name by LoginId (d4_username).
    /// </summary>
    /// <param name="loginId">d4_username</param>
    /// <returns>'name' value of account</returns>
    public String GetAccountNameFromId(String loginId)
    {
        #region validation
        if (String.IsNullOrWhiteSpace(loginId))
        {
            throw new ArgumentNullException("loginId", "Value can not be null.");
        }
        #endregion
        String CurrentAccount = null;
        ConditionExpression NameCondition = new ConditionExpression();
        NameCondition.AttributeName = D4UserName;
        NameCondition.Operator = ConditionOperator.Equal;
        NameCondition.Values.Add(loginId);
        QueryExpression NameQuery = new QueryExpression(EmailConstants.AccountEntityName);
        NameQuery.ColumnSet = new ColumnSet(true);
        NameQuery.Criteria.AddCondition(NameCondition);
        EntityCollection NameValue = null;
        try
        {
            NameValue = OrgServiceInstance.RetrieveMultiple(NameQuery);
        }
        catch (Exception ex)
        {
            Logger.Write(String.Format(CultureInfo.CurrentCulture, "Error in CrmApi.GetAccountNameFromId: {0}", ex.ToString()));
        }
        if (null != NameValue && NameValue.Entities.Count > 0)
        {
            if (NameValue[0].Attributes.Contains(EmailConstants.Name))
            {
                CurrentAccount = NameValue[0][EmailConstants.Name].ToString();
            }
        }
        return CurrentAccount;
    }

    /// <summary>
    /// Retrives an Account using its AccountId as an identifier
    /// </summary>
    /// <param name="accountId"><see cref="System.Guid"/> identifier for the Account.</param>
    /// <returns>Hydrated account matching the accountid.</returns>
    private Entity GetAccountById(Guid accountId)
    {
        Entity CurrentAccount = null;
        ConditionExpression NameCondition = new ConditionExpression();
        NameCondition.AttributeName = EmailConstants.AccountId;
        NameCondition.Operator = ConditionOperator.Equal;
        NameCondition.Values.Add(accountId);
        QueryExpression NameQuery = new QueryExpression(EmailConstants.AccountEntityName);
        NameQuery.ColumnSet = new ColumnSet(true);
        NameQuery.Criteria.AddCondition(NameCondition);
        EntityCollection NameValue = null;
        try
        {
            NameValue = OrgServiceInstance.RetrieveMultiple(NameQuery);
        }
        catch (Exception ex)
        {
            Logger.Write(String.Format(CultureInfo.CurrentCulture, "Error in CrmApi.GetAccountById: {0}", ex.ToString()));
        }
        if (null != NameValue && NameValue.Entities.Count > 0)
        {
            CurrentAccount = NameValue[0];
        }
        return CurrentAccount;
    }
    private String GetAccountNameByAccountId(Guid accountId)
    {
        String ReturnValue = String.Empty;
        ConditionExpression NameCondition = new ConditionExpression();
        NameCondition.AttributeName = EmailConstants.AccountId;
        NameCondition.Operator = ConditionOperator.Equal;
        NameCondition.Values.Add(accountId);
        QueryExpression NameQuery = new QueryExpression(EmailConstants.AccountEntityName);
        NameQuery.ColumnSet = new ColumnSet(true);
        NameQuery.Criteria.AddCondition(NameCondition);
        EntityCollection NameValue = null;
        try
        {
            NameValue = OrgServiceInstance.RetrieveMultiple(NameQuery);
        }
        catch (Exception ex)
        {
            Logger.Write(String.Format(CultureInfo.CurrentCulture, "Error in CrmApi.GetAccountByAccountId: {0}", ex.ToString()));
        }
        if (null != NameValue && NameValue.Entities.Count > 0)
        {
            ReturnValue = NameValue[0][EmailConstants.Name].ToString();
        }
        return ReturnValue;
    }

    //private String BuildMessageBody(Guid accountId, List<String> sharePointLinks, String policyNumber, String clientName, String textNote)
    //{

    //    if (Guid.Empty == accountId)
    //    {
    //        throw new ArgumentOutOfRangeException("accountId", "AccountId can not be an empty Guid.");
    //    }
    //    if (null == sharePointLinks)
    //    {
    //        throw new ArgumentNullException("sharePointLinks", "Value can not be null.");
    //    }
    //    if (String.IsNullOrWhiteSpace(policyNumber))
    //    {
    //        policyNumber = "Policy Number: Not Given";
    //    }
    //    if (String.IsNullOrWhiteSpace(clientName))
    //    {
    //        clientName = "Case Number: Not Given";
    //    }
    //    StringBuilder UrlList = new StringBuilder();
    //    String MemberName = null;
    //    String AccountName = GetAccountNameByAccountId(accountId);
    //    if (clientName.IndexOf('-') > -1)
    //    {
    //        String[] MemberTokens = clientName.Split('-');
    //        if (null != MemberTokens && MemberTokens.Length > 0)
    //        {
    //            MemberName = MemberTokens[0];
    //        }
    //    }
    //    UrlList.AppendFormat(CultureInfo.CurrentCulture, "Dear {0}:<br><br>", AccountName);
    //    if (null != sharePointLinks)
    //    {
    //        UrlList.Append("<font color=\"red\">DO NOT REPLY TO THIS E-MAIL!</font><br><br>");
    //        UrlList.Append("This is only an informational message. If you wish to reply please contact your case manager or account executive for this case!<br><br>");
    //        UrlList.AppendFormat("<b>The case file has been updated for: </b> {0}", MemberName);
    //        UrlList.Append("<br><br><br>");
    //        if (!String.IsNullOrWhiteSpace(textNote))
    //        {
    //            UrlList.AppendFormat("{0}<br><br>", textNote);
    //        }
    //        //UrlList.Append("<font color=\"red\">Note: all meds attached</font><br><br>");
    //        UrlList.Append("<b>The following files have been added:</b></br>");
    //        UrlList.Append("<ul>");
    //        for (Int32 i = 1; i < sharePointLinks.Count; i++)
    //        {
    //            UrlList.AppendFormat(CultureInfo.CurrentCulture, "<li>{0}</li>", sharePointLinks[i]);
    //        }
    //        UrlList.Append("</ul>");
    //        // UrlList.AppendFormat("<b>To visit the account portal <a href=\"{0}\"> Click Here</a></b><br>", sharePointLinks[0]);
    //        UrlList.AppendFormat("<b>To visit the account portal <a href=\"{0}\"> Click Here</a></b><br>", ConfigurationManager.AppSettings[ProjectConstants.PortalUriFull]);
    //        UrlList.AppendFormat("<b>Policy #: {0}  </b></br>", policyNumber);
    //    }
    //    else
    //    {
    //        UrlList.Append("No files have been selected but an email activity was created. Please contact sender for further information.");
    //    }

    //    return UrlList.ToString();
    //}
    /// <summary>
    /// Gets a list of Investors
    /// </summary>
    /// <remarks>
    /// Maps to Account Entity using an OptionSetValue of 4.
    /// </remarks>
    /// <returns><see cref="EntityCollection"/></returns>
    public EntityCollection GetInvestorList()
    {
        EntityCollection InvestorList = null;
        String InvestoryQuery = InvestorFetch();
        FetchExpression Fetch = new FetchExpression(InvestoryQuery);
        try
        {
            InvestorList = OrgServiceInstance.RetrieveMultiple(Fetch);
        }
        catch (Exception ex)
        {
            Logger.Write(String.Format(CultureInfo.CurrentCulture, "Error in CrmApi.GetInvestorList: {0}", ex.ToString()));
        }
        return InvestorList;
    }



    /// <summary>
    /// Returns Policy Amount from a PolicyId
    /// </summary>
    /// <param name="policyId"></param>
    /// <returns>String with the policy #</returns>
    public String GetPolicyAmountFromId(Guid policyId)
    {
        String ReturnValue = String.Empty;
        ConditionExpression NameCondition = new ConditionExpression();
        NameCondition.AttributeName = PolicyConstants.PolicyId;
        NameCondition.Operator = ConditionOperator.Equal;
        NameCondition.Values.Add(policyId);
        QueryExpression NameQuery = new QueryExpression(PolicyConstants.PolicyEntityName);
        NameQuery.ColumnSet = new ColumnSet(true);
        NameQuery.Criteria.AddCondition(NameCondition);
        EntityCollection NameValue = null;
        try
        {
            NameValue = OrgServiceInstance.RetrieveMultiple(NameQuery);
        }
        catch (Exception ex)
        {
            Logger.Write(String.Format(CultureInfo.CurrentCulture, "Error in CrmApi.GetPolicyAmountFromId: {0}", ex.ToString()));
        }
        if (null != NameValue && NameValue.Entities.Count > 0)
        {
            ReturnValue = String.Format(CultureInfo.CurrentCulture, "{0:C}"
                , (NameValue[0][PolicyConstants.DBAmount] as Money).Value);
        }
        return ReturnValue;
    }
    public Entity GetFieldFromId(string entityName, Guid id, string keyFieldName, string[] selectColumns)
    {
        ////String ReturnValue = String.Empty;
        //ConditionExpression NameCondition = new ConditionExpression();
        //NameCondition.AttributeName = keyFieldName;
        //NameCondition.Operator = ConditionOperator.Equal;
        //NameCondition.Values.Add(id);
        //QueryExpression NameQuery = new QueryExpression(entityName);
        ////NameQuery.ColumnSet = new ColumnSet(true);
        //NameQuery.ColumnSet.AddColumns(columns);
        //NameQuery.Criteria.AddCondition(NameCondition);
        //EntityCollection NameValue = null;
        //try
        //{
        //    NameValue = OrgServiceInstance.RetrieveMultiple(NameQuery);
        //}
        //catch (Exception ex)
        //{
        //    Logger.Write(String.Format(CultureInfo.CurrentCulture, "Error in CrmApi.GetPolicyAmountFromId: {0}", ex.ToString()));
        //}
        //if (null != NameValue && NameValue.Entities.Count > 0)
        //{
        //    return NameValue[0];
        //}
        //return null;
        return GetFieldFromIdWithInclude(
            entityName, 
            id, 
            keyFieldName, 
            selectColumns, 
            null, 
            null, 
            null, 
            JoinOperator.Natural
       );
    }
    public Entity GetFieldFromIdWithInclude(
        string entityName, 
        Guid id, 
        string keyFieldName, 
        string[] selectedColumns, 
        string include, 
        string keyFieldNameForInclude, 
        string[] selectedColumnsForInclude,
        JoinOperator join)
    {
        //String ReturnValue = String.Empty;
        ConditionExpression NameCondition = new ConditionExpression();
        NameCondition.AttributeName = keyFieldName;
        NameCondition.Operator = ConditionOperator.Equal;
        NameCondition.Values.Add(id);
        QueryExpression NameQuery = new QueryExpression(entityName);
        if (include != null)
        {
            // build a link relationship with the parent and child entity
            LinkEntity includeLink = new LinkEntity();
            includeLink.LinkFromEntityName = entityName;
            includeLink.LinkFromEntityName = include;
            includeLink.LinkFromAttributeName = keyFieldName;
            includeLink.LinkToAttributeName = keyFieldNameForInclude;

            NameQuery.LinkEntities.Add(includeLink);
        }
        NameQuery.ColumnSet = new ColumnSet(selectedColumns);
        //NameQuery.ColumnSet.AddColumns(selectedColumns);
        NameQuery.Criteria.AddCondition(NameCondition);
        EntityCollection NameValue = null;
        try
        {
            NameValue = OrgServiceInstance.RetrieveMultiple(NameQuery);
        }
        catch (Exception ex)
        {
            Logger.Write(String.Format(CultureInfo.CurrentCulture, "Error in CrmApi.GetPolicyAmountFromId: {0}", ex.ToString()));
        }
        if (null != NameValue && NameValue.Entities.Count > 0)
        {
            return NameValue[0];
        }
        return null;
    }
    public String GetPolicyNumberFromId(Guid policyId)
    {
        String ReturnValue = String.Empty;
        ConditionExpression NameCondition = new ConditionExpression();
        NameCondition.AttributeName = PolicyConstants.PolicyId;
        NameCondition.Operator = ConditionOperator.Equal;
        NameCondition.Values.Add(policyId);
        QueryExpression NameQuery = new QueryExpression(PolicyConstants.PolicyEntityName);
        NameQuery.ColumnSet = new ColumnSet(true);
        NameQuery.Criteria.AddCondition(NameCondition);
        EntityCollection NameValue = null;
        try
        {
            NameValue = OrgServiceInstance.RetrieveMultiple(NameQuery);
        }
        catch (Exception ex)
        {
            Logger.Write(String.Format(CultureInfo.CurrentCulture, "Error in CrmApi.GetPolicyNumberFromId: {0}", ex.ToString()));
        }
        if (null != NameValue && NameValue.Entities.Count > 0)
        {
            ReturnValue = NameValue[0][PolicyConstants.PolicyNumber].ToString();
        }
        return ReturnValue;
    }
    public String GetPolicyFromId(Guid policyId)
    {
        String ReturnValue = String.Empty;
        ConditionExpression NameCondition = new ConditionExpression();
        NameCondition.AttributeName = PolicyConstants.PolicyId;
        NameCondition.Operator = ConditionOperator.Equal;
        NameCondition.Values.Add(policyId);
        QueryExpression NameQuery = new QueryExpression(PolicyConstants.PolicyEntityName);
        NameQuery.ColumnSet = new ColumnSet(true);
        NameQuery.Criteria.AddCondition(NameCondition);
        EntityCollection NameValue = null;
        try
        {
            NameValue = OrgServiceInstance.RetrieveMultiple(NameQuery);
        }
        catch (Exception ex)
        {
            Logger.Write(String.Format(CultureInfo.CurrentCulture, "Error in CrmApi.GetPolicyNumberFromId: {0}", ex.ToString()));
        }
        if (null != NameValue && NameValue.Entities.Count > 0)
        {
            ReturnValue = NameValue[0][PolicyConstants.PolicyNumber].ToString();
        }
        return ReturnValue;
    }
    public EntityCollection GetClientsFromPolicyNumber(String policyNumber)
    {
        if (String.IsNullOrWhiteSpace(policyNumber))
        {
            throw new ArgumentNullException("policyNumber", "Value can not be null.");
        }
        EntityCollection MatchingClients = null;
        StringBuilder Fetch = new StringBuilder();
        Fetch.Append("<fetch version=\"1.0\" output-format=\"xml-platform\" mapping=\"logical\" distinct=\"true\">");
        Fetch.Append(" <entity name=\"d4_client\">");
        Fetch.Append("   <attribute name=\"d4_clientid\" />");
        Fetch.Append("   <attribute name=\"d4_clientname\" />");
        Fetch.Append("  <attribute name=\"createdon\" />");
        Fetch.Append("  <order attribute=\"d4_clientname\" descending=\"false\" />");
        Fetch.Append(" <link-entity name=\"d4_policy\" from=\"d4_clientpolicyid\" to=\"d4_clientid\" alias=\"ab\">");
        Fetch.Append("   <filter type=\"and\">");
        Fetch.AppendFormat(CultureInfo.CurrentCulture, "   <condition attribute=\"d4_policynumber\" operator=\"eq\" value=\"{0}\" />", policyNumber);
        Fetch.Append("   </filter>");
        Fetch.Append("  </link-entity>");
        Fetch.Append(" </entity>");
        Fetch.Append("</fetch>");
        FetchExpression FetchQuery = new FetchExpression(Fetch.ToString());
        try
        {
            MatchingClients = OrgServiceInstance.RetrieveMultiple(FetchQuery);
        }
        catch (Exception ex)
        {
            Logger.Write(String.Format(CultureInfo.CurrentCulture, "Error in CrmApi.GetClientsFromPolicyNumber: {0}", ex.ToString()));
        }
        return MatchingClients;
    }
    private String InvestorFetch()
    {
        StringBuilder Fetch = new StringBuilder();
        Fetch.Append("<fetch version=\"1.0\" output-format=\"xml-platform\" mapping=\"logical\" distinct=\"false\">");
        Fetch.Append("<entity name=\"account\">");
        Fetch.Append("<attribute name=\"emailaddress1\" />");
        Fetch.Append("<attribute name=\"name\" />");
        Fetch.Append(" <attribute name=\"accountid\" />");
        Fetch.Append(" <order attribute=\"name\" descending=\"false\" />");
        Fetch.Append("<filter type=\"and\">");
        Fetch.Append(" <condition attribute=\"customertypecode\" operator=\"eq\" value=\"4\" />");
        Fetch.Append("</filter>");
        Fetch.Append(" </entity>");
        Fetch.Append("</fetch>");
        return Fetch.ToString();
    }

    public void GenerateInvestorEmails(string path, List<Guid> selectedInvestorAccounts, List<string> selectedDocuments, Guid queryId, string textNote, out string errMessage)
    {
        // From 
        Guid fromId = Guid.Parse(ConfigurationManager.AppSettings[ProjectConstants.FromUserId]);

        // policy number
        var policyNumber = GetPolicyNumberFromId(queryId);
        // clients
        var clients = GetClientsFromPolicyNumber(policyNumber);
        // case number
        string clientName = null;
        if (clients != null &&
            clients.Entities.Count > 0)
        {
            if (clients[0].Attributes.Contains(PolicyConstants.ClientName))
            {
                clientName = clients[0][PolicyConstants.ClientName].ToString();
            }
            else
            {
                clientName = ProjectConstants.NotFound;
            }
        }
        else
        {
            clientName = ProjectConstants.NotFound;
        }
        // db amount
        var dbAmount = GetPolicyAmountFromId(queryId);
        string caseId = null;
        // concatenate case number with policy amount
        if (!String.IsNullOrWhiteSpace(dbAmount))
        {
            caseId = String.Format(CultureInfo.CurrentCulture, "{0} - {1}", clientName, dbAmount);
        }

        // loop thru each selected investor accounts
        foreach (var account in selectedInvestorAccounts)
        {
            // new email entity
            Entity newEmail = new Entity(EmailConstants.EmailEntityName);

            // from component of the email
            EntityReference FromReference = new EntityReference(EmailConstants.SystemUserEntityName, fromId);
            Entity FromEntity = new Entity(EmailConstants.ActivityParty);
            FromEntity[EmailConstants.PartyId] = FromReference;
            EntityCollection FromGroup = new EntityCollection();
            FromGroup.EntityName = EmailConstants.SystemUserEntityName;
            FromGroup.Entities.Add(FromEntity);
            // to component of the email
            EntityReference ToReference = new EntityReference(EmailConstants.AccountEntityName, account);
            Entity ToEntity = new Entity(EmailConstants.ActivityParty);
            ToEntity[EmailConstants.PartyId] = ToReference;
            EntityCollection ToGroup = new EntityCollection();
            ToGroup.EntityName = EmailConstants.AccountEntityName;
            ToGroup.Entities.Add(ToEntity);
            // set the RegardingObjectId - It is retrieved from the QueryString and maps to the Policy
            EntityReference RegardingObjectId = new EntityReference(PolicyConstants.PolicyEntityName, queryId);

            newEmail[EmailConstants.From] = FromGroup;
            newEmail[EmailConstants.To] = ToGroup;
            newEmail[EmailConstants.Subject] = caseId;
            string body = BuildInvestorEmailBody(path, queryId, account, selectedDocuments, policyNumber, caseId, clientName, dbAmount, textNote);
            newEmail[EmailConstants.Description] = body;
            newEmail[EmailConstants.RegardingObjectId] = RegardingObjectId;
            // create email entity
            Guid NewEmailId = Guid.Empty;
            try
            {
                NewEmailId = OrgServiceInstance.Create(newEmail);
            }
            catch (FaultException<OrganizationServiceFault> CreateError)
            {
                StringBuilder CreateMessage = new StringBuilder();
                CreateMessage.AppendFormat(CultureInfo.CurrentCulture, "An Error occurred attempting to create Email for Policy: {0} and Case: {1}. \r\n Details: {3}",
                                        policyNumber, clientName, CreateError.ToString());
                Logger.Write(String.Format(CultureInfo.CurrentCulture, "Error in CrmApi.CreateEmailActivity: {0}", CreateError.ToString()));
            }
            // send emails
            if (Guid.Empty != NewEmailId)
            {
                //SendEmailRequest MailRequest = new SendEmailRequest();
                //MailRequest.EmailId = NewEmailId;
                //MailRequest.IssueSend = true;
                //MailRequest.TrackingToken = String.Empty;
                //try
                //{
                //    SendEmailResponse SendResponse = (OrgServiceInstance.Execute(MailRequest) as SendEmailResponse);
                //}
                //catch (FaultException<OrganizationServiceFault> SendError)
                //{
                //    StringBuilder CreateMessage = new StringBuilder();
                //    CreateMessage.AppendFormat(CultureInfo.CurrentCulture, "An Error occurred attempting to Send Email for Policy: {0} and Case: {1}. \r\n Details: {3}. "
                //        + "The Email message was created but it has not been sent. Please reivew the Activities section of this record for further details.",
                //                            policyNumber, clientName, SendError.ToString());
                //    Logger.Write(String.Format(CultureInfo.CurrentCulture, "Error in CrmApi.CreateEmailActivity.Send: {0}", SendError.ToString()));
                //}
            }
        }

        errMessage = null;
    }

    private string BuildInvestorEmailBody(string path, Guid queryId, Guid accountId, List<String> sharePointLinks, String policyNumber, string caseId, String clientName, string dbAmount, String textNote)
    {
        string[] policyColumns = new string[] { 
            PolicyConstants.DateofIssue,
            PolicyConstants.PremiumFinance,
            PolicyConstants.InsuranceCompany,
            PolicyConstants.Premium,
            PolicyConstants.TwentyFirstFifty,
            PolicyConstants.AVS,
            PolicyConstants.OtherLE,
            PolicyConstants.OwnerState
        };

        Entity policy = GetFieldFromId(PolicyConstants.PolicyEntityName, queryId, PolicyConstants.PolicyId, policyColumns);

        var issueDate = Convert.ToDateTime(policy[PolicyConstants.DateofIssue]);
        var premiumFinanced = Convert.ToBoolean(policy[PolicyConstants.PremiumFinance]);
        var insuranceCompany = policy[PolicyConstants.InsuranceCompany].ToString();
        //var premium = policy[PolicyConstants.Premium];
        // var twentyFirstFifty = policy[PolicyConstants.TwentyFirstFifty];
        // var AVS = policy[PolicyConstants.AVS];
        // var OtherLE = policy[PolicyConstants.OtherLE];
        // var OwnerState = policy[PolicyConstants.OwnerState];

        string contents = File.ReadAllText(path + @"/InvestorEmail.html");


        StringBuilder UrlList = new StringBuilder();
        String MemberName = null;
        String AccountName = GetAccountNameByAccountId(accountId);
        if (clientName.IndexOf('-') > -1)
        {
            String[] MemberTokens = clientName.Split('-');
            if (null != MemberTokens && MemberTokens.Length > 0)
            {
                MemberName = MemberTokens[0];
            }
        }
        //contents = string.Format(contents, AccountName);

        for (Int32 i = 1; i < sharePointLinks.Count; i++)
        {
            UrlList.AppendFormat(CultureInfo.CurrentCulture, "<li>{0}</li>", sharePointLinks[i]);
        }

        contents = string.Format(
            contents, caseId, AccountName, 
            clientName, policyNumber, dbAmount, 
            issueDate.ToShortDateString(), premiumFinanced ? "YES" : "NO", 
            UrlList.ToString(), ConfigurationManager.AppSettings[ProjectConstants.PortalUriFull]
        );

        return contents;
    }
}