using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BT_Agent : MonoBehaviour
{
   public BehaviorTree tree;
   public NavMeshAgent agent;

   public enum ActionState
   {
      IDLE,
      WORKING
   }

   public ActionState state = ActionState.IDLE;

   public Node.Status treeStatus = Node.Status.RUNNING;
   
   public void Start()
   {
      agent = this.GetComponent<NavMeshAgent>();
      
      tree = new BehaviorTree();
      
   }

   public void Update()
   {
      if (treeStatus != Node.Status.SUCCESS)
      {
         treeStatus = tree.Process();
      }
   }
   
   public Node.Status GoToLocation(Vector3 destination)
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
