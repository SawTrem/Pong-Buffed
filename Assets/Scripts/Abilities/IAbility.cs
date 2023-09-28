using UnityEngine;

public interface Iability
{
    public void ActivateAbility(Player target);
    public int GetCooldown();
}