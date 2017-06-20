// ***********************************************************************
// Copyright (c) 2017 Dominik Lachance
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using YellowJacket.Core.Enums;
using YellowJacket.Core.Hook;
using YellowJacket.Core.Plugins;

namespace YellowJacket.Core.Contexts
{
    /// <summary>
    /// Represents the execution context.
    /// </summary>
    public sealed class ExecutionContext
    {
        #region Private Members

        private static readonly Lazy<ExecutionContext> Context =
            new Lazy<ExecutionContext>(() => new ExecutionContext());

        private static readonly List<HookInstance> HookInstances =
            new List<HookInstance>();

        private static readonly List<ILogPlugin> LogPlugins =
            new List<ILogPlugin>();

        #endregion

        #region Properties

        /// <summary>
        /// Gets the current context.
        /// </summary>
        /// <value>
        /// The current context.
        /// </value>
        public static ExecutionContext Current => Context.Value;

        #endregion

        #region Constructors

        /// <summary>
        /// Prevents a default instance of the <see cref="ExecutionContext"/> class from being created.
        /// </summary>
        private ExecutionContext() { }

        #endregion

        #region Internal Methods

        /// <summary>
        /// Registers the hook in the context.
        /// </summary>
        /// <param name="hook">The hook to register.</param>
        internal void RegisterHook(HookInstance hook)
        {
            HookInstances.Add(hook);
        }

        /// <summary>
        /// Clears the hook.
        /// </summary>
        internal void ClearHooks()
        {
            HookInstances.Clear();
        }


        /// <summary>
        /// Registers the logplugins.
        /// </summary>
        /// <param name="logPlugin">The log plugin.</param>
        internal void RegisterLogplugins(ILogPlugin logPlugin)
        {
            LogPlugins.Add(logPlugin);
        }

        /// <summary>
        /// Clears the log plugins.
        /// </summary>
        internal void CLearLogPlugins()
        {
            LogPlugins.Clear();
        }

        /// <summary>
        /// Gets the hooks ordered by priority.
        /// </summary>
        /// <returns></returns>
        internal List<HookInstance> GetHooks()
        {
            return HookInstances.OrderBy(x => x.Priority).ToList();
        }


        /// <summary>
        /// Gets the log plugins.
        /// </summary>
        /// <returns></returns>
        internal List<ILogPlugin> GetLogPlugins()
        {
            return LogPlugins;
        }

        #endregion
    }
}
