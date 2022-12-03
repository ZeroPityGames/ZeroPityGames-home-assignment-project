using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    bool hasFoodInHand;
    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "Customer":
                if (hasFoodInHand == true)
                {
                    hasFoodInHand = false;
                    other.gameObject.GetComponentInChildren<SitState>().hasGottenOrder = true;
                    other.gameObject.GetComponentInChildren<WalkState>().isSitting = false;
                    other.gameObject.GetComponent<CustomerController>().customersChair.GetComponentInParent<RestaurantManager>().customersInRestaurant.Remove(other.gameObject.GetComponent<CustomerController>());
                }
                break;
            case "Food":
                Debug.Log("Picked Up Food");
                hasFoodInHand = true;
                break;
            case "Computer":
                other.gameObject.GetComponentInParent<RestaurantManager>().upgradeScreen.SetActive(true);
                break;
            default:
                break;
        }
    }
}
