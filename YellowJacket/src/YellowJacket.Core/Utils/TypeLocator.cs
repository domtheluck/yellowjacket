using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using YellowJacket.Core.Hook;

namespace YellowJacket.Core.Utils
{
    public class TypeLocator
    {
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
