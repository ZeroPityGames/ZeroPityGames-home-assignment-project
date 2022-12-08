using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SitState : State
{
    public EatState eatState;
    public bool hasGottenOrder;

    CustomerController customerController;
    CapsuleCollider capsuleCollider;

    private float rotationSpeed = 1000f;

    private void Start()
    {
        customerController = GetComponentInParent<CustomerController>();
        capsuleCollider = GetComponentInParent<CapsuleCollider>();
    }
    public override State RunCurrentState()
    {
        if (hasGottenOrder)
        {
            customerController.orderIndicator.gameObject.SetActive(false);
            
            capsuleCollider.enabled = false;
            return eatState;
        }
        else
        {
            customerController.transform.forward = Vector3.RotateTowards(customerController.transform.forward, customerController.customersChair.transform.right, 10 * Time.deltaTime, 1);
            customerController.orderIndicator.gameObject.SetActive(true);
            return this;
        }
        
    }
}
