using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using YellowJacket.Models;
using System.Text;
using YellowJacket.Models.Agent;

namespace YellowJacket.Api
{
    /// <summary>
    /// Yellow Jacker API client.
    /// </summary>
    public class ApiClient
    {
        #region Private Members

        private readonly HttpClient _httpClient;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiClient" /> class.
        /// </summary>
        /// <param name="baseUri">The base URI.</param>
        public ApiClient(string baseUri)
        {
            _httpClient = new HttpClient { BaseAddress = new Uri(baseUri) };

            _httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
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
                HttpRequestMessage httpRequestMessage =
                    new HttpRequestMessage(HttpMethod.Put, $"/api/v1/agent/{model.Id}")
                    {
                        Content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")
                    };

                HttpResponseMessage response = await _httpClient.SendAsync(httpRequestMessage);

                AgentModel result = JsonConvert.DeserializeObject<AgentModel>(await response.Content.ReadAsStringAsync());

                return result;
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
                HttpRequestMessage httpRequestMessage =
                    new HttpRequestMessage(HttpMethod.Post, "/api/v1/agent")
                    {
                        Content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")
                    };

                HttpResponseMessage response = await _httpClient.SendAsync(httpRequestMessage);

                AgentModel result = JsonConvert.DeserializeObject<AgentModel>(await response.Content.ReadAsStringAsync());

                return result;
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
                HttpRequestMessage httpRequestMessage =
                    new HttpRequestMessage(HttpMethod.Get, "/api/v1/agent") { Method = HttpMethod.Get };

                HttpResponseMessage response = await _httpClient.SendAsync(httpRequestMessage);

                List<AgentModel> result =
                    JsonConvert.DeserializeObject<List<AgentModel>>(await response.Content.ReadAsStringAsync());

                return result;
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }

            return null;
        }

        #endregion

        #region Private Methods

        private void HandleError(Exception ex)
        {

        }

        #endregion
    }
}
