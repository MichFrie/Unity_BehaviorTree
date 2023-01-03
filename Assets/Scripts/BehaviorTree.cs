using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviorTree : Node
{
   public BehaviorTree()
   {
      Name = "Tree";
   }

   public BehaviorTree(string n)
   {
      Name = n;
   }
   struct NodeLevel
   {
      public int Level;
      public Node Node;
   }

   public override Status Process()
   {
      if (children.Count == 0)
         return Status.SUCCESS;
      
      return children[currentChild].Process();
   }

   public void printTree()
   {
      string treePrintout = "";
      Stack<NodeLevel> nodeStack = new Stack<NodeLevel>();

      Node currentNode = this;
      nodeStack.Push(new NodeLevel{Level = 0, Node = currentNode});

      while (nodeStack.Count != 0)
      {
         NodeLevel nextNode = nodeStack.Pop();
         treePrintout += new string('-', nextNode.Level) + nextNode.Node.Name + "\n";

         for (int i = nextNode.Node.children.Count - 1; i >= 0; i--)
         {
            nodeStack.Push(new NodeLevel{Level = nextNode.Level + 1, Node = nextNode.Node.children[i]});
         }
      }

      Debug.Log(treePrintout);
   }
}
