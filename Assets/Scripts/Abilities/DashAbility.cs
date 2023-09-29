using UnityEngine;

public class DashAbility : Iability
{
    [SerializeField] private float _dashPower = 30.0f;

    private int _coolDown = 3;

    public void Accept(IVisitor visitor)
    {
        visitor.VisitPlayerAbility(this);
    }

    public void ActivateAbility(Player target)
    {
        Rigidbody2D targetrigitBody = target.GetComponent<Rigidbody2D>();
        targetrigitBody.velocity *= _dashPower;
    }

    public int GetCooldown() => _coolDown;
}