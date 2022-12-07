using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuyTable : MonoBehaviour
{
    [SerializeField] private TMP_Text priceText;
    [SerializeField] private GameObject table;
    [SerializeField] private Image boughtAmountImage;
    [Header("Dont make table price lower the 100")]
    [SerializeField] private int tablePrice;
    private float tablePriceForText;
    GameManager gameManager;
    private bool canBuild;
    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        priceText.text = tablePrice.ToString();
        tablePriceForText = tablePrice;
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
            gameManager.DecressMoney(tablePrice * 0.01f);
            tablePriceForText -= tablePrice * 0.01f;
            priceText.text = ((int)tablePriceForText).ToString();
            boughtAmountImage.fillAmount += 0.01f;
            if (boughtAmountImage.fillAmount == 1)
            {
                table.SetActive(true);
                StopCoroutine(BoughtAmount());
                this.gameObject.SetActive(false);
            }
            if (gameManager.money < 1)
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
