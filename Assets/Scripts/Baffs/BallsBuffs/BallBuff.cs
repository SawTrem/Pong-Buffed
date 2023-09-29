using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BallBuff : IBuff
{
    public Ball BallTarget { get; set; }
    abstract public void ApplyBuff();
    abstract public void CancelBuff(); 
}