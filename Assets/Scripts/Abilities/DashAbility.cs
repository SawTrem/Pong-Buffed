using UnityEngine;

public class DashAbility : MonoBehaviour, Iability
{
    [SerializeField] private float _dashPower = 2.0f;

    private int _coolDown = 3;
    public void ActivateAbility(Player target)
    {
        Rigidbody2D targetrigitBody = target.GetComponent<Rigidbody2D>();
        targetrigitBody?.AddForce(targetrigitBody.velocity * _dashPower,ForceMode2D.Impulse);
    }

    public int GetCooldown() => _coolDown;
}