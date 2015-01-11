﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IWorkspaceManagerExtensions.cs" company="Orchestra development team">
//   Copyright (c) 2008 - 2014 Orchestra development team. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.WorkspaceManagement
{
    using System.Linq;
    using System.Threading.Tasks;
    using Catel;
    using Catel.IoC;

    public static class IWorkspaceManagerExtensions
    {
        public static TWorkspace GetWorkspace<TWorkspace>(this IWorkspaceManager workspaceManager)
            where TWorkspace : IWorkspace
        {
            Argument.IsNotNull(() => workspaceManager);

            return (TWorkspace)workspaceManager.Workspace;
        }

        public static void Add(this IWorkspaceManager workspaceManager, IWorkspace workspace, bool autoSelect)
        {
            Argument.IsNotNull(() => workspaceManager);
            Argument.IsNotNull(() => workspace);

            workspaceManager.Add(workspace);

            if (autoSelect)
            {
                workspaceManager.Workspace = workspace;
            }
        }

        public static void AddProvider(this IWorkspaceManager workspaceManager, IWorkspaceProvider workspaceProvider, bool callApplyWorkspaceForCurrentWorkspace)
        {
            Argument.IsNotNull(() => workspaceManager);
            Argument.IsNotNull(() => workspaceProvider);

            workspaceManager.AddProvider(workspaceProvider);

            if (callApplyWorkspaceForCurrentWorkspace)
            {
                var workspace = workspaceManager.Workspace;
                if (workspace != null)
                {
                    workspaceProvider.ApplyWorkspace(workspace);
                }
            }
        }

        public static void AddProvider<TWorkspaceProvider>(this IWorkspaceManager workspaceManager, bool callApplyWorkspaceForCurrentWorkspace)
            where TWorkspaceProvider : IWorkspaceProvider
        {
            Argument.IsNotNull(() => workspaceManager);

            var workspaceProvider = TypeFactory.Default.CreateInstance<TWorkspaceProvider>();
            workspaceManager.AddProvider(workspaceProvider, callApplyWorkspaceForCurrentWorkspace);
        }

        public static async Task Initialize(this IWorkspaceManager workspaceManager, bool addDefaultWorkspaceIfNoWorkspacesAreFound,
            string defaultWorkspaceName = "Default")
        {
            Argument.IsNotNull(() => workspaceManager);

            await workspaceManager.Initialize();

            if (addDefaultWorkspaceIfNoWorkspacesAreFound && !workspaceManager.Workspaces.Any())
            {
                var defaultWorkspace = new Workspace();
                defaultWorkspace.Title = defaultWorkspaceName;
                defaultWorkspace.CanEdit = false;
                defaultWorkspace.CanDelete = false;

                workspaceManager.Add(defaultWorkspace, true);
            }
        }

        public static async Task SetWorkspaceSchemesDirectory(this IWorkspaceManager workspaceManager, string newDirectory, bool addDefaultWorkspaceIfNoWorkspacesAreFound = false, string defaultWorkspaceName = "Default")
        {
            Argument.IsNotNull(() => workspaceManager);
            Argument.IsNotNullOrEmpty(() => newDirectory);
            Argument.IsNotNullOrEmpty(() => defaultWorkspaceName);

            workspaceManager.BaseDirectory = newDirectory;
            await workspaceManager.Initialize(addDefaultWorkspaceIfNoWorkspacesAreFound, defaultWorkspaceName);
        }

        /// <summary>
        /// Stores the workspace and saves it immediately.
        /// </summary>
        /// <param name="workspaceManager">The workspace manager.</param>
        /// <returns>Task.</returns>
        public static async Task StoreAndSave(this IWorkspaceManager workspaceManager)
        {
            Argument.IsNotNull(() => workspaceManager);

            workspaceManager.StoreWorkspace();
            await workspaceManager.Save();
        }
    }
}