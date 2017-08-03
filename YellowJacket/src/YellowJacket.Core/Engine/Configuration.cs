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
using System.Collections.Generic;

namespace YellowJacket.Core.Engine
{
    /// <summary>
    /// Contains the execution configuration.
    /// </summary>
    public sealed class Configuration
    {
        #region Properties

        /// <summary>
        /// Gets or sets the test assembly full name.
        /// </summary>
        /// <value>
        /// The test assembly full name.
        /// </value>
        public string TestAssemblyFullName { get; set; }

        /// <summary>
        /// Gets or sets the plugin assemblies.
        /// </summary>
        /// <value>
        /// The plugin assemblies.
        /// </value>
        public List<string> PluginAssemblies { get; set; }

        /// <summary>
        /// Gets or sets the browser configuration.
        /// </summary>
        /// <value>
        /// The browser configuration.
        /// </value>
        public BrowserConfiguration BrowserConfiguration { get; set; }

        /// <summary>
        /// Gets or sets the features.
        /// </summary>
        /// <value>
        /// The features.
        /// </value>
        public List<string> Features { get; set; }

        #endregion

        #region Constructors

        public Configuration()
        {
            PluginAssemblies = new List<string>();
            Features = new List<string>();
        }

        #endregion
    }
}
