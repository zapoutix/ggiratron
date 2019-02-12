using Godot;
using System;

public interface IState<TNode>
 where TNode : Node 
{
    void Enter(TNode node);
    void Exit(TNode node);
    string Update(TNode node, float delta);
    string HandleInput(TNode node, InputEvent inputEnvent);
    string StateName {get;}
}
