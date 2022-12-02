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

    public GameObject myCustomer;
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
}
