using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaf : Node
{
   public delegate Status Tick();

   public Tick ProcessMethod;

   public Leaf()
   {
      
   }

   public Leaf(string n, Tick pm)
   {
      Name = n;
      ProcessMethod = pm;
   }
   public Leaf(string n, Tick pm, int order)
   {
      Name = n;
      ProcessMethod = pm;
      sortOrder = order;
   }
   public override Status Process()
   {
      if (ProcessMethod != null)
      {
         return ProcessMethod();
      }

      return Status.FAILURE;
   }
}
