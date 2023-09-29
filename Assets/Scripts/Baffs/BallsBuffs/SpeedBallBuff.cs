using UnityEngine;

public class SpeedBallBuff : BallBuff
{
    readonly private float _speedBonusValue = 0.2f;
    private float _speedBonus = 0.0f;
    override public void ApplyBuff()
    {
        _speedBonus = BallTarget.CurrentMovementSpeed * _speedBonusValue;
        BallTarget.CurrentMovementSpeed += _speedBonus;
    }

    override public void CancelBuff()
    {
        BallTarget.CurrentMovementSpeed -= _speedBonus;
    }
}
