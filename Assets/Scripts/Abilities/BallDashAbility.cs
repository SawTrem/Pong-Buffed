using UnityEngine;

public class BallDashAbility : MonoBehaviour, Iability
{
    private float _dashBall = 2.0f;

    private int _cooldown = 5;

    public void ActivateAbility(Player target)
    {
        Rigidbody2D rigidbody2D = target.GetBallReference().GetComponent<Rigidbody2D>();
        rigidbody2D.AddForce(rigidbody2D.velocity * _dashBall, ForceMode2D.Impulse);
    }

    public int GetCooldown() => _cooldown;
}