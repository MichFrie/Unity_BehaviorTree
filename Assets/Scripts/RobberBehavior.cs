using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RobberBehavior : MonoBehaviour
{
   BehaviorTree tree;
   public GameObject Diamond;
   public GameObject Van;

   NavMeshAgent agent;

   void Start()
   {
      agent = this.GetComponent<NavMeshAgent>();
      
      tree = new BehaviorTree();
      Node steal = new Node("Steal something");
      Leaf goToDiamond = new Leaf("Go to Diamond", GotToDiamond);
      Leaf goToVan = new Leaf("Go to Van", GotToVan);
      
      steal.AddChild(goToDiamond);
      steal.AddChild(goToVan);
      tree.AddChild(steal);
      
      //tree.printTree();

      tree.Process();

   }

   public Node.Status GotToDiamond()
   {
      agent.SetDestination(Diamond.transform.position);
      return Node.Status.SUCCESS;
   }
   
   public Node.Status GotToVan()
   {
      agent.SetDestination(Van.transform.position);
      return Node.Status.SUCCESS;
   }
   
}
