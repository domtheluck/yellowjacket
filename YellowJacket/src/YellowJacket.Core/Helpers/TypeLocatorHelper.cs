using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NUnit.Framework;
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

        /// <summary>
        /// Gets the type of the feature.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        /// <param name="feature">The feature.</param>
        /// <returns></returns>
        /// <exception cref="Exception">.</exception>
        public Type GetFeatureType(Assembly assembly, string feature)
        {
            List<Type> types =
                assembly.GetTypes()
                    .Where(t => t.GetCustomAttributes(typeof(DescriptionAttribute)).Any()).ToList();

            if (!types.Any())
                throw new Exception($"Cannot find the feature {feature} in the assembly {assembly.FullName}");

            foreach (Type type in types)
            {
                IList<CustomAttributeData> customAttributeData =
                    type.GetCustomAttributesData().Where(x => x.AttributeType == typeof(DescriptionAttribute)).ToList();

                if (customAttributeData
                    .SelectMany(item => item.ConstructorArguments)
                    .Any(
                        constructorArgument =>
                            string.Equals(constructorArgument.Value.ToString(), feature,
                                StringComparison.InvariantCultureIgnoreCase)))
                    return type;
            }

            return null;
        }
    }
}
