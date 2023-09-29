using TMPro;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName =" cards settings ")]
public class AbilityCardSettings : ScriptableObject
{
    public string abilityName;
    public Image abilityImage;
    public IBonus bonus;
}