﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information
namespace DotNetNuke.Security.Permissions
{
    using System;
    using System.Globalization;

    using DotNetNuke.Common.Utilities;
    using DotNetNuke.Data;
    using DotNetNuke.Entities.Portals;
    using DotNetNuke.Entities.Users;
    using DotNetNuke.Services.Log.EventLog;

    /// <summary>DesktopModulePermissionController provides the Business Layer for DesktopModule Permissions.</summary>
    [Serializable]
    public class DesktopModulePermissionController
    {
        private static readonly PermissionProvider Provider = PermissionProvider.Instance();

        /// <summary>AddDesktopModulePermission adds a DesktopModule Permission to the Database.</summary>
        /// <param name="objDesktopModulePermission">The DesktopModule Permission to add.</param>
        /// <returns>The new desktop module permission ID.</returns>
        public static int AddDesktopModulePermission(DesktopModulePermissionInfo objDesktopModulePermission)
        {
            int id = DataProvider.Instance().AddDesktopModulePermission(
                objDesktopModulePermission.PortalDesktopModuleID,
                objDesktopModulePermission.PermissionID,
                objDesktopModulePermission.RoleID,
                objDesktopModulePermission.AllowAccess,
                objDesktopModulePermission.UserID,
                UserController.Instance.GetCurrentUserInfo().UserID);
            EventLogController.Instance.AddLog(
                objDesktopModulePermission,
                PortalController.Instance.GetCurrentPortalSettings(),
                UserController.Instance.GetCurrentUserInfo().UserID,
                string.Empty,
                EventLogController.EventLogType.DESKTOPMODULEPERMISSION_CREATED);
            ClearPermissionCache();
            return id;
        }

        /// <summary>DeleteDesktopModulePermission deletes a DesktopModule Permission in the Database.</summary>
        /// <param name="desktopModulePermissionID">The ID of the DesktopModule Permission to delete.</param>
        public static void DeleteDesktopModulePermission(int desktopModulePermissionID)
        {
            DataProvider.Instance().DeleteDesktopModulePermission(desktopModulePermissionID);
            EventLogController.Instance.AddLog(
                "DesktopModulePermissionID",
                desktopModulePermissionID.ToString(CultureInfo.InvariantCulture),
                PortalController.Instance.GetCurrentPortalSettings(),
                UserController.Instance.GetCurrentUserInfo().UserID,
                EventLogController.EventLogType.DESKTOPMODULEPERMISSION_DELETED);
            ClearPermissionCache();
        }

        /// <summary>DeleteDesktopModulePermissionsByPortalDesktopModuleID deletes a DesktopModule's DesktopModule Permission in the Database.</summary>
        /// <param name="portalDesktopModuleID">The ID of the DesktopModule to delete.</param>
        public static void DeleteDesktopModulePermissionsByPortalDesktopModuleID(int portalDesktopModuleID)
        {
            DataProvider.Instance().DeleteDesktopModulePermissionsByPortalDesktopModuleID(portalDesktopModuleID);
            EventLogController.Instance.AddLog(
                "PortalDesktopModuleID",
                portalDesktopModuleID.ToString(CultureInfo.InvariantCulture),
                PortalController.Instance.GetCurrentPortalSettings(),
                UserController.Instance.GetCurrentUserInfo().UserID,
                EventLogController.EventLogType.DESKTOPMODULE_DELETED);
            ClearPermissionCache();
        }

        /// <summary>DeleteDesktopModulePermissionsByUserID deletes a user's DesktopModule Permission in the Database.</summary>
        /// <param name="objUser">The user.</param>
        public static void DeleteDesktopModulePermissionsByUserID(UserInfo objUser)
        {
            DataProvider.Instance().DeleteDesktopModulePermissionsByUserID(objUser.UserID, objUser.PortalID);
            EventLogController.Instance.AddLog(
                "UserID",
                objUser.UserID.ToString(CultureInfo.InvariantCulture),
                PortalController.Instance.GetCurrentPortalSettings(),
                UserController.Instance.GetCurrentUserInfo().UserID,
                EventLogController.EventLogType.DESKTOPMODULE_DELETED);
            ClearPermissionCache();
        }

        /// <summary>GetDesktopModulePermission gets a DesktopModule Permission from the Database.</summary>
        /// <param name="desktopModulePermissionID">The ID of the DesktopModule Permission.</param>
        /// <returns>A <see cref="DesktopModulePermissionInfo"/> instance or <see langword="null"/>.</returns>
        public static DesktopModulePermissionInfo GetDesktopModulePermission(int desktopModulePermissionID)
        {
            return Provider.GetDesktopModulePermission(desktopModulePermissionID);
        }

        /// <summary>GetDesktopModulePermissions gets a DesktopModulePermissionCollection.</summary>
        /// <param name="portalDesktopModuleID">The ID of the DesktopModule.</param>
        /// <returns>A <see cref="DesktopModulePermissionCollection"/> with the desktop module permissions, or an empty <see cref="DesktopModulePermissionCollection"/> if the desktop module wasn't found.</returns>
        public static DesktopModulePermissionCollection GetDesktopModulePermissions(int portalDesktopModuleID)
        {
            return Provider.GetDesktopModulePermissions(portalDesktopModuleID);
        }

        /// <summary>HasDesktopModulePermission checks whether the current user has a specific DesktopModule Permission.</summary>
        /// <param name="objDesktopModulePermissions">The Permissions for the DesktopModule.</param>
        /// <param name="permissionKey">The Permission to check.</param>
        /// <returns><see langword="true"/> if the current user has the requested permission, otherwise <see langword="false"/>.</returns>
        public static bool HasDesktopModulePermission(DesktopModulePermissionCollection objDesktopModulePermissions, string permissionKey)
        {
            return Provider.HasDesktopModulePermission(objDesktopModulePermissions, permissionKey);
        }

        /// <summary>UpdateDesktopModulePermission updates a DesktopModule Permission in the Database.</summary>
        /// <param name="objDesktopModulePermission">The DesktopModule Permission to update.</param>
        public static void UpdateDesktopModulePermission(DesktopModulePermissionInfo objDesktopModulePermission)
        {
            DataProvider.Instance().UpdateDesktopModulePermission(
                objDesktopModulePermission.DesktopModulePermissionID,
                objDesktopModulePermission.PortalDesktopModuleID,
                objDesktopModulePermission.PermissionID,
                objDesktopModulePermission.RoleID,
                objDesktopModulePermission.AllowAccess,
                objDesktopModulePermission.UserID,
                UserController.Instance.GetCurrentUserInfo().UserID);
            EventLogController.Instance.AddLog(
                objDesktopModulePermission,
                PortalController.Instance.GetCurrentPortalSettings(),
                UserController.Instance.GetCurrentUserInfo().UserID,
                string.Empty,
                EventLogController.EventLogType.DESKTOPMODULEPERMISSION_UPDATED);
            ClearPermissionCache();
        }

        /// <summary>ClearPermissionCache clears the DesktopModule Permission Cache.</summary>
        private static void ClearPermissionCache()
        {
            DataCache.ClearDesktopModulePermissionsCache();
        }
    }
}
