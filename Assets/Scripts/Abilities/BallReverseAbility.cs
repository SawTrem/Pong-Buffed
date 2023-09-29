using UnityEngine;

public class BallReverseAbility : Iability
{
    readonly private int _coolDown = 30;

    public void Accept(IVisitor visitor)
    {
        visitor.VisitPlayerAbility(this);
    }

    public void ActivateAbility(Player target)
    {
        Ball ball = target.GetBallReference();
        Rigidbody2D ballRigidBody = ball.GetComponent<Rigidbody2D>();
        Vector2 vector2 = ballRigidBody.velocity;
        vector2.y *= -1;
        ballRigidBody.velocity = vector2;
        //ball.ChangeDirectionAction?.Invoke();
    }

    public int GetCooldown() => _coolDown;
}