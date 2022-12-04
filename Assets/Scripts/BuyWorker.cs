using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyWorker : MonoBehaviour
{
    GameManager gameManager;
    RestaurantManager restaurantManager;
    [SerializeField] private GameObject worker;
    [SerializeField] private Transform spawnPosition;
    private int workerCount = 0;

    [SerializeField] private Button[] levelUpButtons;
    [SerializeField] private Button test;
    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        restaurantManager = GetComponentInParent<RestaurantManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            restaurantManager.workerStation.SetActive(true);
        }
    }

    public void InstantiateWorker()
    {
        
        gameManager.DecressMoney(restaurantManager.workerPrice);
        GameObject workerInstance = Instantiate(worker, spawnPosition.position, Quaternion.identity);
        workerInstance.GetComponentInChildren<WaitingForOrderState>().restaurantManager = GetComponentInParent<RestaurantManager>();
        workerInstance.GetComponentInChildren<PickUpFoodState>().foodPickupLocation = GetComponentInParent<RestaurantManager>().GetComponentInChildren<FoodPickup>().transform;
        levelUpButtons[workerCount].onClick.AddListener(workerInstance.GetComponent<WorkerController>().LevelUpWorker);
        workerCount++;
    }

   
}
