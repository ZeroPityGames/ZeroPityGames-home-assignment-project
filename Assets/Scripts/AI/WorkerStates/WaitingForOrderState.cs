using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaitingForOrderState : State
{
    public PickUpFoodState pickUpFoodState;
    WorkerController workerController;
    NavMeshAgent navMeshAgent;
    public Transform waitPosition;

    public bool hasOrder;

    float scanCounter = 1f;

    bool pathIsSet = false;

    [HideInInspector] public RestaurantManager restaurantManager;
    private void Start()
    {
        workerController = GetComponentInParent<WorkerController>();
        navMeshAgent = GetComponentInParent<NavMeshAgent>();
        
    }
    public override State RunCurrentState()
    {
        if (hasOrder)
        {
            return pickUpFoodState;
        }
        else
        {
            //GameObject[] customers = GameObject.FindGameObjectsWithTag(workerController.specificCustomerTag);
            //for (int i = 0; i < customers.Length; i++)
            //{
            //    if (customers[i].GetComponentInChildren<WalkState>().isSitting && !customers[i].GetComponent<CustomerController>().hasWorker)
            //    {
            //        Debug.Log("SCANNING...");
            //        customers[i].GetComponentInChildren<WalkState>().isSitting = false;
            //        workerController.myCustomer = customers[i];
            //        hasOrder = true;

            //        return pickUpFoodState;
            //        //break;
            //    }
            //    else
            //    {
            //        //if (!pathIsSet)
            //        //{
            //        //    SetPath();
            //        //}
            //        navMeshAgent.SetDestination(new Vector3(7.50f, 0f, 3.5f));
            //    }
            //}
            CustomerController[] customers = restaurantManager.customersInRestaurant.ToArray();
            for (int i = 0; i < customers.Length; i++)
            {
                if (customers[i].GetComponentInChildren<WalkState>().isSitting && !customers[i].GetComponent<CustomerController>().hasWorker)
                {
                    Debug.Log("SCANNING...");
                    customers[i].GetComponentInChildren<WalkState>().isSitting = false;
                    workerController.myCustomer = customers[i].gameObject;
                    hasOrder = true;
                    restaurantManager.customersInRestaurant.Remove(customers[i]);
                    return pickUpFoodState;
                    //break;
                }
                else
                {
                    //if (!pathIsSet)
                    //{
                    //    SetPath();
                    //}
                    navMeshAgent.SetDestination(waitPosition.position);
                }
            }

            return this;
        }

        
    }

    void SetPath()
    {
        pathIsSet = true;
        navMeshAgent.SetDestination(new Vector3(7.50f, 0f, 3.5f));
    }
}
