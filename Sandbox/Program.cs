using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sandbox
{

    class Program
    {
        static void Main(string[] args)
        {  
            Node<int> root = new Node<int>(4);
            root.TreeAdd(new Node<int>(2));
            root.TreeAdd(new Node<int>(5));
            root.TreeAdd(new Node<int>(1));
            root.TreeAdd(new Node<int>(3));

            root.PrintTree();

            root.ChangeMode(Node<int>.DatastructureMode.List).PrintList();

            Console.WriteLine();

        }
    }
}
