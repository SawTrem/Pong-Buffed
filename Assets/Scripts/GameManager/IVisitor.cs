using UnityEngine;

public interface IVisitor
{
    void VisitMapBuff();
    void VisitBallBuff(BallBuff ballBuff);
    void VisitPlayerBuff(PlayerBuff playerBuff);

    void VisitPlayerAbility(Iability ability); 
}
