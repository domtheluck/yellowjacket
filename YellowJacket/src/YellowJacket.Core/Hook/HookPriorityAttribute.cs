using System;

namespace YellowJacket.Core.Hook
{
    /// <summary>
    /// Uses for hook priority.
    /// </summary>
    /// <seealso cref="System.Attribute" />
    [AttributeUsage(AttributeTargets.Class)]
    public class HookPriorityAttribute : Attribute
    {
        #region Properties

        public int Priority { get; } = 0;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="HookPriorityAttribute"/> class.
        /// </summary>
        /// <param name="priority">The priority.</param>
        public HookPriorityAttribute(int priority)
        {
            Priority = priority;
        }

        #endregion
    }
}
