using Godot;
using System;
using System.Collections.Generic;

public class Giraffe : AnimatedSprite
{
    StateMachine<Giraffe> _stateMachine;
    
    [Export]
    public int SPEED = 200;
    public override void _Ready()
    {
        var idleState = new Idle();
        var moveState = new Move();
        _stateMachine = new StateMachine<Giraffe>(new List<IState<Giraffe>>
        {
            idleState,
            moveState
        }, idleState, this);

        _stateMachine.Start();
        GD.Print("Hello from giraffe");
    }

   public override void _Process(float delta) => _stateMachine.Update(delta);

   public override void _UnhandledInput(InputEvent @event) => _stateMachine.HandleInput(@event);
}
