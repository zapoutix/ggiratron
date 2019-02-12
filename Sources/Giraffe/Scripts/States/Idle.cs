using Godot;
using System;

public class Idle : Node, IState<Giraffe>
{
    public string StateName => "IDLE";
    public void Enter(Giraffe node)
    {
        //Set anim to idle
        node.Animation = "Idle";
    }

    public void Exit(Giraffe node)
    {
    }

    public string HandleInput(Giraffe node, InputEvent inputEnvent)
    {
        return null;
    }

    public string Update(Giraffe node, float delta)
    {
        if (Input.IsActionPressed("ui_right"))
            return "MOVE";
        if (Input.IsActionPressed("ui_left"))
            return "MOVE";
        if (Input.IsActionPressed("ui_up"))
            return "MOVE";
        if (Input.IsActionPressed("ui_down"))
            return "MOVE";

        return null;
        
    }

}
