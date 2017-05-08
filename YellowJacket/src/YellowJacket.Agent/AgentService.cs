using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using YellowJacket.Api;
using YellowJacket.Models;

namespace YellowJacket.Agent
{
    public class AgentService
    {
        //readonly Timer _timer;

        #region Private Members

        private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
        private readonly CancellationToken _cancellationToken;

        private bool _isRegistered;

        #endregion

        public AgentService()
        {
            //_timer = new Timer(1000) { AutoReset = true };
            //_timer.Elapsed +=
            //    (sender, eventArgs) => Run(); //Console.WriteLine("It is {0} and all is well", DateTime.Now);

            _cancellationToken = _cancellationTokenSource.Token;
        }

        #region Public Methods

        public void Start()
        {
            //_cancellationTokenSource.CancelAfter(new TimeSpan(0, 0, 0, 60));

            //_timer.Start();
            Task mainTask = Task.Run(async () =>
            {
                await UpdateHeartbeat(new TimeSpan(0, 0, 30), _cancellationToken);
            }, _cancellationToken);

            ApiClient client = new ApiClient();

            IEnumerable<AgentModel> agents = new List<AgentModel>();

            Task test = Task.Run(async () =>
            {
                agents = await client.GetAgents();

                Console.WriteLine();

            }, _cancellationToken);
        }

        public void Stop()
        {
            //_timer.Stop(); 

            _cancellationTokenSource.Cancel();
        }

        #endregion

        #region Private Methods

        public async Task UpdateHeartbeat(TimeSpan interval, CancellationToken cancellationToken)
        {
            while (true)
            {
                if (!_isRegistered)
                    await Register();
            }
        }

        private async Task Register()
        {
            
        }

        #endregion
    }
}
