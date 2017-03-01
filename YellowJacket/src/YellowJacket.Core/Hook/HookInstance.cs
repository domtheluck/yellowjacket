using System;

namespace YellowJacket.Core.Hook
{
    public class HookInstance
    {
        #region Properties

        public IHook Instance { get; set; }

        public int Priority { get; set; }

        #endregion
    }
}
