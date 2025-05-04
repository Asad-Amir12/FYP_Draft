using UnityEngine;

public abstract class EnemyStateMachine<T> : MonoBehaviour
{
    protected EnemyState<T> CurrentState;

    protected void TransitionToState(EnemyState<T> newState)
    {
        CurrentState?.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }

    protected virtual void Update()
    {
        CurrentState?.Tick();
    }

    protected virtual void FixedUpdate()
    {
        CurrentState?.FixedTick();
    }
}