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
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Newtonsoft.Json;
using YellowJacket.Api.Extensions;
using YellowJacket.Models;

namespace YellowJacket.Api.Processors
{
    /// <inheritdoc />
    /// <summary>
    /// Used to handle Job Instance api request.
    /// </summary>
    /// <seealso cref="BaseProcessor" />
    public class JobInstanceProcessor : BaseProcessor
    {
        #region Private Members

        private readonly HttpClient _httpClient;

        #endregion

        #region Constructors

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="JobInstanceProcessor" /> class.
        /// </summary>
        /// <param name="httpClient">The HTTP client.</param>
        internal JobInstanceProcessor(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets the first job instance available.
        /// </summary>
        /// <param name="agentId">The agent identifier.</param>
        /// <returns><see cref="JobInstanceModel"/>.</returns>
        public async Task<JobInstanceModel> GetFirstAvailable(string agentId)
        {
            try
            {
                HttpResponseMessage httpResponseMessage =
                    await _httpClient.SendRequestAsync(HttpMethod.Get, $"/api/v1/jobinstance/firstAvailable/{agentId}", null);

                return
                    JsonConvert.DeserializeObject<JobInstanceModel>(await httpResponseMessage.Content.ReadAsStringAsync());
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }

            return null;
        }

        /// <summary>
        /// Updates the job instance.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns><see cref="AgentModel"/>.</returns>
        public async Task<JobInstanceModel> Update(JobInstanceModel model)
        {
            try
            {
                HttpResponseMessage httpResponseMessage =
                    await _httpClient.SendRequestAsync(HttpMethod.Put, $"/api/v1/jobinstance/{model.Id}", model);

                return
                    JsonConvert.DeserializeObject<JobInstanceModel>(await httpResponseMessage.Content.ReadAsStringAsync());
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
