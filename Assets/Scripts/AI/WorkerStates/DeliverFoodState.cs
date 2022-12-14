using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DeliverFoodState : State
{
    NavMeshAgent navMeshAgent;
    WorkerController workerController;
    
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

        if (pickUpFoodState.isPickedUp == true && workerController.myCustomer.GetComponentInChildren<SitState>().hasGottenOrder == true)
        {
            Debug.Log("LOOKING FOR NEW CUSTOMER");
            CustomerController[] customers = waitingForOrderState.restaurantManager.customersInRestaurant.ToArray();
            for (int i = 0; i < customers.Length; i++)
            {
                if (customers[i].GetComponentInChildren<WalkState>().isSitting && !customers[i].GetComponent<CustomerController>().hasWorker)
                {
                    Debug.Log("SCANNING...");
                    customers[i].GetComponentInChildren<WalkState>().isSitting = false;
                    workerController.myCustomer = customers[i].gameObject;
                    waitingForOrderState.restaurantManager.customersInRestaurant.Remove(customers[i]);
                    return this;
                    //break;
                }
                else
                {
                    isDoneWithOrder = true;
                    return this;
                }
            }
            isDoneWithOrder = true;
            return this;
        }
        else
        {
            if (workerController.myCustomer.GetComponentInChildren<SitState>().hasGottenOrder == true)
            {
                isDoneWithOrder = true;
                return this;
            }

            if (Vector3.Distance(transform.position, workerController.myCustomer.transform.position) < 1f)
            {
                workerController.myCustomer.GetComponentInChildren<SitState>().hasGottenOrder = true;
                isDoneWithOrder = true;
                return this;
            }
            navMeshAgent.SetDestination(workerController.myCustomer.transform.position);
            return this;
        }
    }
}
