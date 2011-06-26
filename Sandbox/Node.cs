using System;

namespace Sandbox
{
	// nodes are either lists or trees, but not both
	public class Node<T>
	{
		// actual a and b nodes
		private Node<T> _a;
		private Node<T> _b;

		// property to use nodes in 'Tree' style
		public Node<T> Small
		{
			get { return _a; }
			private set { _a = value; }
		}

		public Node<T> Large
		{
			get { return _b; }
			private set { _b = value; }
		}


		// property to use nodes in alternate 'Tree' style
		public Node<T> Left
		{
			get { return _a; }
			private set { _a = value; }
		}

		public Node<T> Right
		{
			get { return _b; }
			private set { _b = value; }
		}


		// property to use nodes in 'List' style
		public Node<T> Previous
		{
			get { return _a; }
			private set { _a = value; }
		}

		public Node<T> Next
		{
			get { return _b; }
			private set { _b = value; }
		}

		// The different modes this node datastructure supports
		public enum DatastructureMode
		{
			Tree,
			List
		}

		// the current mode of this data structure
		private DatastructureMode Mode { get; set; }

		public Node(T value)
		{
			Value = value;

			// default is tree mode
			Mode = DatastructureMode.Tree;    
		}

		public T Value { get; set; }

		//public void Add(Node<T> newNode)
		//{
			
		//}

		public void ListAdd(Node<T> newNode)
		{
			CheckMode(DatastructureMode.List, "ListAdd");

			newNode.Previous = this;
			newNode.Next = Next;

			Next.Previous = newNode;
			Next = newNode;
		}

		public void TreeAdd(Node<T> newNode)
		{
			CheckMode(DatastructureMode.Tree, "TreeAdd");

			if (Left == null)
			{
				Left = newNode;
			}
			else if (Right == null)
			{
				Right = newNode;
			}
			else if (Left.TreeDepth() <= Right.TreeDepth() + 1)
			{
				Left.TreeAdd(newNode);
			}
			else
			{
				Right.TreeAdd(newNode);
			}
		}

		public int TreeWeight()
		{
			CheckMode(DatastructureMode.Tree, "TreeWeight");

			int leftWeight = Left == null ? 0 : Left.TreeWeight();
			int rightWeight = Right == null ? 0 : Right.TreeWeight();

			return 1 + leftWeight + rightWeight;
		}

		public int TreeDepth()
		{
			CheckMode(DatastructureMode.Tree, "TreeDepth");

			int leftDepth = Left == null ? 0 : Left.TreeDepth();
			int rightDepth = Right == null ? 0 : Right.TreeDepth();

			return Math.Max(leftDepth, rightDepth);
		}

		public void PrintTree(int depth = 0)
		{
			CheckMode(DatastructureMode.Tree, "PrintTree");

			if (Right != null)
			{
				Right.PrintTree(depth + 1);
			}

			int counter = depth;
			while (counter > 0)
			{ 
				Console.Write("  ");
				counter--;
			}
			
			Console.WriteLine(Value);

			if (Left != null)
			{
				Left.PrintTree(depth + 1);
			}
		}

		public void PrintList()
		{
			CheckMode(DatastructureMode.List, "PrintList");

			Console.WriteLine("Current List:");
			Node<T> current = this;
			Console.WriteLine(Value);

			while (current.Next != this)
			{    
				current = current.Next;
				Console.WriteLine(current.Value);   
			}
		}

		/// <summary>
		/// Changes to mode.
		/// </summary>
		/// <param name="newMode">The new mode.</param>
		/// <returns></returns>
		public Node<T> ChangeMode(DatastructureMode newMode)
		{
			if (Mode == newMode)
			{
				// no change needed, return this
				return this;
			}

			switch (newMode)
			{
				case DatastructureMode.Tree:
					throw new NotImplementedException();
					
				case DatastructureMode.List:
					if (Mode != DatastructureMode.Tree)
					{
						throw new NotImplementedException();
					}
					
					return TreeToList(this);
				default:
					throw new ArgumentOutOfRangeException("newMode");
			}

		}

		private static Node<T> TreeToList(Node<T> root)
		{
			if (root == null)
			{
				return null;
			}

			root.Mode = DatastructureMode.List;

			Node<T> oldLeft = root.Left;
			Node<T> oldRight = root.Right;

			// Node<T> newList = Left;

			// convert this to a doubly linked list
			root.Previous = root;
			root.Next = root;

			Node<T> tempMidList = MergeLists(TreeToList(oldLeft), root);
			return MergeLists(tempMidList, TreeToList(oldRight));
		}

		private static Node<T> MergeLists(Node<T> listA, Node<T> listB)
		{
			if (listA == null)
			{
				return listB; // may be null, expected
			}

			if (listB == null)
			{
				return listA;
			}

			// swap end of list next values (while they are still accessible through previous)
			listA.Previous.Next = listB;
			listB.Previous.Next = listA;

			// swap begin previous values (with a temporary)
			Node<T> tempListBPrevious = listB.Previous;
			listB.Previous = listA.Previous;
			listA.Previous = tempListBPrevious;

			return listA;
		}


		private void CheckMode(DatastructureMode correctMode, string operation)
		{
			if(Mode != correctMode)
			{
				throw new InvalidOperationException(string.Format("Cannot use operation '{0}' while not in mode '{1}'. Current mode is '{2}'.", operation, correctMode, Mode));
			}
		}
	}
}
