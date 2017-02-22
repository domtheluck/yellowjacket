using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using YellowJacket.Core.Hook;

namespace YellowJacket.Core.Helpers
{
    /// <summary>
    /// Helper class used to find specific type in assembly.
    /// </summary>
    public class TypeLocatorHelper
    {
        /// <summary>
        /// Gets the hook types.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        /// <returns><see cref="List{Type}"/>.</returns>
        public List<Type> GetHookTypes(Assembly assembly)
        {
            return
                assembly.GetTypes()
                .Where(t => t.GetInterfaces().Contains(typeof(IHook)) &&
                    t.IsClass && 
                    t.GetConstructor(Type.EmptyTypes) != null)
                .ToList();
        }
    }
}
