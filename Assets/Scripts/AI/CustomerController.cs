using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CustomerController : MonoBehaviour
{
    public Vector3 chairLocation;
    public Vector3 exitLocation; //Make a tag with the exit location later on
    public bool canMoveToChair;

    public State currentCustomerState;

    public Canvas orderIndicator;
    public Canvas eatingIndicator;

    public bool hasWorker;

    [HideInInspector] public Chair customersChair;

    NavMeshAgent navMeshAgent;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        RunStateMachine();

    }

    private void RunStateMachine()
    {
        State nextState = currentCustomerState?.RunCurrentState();

        if (nextState != null)
        {
            SwitchToNextState(nextState);
        }
    }

    private void SwitchToNextState(State nextState)
    {
        currentCustomerState = nextState;
    }

}
