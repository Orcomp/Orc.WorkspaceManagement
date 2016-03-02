﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WorkspaceManagerFacts.cs" company="WildGums">
//   Copyright (c) 2008 - 2014 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.WorkspaceManagement.Test.Managers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Mocks;
    using Moq;
    using NUnit.Framework;

    public class WorkspaceManagerFacts
    {
        [TestFixture]
        public class TheAddMethod
        {
            [TestCase]
            public async void AddsTheWorkspace()
            {
                var workspaceManager = Factories.WorkspaceManager.WithEmptyInitializer();

                var workspace = new Workspace
                {
                    Title = WorkspaceNameHelper.GetRandomWorkspaceName()
                };

                await workspaceManager.AddAsync(workspace);

                Assert.IsTrue(workspaceManager.Workspaces.Contains(workspace));
            }

            [TestCase]
            public async void RaisesWorkspaceAddedEvent()
            {
                var workspaceManager = Factories.WorkspaceManager.WithEmptyInitializer();

                var eventRaised = false;
                workspaceManager.WorkspaceAdded += (sender, e) => eventRaised = true;

                await workspaceManager.AddAsync(new Workspace());

                Assert.IsTrue(eventRaised);
            }

            [TestCase]
            public async void RaisesWorkspacesChangedEvent()
            {
                var workspaceManager = Factories.WorkspaceManager.WithEmptyInitializer();

                var eventRaised = false;
                workspaceManager.WorkspacesChanged += (sender, e) => eventRaised = true;

                await workspaceManager.AddAsync(new Workspace());

                Assert.IsTrue(eventRaised);
            }
        }

        [TestFixture]
        public class TheRemoveMethod
        {
            [TestCase]
            public async void RemovesTheWorkspace()
            {
                var workspaceManager = Factories.WorkspaceManager.WithEmptyInitializer();

                var workspace = new Workspace
                {
                    Title = WorkspaceNameHelper.GetRandomWorkspaceName()
                };

                await workspaceManager.AddAsync(workspace);

                Assert.IsTrue(workspaceManager.Workspaces.Contains(workspace));

                await workspaceManager.RemoveAsync(workspace);

                Assert.IsFalse(workspaceManager.Workspaces.Contains(workspace));
            }

            [TestCase]
            public async void DoesNotRemoveWorkspaceWithCanDeleteIsFalse()
            {
                var workspaceManager = Factories.WorkspaceManager.WithEmptyInitializer();

                var workspace = new Workspace
                {
                    Title = WorkspaceNameHelper.GetRandomWorkspaceName(),
                    CanDelete = false
                };

                await workspaceManager.AddAsync(workspace);

                Assert.IsTrue(workspaceManager.Workspaces.Contains(workspace));

                await workspaceManager.RemoveAsync(workspace);

                Assert.IsTrue(workspaceManager.Workspaces.Contains(workspace));
            }

            [TestCase]
            public async void RaisesWorkspaceRemovedEvent()
            {
                var workspaceManager = Factories.WorkspaceManager.WithEmptyInitializer();

                var workspace = new Workspace
                {
                    Title = WorkspaceNameHelper.GetRandomWorkspaceName()
                };

                await workspaceManager.AddAsync(workspace);

                var eventRaised = false;
                workspaceManager.WorkspacesChanged += (sender, e) => eventRaised = true;

                await workspaceManager.RemoveAsync(workspace);

                Assert.IsTrue(eventRaised);
            }

            [TestCase]
            public async void RaisesWorkspacesChangedEvent()
            {
                var workspaceManager = Factories.WorkspaceManager.WithEmptyInitializer();

                var workspace = new Workspace
                {
                    Title = WorkspaceNameHelper.GetRandomWorkspaceName()
                };

                await workspaceManager.AddAsync(workspace);

                var eventRaised = false;
                workspaceManager.WorkspacesChanged += (sender, e) => eventRaised = true;

                await workspaceManager.RemoveAsync(workspace);

                Assert.IsTrue(eventRaised);
            }
        }

        [TestFixture]
        public class TheInitializeMethod
        {
            [TestCase]
            public async Task RaisesInitializingEvent()
            {
                var workspaceManager = Factories.WorkspaceManager.WithEmptyInitializer();

                var eventRaised = false;
                workspaceManager.Initializing += (sender, e) => eventRaised = true;

                await workspaceManager.InitializeAsync();

                Assert.IsTrue(eventRaised);
            }

            [TestCase]
            public async Task RaisesInitializedEvent()
            {
                var workspaceManager = Factories.WorkspaceManager.WithEmptyInitializer();

                var eventRaised = false;
                workspaceManager.Initialized += (sender, e) => eventRaised = true;

                await workspaceManager.InitializeAsync();

                Assert.IsTrue(eventRaised);
            }
        }

        [TestFixture]
        public class TheStoreMethod
        {
            [TestCase]
            public async void PreventsSaveForReadonlyWorkspaces()
            {
                var workspaceManager = Factories.WorkspaceManager.WithEmptyInitializer();

                var workspace = new Workspace
                {
                    Title = WorkspaceNameHelper.GetRandomWorkspaceName(),
                    CanEdit = false
                };

                await workspaceManager.AddAsync(workspace);
                await workspaceManager.SetWorkspaceAsync(workspace);

                var eventRaised = false;
                workspaceManager.WorkspaceInfoRequested += (sender, e) => eventRaised = true;

                await workspaceManager.StoreWorkspaceAsync();

                Assert.IsFalse(eventRaised);
            }
        }

        [TestFixture]
        public class TheSaveMethod
        {
            [TestCase]
            public async Task RaisesSavingEvent()
            {
                var workspaceManager = Factories.WorkspaceManager.WithEmptyInitializer();

                var eventRaised = false;
                workspaceManager.Saving += (sender, e) => eventRaised = true;

                await workspaceManager.SaveAsync();

                Assert.IsTrue(eventRaised);
            }

            [TestCase]
            public async Task RaisesSavedEvent()
            {
                var workspaceManager = Factories.WorkspaceManager.WithEmptyInitializer();

                var eventRaised = false;
                workspaceManager.Saved += (sender, e) => eventRaised = true;

                await workspaceManager.SaveAsync();

                Assert.IsTrue(eventRaised);
            }
        }

        [TestFixture]
        public class ThePersistenceLogic
        {
            // TODO : write unit tests
        }

        [TestFixture]
        public class TheAddProviderMethod
        {
            [TestCase]
            public async Task CallsProviderWhenStoringWorkspace()
            {
                var workspaceManager = Factories.WorkspaceManager.WithEmptyInitializer();
                var workspace = new Workspace
                {
                    Title = WorkspaceNameHelper.GetRandomWorkspaceName()
                };

                await workspaceManager.AddAsync(workspace, true);

                var workspaceProvider = new WorkspaceProvider("key1", "value1");
                workspaceManager.AddProvider(workspaceProvider);

                await workspaceManager.StoreWorkspaceAsync();

                Assert.AreEqual("value1", workspace.GetWorkspaceValue("key1", "unexpected"));
            }
        }

        [TestFixture]
        public class TheRemoveProviderMethod
        {
            [TestCase]
            public async Task CorrectlyRemovesProviderWhenStoringWorkspace()
            {
                var workspaceManager = Factories.WorkspaceManager.WithEmptyInitializer();
                var workspace = new Workspace
                {
                    Title = WorkspaceNameHelper.GetRandomWorkspaceName()
                };

                await workspaceManager.AddAsync(workspace, true);

                var workspaceProvider = new WorkspaceProvider("key1", "value1");
                workspaceManager.AddProvider(workspaceProvider);
                workspaceManager.RemoveProvider(workspaceProvider);

                await workspaceManager.StoreWorkspaceAsync();

                Assert.AreEqual("expected", workspace.GetWorkspaceValue("key1", "expected"));
            }
        }
    }
}