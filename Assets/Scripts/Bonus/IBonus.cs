using UnityEngine;

public interface IBonus
{
    public void Accept(IVisitor visitor);
}
