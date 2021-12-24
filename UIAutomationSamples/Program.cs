using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIAutomationSamples
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var automationElementTree = new AutomationElementTree();
            var element = automationElementTree.ListAutomationElementTree("Calculator");
            Console.WriteLine(element);
            Console.ReadLine();
        }
    }
}
