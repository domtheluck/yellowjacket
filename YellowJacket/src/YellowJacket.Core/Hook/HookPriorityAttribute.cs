using System;

namespace YellowJacket.Core.Hook
{
    [AttributeUsage(AttributeTargets.Class)]
    public class HookPriorityAttribute : Attribute
    {
        public int Priority { get; } = 0;

        public HookPriorityAttribute(int priority)
        {
            Priority = priority;
        }
    }
}
