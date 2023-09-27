using UnityEngine;

[RequireComponent(typeof(CircleCollider2D),typeof(Rigidbody2D))]
public class Ball : MonoBehaviour
{
    [SerializeField] private float _movementSpeed = 5.0f;
    private Vector2 _currentDirection = Vector2.right;
    private Rigidbody2D _rigidBody;
    private Vector2 _currentVector;

    [SerializeField]private SoundPlayer _soundPlayer;

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _currentVector = _currentDirection * _movementSpeed;
        _rigidBody.velocity = _currentVector;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        _soundPlayer.PlayHitSoundAction.Invoke();
        float yvalue = 0.0f;
        if (collision.gameObject.GetComponent<Player>())
        {
            _currentDirection *= -1;
            yvalue = ( this.transform.position.y - collision.transform.position.y ) / collision.collider.bounds.size.y;
            _currentVector = new Vector2(_currentDirection.x, yvalue);
        }
        else
            _currentVector.y *= -1; 
        _rigidBody.velocity =_currentVector * _movementSpeed;
    }
}