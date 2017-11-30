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
using System.Net.Http;
using YellowJacket.Api.Processors;

namespace YellowJacket.Api
{
    /// <summary>
    /// Yellow Jacker API client.
    /// </summary>
    public class ApiClient
    {
        #region Constants

        #endregion

        #region Private Members

        #endregion

        #region Properties
        public AgentProcessor AgentProcessor { get; }

        /// <summary>
        /// Gets the job instance processor.
        /// </summary>
        /// <value>
        /// The job instance processor.
        /// </value>
        public JobInstanceProcessor JobInstanceProcessor { get; }

        /// <summary>
        /// Gets the package processor.
        /// </summary>
        /// <value>
        /// The package processor.
        /// </value>
        public PackageProcessor PackageProcessor { get; }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the API base URI.
        /// </summary>
        /// <value>
        /// The API base URI.
        /// </value>
        public string ApiBaseUri { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiClient" /> class.
        /// </summary>
        /// <param name="apiBaseUri">The api base URI.</param>
        public ApiClient(string apiBaseUri)
        {
            HttpClient httpClient = new HttpClient { BaseAddress = new Uri(apiBaseUri) };

            ApiBaseUri = apiBaseUri;

            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");

            AgentProcessor = new AgentProcessor(httpClient);

            JobInstanceProcessor = new JobInstanceProcessor(httpClient);

            PackageProcessor = new PackageProcessor(httpClient);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Handles the error.
        /// </summary>
        /// <param name="ex">The ex.</param>
        private void HandleError(Exception ex)
        {
            Console.WriteLine(ex);
        }

        #endregion
    }
}
