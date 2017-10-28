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
using System.Configuration;
using System.Threading;
using System.Threading.Tasks;
using YellowJacket.Agent.Enums;
using YellowJacket.Api;
using YellowJacket.Models;

namespace YellowJacket.Agent
{
    internal class AgentService
    {
        #region Private Members

        private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
        private readonly CancellationToken _cancellationToken;

        private bool _isRegistered;
        private readonly ApiClient _apiClient;

        private AgentModel _agent;

        private string _apiBaseUrl;
        private string _name;

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="AgentService"/> class.
        /// </summary>
        public AgentService()
        {
            ReadConfiguration();

            _cancellationToken = _cancellationTokenSource.Token;

            _apiClient = new ApiClient(_apiBaseUrl);

            _agent = new AgentModel
            {
                Name = _name,
                Id = Environment.MachineName,
                Status = Status.Idle.ToString()
            };
        }

        #region Public Methods

        /// <summary>
        /// Called when the service start.
        /// </summary>
        public void Start()
        {
            while (!_isRegistered)
                TryToRegister();

            Console.WriteLine("Agent registered");

            Task.Run(async () =>
            {
                await UpdateHeartbeat(new TimeSpan(0, 0, 15), _cancellationToken); // TODO: need to be dynamic
            }, _cancellationToken);
        }

        /// <summary>
        /// Call when the service stop.
        /// </summary>
        public void Stop()
        {
            _cancellationTokenSource.Cancel();

            _isRegistered = false;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Updates the heartbeat.
        /// </summary>
        /// <param name="interval">The interval.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task UpdateHeartbeat(TimeSpan interval, CancellationToken cancellationToken)
        {
            while (true)
            {
                _agent.LastUpdateOn = DateTime.Now;

                AgentModel model = await _apiClient.AgentProcessor.UpdateAgent(_agent);

                if (model != null)
                    _agent = model;

                await Task.Delay(interval, cancellationToken);

                if (_cancellationToken.IsCancellationRequested)
                    break;
            }
        }

        /// <summary>
        /// Try to register the agent.
        /// </summary>
        private void TryToRegister()
        {
            DateTime now = DateTime.Now;

            _agent.RegisteredOn = now;
            _agent.LastUpdateOn = now;

            AgentModel model = new AgentModel();

            Task registerTask = Task.Run(async () =>
            {
                model = await _apiClient.AgentProcessor.RegisterAgent(_agent);
            }, _cancellationToken);

            registerTask.Wait(_cancellationToken);
            _isRegistered = true;

            _agent = model ?? throw new NullReferenceException();
        }

        /// <summary>
        /// Reads the configuration.
        /// </summary>
        /// <exception cref="ConfigurationErrorsException">The apiBaseUri configuration key is required.</exception>
        private void ReadConfiguration()
        {
            if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["ApiBaseUri"]))
                throw new ConfigurationErrorsException("The apiBaseUri configuration key is required");

            _apiBaseUrl = ConfigurationManager.AppSettings["ApiBaseUri"];

            if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["Name"]))
                throw new ConfigurationErrorsException("The Name configuration key is required");

            _name = ConfigurationManager.AppSettings["Name"];
        }

        #endregion
    }
}
