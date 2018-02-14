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
using System.IO;
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

        /// <summary>
        /// Gets the plugins.
        /// </summary>
        /// <value>
        /// The plugins.
        /// </value>
        public Plugins Plugins { get; } = new Plugins();

        #endregion

        #region Constructors

        /// <summary>
        /// Prevents a default instance of the <see cref="ExecutionContext"/> class from being created.
        /// </summary>
        private ExecutionContext() { }

        #endregion

        #region Events

        internal event EventHandler<EventArgs> BeforeExecution;
        internal event EventHandler<EventArgs> AfterExecution;

        internal event EventHandler<EventArgs> ExecutionBeforeFeature;
        internal event EventHandler<EventArgs> ExecutionAfterFeature;

        internal event EventHandler<EventArgs> ExecutionBeforeScenario;
        internal event EventHandler<EventArgs> ExecutionAfterScenario;

        internal event EventHandler<EventArgs> ExecutionBeforeStep;
        internal event EventHandler<EventArgs> ExecutionAfterStep;

        #endregion

        #region Internal Methods

        /// <summary>
        /// Fires the before execution event.
        /// </summary>
        internal void FireBeforeExecutionEvent()
        {
            BeforeExecution?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// Fires the after execution event.
        /// </summary>
        internal void FireAfterExecutionEvent()
        {
            AfterExecution?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// Fires the execution before feature event.
        /// </summary>
        internal void FireExecutionBeforeFeatureEvent()
        {
            ExecutionBeforeFeature?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// Fires the execution after feature event.
        /// </summary>
        internal void FireExecutionAfterFeatureEvent()
        {
            ExecutionAfterFeature?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// Fires the execution before scenario event.
        /// </summary>
        internal void FireExecutionBeforeScenarioEvent()
        {
            ExecutionBeforeScenario?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// Fires the execution after scenario event.
        /// </summary>
        internal void FireExecutionAfterScenarioEvent()
        {
            ExecutionAfterScenario?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// Fires the execution before step event.
        /// </summary>
        internal void FireExecutionBeforeStepEvent()
        {
            ExecutionBeforeStep?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// Fires the execution after step event.
        /// </summary>
        internal void FireExecutionAfterStepEvent()
        {
            ExecutionAfterStep?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// Registers the hook in the context.
        /// </summary>
        /// <param name="hook">The hook to register.</param>
        internal void RegisterHook(HookInstance hook) => Hooks.Add(hook);

        /// <summary>
        /// Clears the hook.
        /// </summary>
        internal void ClearHooks() => Hooks.Clear();

        /// <summary>
        /// Clears the registered plugins.
        /// </summary>
        internal void ClearPlugins()
        {
            Plugins.LogPlugins.Clear();
        }

        /// <summary>
        /// Registers the Log plugin.
        /// </summary>
        /// <param name="logPlugin">The plugin.</param>
        internal void RegisterLogPlugin(ILogPlugin logPlugin)
        {
            Plugins.LogPlugins.Add(logPlugin);
        }

        #endregion
    }
}
