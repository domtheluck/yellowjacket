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
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using YellowJacket.Api.Extensions;
using YellowJacket.Models;

namespace YellowJacket.Api.Processors
{
    /// <inheritdoc />
    /// <summary>
    /// Used to handle Agent api request.
    /// </summary>
    /// <seealso cref="BaseProcessor" />
    public class AgentProcessor : BaseProcessor
    {
        #region Private Members

        private readonly HttpClient _httpClient;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AgentProcessor"/> class.
        /// </summary>
        /// <param name="httpClient">The HTTP client.</param>
        internal AgentProcessor(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Updates the agent.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns><see cref="AgentModel"/>.</returns>
        public async Task<AgentModel> UpdateAgent(AgentModel model)
        {
            try
            {
                HttpResponseMessage httpResponseMessage =
                    await _httpClient.SendRequestAsync(HttpMethod.Put, $"/api/v1/agent/{model.Id}", model);

                return
                    JsonConvert.DeserializeObject<AgentModel>(await httpResponseMessage.Content.ReadAsStringAsync());
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }

            return null;
        }

        /// <summary>
        /// Registers the agent.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns><see cref="AgentModel"/>.</returns>
        public async Task<AgentModel> RegisterAgent(AgentModel model)
        {
            try
            {
                HttpResponseMessage httpResponseMessage =
                    await _httpClient.SendRequestAsync(HttpMethod.Post, "/agent", model);

                return
                    JsonConvert.DeserializeObject<AgentModel>(await httpResponseMessage.Content.ReadAsStringAsync());
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }

            return null;
        }

        /// <summary>
        /// Gets the agents.
        /// </summary>
        /// <returns><see cref="IEnumerable{AgentModel}"/>.</returns>
        public async Task<IEnumerable<AgentModel>> GetAgents()
        {
            try
            {
                HttpResponseMessage httpResponseMessage =
                    await _httpClient.SendRequestAsync(HttpMethod.Get, "/agent", null);

                return
                    JsonConvert.DeserializeObject<List<AgentModel>>(await httpResponseMessage.Content.ReadAsStringAsync());
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
