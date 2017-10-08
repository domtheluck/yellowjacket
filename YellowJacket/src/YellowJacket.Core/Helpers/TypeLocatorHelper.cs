// ***********************************************************************
// Copyright (c) 2017 Dominik Lachance
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// ***********************************************************************

using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NUnit.Framework;
using YellowJacket.Core.Interfaces;

namespace YellowJacket.Core.Helpers
{
    /// <summary>
    /// Helper class used to find specific type in assembly.
    /// </summary>
    public class TypeLocatorHelper
    {
        /// <summary>
        /// Gets thew implemented types.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        /// <returns><see cref="List{Type}"/>.</returns>
        public List<Type> GetImplementedTypes<T>(Assembly assembly)
        {
            return
                assembly.GetTypes()
                    .Where(t => t.GetInterfaces().Contains(typeof(T)) &&
                                t.IsClass &&
                                t.GetConstructor(Type.EmptyTypes) != null)
                    .ToList();
        }

        /// <summary>
        /// Gets the all feature types from the specifed assembly.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        /// <returns><see cref="List{Type}"/></returns>
        public List<Type> GetFeatureTypes(Assembly assembly)
        {
            return assembly.GetTypes()
                .Where(t => t.GetCustomAttributes(typeof(GeneratedCodeAttribute))
                .Any())
                .Where(type => ((GeneratedCodeAttribute) type.GetCustomAttributes(typeof(GeneratedCodeAttribute))
                .FirstOrDefault())?.Tool == "TechTalk.SpecFlow").ToList();
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
            // TODO: to rewrite

            List<Type> types =
                assembly.GetTypes()
                    .Where(t => t.GetCustomAttributes(typeof(DescriptionAttribute)).Any()).ToList();

            if (!types.Any())
                throw new Exception($"Cannot find any feature in assembly {assembly.FullName}");

            foreach (Type type in types)
            {
                IList<CustomAttributeData> customAttributeData =
                    type.GetCustomAttributesData()
                    .Where(x => x.AttributeType == typeof(DescriptionAttribute)).ToList();

                if (customAttributeData
                    .SelectMany(item => item.ConstructorArguments)
                    .Any(
                        constructorArgument =>
                            string.Equals(constructorArgument.Value.ToString(), feature,
                                StringComparison.InvariantCultureIgnoreCase)))
                    return type;
            }

            throw new Exception($"Cannot find the feature {feature} in the assembly {assembly.FullName}");
        }
    }
}
