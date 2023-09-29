using UnityEngine;

public interface IBuff :IBonus
{
    public void ApplyBuff();
    public void CancelBuff();
}
