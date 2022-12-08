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

    bool customerHasSat = false;

    private void Start()
    {
        navMeshAgent = GetComponentInParent<NavMeshAgent>();
        customerController = GetComponentInParent<CustomerController>();

        customerController.anim.SetBool("isSitting", false);
    }

    public override State RunCurrentState()
    {
        if (Vector2.Distance(new Vector2(customerController.transform.position.x, customerController.transform.position.z), new Vector2(customerController.chairLocation.x, customerController.chairLocation.z)) < 0.25f)
        {
            //Debug.Log("SAT");
            customerController.anim.SetBool("isSitting", true);
            isSitting = true;
            return sitState;
        }
        else
        {
            //if (!pathIsSet)
            //{
            //    SetPath();
            //}
            if (!customerHasSat)
            {
                navMeshAgent.SetDestination(customerController.chairLocation);
                Debug.Log("walking");
                customerHasSat = true;
            }
            
            //Debug.Log("WALKING");
            return this;
        }
    }

    
}
