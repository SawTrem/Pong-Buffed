using UnityEngine;

public class SpeedPlayerBuff: PlayerBuff
{
    readonly private float _speedBonusValue = 0.2f;
    private float _speedBonus;
    override public void ApplyBuff() 
    {
        _speedBonus = PlayerTarget.CurrentMovementSpeed * _speedBonusValue;
        PlayerTarget.CurrentMovementSpeed += _speedBonus;
    }

    override public void CancelBuff()
    {
        PlayerTarget.CurrentMovementSpeed -= _speedBonus;
    }
}