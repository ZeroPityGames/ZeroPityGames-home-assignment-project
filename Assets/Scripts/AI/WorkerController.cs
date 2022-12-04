using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class WorkerController : MonoBehaviour
{
    public string specificCustomerTag;
    public State currentWorkerState;

    public bool hasFoodInHand;
    public float carryCapacity = 1;
    public float carryAmount;
    public float upgradeLevel;

    public GameObject myCustomer;

    int i;

    NavMeshAgent navMeshAgent;

    private float startingSpeed;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
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
        upgradeLevel++;
        navMeshAgent.speed += startingSpeed * 0.01f;
        if (upgradeLevel % 10 == 0)
        {
            carryCapacity++;
        }
        
    }
}
