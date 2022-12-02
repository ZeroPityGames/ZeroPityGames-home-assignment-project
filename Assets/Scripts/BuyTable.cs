using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyTable : MonoBehaviour
{
    [SerializeField] private GameObject table;
    GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (gameManager.money >= 250)
            {
                this.gameObject.SetActive(false);
                table.SetActive(true);
            }
        }
    }
}
