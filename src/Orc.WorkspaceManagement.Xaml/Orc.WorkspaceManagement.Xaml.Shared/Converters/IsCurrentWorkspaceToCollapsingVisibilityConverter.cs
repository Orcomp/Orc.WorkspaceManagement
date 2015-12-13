﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IsCurrentWorkspaceToCollapsingVisibilityConverter.cs" company="Wild Gums">
//   Copyright (c) 2008 - 2015 Wild Gums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.WorkspaceManagement.Converters
{
    using System;
    using System.Windows;
    using Catel;
    using Catel.IoC;
    using Catel.MVVM.Converters;

    public class IsCurrentWorkspaceToCollapsingVisibilityConverter : VisibilityConverterBase
    {
        #region Fields
        private readonly IServiceLocator _serviceLocator;
        #endregion

        #region Constructors
        public IsCurrentWorkspaceToCollapsingVisibilityConverter()
            : base(Visibility.Collapsed)
        {
            _serviceLocator = this.GetServiceLocator();
        }
        #endregion

        #region Methods
        protected override bool IsVisible(object value, Type targetType, object parameter)
        {
            var workspace = value as IWorkspace;
            if (workspace == null)
            {
                return false;
            }

            var workspaceManager = _serviceLocator.ResolveType<IWorkspaceManager>(workspace.Tag);
            return workspaceManager != null && ObjectHelper.AreEqual(workspaceManager.Workspace, workspace);
        }
        #endregion
    }
}