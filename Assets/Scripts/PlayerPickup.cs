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
                }
                break;
            case "Food":
                Debug.Log("Picked Up Food");
                hasFoodInHand = true;
                break;
            default:
                break;
        }
    }
}
