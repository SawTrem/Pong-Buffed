using UnityEngine;

public abstract class PlayerBuff : IBuff
{
    public Player PlayerTarget { get; set;}

    public void Accept(IVisitor visitor)
    {
        visitor.VisitPlayerBuff(this);
    }

    abstract public void ApplyBuff();
    abstract public void CancelBuff();

}