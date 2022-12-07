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
    GameManager gameManager;
    private bool canBuild;
    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        doorAnim.enabled = false;
        priceText.text = restaurantPrice.ToString();
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
            gameManager.DecressMoney(1);
            //priceText.text = ((int)(restaurantPrice - (restaurantPrice * 0.01))).ToString();
            priceText.text = restaurantPrice--.ToString();
            boughtAmountImage.fillAmount += 0.01f;
            if (boughtAmountImage.fillAmount == 1)
            {
                gameManager.IncressMoney(1);
                doorAnim.enabled = true;
                StopCoroutine(BoughtAmount());
                this.gameObject.SetActive(false);
            }
            if (gameManager.money < restaurantPrice - (restaurantPrice - 1))
            {
                StopCoroutine(BoughtAmount());
            }
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
