using UnityEngine;

public class BallReverseAbility : Iability
{
    private int _coolDown = 30;
    public void ActivateAbility(Player target)
    {
        Ball ball = target.GetBallReference();
        Rigidbody2D ballRigidBody = ball.GetComponent<Rigidbody2D>();
        ballRigidBody.velocity *= -1;
        ball.ChangeDirectionAction?.Invoke();
    }

    public int GetCooldown() => _coolDown;
}