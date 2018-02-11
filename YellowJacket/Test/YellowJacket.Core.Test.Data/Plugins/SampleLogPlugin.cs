using System;
using System.Collections.Generic;
using YellowJacket.Core.Plugins.Interfaces;

namespace YellowJacket.Core.Test.Data.Plugins
{
    internal class SampleLogPlugin : ILogPlugin
    {
        public void Write(string value)
        {
            
        }

        public void WriteAllLine(IEnumerable<string> lines)
        {
            
        }

        public void WriteLine(string line)
        {
            
        }
    }
}
