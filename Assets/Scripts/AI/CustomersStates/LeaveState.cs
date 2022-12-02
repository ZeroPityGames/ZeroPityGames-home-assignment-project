using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LeaveState : State
{
    NavMeshAgent navMeshAgent;
    CustomerController customerController;

    private void Start()
    {
        navMeshAgent = GetComponentInParent<NavMeshAgent>();
        customerController = GetComponentInParent<CustomerController>();
    }
    public override State RunCurrentState()
    {
        if (Vector2.Distance(new Vector2(customerController.transform.position.x, customerController.transform.position.z), new Vector2(customerController.exitLocation.x, customerController.exitLocation.z)) < 0.1f)
        {
            customerController.customersChair.isEmpty = true;
            Destroy(customerController.gameObject);
            return this;
        }
        else
        {
            navMeshAgent.SetDestination(customerController.exitLocation);
            return this;
        }
        
    }
}
