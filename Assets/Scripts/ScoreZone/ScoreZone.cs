using UnityEngine;

public class ScoreZone : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private PlayerSide _playerSide = PlayerSide.rightPlayer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Ball>()) _gameManager.UpdatePlayerScore?.Invoke(_playerSide);
    }
}