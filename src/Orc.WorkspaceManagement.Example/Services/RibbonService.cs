﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RibbonService.cs" company="Orchestra development team">
//   Copyright (c) 2008 - 2015 Orchestra development team. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Orc.WorkspaceManagement.Example.Services
{
    using System.Windows;
    using Orchestra.Services;
    using Views;

    public class RibbonService : IRibbonService
    {
        public FrameworkElement GetRibbon()
        {
            return new RibbonView();
        }

        public FrameworkElement GetMainView()
        {
            return new MainView();
        }

        public FrameworkElement GetStatusBar()
        {
            return new StatusBarView();
        }
    }
}