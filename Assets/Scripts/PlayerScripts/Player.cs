using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D), typeof(Rigidbody2D))]
public class Player : MonoBehaviour,IAbilitable,IBuffable
{
    [SerializeField] private Ball _ball;

    readonly private float _defaultMovementSpeed = 15.0f;
    readonly private Vector2 _defaultScale = Vector2.one;
    public float CurrentMovementSpeed { get; set; } = 15.0f;
    public Vector2 CurrentScale { get; set; } = Vector2.one;

    private Rigidbody2D _rigidBody;

    private Iability _ability;
    private bool _isAbleToUseAbility = true;

    readonly private List<IBuff> _listOfBuffs = new();

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        transform.localScale = CurrentScale;
    }

    public void Move(Vector2 direction) 
    { 
        _rigidBody.velocity = direction * CurrentMovementSpeed;
    }

    public void UseAbility() 
    {
        if (!_isAbleToUseAbility) return;
        if (_ability is null) return;
        _ability.ActivateAbility(this);
        StartCoroutine(CoolDownAbility(_ability.GetCooldown()));
    }

    IEnumerator CoolDownAbility(int coolDownSeconds)
    {
        Debug.Log("start");
        _isAbleToUseAbility = false;
        yield return new WaitForSeconds(coolDownSeconds);
        _isAbleToUseAbility = true;
        Debug.Log("end");
    }

    public Ball GetBallReference() => _ball;

    public void SetAbility(Iability ability) => _ability = ability;
    
    public void RemoveAbility() => _ability = null;
    

    public void SetBuff(IBuff buff)
    {
        _listOfBuffs.Add(buff);
        buff.ApplyBuff();
    }

    public void RemoveBuff(IBuff buff)
    {
        if (!_listOfBuffs.Contains(buff)) return;
        _listOfBuffs.Remove(buff);
        buff.CancelBuff();
    }
    public void RestoreToDegaults() 
    {
        _listOfBuffs.Clear();
        _ability = null;
        _isAbleToUseAbility = true;
        CurrentMovementSpeed = _defaultMovementSpeed;
        CurrentScale = _defaultScale;
        transform.localScale = CurrentScale;
    }
}