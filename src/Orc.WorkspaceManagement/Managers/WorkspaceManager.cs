﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WorkspaceManager.cs" company="Simulation Modelling Services">
//   Copyright (c) 2008 - 2014 Simulation Modelling Services. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.WorkspaceManagement
{
    using System;
    using Catel;
    using Catel.Logging;

    public class WorkspaceManager : IWorkspaceManager
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        private readonly IWorkspaceInitializer _workspaceInitializer;
        private readonly IWorkspaceReader _workspaceReader;
        private readonly IWorkspaceWriter _workspaceWriter;

        private IWorkspace _workspace;

        #region Constructors
        public WorkspaceManager(IWorkspaceInitializer workspaceInitializer, IWorkspaceReader workspaceReader, IWorkspaceWriter workspaceWriter)
        {
            Argument.IsNotNull(() => workspaceInitializer);
            Argument.IsNotNull(() => workspaceReader);
            Argument.IsNotNull(() => workspaceWriter);

            _workspaceInitializer = workspaceInitializer;
            _workspaceReader = workspaceReader;
            _workspaceWriter = workspaceWriter;

            var location = workspaceInitializer.GetInitialLocation();

            Location = location;

            if (!string.IsNullOrEmpty(location))
            {
                Log.Debug("Initial location is '{0}', loading initial workspace", location);

                Load(location);
            }
        }
        #endregion

        #region Properties
        public string Location { get; private set; }

        public IWorkspace Workspace
        {
            get { return _workspace; }
            private set
            {
                var oldWorkspace = _workspace;
                var newWorkspace = value;

                _workspace = value;

                WorkspaceUpdated.SafeInvoke(this, new WorkspaceUpdatedEventArgs(oldWorkspace, newWorkspace));
            }
        }
        #endregion

        #region Events
        public event EventHandler<WorkspaceEventArgs> WorkspaceLoading;
        public event EventHandler<WorkspaceEventArgs> WorkspaceLoaded;

        public event EventHandler<WorkspaceUpdatedEventArgs> WorkspaceUpdated;

        public event EventHandler<WorkspaceEventArgs> WorkspaceClosing;
        public event EventHandler<WorkspaceEventArgs> WorkspaceClosed;
        #endregion

        #region IWorkspaceManager Members
        public void Refresh()
        {
            if (Workspace == null)
            {
                return;
            }

            var location = Location;

            Log.Debug("Refreshing workspace from '{0}'", location);

            Load(location);

            Log.Info("Refreshed workspace from '{0}'", location);
        }

        public void Load(string location)
        {
            Argument.IsNotNullOrWhitespace("location", location);

            Log.Debug("Loading workspace from '{0}'", location);

            var workspace = _workspaceReader.Read(location);

            Workspace = workspace;

            WorkspaceLoaded.SafeInvoke(this, new WorkspaceEventArgs(workspace));

            Log.Info("Loaded workspace from '{0}'", location);
        }

        public void Save(string location = null)
        {
            var workspace = Workspace;
            if (workspace == null)
            {
                Log.Error("Cannot save empty workspace");
                throw new InvalidWorkspaceException(workspace);
            }

            if (string.IsNullOrWhiteSpace(location))
            {
                location = Location;
            }

            Log.Debug("Saving workspace '{0}' to '{1}'", workspace, location);

            _workspaceWriter.Write(workspace, location);

            Log.Info("Saved workspace '{0}' to '{1}'", workspace, location);
        }

        public void Close()
        {
            var workspace = Workspace;
            if (workspace == null)
            {
                return;
            }

            Log.Info("Closing workspace '{0}'", workspace);

            Workspace = null;
            Location = null;
        }
        #endregion
    }
}