using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class WorkerController : MonoBehaviour
{
    public string specificCustomerTag;
    public State currentWorkerState;
    private GameManager gameManager;

    public bool hasFoodInHand;
    public float carryCapacity = 1;
    public float carryAmount;
    public float upgradeLevel;
    public float startingUpgradePrice;
    private float currentPrice;

    public GameObject myCustomer;

    int i;

    NavMeshAgent navMeshAgent;

    private float startingSpeed;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        gameManager = FindObjectOfType<GameManager>();
        startingSpeed = navMeshAgent.speed;
    }

    private void Update()
    {
        RunStateMachine();

    }

    private void RunStateMachine()
    {
        State nextState = currentWorkerState?.RunCurrentState();

        if (nextState != null)
        {
            SwitchToNextState(nextState);
        }
    }

    private void SwitchToNextState(State nextState)
    {
        currentWorkerState = nextState;
    }

    public void LevelUpWorker()
    {
        Debug.Log("Worker is faster");
        currentPrice = (int)startingUpgradePrice * Mathf.Pow(1.15f,upgradeLevel);
        Debug.Log(currentPrice);
        if (gameManager.money >= currentPrice)
        {
            gameManager.DecressMoney((int)currentPrice);
            upgradeLevel++;
            navMeshAgent.speed += startingSpeed * 0.01f;
            if (upgradeLevel % 10 == 0)
            {
                carryCapacity++;
            }
        }
        else
        {
            Debug.Log("NotEnaughMoney");
        }
        
        
    }
}
