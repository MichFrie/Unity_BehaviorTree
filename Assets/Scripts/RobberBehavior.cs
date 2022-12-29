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
   public GameObject BackDoor;
   public GameObject FrontDoor;

   NavMeshAgent agent;

   public enum ActionState
   {
      IDLE,
      WORKING
   }

   ActionState state = ActionState.IDLE;

   Node.Status treeStatus = Node.Status.RUNNING;
   
   void Start()
   {
      agent = this.GetComponent<NavMeshAgent>();
      
      tree = new BehaviorTree();
      Sequence steal = new Sequence("Steal something");
      Leaf goToBackDoor = new Leaf("Go To BackDoor", GoToBackDoor);
      Leaf goToFrontDoor = new Leaf("Go To FrontDoor", GoToFrontDoor);
      Leaf goToDiamond = new Leaf("Go to Diamond", GotToDiamond);
      Leaf goToVan = new Leaf("Go to Van", GotToVan);
      Selector openDoor = new Selector("Open Door");

      openDoor.AddChild(goToFrontDoor);
      openDoor.AddChild(goToBackDoor);
      
      steal.AddChild(openDoor);
      steal.AddChild(goToDiamond);
      //steal.AddChild(goToBackDoor);
      steal.AddChild(goToVan);
      tree.AddChild(steal);
      
      //tree.printTree();
   }

   void Update()
   {
      if (treeStatus == Node.Status.RUNNING)
      {
         treeStatus = tree.Process();
      }
   }

   public Node.Status GoToBackDoor()
   {
      return GoToLocation(BackDoor.transform.position);
   }
   public Node.Status GoToFrontDoor()
   {
      return GoToLocation(FrontDoor.transform.position);
   }

   public Node.Status GotToDiamond()
   {
      return GoToLocation(Diamond.transform.position);
   }
   
   public Node.Status GotToVan()
   {
      return GoToLocation(Van.transform.position);
   }

   Node.Status GoToLocation(Vector3 destination)
   {
      float distanceToTarget = Vector3.Distance(destination, this.transform.position);
      if (state == ActionState.IDLE)
      {
         agent.SetDestination(destination);
         state = ActionState.WORKING;
      }
      else if(Vector3.Distance(agent.pathEndPosition, destination) >= 2f)
      {
         state = ActionState.IDLE;
         return Node.Status.FAILURE;
      }
      else if(distanceToTarget < 2)
      {
         state = ActionState.IDLE;
         return Node.Status.SUCCESS;
      }
      return Node.Status.RUNNING;
   }
}
