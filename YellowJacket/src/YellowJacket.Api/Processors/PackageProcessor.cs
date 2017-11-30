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
using System.Threading.Tasks;
using Newtonsoft.Json;
using YellowJacket.Api.Extensions;
using YellowJacket.Models;

namespace YellowJacket.Api.Processors
{
    public class PackageProcessor : BaseProcessor
    {
        #region Private Members

        private readonly HttpClient _httpClient;

        #endregion

        #region Constructors

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="Processors.PackageProcessor" /> class.
        /// </summary>
        /// <param name="httpClient">The HTTP client.</param>
        internal PackageProcessor(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        #endregion

        #region Public Methods

        public async Task<byte[]> Download(string packageName)
        {
            byte[] result = null;

            try
            {
                HttpResponseMessage httpResponseMessage =
                    await _httpClient.SendRequestAsync(HttpMethod.Put, $"/api/v1/package/{packageName}/download", null);

                result = await httpResponseMessage.Content.ReadAsByteArrayAsync();
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }

            return result;
        }

        /// <summary>
        /// Gets the specified agent.
        /// </summary>
        /// <param name="packageName">Name of the package.</param>
        /// <returns>
        ///   <see cref="PackageModel" />.
        /// </returns>
        public async Task<PackageModel> Get(string packageName)
        {
            try
            {
                HttpResponseMessage httpResponseMessage =
                    await _httpClient.SendRequestAsync(HttpMethod.Get, $"/package/{packageName}", null);

                return
                    JsonConvert.DeserializeObject<PackageModel>(await httpResponseMessage.Content.ReadAsStringAsync());
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }

            return null;
        }

        #endregion
    }
}
