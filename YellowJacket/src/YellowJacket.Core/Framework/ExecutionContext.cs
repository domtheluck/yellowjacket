using System;
using System.Collections.Generic;
using System.Linq;
using YellowJacket.Core.Hook;
using YellowJacket.Core.Logging;

namespace YellowJacket.Core.Framework
{
    /// <summary>
    /// Execution context.
    /// </summary>
    public sealed class ExecutionContext
    {
        #region Private Members

        private static readonly Lazy<ExecutionContext> _context =
            new Lazy<ExecutionContext>(() => new ExecutionContext());

        private static readonly List<HookInstance> _hookInstances =
            new List<HookInstance>();

        private static readonly List<ILogger> _loggers =
            new List<ILogger>();

        #endregion

        #region Properties

        /// <summary>
        /// Gets the current context.
        /// </summary>
        /// <value>
        /// The current context.
        /// </value>
        public static ExecutionContext Current => _context.Value;

        #endregion

        #region Constructors

        /// <summary>
        /// Prevents a default instance of the <see cref="ExecutionContext"/> class from being created.
        /// </summary>
        private ExecutionContext() { }

        #endregion

        #region Public Methods

        /// <summary>
        /// Logs the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="isNewLine">if set to <c>true</c> log as a new line; otherwise, <c>false</c>.</param>
        public static void Log(string value, bool isNewLine = true)
        {
            _loggers.ForEach(x =>
            {
                if (isNewLine)
                    x.WriteLine(value);
                else
                    x.Write(value);
            });
        }

        /// <summary>
        /// Registers the hook in the context.
        /// </summary>
        /// <param name="hook">The hook to register.</param>
        internal void RegisterHook(HookInstance hook)
        {
            _hookInstances.Add(hook);
        }

        /// <summary>
        /// Clears the hook.
        /// </summary>
        internal void ClearHooks()
        {
            _hookInstances.Clear();
        }

        /// <summary>
        /// Registers the logger.
        /// </summary>
        /// <param name="logger">The logger.</param>
        internal void RegisterLogger(ILogger logger)
        {
            _loggers.Add(logger);
        }

        /// <summary>
        /// Clears the loggers.
        /// </summary>
        internal void ClearLoggers()
        {
            _loggers.Clear();
        }

        /// <summary>
        /// Gets the hooks.
        /// </summary>
        /// <returns></returns>
        internal IEnumerable<HookInstance> GetHooks()
        {
            return _hookInstances.OrderBy(x => x.Priority).ToList();
        }

        #endregion
    }
}
