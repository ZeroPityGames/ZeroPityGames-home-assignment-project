using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PickUpFoodState : State
{
    [HideInInspector] public Transform foodPickupLocation;
    NavMeshAgent navMeshAgent;
    WorkerController workerController;

    [SerializeField] DeliverFoodState deliverFoodState;
    public bool isPickedUp;

    bool pathIsSet;

    private void Start()
    {
        navMeshAgent = GetComponentInParent<NavMeshAgent>();
        workerController = GetComponentInParent<WorkerController>();
    }
    public override State RunCurrentState()
    {
        
        if (isPickedUp)
        {
            return deliverFoodState;
        }
        else
        {
            if (Vector3.Distance(transform.position, foodPickupLocation.position) < 1f)
            {
                isPickedUp = true;
                workerController.carryAmount = workerController.carryCapacity;
            }
            navMeshAgent.SetDestination(foodPickupLocation.position);
            
            
            return this;
        }
        
    }

    void SetPath()
    {
        pathIsSet = true;
        navMeshAgent.SetDestination(foodPickupLocation.position);
    }
}
