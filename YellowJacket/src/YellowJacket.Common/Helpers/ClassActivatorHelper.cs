﻿// ***********************************************************************
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

namespace YellowJacket.Common.Helpers
{
    /// <summary>
    /// Uses to create instance of a specific type.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ClassActivatorHelper<T>
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

        /// <summary>
        /// Uses to create instance of a specific type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="args">The arguments.</param>
        /// <returns></returns>
        public static T CreateInstance(Type type, params object[] args)
        {
            return (T)Activator.CreateInstance(type, args);
        }
    }
}
