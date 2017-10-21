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

namespace SimpleFeature
{
    internal class Calculator
    {
        #region Private Members

        private static Calculator _instance;

        private readonly List<int> _numbers;

        private int _result;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>
        /// The instance.
        /// </value>
        public static Calculator Instance => _instance ?? (_instance = new Calculator());

        #endregion

        #region Constructors

        /// <summary>
        /// Prevents a default instance of the <see cref="Calculator"/> class from being created.
        /// </summary>
        private Calculator()
        {
            _numbers = new List<int>();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Resets the calculator to initial state.
        /// </summary>
        public void Reset()
        {
            _result = 0;

            _numbers.Clear();
        }

        /// <summary>
        /// Enter a number in the calculator.
        /// </summary>
        /// <param name="number">The number.</param>
        public void EnterNumber(int number)
        {
            _numbers.Add(number);
        }

        /// <summary>
        /// Adds the numbers enterred.
        /// </summary>
        public void ComputeAdd()
        {
            _result = 0;

            _numbers.ForEach(x => _result += x);
        }

        public int GetResult()
        {
            return _result;
        }

        #endregion
    }
}
