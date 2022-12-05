using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyTable : MonoBehaviour
{
    [SerializeField] private GameObject table;
    [SerializeField] private Image boughtAmountImage;
    [Header("Dont make table price lower the 100")]
    [SerializeField] private int tablePrice;
    GameManager gameManager;
    private bool canBuild;
    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
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
            gameManager.DecressMoney((int)(tablePrice * 0.01));
            boughtAmountImage.fillAmount += 0.01f;
            if (boughtAmountImage.fillAmount == 1)
            {
                this.gameObject.SetActive(false);
                table.SetActive(true);
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
