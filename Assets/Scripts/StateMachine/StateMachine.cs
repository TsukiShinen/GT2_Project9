using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine<T>
{
    private IState<T> _currentState;

    private T Entity;

    public void Init(T entity)
    {
        Entity = entity;
    }

    public void Update()
    {
        if (_currentState == null) { return; }
        ChangeState(_currentState.Handle(Entity));
        _currentState.Update(Entity);
    }

    public void FixedUpdate()
    {
        if (_currentState == null) { return; }
        _currentState.FixedUpdate(Entity);
    }

    public void ChangeState(IState<T> newState)
    {
        if (_currentState == newState) { return; }
        if (_currentState != null)
        {
            _currentState.Exit(Entity);
        }
        _currentState = newState;
        _currentState.Enter(Entity);
    }
}
