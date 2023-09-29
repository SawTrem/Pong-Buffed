using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BallBuff : IBuff
{
    public Ball BallTarget { get; set; }

    public void Accept(IVisitor visitor)
    {
        visitor.VisitBallBuff(this);
    }

    abstract public void ApplyBuff();
    abstract public void CancelBuff(); 
}