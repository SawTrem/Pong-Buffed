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
    readonly private int _needsToPlay = 8;


    public Action<PlayerSide> UpdatePlayerScore;
    public static Action PauseGameAction;
    public Action ResumeGameAction;
    public Action<IBonus> SetAbilityAction;

    private void Awake()
    {
        /*_rightPlayer.SetAbility(new BallDashAbility());
        _leftPlayer.SetAbility(new BallReverseAbility());
        ScaleBallBuff scalePlayerBuff = new()
        {
            BallTarget = _ball
        };
        _ball.SetBuff(scalePlayerBuff);*/
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
        if(_playedRounds % 3 == 0) StartAbilityMenu();
        if (_needsToPlay == _playedRounds) { 
           RestartGame();
        }
    }

    private void StartAbilityMenu()
    {
        Time.timeScale = 0f;
        _uiManger.ShowAbilityMenuAction?.Invoke();
    }

    private void SetAbility(IBonus bonus) 
    {
        bonus.Accept(this);
        Time.timeScale = 1f;
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

    public void VisitBallBuff(BallBuff ballBuff)
    {
        Debug.Log("BallBuff");
        ballBuff.BallTarget = _ball;
        _ball.SetBuff(ballBuff);
    }

    public void VisitPlayerBuff(PlayerBuff playerBuff)
    {
        Debug.Log("playerBuff");
        playerBuff.PlayerTarget = _leftPlayer;
        _leftPlayer.SetBuff(playerBuff);
    }

    public void VisitPlayerAbility(Iability ability) 
    {
        Debug.Log("ability");
        _leftPlayer.SetAbility(ability);
    }

    private void OnEnable()
    {
        UpdatePlayerScore += UpdateScore;
        PauseGameAction += PauseGame;
        ResumeGameAction += ResumeGame;
        SetAbilityAction += SetAbility;
    }
    private void OnDisable()
    {
        UpdatePlayerScore -= UpdateScore;
        PauseGameAction -= PauseGame;
        ResumeGameAction -= ResumeGame;
        SetAbilityAction -= SetAbility;
    }
}