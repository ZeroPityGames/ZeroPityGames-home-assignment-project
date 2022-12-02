using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    private int money;
    [SerializeField] private TMP_Text moneyText;

    public void IncressMoney(int moneyAmount)
    {
        money += moneyAmount;
        UpdateCanvas();
    }

    private void UpdateCanvas()
    {
        moneyText.text = money.ToString();
    }
}
