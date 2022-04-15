using UnityEngine;

public class StateMachine<T> : MonoBehaviour
{
    private IState<T> _currentState;

    private T _entity;

    public void Init(T entity)
    {
        _entity = entity;
    }

    private void Update()
    {
        if (_currentState == null) { return; }
        ChangeState(_currentState.Handle(_entity));
        _currentState.Update(_entity);
    }

    private void FixedUpdate()
    {
        if (_currentState == null) { return; }
        _currentState.FixedUpdate(_entity);
    }

    public void ChangeState(IState<T> newState)
    {
        if (_currentState == newState) { return; }
        if (_currentState != null)
        {
            _currentState.Exit(_entity);
        }
        _currentState = newState;
        _currentState.Enter(_entity);
    }
}
