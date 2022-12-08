using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(LineRenderer))]
public class Tutorial : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private GameManager gameManager;

    private int order = 1;

    [SerializeField] private Transform playerLocation;
    [SerializeField] private Transform pickupFoodLocation;
    [SerializeField] private Transform deliverFoodLocation;
    [SerializeField] private Transform buyWorkerLocation;
    [SerializeField] private Transform buyTableLocation;
    [SerializeField] private TMP_Text tutorialText;
    
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetInt("Tutorial") > 1)
        {
            this.enabled = false;
        }
        else
        {
            DoTutorial();
        }
    }

    private void DoTutorial()
    {
        switch (order)
        {
            case 1:
                tutorialText.text = "pick up food";
                RenderLine(playerLocation.position, pickupFoodLocation.position);
                if (Vector3.Distance(playerLocation.position, pickupFoodLocation.position) < 1.5f)
                {
                    order = 2;
                }
                break;
            case 2:
                tutorialText.text = "deliver food";
                RenderLine(playerLocation.position, deliverFoodLocation.position);
                if (Vector3.Distance(playerLocation.position, deliverFoodLocation.position) < 2f)
                {
                    order = 3;
                }
                break;
            case 3:
                if (gameManager.money >= 60)
                {
                    tutorialText.text = "buy worker";
                    lineRenderer.enabled = true;
                    RenderLine(playerLocation.position, buyWorkerLocation.position);
                    if (Vector3.Distance(playerLocation.position, buyWorkerLocation.position) < 2f)
                    {
                        order = 4;
                        lineRenderer.enabled = false;
                        Debug.Log("Go to 4");
                    }
                }
                else
                {
                    lineRenderer.enabled = false;
                    tutorialText.text = "";
                }
                break;
            case 4:
                if (gameManager.money >= 110)
                {
                    tutorialText.text = "buy table";
                    lineRenderer.enabled = true;
                    RenderLine(playerLocation.position, buyTableLocation.position);
                    if (Vector3.Distance(playerLocation.position, buyTableLocation.position) < 2f)
                    {
                        Debug.Log("DISABLING THIS GAME OBJECT BY ORDER 4");
                        //PlayerPrefs.SetInt("Tutorial", 1);
                        order = 5;
                        gameObject.SetActive(false);
                        tutorialText.text = "";
                    }
                }
                else
                {
                    lineRenderer.enabled = false;
                    tutorialText.text = "";
                }
                break;
            default:
                
                break;
        }

    }

    private void RenderLine(Vector3 position1, Vector3 position2)
    {
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, position1);
        lineRenderer.SetPosition(1, position2);
    }
}
