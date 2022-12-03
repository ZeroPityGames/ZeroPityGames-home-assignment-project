using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DeliverFoodState : State
{
    NavMeshAgent navMeshAgent;
    WorkerController workerController;

    bool pathIsSet;
    bool isDoneWithOrder;
    [SerializeField] WaitingForOrderState waitingForOrderState;
    [SerializeField] PickUpFoodState pickUpFoodState;
    private void Start()
    {
        navMeshAgent = GetComponentInParent<NavMeshAgent>();
        workerController = GetComponentInParent<WorkerController>();
        
    }
    public override State RunCurrentState()
    {
        if (isDoneWithOrder)
        {
            Debug.Log("RETURN TO WAITING");
            waitingForOrderState.hasOrder = false;
            pickUpFoodState.isPickedUp = false;
            isDoneWithOrder = false;
            return waitingForOrderState;
        }
        else
        {
            //if (!pathIsSet)
            //{
            //    SetPath();
            //}
            if (workerController.myCustomer.GetComponentInChildren<SitState>().hasGottenOrder == true)
            {
                isDoneWithOrder = true;
            }

            if (Vector3.Distance(transform.position, workerController.myCustomer.transform.position) < 2f)
            {
                workerController.myCustomer.GetComponentInChildren<SitState>().hasGottenOrder = true;
                isDoneWithOrder = true;
            }
            navMeshAgent.SetDestination(workerController.myCustomer.transform.position);
            return this;
        }
    }

    void SetPath()
    {
        pathIsSet = true;
        navMeshAgent.SetDestination(workerController.myCustomer.transform.position);
    }
}
