using System;
using System.Collections.Generic;
using System.Linq;
using YellowJacket.Core.Hook;

namespace YellowJacket.Core
{
    /// <summary>
    /// Execution context.
    /// </summary>
    public sealed class Context
    {
        #region Private Members

        private static readonly Lazy<Context> _context =
            new Lazy<Context>(() => new Context());

        private static readonly List<HookInstance> _hookInstances = new List<HookInstance>();

        #endregion

        #region Properties

        /// <summary>
        /// Gets the current context.
        /// </summary>
        /// <value>
        /// The current context.
        /// </value>
        public static Context CurrentContext => _context.Value;

        #endregion

        #region Constructors

        /// <summary>
        /// Prevents a default instance of the <see cref="Context"/> class from being created.
        /// </summary>
        private Context()
        {

        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Registers the hook in the context.
        /// </summary>
        /// <param name="hook">The hook to register.</param>
        public void RegisterHook(HookInstance hook)
        {
            _hookInstances.Add(hook);
        }

        /// <summary>
        /// Cleans the hook.
        /// </summary>
        public void CleanHook()
        {
            _hookInstances.Clear();
        }

        /// <summary>
        /// Gets the hooks.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<HookInstance> GetHooks()
        {
            return _hookInstances.OrderBy(x => x.Priority).ToList();
        }

        #endregion
    }
}
