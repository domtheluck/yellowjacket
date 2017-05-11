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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace YellowJacket.Core.Helpers
{
    /// <summary>
    /// Enumerate the possible members of an enum.
    /// </summary>
    public class EnumDefinition
    {
        #region Properties

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public int Value { get; set; }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="EnumDefinition"/> class.
        /// </summary>
        internal EnumDefinition() { }

        #endregion Constructors
    }

    /// <summary>
    /// This class implements helper methods for enumeration.
    /// </summary>
    public class EnumHelper
    {
        /// <summary>
        /// Convert enum to enum.
        /// </summary>
        public T EnumToEnum<T, TU>(TU enumArg)
        {
            if (!typeof(T).IsEnum)
                throw new InvalidCastException("This method only takes enumerations.");

            if (!typeof(TU).IsEnum)
                throw new InvalidCastException("This method only takes enumerations.");

            return (T)Enum.ToObject(typeof(TU), enumArg);
        }

        /// <summary>
        /// Converts a definition list from an enum.
        /// </summary>
        /// <returns><see cref="List{EnumDefinition}"/> object.</returns>
        public List<EnumDefinition> GetDefinitionListFromEnum<T>()
        {
            if (!typeof(T).IsEnum)
                throw new InvalidCastException("This method only takes enumeration.");

            Type enumType = typeof(T);
            string[] names = Enum.GetNames(enumType);

            return names.Select(name => new EnumDefinition
            {
                Name = name,
                Value =
                Convert.ToInt32(Enum.Parse(enumType, name)),
                Description = GetEnumFieldDescription((Enum)Enum.Parse(typeof(T), name))
            }).ToList();
        }

        /// <summary>
        /// Reads the description attribute for the given enum field and returns it's string representation.
        /// </summary>
        public string GetEnumFieldDescription(Enum value)
        {
            FieldInfo fieldInfo = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes =
            (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

            foreach (DescriptionAttribute descriptionAttribute in attributes
                .Where(descriptionAttribute => descriptionAttribute.GetType() == typeof(DescriptionAttribute)))

                return descriptionAttribute.Description;

            return value.ToString();
        }

        /// <summary>
        /// Returns a strongly typed enumeration field for given enum type.
        /// </summary>
        public T GetEnumType<T>(int value)
        {
            return (T)Enum.Parse(typeof(T), value.ToString(CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Returns a strongly typed enumeration field for given enum type.
        /// </summary>
        public T GetEnumType<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value);
        }

        /// <summary>
        /// Returns an enum filed for the given enum description.
        /// </summary>
        public T GetEnumTypeFromDescription<T>(string description)
        {
            Type enumType = typeof(T);
            string[] names = Enum.GetNames(enumType);

            foreach (string name in names.Where(name => GetEnumFieldDescription((Enum)Enum.Parse(enumType, name)).Equals(description)))
                return (T)Enum.Parse(enumType, name);

            throw new ArgumentException("The string is not a description or value of the specified enum.");
        }

        /// <summary>
        /// Returns an integer value corresponding to the given enumeration field for a given enum type.
        /// </summary>
        public int GetEnumValue<T>(T enumField)
        {
            return Convert.ToInt32(enumField);
        }
        /// <summary>
        /// Gets list of enum fields for the given enum type.
        /// </summary>
        public List<string> GetFieldsFromEnum<T>()
        {
            return Enum.GetNames(typeof(T)).ToList();
        }
    }
}