using UnityEngine;
using System.Collections;

public class StateMachine : MonoBehaviour
{
    public State startingState;

    private State currentState;
    private State previousState;

    void Start()
    {
        currentState = startingState;
    }

	void Update ()
	{
        currentState?.OnUpdate(gameObject);
	}

    public void ChangeToState(State newState)
    {
        previousState = currentState;
        currentState.OnExit(gameObject);
        currentState = newState;
        currentState?.OnEnter(gameObject);
    }

    public void RevertToPreviousState()
    {
        ChangeToState(previousState);
    }
}
