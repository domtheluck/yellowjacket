using System;

namespace YellowJacket.Core.Helpers
{
    /// <summary>
    /// Uses to create instance of a specific type.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ClassActivator<T>
    {
        /// <summary>
        /// Creates an instance of the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>Instance of type <see cref="T"/>.</returns>
        public static T CreateInstance(Type type)
        {
            return (T)Activator.CreateInstance(type);
        }
    }
}
