using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class AbilityCardGenerator : MonoBehaviour
{
    [SerializeField] private List<AbilityCardSettings> abilityCardList;
    private int _sizeOfList;
    private void Start()
    {
        abilityCardList[0].bonus = new BallDashAbility();
        abilityCardList[1].bonus = new BallReverseAbility();
        abilityCardList[2].bonus = new ScaleBallBuff();
        abilityCardList[3].bonus = new ScaleBallBuff();
        abilityCardList[4].bonus = new DashAbility();
        abilityCardList[5].bonus = new ScalePlayerBuff();
        abilityCardList[6].bonus = new SpeedPlayerBuff();
        _sizeOfList = abilityCardList.Count - 1;
        ShuffleList();
    }
    private void ShuffleList() 
    {
        int n = _sizeOfList;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0,n);
            var value = abilityCardList[k];
            abilityCardList[k] = abilityCardList[n];
            abilityCardList[n] = value;
        }
    }
    public AbilityCardSettings GetVisual(int index) => abilityCardList[index];

    public IBonus GetBonus(int index) 
    {
        var result = abilityCardList[index].bonus;
        this.SwitchElements(index);
        return result;
    }
    private void SwitchElements(int index) 
    {
        var buf = abilityCardList[index];
        abilityCardList[index] = abilityCardList[_sizeOfList];
        abilityCardList[_sizeOfList] = buf;
        _sizeOfList --;
    }

}