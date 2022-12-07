using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public float money;
    [SerializeField] private TMP_Text moneyText;
    [SerializeField] private TMP_Text test;

    private void Start()
    {
        UpdateCanvas();
    }
    public void IncressMoney(float moneyAmount)
    {
        money += moneyAmount;
        UpdateCanvas();
    }

    public void DecressMoney(float moneyAmount)
    {
        money -= moneyAmount;
        UpdateCanvas();
    }

    public void SetMoney(float moneyAmount)
    {
        money = moneyAmount;
        UpdateCanvas();
    }

    private void UpdateCanvas()
    {
        moneyText.text = ((int)money).ToString() + "$";
    }

    void ChangeCramerRate()
    {
        test.text = "FPS SHOUD HAVE CHANGED";
        Debug.Log("Changed framse");
        Application.targetFrameRate = 45;
        QualitySettings.vSyncCount = 0;
    }
}
