using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using YellowJacket.Models;

namespace YellowJacket.Api
{
    public class ApiClient
    {
        #region Private Members

        private readonly HttpClient _httpClient;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiClient"/> class.
        /// </summary>
        public ApiClient()
        {
            _httpClient = new HttpClient { BaseAddress = new Uri("http://localhost:85") }; // TODO: need to get it from the App.config

        }

        #endregion

        #region Public Methods

        public async Task RegisterAgent(AgentModel model)
        {

        }

        public async Task<IEnumerable<AgentModel>> GetAgents()
        {
            HttpRequestMessage httpRequestMessage =
                new HttpRequestMessage(HttpMethod.Get, "/api/agent") { Method = HttpMethod.Get };

            HttpResponseMessage response = await _httpClient.SendAsync(httpRequestMessage);

            List<AgentModel> result =
                JsonConvert.DeserializeObject<List<AgentModel>>(await response.Content.ReadAsStringAsync());

            return result;
        }

        #endregion

        #region Private Methods

        #endregion
    }
}
