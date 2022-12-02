using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WalkState : State
{
    public SitState sitState;
    public bool isSitting;
    
    NavMeshAgent navMeshAgent;
    CustomerController customerController;

    bool pathIsSet = false;

    private void Start()
    {
        navMeshAgent = GetComponentInParent<NavMeshAgent>();
        customerController = GetComponentInParent<CustomerController>();
    }

    public override State RunCurrentState()
    {
        if (Vector2.Distance(new Vector2(customerController.transform.position.x, customerController.transform.position.z), new Vector2(customerController.chairLocation.x, customerController.chairLocation.z)) < 0.25f)
        {
            //Debug.Log("SAT");
            isSitting = true;
            return sitState;
        }
        else
        {
            //if (!pathIsSet)
            //{
            //    SetPath();
            //}
            navMeshAgent.SetDestination(customerController.chairLocation);
            //Debug.Log("WALKING");
            return this;
        }
    }

    void SetPath()
    {
        pathIsSet = true;
        navMeshAgent.SetDestination(customerController.chairLocation);
    }
}
