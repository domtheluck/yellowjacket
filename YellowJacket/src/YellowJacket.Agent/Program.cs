using Topshelf;

namespace YellowJacket.Agent
{
    class Program
    {
        static void Main(string[] args)
        {
            HostFactory.Run(x =>
            {
                x.Service<AgentService>(s =>
                {
                    s.ConstructUsing(name => new AgentService());

                    s.WhenStarted(t => t.Start());
                    s.WhenStopped(t => t.Stop());
                });
                x.RunAsLocalSystem();

                x.SetDescription("YellowJacket Agent");
                x.SetDisplayName("YellowJacket.Agent");
                x.SetServiceName("YellowJacket.Agent");
            });
        }
    }
}
