using UnityEngine;
public enum PlayerAxes
{
    Vertical,
    Vertical2
}

[RequireComponent(typeof(BoxCollider2D), typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [SerializeField] private float _movementSpeed = 5.0f;

    private Rigidbody2D _rigidBody;

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();        
    }

    public void Move(Vector2 direction) 
    { 
        _rigidBody.velocity = direction * _movementSpeed;
    }
}