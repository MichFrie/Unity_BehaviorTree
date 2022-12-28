using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobberBehavior : MonoBehaviour
{
   BehaviorTree tree;

   void Start()
   {
      tree = new BehaviorTree();
      Node steal = new Node("Steal something");
      Node goToDiamond = new Node("Go to Diamond");
      Node goToVan = new Node("Go to Van");
      
      steal.AddChild(goToDiamond);
      steal.AddChild(goToVan);
      tree.AddChild(steal);
      
      tree.printTree();
   }
}
