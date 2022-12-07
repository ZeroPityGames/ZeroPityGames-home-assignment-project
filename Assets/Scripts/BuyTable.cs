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
    GameManager gameManager;
    private bool canBuild;
    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        priceText.text = tablePrice.ToString();
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
            gameManager.DecressMoney(1);
            priceText.text = tablePrice--.ToString();
            boughtAmountImage.fillAmount += 0.01f;
            if (boughtAmountImage.fillAmount == 1)
            {
                gameManager.IncressMoney(1);
                table.SetActive(true);
                this.gameObject.SetActive(false);
            }
            if (gameManager.money < (int)(tablePrice * 0.01))
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
