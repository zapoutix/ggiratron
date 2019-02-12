using Godot;
using System;

public class Move : Node, IState<Giraffe>
{
    private Vector2 _screenSize;
    public Move()
    {
        
    }

    public string StateName => "MOVE";
    public void Enter(Giraffe node)
    {
        //Set anim to idle
        node.Animation = "Move";
        _screenSize = node.GetViewport().GetSize();
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
        var velocity = new Vector2();
        if (Input.IsActionPressed("ui_right"))
            velocity.x += 1;
        if (Input.IsActionPressed("ui_left"))
            velocity.x += -1;
        if (Input.IsActionPressed("ui_up"))
            velocity.y += -1;
        if (Input.IsActionPressed("ui_down"))
            velocity.y += 1;

        if (velocity.Length() > 0)
            velocity = velocity.Normalized() * node.SPEED;
        if (velocity == Vector2.Zero)
            return "IDLE";

        node.Position += velocity * delta;
        var posx = Mathf.Clamp(node.Position.x, 0, _screenSize.x);
        var posy = Mathf.Clamp(node.Position.y, 0, _screenSize.y);

        node.SetPosition(new Vector2(posx, posy));


        return null;
        
    }

}
