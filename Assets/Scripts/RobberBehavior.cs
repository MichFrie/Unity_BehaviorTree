using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RobberBehavior : BT_Agent
{
   public GameObject Diamond;
   public GameObject Van;
   public GameObject BackDoor;
   public GameObject FrontDoor;
   
   [Range(0,1000)]
   public int money = 800;
   
   new void Start()
   {
      base.Start();
      Selector openDoor = new Selector("Open Door");
      
      Sequence steal = new Sequence("Steal something");
      
      Leaf goToBackDoor = new Leaf("Go To BackDoor", GoToBackDoor);
      Leaf goToFrontDoor = new Leaf("Go To FrontDoor", GoToFrontDoor);
      Leaf hasGotMoney = new Leaf("Has got money", HasMoney);
      Leaf goToDiamond = new Leaf("Go to Diamond", GotToDiamond);
      Leaf goToVan = new Leaf("Go to Van", GotToVan);
      Inverter invertMoney = new Inverter("Invert Money");
      
      invertMoney.AddChild(hasGotMoney);
      
      openDoor.AddChild(goToFrontDoor);
      openDoor.AddChild(goToBackDoor);
      
      steal.AddChild(invertMoney);
      steal.AddChild(openDoor);
      steal.AddChild(goToDiamond);
      //steal.AddChild(goToBackDoor);
      steal.AddChild(goToVan);
      tree.AddChild(steal);
      
      //tree.printTree();
   }
   public Node.Status HasMoney()
   {
      if (money < 500)
      {
         return Node.Status.FAILURE;
      }

      return Node.Status.SUCCESS;
   }
   
   public Node.Status GoToBackDoor()
   {
      return GoToDoor(BackDoor);
   }
   public Node.Status GoToFrontDoor()
   {
      return GoToDoor(FrontDoor);
   }

   public Node.Status GoToDoor(GameObject door)
   {
      Node.Status s = GoToLocation(door.transform.position);
      if (s == Node.Status.SUCCESS)
      {
         if (!door.GetComponent<Lock>().isLocked)
         {
            door.SetActive(false);
            return Node.Status.SUCCESS;
         }

         return Node.Status.FAILURE;
      }
      else
      {
         return s;
      }
   }
   
   public Node.Status GotToDiamond()
   {
      Node.Status s = GoToLocation(Diamond.transform.position);
      if (s == Node.Status.SUCCESS)
      {
         Diamond.transform.parent = this.gameObject.transform;
      }

      return s;
   }
   
   
   public Node.Status GotToVan()
   {
      Node.Status s = GoToLocation(Van.transform.position);
      if (s == Node.Status.SUCCESS)
      {
         money += 300;
         Diamond.SetActive(false);
      }
      return s;
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
