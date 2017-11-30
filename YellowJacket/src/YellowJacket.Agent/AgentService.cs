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
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using YellowJacket.Api;
using YellowJacket.Common.Enums;
using YellowJacket.Common.Helpers;
using YellowJacket.Models;

namespace YellowJacket.Agent
{
    internal class AgentService
    {
        #region Private Members

        private readonly CancellationTokenSource _mainCancellationTokenSource = new CancellationTokenSource();
        private readonly CancellationToken _mainCancellationToken;

        private bool _isActive;

        private bool _isRegistered;
        private readonly ApiClient _apiClient;

        private AgentModel _agent;

        private string _apiBaseUrl;
        private string _name;

        private readonly string _packageTestPath;

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="AgentService"/> class.
        /// </summary>
        public AgentService()
        {
            ReadConfiguration();

            _mainCancellationToken = _mainCancellationTokenSource.Token;

            _apiClient = new ApiClient(_apiBaseUrl);

            _agent = new AgentModel
            {
                Name = _name,
                Id = Environment.MachineName,
                Status = AgentStatus.Idle.ToString()
            };

            string packageRootPath = Path.Combine(Path.GetTempPath(), $@"YellowJacket\{_agent.Id}\Packages");

            _packageTestPath = Path.Combine(packageRootPath, "Test");
        }

        #region Public Methods

        /// <summary>
        /// Called when the service start.
        /// </summary>
        public void Start()
        {
            while (!_isRegistered)
            {
                TryToRegister();

                Thread.Sleep(5000); // TODO: need to be dynamic
            }

            Task.Run(async () =>
            {
                await UpdateHeartbeat(new TimeSpan(0, 0, 15), _mainCancellationToken); // TODO: need to be dynamic
            }, _mainCancellationToken);

            Task.Run(async () =>
            {
                await Process(_mainCancellationToken);
            }, _mainCancellationToken);

            Thread.Sleep(5000);  // TODO: need to be dynamic
        }

        /// <summary>
        /// Call when the service stop.
        /// </summary>
        public void Stop()
        {
            _mainCancellationTokenSource.Cancel();

            _isRegistered = false;
            _isActive = false;
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

                AgentModel model = await _apiClient.AgentProcessor.Update(_agent);

                if (model != null)
                    _agent = model;

                await Task.Delay(interval, cancellationToken);

                if (_mainCancellationToken.IsCancellationRequested)
                    break;
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Processes the job instance.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns><see cref="Task"/>.</returns>
        private async Task Process(CancellationToken cancellationToken)
        {
            while (_isActive)
            {
                JobInstanceModel jobInstance =
                    await _apiClient.JobInstanceProcessor.GetFirstAvailable(_agent.Id);

                if (jobInstance != null)
                {
                    PackageModel package = await _apiClient.PackageProcessor.Get(jobInstance.Job.PackageName);

                    if (package == null)
                        throw new Exception(""); // TODO: to modify

                    if (!IsPackageValid(package))
                        DownloadTestPackage(package);

                    if (!IsPackageValid(package))
                        throw new Exception(""); // TODO: to modify

                    // TODO: Execute test

                }

                await Task.Delay(1000, cancellationToken); //  TODO: the delay value need to be dynamic
            }
        }

        /// <summary>
        /// Updates the agent status.
        /// </summary>
        /// <param name="agentStatus">The agent status.</param>
        private void UpdateAgentStatus(AgentStatus agentStatus)
        {
            _agent.Status = agentStatus.ToString();

            Task task = Task.Run(async () =>
            {
                await _apiClient.AgentProcessor.Update(_agent);
            }, _mainCancellationToken);

            task.Wait(_mainCancellationToken);
        }

        /// <summary>
        /// Determines whether the specified package is valid or not.
        /// </summary>
        /// <param name="package">The package.</param>
        /// <returns>
        ///   <c>true</c> if the specified package is valid; otherwise, <c>false</c>.
        /// </returns>
        private bool IsPackageValid(PackageModel package)
        {
            string packageConfigurationFileFullName =
                Path.Combine(_packageTestPath, $"{package.Name}.json");

            string packageFileFullName =
                Path.Combine(_packageTestPath, $"{package.Name}.zip");

            if (!File.Exists(packageConfigurationFileFullName) || !File.Exists(packageFileFullName))
                return false;

            return CryptographyHelper.GetMd5HashFromFile(packageFileFullName).Equals(package.Hash);
        }

        /// <summary>
        /// Downloads the test package.
        /// </summary>
        /// <param name="package">The package.</param>
        /// <exception cref="Exception"></exception>
        private void DownloadTestPackage(PackageModel package)
        {
            UpdateAgentStatus(AgentStatus.DownloadPackage);

            string packageConfigurationFileFullName =
                Path.Combine(_packageTestPath, $"{package.Name}.json");

            string packageFileFullName =
                Path.Combine(_packageTestPath, $"{package.Name}.zip");

            if (File.Exists(packageConfigurationFileFullName))
                File.Delete(packageConfigurationFileFullName);

            if (File.Exists(packageFileFullName))
                File.Delete(packageFileFullName);

            File.WriteAllText(
                packageConfigurationFileFullName,
                JsonConvert.SerializeObject(package));

            byte[] result = null;

            Task downloadPackageTask = Task.Run(async () =>
            {
                result = await _apiClient.PackageProcessor.Download(package.Name);
            }, _mainCancellationToken);

            downloadPackageTask.Wait(_mainCancellationToken);

            if (result == null)
            {
                throw new IOException($"Unable to download the package {package.Name} at {_apiClient.ApiBaseUri}");
            }

            File.WriteAllBytes(packageFileFullName, result);

            UpdateAgentStatus(AgentStatus.Idle);
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
                model = await _apiClient.AgentProcessor.Register(_agent);
            }, _mainCancellationToken);

            registerTask.Wait(_mainCancellationToken);
            _isRegistered = true;
            _isActive = true;

            _agent = model ?? throw new NullReferenceException();
        }

        /// <summary>
        /// Reads the configuration.
        /// </summary>
        /// <exception cref="ConfigurationErrorsException">The apiBaseUri configuration key is required.</exception>
        private void ReadConfiguration()
        {
            if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["ApiBaseUri"]))
                throw new ConfigurationErrorsException("The ApiBaseUri configuration key is required");

            _apiBaseUrl = ConfigurationManager.AppSettings["ApiBaseUri"];

            if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["Name"]))
                throw new ConfigurationErrorsException("The Name configuration key is required");

            _name = ConfigurationManager.AppSettings["Name"];
        }

        #endregion
    }
}
