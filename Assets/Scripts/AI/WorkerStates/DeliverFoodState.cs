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

        if (workerController.carryAmount <= 0)
        {
            Debug.Log("RETURN TO WAITING");
            waitingForOrderState.hasOrder = false;
            pickUpFoodState.isPickedUp = false;
            isDoneWithOrder = false;
            return waitingForOrderState;
        }

        if (pickUpFoodState.isPickedUp == true && workerController.myCustomer.GetComponentInChildren<SitState>().hasGottenOrder == true)
        {
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
            return this;
        }
        else
        {
            if (workerController.myCustomer.GetComponentInChildren<SitState>().hasGottenOrder == true)
            {
                //if (workerController.carryAmount <= 0)
                //{
                //    isDoneWithOrder = true;
                //    return this;
                //}
                //else
                //{
                //    return this;
                //}
                isDoneWithOrder = true;
                return this;
            }

            if (Vector3.Distance(transform.position, workerController.myCustomer.transform.position) < 2f)
            {
                //if (workerController.carryAmount > 0)
                //{
                //    workerController.carryAmount--;
                //    workerController.myCustomer.GetComponentInChildren<SitState>().hasGottenOrder = true;
                //    if (workerController.carryAmount <= 0)
                //    {
                //        isDoneWithOrder = true;
                //        return this;
                //    }
                //    else
                //    {
                //        return this;
                //    }

                //}
                //else
                //{
                //    isDoneWithOrder = true;
                //    return this;
                //}
                workerController.myCustomer.GetComponentInChildren<SitState>().hasGottenOrder = true;
                isDoneWithOrder = true;
                return this;
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

    State DoneWithOrder()
    {
        Debug.Log("RETURN TO WAITING");
        waitingForOrderState.hasOrder = false;
        pickUpFoodState.isPickedUp = false;
        isDoneWithOrder = false;
        return waitingForOrderState;
    }
}
