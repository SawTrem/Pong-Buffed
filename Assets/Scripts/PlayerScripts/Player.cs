using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(BoxCollider2D), typeof(Rigidbody2D))]
public class Player : MonoBehaviour,IAbilitable,IBuffable
{
    [SerializeField] private Ball _ball;

    readonly private float _defaultMovementSpeed = 15;
    readonly private Vector2 _defaultScale = Vector2.one;
    public float CurrentMovementSpeed {get; set;}
    public Vector2 CurrentScale { get; set; }

    private Rigidbody2D _rigidBody;

    private Iability _ability = new BallDashAbility();
    private bool _isAbleToUseAbility = true;

    readonly private List<IBuff> _listOfBuffs = new();

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        CurrentMovementSpeed = _defaultMovementSpeed;
        CurrentScale = _defaultScale;
        transform.localScale = CurrentScale;
    }

    public void Move(Vector2 direction) 
    { 
        _rigidBody.velocity = direction * CurrentMovementSpeed;
    }

    public void UseAbility() 
    {
        if (!_isAbleToUseAbility) return;
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
}