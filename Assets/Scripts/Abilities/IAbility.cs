using UnityEngine;

public interface Iability:IBonus
{
    public void ActivateAbility(Player target);
    public int GetCooldown();
}