using UnityEngine;

public class ScaleBallBuff : BallBuff
{
    readonly private float _scaleBonusValue = 0.5f;
    private Vector2 _scaledBonus = Vector2.zero;
    override public void ApplyBuff()
    {
        _scaledBonus = BallTarget.CurrentScale * _scaleBonusValue;
        BallTarget.CurrentScale -= _scaledBonus;
        BallTarget.transform.localScale = BallTarget.CurrentScale;
    }

    override public void CancelBuff()
    {
        BallTarget.CurrentScale -= _scaledBonus;
        BallTarget.transform.localScale = BallTarget.CurrentScale;
    }
}
