using UnityEngine;

public class BallDashAbility : Iability
{
    readonly private float _dashBall = 2.0f;

    readonly private int _cooldown = 5;

    public void Accept(IVisitor visitor)
    {
        visitor.VisitPlayerAbility(this);
    }

    public void ActivateAbility(Player target)
    {
        Rigidbody2D rigidbody2D = target.GetBallReference().GetComponent<Rigidbody2D>();
        rigidbody2D.AddForce(rigidbody2D.velocity * _dashBall, ForceMode2D.Impulse);
    }

    public int GetCooldown() => _cooldown;
}