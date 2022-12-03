using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestaurantManager : MonoBehaviour
{
    [SerializeField] private Chair[] chairs;
    [SerializeField] private GameObject[] customers;
    [SerializeField] private Transform customerSpawnPosition;
    [SerializeField] private Transform exitLocation;
    public GameObject upgradeScreen;
     
    [HideInInspector] public List<CustomerController> customersInRestaurant = new List<CustomerController>();
    public int foodPrice;
 
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
                    customer.GetComponent<CustomerController>().exitLocation = exitLocation.position;
                    customer.GetComponent<CustomerController>().customersChair = item;
                    customer.GetComponent<CustomerController>().canMoveToChair = true;
                    item.isEmpty = false;
                    break;
                }
            }
        }
    }
}
