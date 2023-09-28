using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D), typeof(Rigidbody2D))]
public class Player : MonoBehaviour,IAbilitable
{
    [SerializeField] private float _movementSpeed = 2.0f;
    private Iability _ability = new BallReverseAbility();

    [SerializeField] private Ball _ball;

    private Rigidbody2D _rigidBody;

    private bool _isAbleToUseAbility = true;

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    public void Move(Vector2 direction) 
    { 
        _rigidBody.velocity = direction * _movementSpeed;
    }

    public void UseAbility() 
    {
        if(_isAbleToUseAbility)
        _ability?.ActivateAbility(this);
        StartCoroutine(CoolDownAbility(_ability.GetCooldown()));
    }

    IEnumerator CoolDownAbility(int coolDownSeconds)
    {
        _isAbleToUseAbility = false;
        yield return new WaitForSeconds(coolDownSeconds);
        _isAbleToUseAbility = true;
    }

    public Ball GetBallReference() => _ball;

    public void SetAbility(Iability ability)
    {
        this._ability = ability;
    }

    public void RemoveAbility()
    {
        this._ability = null;
    }
}