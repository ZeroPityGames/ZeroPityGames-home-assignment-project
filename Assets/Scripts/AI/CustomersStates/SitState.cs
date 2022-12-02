using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SitState : State
{
    public EatState eatState;
    public bool hasGottenOrder;

    CustomerController customerController;

    private void Start()
    {
        customerController = GetComponentInParent<CustomerController>();
    }
    public override State RunCurrentState()
    {
        if (hasGottenOrder)
        {
            customerController.orderIndicator.gameObject.SetActive(false);
            return eatState;
        }
        else
        {
            customerController.orderIndicator.gameObject.SetActive(true);
            return this;
        }
        
    }
}
