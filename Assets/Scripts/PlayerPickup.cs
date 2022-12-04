using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    private bool hasFoodInHand;
    [SerializeField] private int amountOfFood;
    [SerializeField] private int maxCarryFood;
    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "Customer":
                if (hasFoodInHand == true)
                {
                    other.gameObject.GetComponentInChildren<SitState>().hasGottenOrder = true;
                    other.gameObject.GetComponentInChildren<WalkState>().isSitting = false;
                    other.gameObject.GetComponent<CustomerController>().customersChair.GetComponentInParent<RestaurantManager>().customersInRestaurant.Remove(other.gameObject.GetComponent<CustomerController>());
                    amountOfFood--;
                    if (amountOfFood <= 0)
                    {
                        hasFoodInHand = false;
                    }
                }
                break;
            case "Food":
                Debug.Log("Picked Up Food");
                hasFoodInHand = true;
                amountOfFood = maxCarryFood;
                break;
            case "Computer":
                other.gameObject.GetComponentInParent<RestaurantManager>().upgradeScreen.SetActive(true);
                break;
            default:
                break;
        }
    }
}
