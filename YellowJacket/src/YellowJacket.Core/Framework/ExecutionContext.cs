using System;
using System.Collections.Generic;
using System.Linq;
using YellowJacket.Core.Helpers;
using YellowJacket.Core.Hook;
using YellowJacket.Core.Infrastructure;
using YellowJacket.Core.Interfaces;

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

        #region Public MethodsFile.AppendAllLines(_path, new List<string> {value});

        /// <summary>
        /// Logs the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="isNewLine">if set to <c>true</c> log as a new line; otherwise, <c>false</c>.</param>
        public void Log(string value, bool isNewLine = true)
        {
            _loggers.ForEach(x =>
            {
                if (isNewLine)
                    x.WriteLine(value);
                else
                    x.Write(value);
            });
        }

        #endregion

        #region Internal Methods

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
        /// Gets the hooks ordered by priority.
        /// </summary>
        /// <returns></returns>
        internal List<HookInstance> GetHooks()
        {
            return _hookInstances.OrderBy(x => x.Priority).ToList();
        }

        /// <summary>
        /// Exports the current configuration.
        /// </summary>
        /// <param name="path">The path.</param>
        internal void ExportConfiguration(string path)
        {
            Settings settings = new Settings
            {
                Hooks = _hookInstances,
                Loggers = _loggers
            };

            SerializationHelper.WriteToBinaryFile(path, settings);
        }

        /// <summary>
        /// Imports the selected configuration.
        /// </summary>
        /// <param name="path">The path.</param>
        internal void ImportConfiguration(string path)
        {
            Settings settings = SerializationHelper.ReadFromBinaryFile<Settings>(path);

            _hookInstances.AddRange(settings.Hooks);
            _loggers.AddRange(settings.Loggers);
        }

        #endregion
    }
}
