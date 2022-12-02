using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EatState : State
{
    public LeaveState leaveState;
    public bool isDoneEating;

    CustomerController customerController;

    float eatingSeconds = 3;

    private void Start()
    {
        customerController = GetComponentInParent<CustomerController>();
    }

    public override State RunCurrentState()
    {
        if (isDoneEating)
        {
            return leaveState;
        }
        else
        {
            Debug.Log(eatingSeconds);
            customerController.eatingIndicator.gameObject.SetActive(true);
            eatingSeconds -= Time.deltaTime;
            customerController.eatingIndicator.GetComponentInChildren<TMP_Text>().text = ((int)eatingSeconds).ToString();

            if (eatingSeconds <= 0)
            {
                isDoneEating = true;
            }
            
            return this;
        }
    }
}
