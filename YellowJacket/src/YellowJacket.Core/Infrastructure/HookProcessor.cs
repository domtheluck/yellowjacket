using System.Collections.Generic;
using YellowJacket.Core.Hook;

namespace YellowJacket.Core.Infrastructure
{
    /// <summary>
    /// Uses to process the hooks.
    /// </summary>
    internal class HookProcessor
    {
        /// <summary>
        /// Processes the hooks.
        /// </summary>
        /// <param name="hookType">Type of the hook.</param>
        public static void Process(Enums.HookType hookType)
        {
            List<HookInstance> hooks = Framework.ExecutionContext.Current.GetHooks();

            hooks.ForEach(x =>
            {
                x.Instance.GetType().GetMethod(hookType.ToString()).Invoke(x, null);
            });
        }
    }
}

