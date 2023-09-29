using System;
using UnityEngine;

public enum PlayerSide
{
    rightPlayer,
    leftPlayer
}

public class GameManager : MonoBehaviour, IVisitor
{
    [SerializeField] private Ball _ball;
    [SerializeField] private Player _rightPlayer;
    [SerializeField] private Player _leftPlayer;

    [SerializeField] private Vector3 _ballStartPosition;

    [SerializeField] private UIManager _uiManger;

    private int _rightPlayerScore = 0;
    private int _leftPlayerScore = 0;

    private int _playedRounds = 0;
    readonly private int _needsToPlay = 10;


    public Action<PlayerSide> UpdatePlayerScore;

    public static Action PauseGameAction;
    public Action ResumeGameAction;

    private void Awake()
    {
        _rightPlayer.SetAbility(new BallDashAbility());
        _leftPlayer.SetAbility(new BallReverseAbility());
        ScaleBallBuff scalePlayerBuff = new()
        {
            BallTarget = _ball
        };
        _rightPlayer.SetBuff(scalePlayerBuff);
    }

    private void StartGame() 
    { 
    
    }

    private void ResetPositions() 
    {
        _ball.transform.position = Vector2.zero;
    }

    private void PauseGame() 
    {
        Time.timeScale = 0f;
        _uiManger.ShowPauseAction?.Invoke();
    }

    private void ResumeGame() 
    {
        Time.timeScale = 1f;
    }

    private void UpdateScore(PlayerSide playerType) 
    {
        if (playerType is PlayerSide.rightPlayer) _rightPlayerScore++;
        else _leftPlayerScore++;
        _playedRounds++;
        _uiManger.UpdateGameDataAction?.Invoke(_playedRounds, _leftPlayerScore, _rightPlayerScore);
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
        _uiManger.ResetGameDataAction?.Invoke();
    }

    public void VisitMapBuff()
    {
        throw new NotImplementedException();
    }

    public void VisitBallBuff()
    {
        throw new NotImplementedException();
    }

    public void VisitPlayerBuff()
    {
        throw new NotImplementedException();
    }

    private void OnEnable()
    {
        UpdatePlayerScore += UpdateScore;
        PauseGameAction += PauseGame;
        ResumeGameAction += ResumeGame;
    }
    private void OnDisable()
    {
        UpdatePlayerScore -= UpdateScore;
        PauseGameAction -= PauseGame;
        ResumeGameAction -= ResumeGame;
    }
}