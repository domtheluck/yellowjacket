using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;


namespace YellowJacket.Agent
{
    public class AgentService
    {
        readonly Timer _timer;
        public AgentService()
        {
            _timer = new Timer(1000) { AutoReset = true };
            _timer.Elapsed += (sender, eventArgs) => Console.WriteLine("It is {0} and all is well", DateTime.Now);
        }
        public void Start() { _timer.Start(); }
        public void Stop() { _timer.Stop(); }
    }
}
