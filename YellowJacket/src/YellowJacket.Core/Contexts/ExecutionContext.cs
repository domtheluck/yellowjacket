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
using YellowJacket.Core.Hook;
using YellowJacket.Core.Plugins.Interfaces;

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

        //private static IWebDriverConfigurationPlugin _webDriverConfigurationPlugin;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the current context.
        /// </summary>
        /// <value>
        /// The current context.
        /// </value>
        public static ExecutionContext Instance => Context.Value;

        /// <summary>
        /// Gets the hook instances.
        /// </summary>
        /// <value>
        /// The hook instances.
        /// </value>
        public List<HookInstance> Hooks { get; } = new List<HookInstance>();

        public Run Run { get; } = new Run();

        public Plugins Plugins { get; } = new Plugins();

        ///// <summary>
        ///// Gets or sets the driver.
        ///// </summary>
        ///// <value>
        ///// The driver.
        ///// </value>
        //public IWebDriver WebDriver { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Prevents a default instance of the <see cref="ExecutionContext"/> class from being created.
        /// </summary>
        private ExecutionContext() { }

        #endregion

        #region Internal Methods

        #region Hooks

        /// <summary>
        /// Registers the hook in the context.
        /// </summary>
        /// <param name="hook">The hook to register.</param>
        internal void RegisterHook(HookInstance hook) => Hooks.Add(hook);

        /// <summary>
        /// Clears the hook.
        /// </summary>
        internal void ClearHooks() => Hooks.Clear();

        #endregion

        #region Plugins

        /// <summary>
        /// Clears the registered plugins.
        /// </summary>
        internal void ClearPlugins() => Plugins.LogPlugins.Clear();//_webDriverConfigurationPlugin = null;

        /// <summary>
        /// Registers the Log plugin.
        /// </summary>
        /// <param name="logPlugin">The plugin.</param>
        internal void RegisterLogPlugin(ILogPlugin logPlugin) => Plugins.LogPlugins.Add(logPlugin);

        ///// <summary>
        ///// Registers the web driver configuration plugin.
        ///// </summary>
        ///// <param name="webDriverConfigurationPlugin">The web driver configuration plugin.</param>
        //internal void RegisterWebDriverConfigurationPlugin(IWebDriverConfigurationPlugin webDriverConfigurationPlugin)
        //{
        //    _webDriverConfigurationPlugin = webDriverConfigurationPlugin;
        //}

        ///// <summary>
        ///// Gets the web driver configuration plugin.
        ///// </summary>
        ///// <returns><see cref="IWebDriverConfigurationPlugin"/>.</returns>
        //internal IWebDriverConfigurationPlugin GetWebDriverConfigurationPlugin()
        //{
        //    return _webDriverConfigurationPlugin;
        //}

        #endregion

        #endregion
    }
}
