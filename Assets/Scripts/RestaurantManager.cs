using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RestaurantManager : MonoBehaviour
{
    [SerializeField] private Chair[] chairs;
    [SerializeField] private GameObject[] customers;
    [SerializeField] private Transform customerSpawnPosition;
    [SerializeField] private Transform exitLocation;
    public Transform workerWaitPosition;
    public GameObject upgradeScreen;
    public GameObject workerStation;
    public int workerPrice;
    [HideInInspector] public int tablesBuilt;
    [HideInInspector] public int customersServed;
    [SerializeField] private GameObject customerCounterCanvas;
    [SerializeField] private Slider numberOfCustomerSlider;
    [SerializeField] private TMP_Text numberOfCustomerText;

    [HideInInspector] public List<CustomerController> customersInRestaurant = new List<CustomerController>();
    public int foodPrice;

    public string restaurantType;

    

    private void Start()
    {
        StartCoroutine(SlowUpdate());
    }

    IEnumerator SlowUpdate()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(0.5f, 1.5f));
            foreach (Chair item in chairs)
            {
                //Debug.Log("test");
                if (item.isEmpty == true && item.isActiveAndEnabled)
                {
                    //Spawn customer and walk him to the table
                    GameObject customer = Instantiate(customers[Random.Range(0, customers.Length)], customerSpawnPosition.position, Quaternion.identity);
                    customersInRestaurant.Add(customer.GetComponent<CustomerController>());
                    customer.GetComponent<CustomerController>().chairLocation = item.transform.position;
                    customer.GetComponent<CustomerController>().exitLocation = exitLocation;
                    customer.GetComponent<CustomerController>().customersChair = item;
                    customer.GetComponent<CustomerController>().canMoveToChair = true;
                    item.isEmpty = false;
                    break;
                }
            }
        }
    }

    public void TurnOfOnSlider(bool state)
    {
        if (state)
        {
            customerCounterCanvas.SetActive(true);
        }
        else
        {
            customerCounterCanvas.SetActive(false);
        }
    }

    public void UpdateSlider()
    {
        customersServed++;
        numberOfCustomerSlider.value = customersServed;
        numberOfCustomerText.text = customersServed.ToString();
    }
}
