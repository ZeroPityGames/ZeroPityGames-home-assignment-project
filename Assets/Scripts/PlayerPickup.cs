using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    private bool hasFoodInHand;
    [SerializeField] private int amountOfFood;
    [SerializeField] private int maxCarryFood;

    [SerializeField] private GameObject pizza;
    [SerializeField] private GameObject donut;
    [SerializeField] private GameObject coffe;
    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "Customer":
                if (hasFoodInHand == true)
                {
                    pizza.SetActive(false);
                    donut.SetActive(false);
                    coffe.SetActive(false);
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
                if (other.gameObject.GetComponentInParent<RestaurantManager>().restaurantType == "Pizza")
                {
                    pizza.SetActive(true);
                }
                if (other.gameObject.GetComponentInParent<RestaurantManager>().restaurantType == "Donut")
                {
                    donut.SetActive(true);
                }
                if (other.gameObject.GetComponentInParent<RestaurantManager>().restaurantType == "Coffe")
                {
                    coffe.SetActive(true);
                }

                break;
            case "Computer":
                other.gameObject.GetComponentInParent<RestaurantManager>().upgradeScreen.SetActive(true);
                break;
            case "Floor2":
                other.gameObject.GetComponentInParent<RestaurantManager>().TurnOfOnSlider(true);
                break;
            default:
                break;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Floor")
        {
            other.gameObject.GetComponentInParent<RestaurantManager>().TurnOfOnSlider(false);
        }
    }
}
