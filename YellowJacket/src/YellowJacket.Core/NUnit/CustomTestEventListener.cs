using System;
using NUnit.Engine;

namespace YellowJacket.Core.NUnit
{
    public class CustomTestEventListener: ITestEventListener
    {
        public void OnTestEvent(string report)
        {
            Console.WriteLine(report);
        }
    }
}
