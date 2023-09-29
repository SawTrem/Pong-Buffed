using UnityEngine;

public abstract class PlayerBuff : IBuff
{
    public Player PlayerTarget { get; set;}

    abstract public void ApplyBuff();
    abstract public void CancelBuff();
}