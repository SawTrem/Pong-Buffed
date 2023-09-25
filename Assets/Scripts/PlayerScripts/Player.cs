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
    [SerializeField] private PlayerAxes _currentAxis  = PlayerAxes.Vertical; 
    private string[] axisMode = { "Vertical","Vertical2"};

    private Rigidbody2D _rigidBody;

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();        
    }

    private void Update()
    {
        _rigidBody.velocity = Vector2.up * Input.GetAxis(axisMode[(int)_currentAxis]) * _movementSpeed;
    }
}