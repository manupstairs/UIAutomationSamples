using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation;
using System.Xml.Linq;

namespace UIAutomationSamples
{
    

    internal class AutomationElementTree
    {
        internal XElement ListAutomationElementTree(string windowName)
        {
            IntPtr hWnd = WindowsInterop.FindWindow(null, windowName);
            if (hWnd != IntPtr.Zero)
            {
                var rootElement = AutomationElement.FromHandle(hWnd);
                if (rootElement != null)
                {
                    XElement rootNode = new XElement("Root");
                    rootNode.SetAttributeValue("Name", rootElement.Current.Name);
                    rootNode.SetAttributeValue("LocalizedControlType", rootElement.Current.LocalizedControlType);
                    rootNode.SetAttributeValue("AutomationId", rootElement.Current.AutomationId);
                    rootNode.SetAttributeValue("FrameworkId", rootElement.Current.FrameworkId);
                    WalkControlElements(rootElement, rootNode);
                    return rootNode;
                }
            }

            return null;
        }

        private void WalkControlElements(AutomationElement rootElement, XElement treeNode)
        {
            // Conditions for the basic views of the subtree (content, control, and raw) 
            // are available as fields of TreeWalker, and one of these is used in the 
            // following code.
            AutomationElement elementNode = TreeWalker.ControlViewWalker.GetFirstChild(rootElement);

            while (elementNode != null)
            {
                XElement childTreeNode = new XElement("Element");
                childTreeNode.SetAttributeValue("Name", elementNode.Current.Name);
                childTreeNode.SetAttributeValue("LocalizedControlType", elementNode.Current.LocalizedControlType);
                childTreeNode.SetAttributeValue("AutomationId", elementNode.Current.AutomationId);
                childTreeNode.SetAttributeValue("FrameworkId", elementNode.Current.FrameworkId);
                WalkControlElements(elementNode, childTreeNode);
                treeNode.Add(childTreeNode);
                elementNode = TreeWalker.ControlViewWalker.GetNextSibling(elementNode);
            }
        }
    }
}
