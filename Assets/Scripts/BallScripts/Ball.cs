using System;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D),typeof(Rigidbody2D))]
public class Ball : MonoBehaviour
{
    [SerializeField] private SoundPlayer _soundPlayer;
    
    readonly private float _defaultmovementSpeed = 7.0f;
    readonly private Vector2 _defaultScale = Vector2.one;
    public float CurrentMovementSpeed { get; set; } = 7.0f;
    public Vector2 CurrentScale { get; set; } = Vector2.one;

    private Vector2 _currentDirection = Vector2.right;
    private Rigidbody2D _rigidBody;

    public Action ChangeDirectionAction;

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _rigidBody.velocity = _currentDirection * CurrentMovementSpeed;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        _soundPlayer.PlayHitSoundAction.Invoke();
        float yvalue = 0.0f;
        if (collision.gameObject.GetComponent<Player>())
        {
            _currentDirection *= -1;
            yvalue = ( this.transform.position.y - collision.transform.position.y ) / collision.collider.bounds.size.y;
            _rigidBody.velocity = new Vector2(_currentDirection.x, yvalue) * CurrentMovementSpeed;
        }
    }

    private void ChangeDirection() => _currentDirection *= -1;

    private void OnEnable() => ChangeDirectionAction += ChangeDirection; 
    
    private void OnDisable() => ChangeDirectionAction -= ChangeDirection;
}