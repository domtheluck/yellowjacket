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

using System.Collections.Generic;
using YellowJacket.Core.Hook;
using YellowJacket.Core.Interfaces;

namespace YellowJacket.Core.Infrastructure
{
    // TODO: Need to validate if we need to keep this class or not.
    
    /// <summary>
    /// Contains all the settings.
    /// </summary>
    internal class Settings
    {
        #region Properties

        /// <summary>
        /// Gets or sets the hooks.
        /// </summary>
        /// <value>
        /// The hooks.
        /// </value>
        internal List<HookInstance> Hooks { get; set; }

        /// <summary>
        /// Gets or sets the loggers.
        /// </summary>
        /// <value>
        /// The loggers.
        /// </value>
        internal List<ILogger> Loggers { get; set; }

        #endregion
    }
}
