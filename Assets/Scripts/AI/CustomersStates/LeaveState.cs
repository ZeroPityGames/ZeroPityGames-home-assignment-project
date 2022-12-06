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
        
        if (Vector2.Distance(new Vector2(customerController.transform.position.x, customerController.transform.position.z), new Vector2(customerController.exitLocation.position.x, customerController.exitLocation.position.z)) < 0.1f)
        {
            customerController.customersChair.isEmpty = true;
            customerController.customersChair.GetComponentInParent<RestaurantManager>().UpdateSlider();
            Destroy(customerController.gameObject);
            return this;
        }
        else
        {
            navMeshAgent.SetDestination(customerController.exitLocation.position);
            return this;
        }
        
    }
}
