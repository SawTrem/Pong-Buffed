using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D),typeof(Rigidbody2D))]
public class Ball : MonoBehaviour,IBuffable
{
    [SerializeField] private SoundPlayer _soundHitPlayer;
    [SerializeField] private SoundPlayer _soundHurtPlayer;
    [SerializeField] private ParticleEmission _particleEmission;
    [SerializeField] private ShakeCamera _shakeCamera;

    readonly private float _shakeDuration = 0.1f;
    readonly private float _shakeAmount = 0.1f;
    readonly private float _defaultmovementSpeed = 10.0f;
    readonly private Vector2 _defaultScale = new Vector2(0.7f, 0.7f);
    public float CurrentMovementSpeed { get; set; } = 10.0f;
    public Vector2 CurrentScale { get; set; } = new Vector2(0.7f, 0.7f);

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
        _soundHitPlayer.PlaySoundAction.Invoke();
        _particleEmission.EmitParticleAction.Invoke();
        //StartCoroutine(_shakeCamera.Shake(_shakeDuration, _shakeAmount));
        float yvalue;
        if (collision.gameObject.GetComponent<Player>())
        {
            _currentDirection *= -1;
            yvalue = ( transform.position.y - collision.transform.position.y ) / collision.collider.bounds.size.y;
            _rigidBody.velocity = new Vector2(_currentDirection.x, yvalue) * CurrentMovementSpeed;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _soundHurtPlayer.PlaySoundAction.Invoke();
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