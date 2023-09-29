using UnityEngine;

public class ScalePlayerBuff : PlayerBuff
{
    private float _scaleBonusValue = 0.2f;
    private Vector2 _scaledBonus = Vector2.zero;
    override public void ApplyBuff()
    {
        _scaledBonus.y = PlayerTarget.CurrentScale.y * _scaleBonusValue;
        PlayerTarget.CurrentScale += _scaledBonus;
        PlayerTarget.transform.localScale = PlayerTarget.CurrentScale;
    }
    override public void CancelBuff() 
    {
        PlayerTarget.CurrentScale -= _scaledBonus;
        PlayerTarget.transform.localScale = PlayerTarget.CurrentScale;
    }   
}
