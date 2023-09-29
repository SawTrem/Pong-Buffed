using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D),typeof(Rigidbody2D))]
public class Ball : MonoBehaviour,IBuffable
{
    [SerializeField] private SoundPlayer _soundPlayer;
    [SerializeField]private ParticleSystem _particleSystem;
    
    readonly private float _defaultmovementSpeed = 7.0f;
    readonly private Vector2 _defaultScale = Vector2.one;
    public float CurrentMovementSpeed { get; set; } = 7.0f;
    public Vector2 CurrentScale { get; set; } = Vector2.one;

    private Vector2 _currentDirection = Vector2.right;
    private Rigidbody2D _rigidBody;

    readonly private List<IBuff> buffs = new();

    public Action ChangeDirectionAction;


    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _rigidBody.velocity = _currentDirection * CurrentMovementSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _soundPlayer.PlayHitSoundAction.Invoke();
        _particleSystem.Play();
        float yvalue;
        if (collision.gameObject.GetComponent<Player>())
        {
            _currentDirection *= -1;
            yvalue = ( transform.position.y - collision.transform.position.y ) / collision.collider.bounds.size.y;
            _rigidBody.velocity = new Vector2(_currentDirection.x, yvalue) * CurrentMovementSpeed;
        }
    }

    private void ChangeDirection() => _currentDirection *= -1;

    public void SetBuff(IBuff buff)
    {
        buffs.Add(buff);
        buff.ApplyBuff();
    }

    public void RemoveBuff(IBuff buff)
    {
        if (!buffs.Contains(buff)) return;
        buffs.Remove(buff);
        buff.CancelBuff();
    }

    public void RestoreToDefaults()
    {
        buffs.Clear();
        CurrentMovementSpeed = _defaultmovementSpeed;
        CurrentScale = _defaultScale;
        transform.localScale = CurrentScale;
        _rigidBody.velocity = _currentDirection;
    }
    public Vector2 GetCurrentDirection() => _currentDirection;
    private void OnEnable() => ChangeDirectionAction += ChangeDirection;

    private void OnDisable() => ChangeDirectionAction -= ChangeDirection;
}