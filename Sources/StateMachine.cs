using Godot;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class StateMachine<TNode>
{
    IState<TNode> _currentSate;
    IEnumerable<IState<TNode>> _availibleStates;
    TNode _node;

    public StateMachine(IEnumerable<IState<TNode>> availibleStates, IState<TNode> defaultState, TNode node)
    {
        _currentSate = defaultState;
        _availibleStates = availibleStates;
        _node = node;
    }

    public StateMachine()
    {
        _currentSate.Enter(_node);
    }

    public void Update(float delta)
    {
        if (_currentSate != null)
        {
            var newState = _currentSate.Update(_node, delta);

            //Check change state
            if (newState != null)
                ChangeState(newState);
        }
    }

    private void ChangeState(string newState)
    {
        var state = _availibleStates.FirstOrDefault(o => o.StateName.ToLowerInvariant() == newState.ToLowerInvariant());

        if (state == null)
            return;

        if (state == _currentSate)
            return;

        _currentSate.Exit(_node);
        _currentSate = state;
        state.Enter(_node);
    }

    public void HandleInput(InputEvent inputEnvent)
    {
        if (_currentSate != null)
        {
            var newState = _currentSate.HandleInput(_node, inputEnvent);

            //Check change state
            if (newState != null)
                ChangeState(newState);
        }
    }
} 
