using UnityEngine;

public class SpeedBallBuff : BallBuff
{
    private float _speedBonusValue = 2.2f;
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
