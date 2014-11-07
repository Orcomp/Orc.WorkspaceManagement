// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MethodTimeLogger.cs" company="Orchestra development team">
//   Copyright (c) 2008 - 2014 Orchestra development team. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


using System;
using System.Reflection;
using Catel.Logging;

/// <summary>
/// Note: do not rename this class or put it inside a namespace.
/// </summary>
public static class MethodTimeLogger
{
    #region Methods
    /// <summary>
    /// Used by MethodTimer.
    /// </summary>
    /// <param name="methodBase"></param>
    /// <param name="milliseconds"></param>
    public static void Log(MethodBase methodBase, long milliseconds)
    {
        Log(methodBase.DeclaringType, methodBase.Name, milliseconds);
    }

    /// <summary>
    /// Used by custom code.
    /// </summary>
    /// <param name="type"></param>
    /// <param name="methodName"></param>
    /// <param name="milliseconds"></param>
    public static void Log(Type type, string methodName, long milliseconds)
    {
        if (type == null)
        {
            return;
        }

        var logger = LogManager.GetLogger(type);
        logger.Debug("[METHODTIMER] {0}.{1} took '{2}' ms", type.Name, methodName, milliseconds);
    }
    #endregion
}