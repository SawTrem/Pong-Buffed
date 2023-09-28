using UnityEngine;

public class BallReverseAbility : MonoBehaviour, Iability
{
    private int _coolDown = 30;
    public void ActivateAbility(Player target)
    {
        Rigidbody2D ballRigidBody = target.GetBallReference().GetComponent<Rigidbody2D>();
        ballRigidBody.velocity *= -1;
    }

    public int GetCooldown() => _coolDown;
}