using System;
using UnityEngine;

public enum PlayerSide
{
    rightPlayer = 1,
    leftPlayer = 2
}

public class GameManager : MonoBehaviour
{
    [SerializeField] private Ball _ball;
    [SerializeField] private Player _rightPlayer;
    [SerializeField] private Player _leftPlayer;

    [SerializeField] private Vector3 _ballPosition;

    private int _rightPlayerScore = 0;
    private int _leftPlayerScore = 0;

    private int _playedRounds = 0;
    readonly private int _needsToPlay = 10;


    public Action<PlayerSide> UpdatePlayerScore;
    //[SrializeField]private UI basic ui; 

    private void StartGame() 
    { 
    
    }

    private void ResetPositions() 
    {
        _ball.transform.position = Vector2.zero;
    }

    private void PauseGame() 
    { 

    }

    private void UpdateScore(PlayerSide playerType) 
    {
        if (playerType is PlayerSide.rightPlayer) _rightPlayerScore++;
        else _leftPlayerScore++;
        _playedRounds++;
        //UI Update
        Debug.Log($"Score:{_leftPlayerScore}||{_rightPlayerScore} \n Played rounds: {_playedRounds}");
        ResetPositions();
        if (_needsToPlay == _playedRounds) { 
           RestartGame();
        }
    }

    private void RestartGame()
    {
        ResetGameData();
        ResetPositions();
    }
    private void ResetGameData() 
    {
        _playedRounds = 0;
        _leftPlayerScore = 0;
        _rightPlayerScore = 0;
        //UI reset
    }

    private void OnEnable()
    {
        UpdatePlayerScore += UpdateScore;
    }
    private void OnDisable()
    {
        UpdatePlayerScore -= UpdateScore;
    }
}