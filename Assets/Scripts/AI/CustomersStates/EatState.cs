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
    

    GameManager gameManager;

    private void Start()
    {
        customerController = GetComponentInParent<CustomerController>();
        gameManager = FindObjectOfType<GameManager>();
    }

    public override State RunCurrentState()
    {
        if (isDoneEating)
        {
            customerController.anim.SetBool("isSitting", false);
            return leaveState;
        }
        else
        {
            //customerController.eatingIndicator.SetActive(true);
            eatingSeconds -= Time.deltaTime;
            //customerController.eatingIndicator.GetComponentInChildren<TMP_Text>().text = ((int)eatingSeconds).ToString();

            if (eatingSeconds <= 0)
            {
                isDoneEating = true;
                gameManager.IncressMoney(GetComponentInParent<CustomerController>().customersChair.GetComponentInParent<RestaurantManager>().foodPrice);
            }
            
            return this;
        }
    }
}
