using Microsoft.Practices.EnterpriseLibrary.Logging;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Utilities
/// </summary>
public static class Utilities
{
    #region extensions
    /// <summary>
    /// Logs a string message to the message log.
    /// </summary>
    /// <param name="message"><see cref="System.String"/></param>
    /// <returns><see cref="System.String"/></returns>
    public static String LogMessage(this String message)
    {
        try
        {
            Logger.Write(message);
        }
        catch (Exception)
        {
            // TODO - refine this for Advanced Settlements.
            // Just eat it
        }
        return message;
    }
    /// <summary>
    /// Logs an exception message using all default configuration values.
    /// </summary>
    /// <param name="currentException"><see cref="System.Exception"/> that was thrown.</param>
    /// <param name="message"><see cref="System.String"/> message to be logged.</param>
    public static void LogException(this Exception currentException, String message)
    {
        try
        {
            Logger.Write(message);
        }
        catch (Exception)
        {
            // TODO - refine this for Advanced Settlements.
            // Just eat it
        }
    }
    /// <summary>
    /// Converts an <see cref="Microsoft.Xrm.Sdk.EntityReference"/> to an <see cref="Microsoft.Xrm.Sdk.Entity"/>
    /// </summary>
    /// <param name="currentReference"><see cref="Microsoft.Xrm.Sdk.EntityReference"/></param>
    /// <param name="serviceInstance"><see cref="Microsoft.Xrm.Sdk.IOrganizationService"/></param>
    /// <returns><see cref="Microsoft.Xrm.Sdk.Entity"/></returns>
    public static Entity ToEntity(this EntityReference currentReference, IOrganizationService serviceInstance)
    {
        return serviceInstance.Retrieve(currentReference.LogicalName, currentReference.Id, new ColumnSet(true));
    }
    #endregion
}