using Godot;
using System;

public interface IState<TNode>
{
    void Enter(TNode node);
    void Exit(TNode node);
    string Update(TNode node, float delta);
    string HandleInput(TNode node, InputEvent inputEnvent);
    string StateName {get;}
}
