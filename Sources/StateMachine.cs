using Godot;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class StateMachine<TNode>
where TNode : Node
{
    IState<TNode> _currentSate;
    IEnumerable<IState<TNode>> _availibleStates;
    TNode _node;
    Stack<IState<TNode>> _historics; 

    public StateMachine(IEnumerable<IState<TNode>> availibleStates, IState<TNode> defaultState, TNode node)
    {
        _historics = new Stack<IState<TNode>>();

        _currentSate = defaultState;
        _availibleStates = availibleStates;
        _node = node;
    }

    public void Start()
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
        IState<TNode> state;

        if (newState.ToLowerInvariant() == "previous")
        {
            if (!_historics.Any())
                return;
            state = _historics.Pop();
        }
        else
        {
            state = _availibleStates.FirstOrDefault(o => o.StateName.ToLowerInvariant() == newState.ToLowerInvariant());

            if (state == null)
                return;

            if (state == _currentSate)
                return;
        }


        _currentSate.Exit(_node);
        _historics.Push(_currentSate);
        _currentSate = state;
        GD.Print("Change State :" + state.StateName);
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
