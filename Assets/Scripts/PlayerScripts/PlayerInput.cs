using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private PlayerSide _currentPlayerSide = PlayerSide.rightPlayer;
    [SerializeField] private KeyCode _actionButton = KeyCode.LeftShift;
    private string[] axisMode = { "Vertical", "Vertical2" };


    private void Update()
    {
        _player.Move(Vector2.up * Input.GetAxis(axisMode[(int)_currentPlayerSide]));
    }
}