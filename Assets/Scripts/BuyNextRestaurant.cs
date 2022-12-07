using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuyNextRestaurant : MonoBehaviour
{
    [SerializeField] Animator doorAnim;
    [SerializeField] private Image boughtAmountImage;
    [SerializeField] private TMP_Text priceText;
    [Header("Dont make table price lower the 100")]
    [SerializeField] private int restaurantPrice;
    private float restaurantPriceForText;
    GameManager gameManager;
    private bool canBuild;
    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        doorAnim.enabled = false;
        priceText.text = restaurantPrice.ToString();
        restaurantPriceForText = restaurantPrice;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            canBuild = true;
            StartCoroutine(BoughtAmount());
        }
    }



    IEnumerator BoughtAmount()
    {
        while (gameManager.money > 0 && canBuild)
        {
            yield return new WaitForSeconds(0.01f);
            //gameManager.DecressMoney((int)(restaurantPrice * 0.01));
            gameManager.DecressMoney(restaurantPrice * 0.01f);
            //priceText.text = ((int)(restaurantPrice - (restaurantPrice * 0.01))).ToString();
            restaurantPriceForText -= restaurantPrice * 0.01f;
            priceText.text = ((int)restaurantPriceForText).ToString();
            boughtAmountImage.fillAmount += 0.01f;
            if (boughtAmountImage.fillAmount == 1)
            {
                
                doorAnim.enabled = true;
                StopCoroutine(BoughtAmount());
                this.gameObject.SetActive(false);
            }
            if (gameManager.money < restaurantPrice - (restaurantPrice - 1))
            {
                StopCoroutine(BoughtAmount());
            }
        }

        if (gameManager.money < 0)
        {
            gameManager.SetMoney(0);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("PlayerLeft");
            canBuild = false;
            StopCoroutine(BoughtAmount());
        }
    }
}
