using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    private PlayerSide _lastLostPlayer;


    public Action<PlayerSide> UpdatePlayerScore;
    public static Action PauseGameAction;
    public Action ResumeGameAction;
    public Action<IBonus> SetAbilityAction;
    public Action RestartGameAction;
    public Action ExitAction;

    private void PauseGame()
    {
        Time.timeScale = 0f;
        _uiManger.ShowPauseAction?.Invoke();
    }

    private void ResumeGame()
    {
        Time.timeScale = 1f;
    }
    
    private void ResetPositions() 
    {
        _ball.transform.position = Vector2.zero;
        _ball.ChangeDirectionAction?.Invoke();

        _ball.GetComponent<Rigidbody2D>().velocity = _ball.GetCurrentDirection() * _ball.CurrentMovementSpeed;

        _leftPlayer.transform.position = new Vector3(_leftPlayer.transform.position.x, 0, 
            _leftPlayer.transform.position.z);
        _rightPlayer.transform.position = new Vector3(_rightPlayer.transform.position.x, 0,
            _rightPlayer.transform.position.z);
    }

    private void StopGame()
    {
        Time.timeScale = 0f;
        _uiManger.ShowEndGameAction?.Invoke();
    }

    private void RestartGame()
    {
        ResetGameData();
        ResetPositions();
        Time.timeScale = 1f;
    }

    private void ResetGameData()
    {
        _playedRounds = 0;
        _leftPlayerScore = 0;
        _rightPlayerScore = 0;
        _uiManger.ResetGameDataAction?.Invoke();
        _ball.RestoreToDefaults();
        _leftPlayer.RestoreToDegaults();
        _rightPlayer.RestoreToDegaults();
    }

    private void ExitGame() 
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    private void UpdateScore(PlayerSide playerType) 
    {
        _lastLostPlayer = playerType;
        if (playerType is PlayerSide.rightPlayer) _rightPlayerScore++;
        else _leftPlayerScore++;

        _playedRounds++;
        _uiManger.UpdateGameDataAction?.Invoke(_playedRounds, _leftPlayerScore, _rightPlayerScore);
        
        ResetPositions();
        if(_playedRounds % 3 == 0) StartAbilityMenu();
        if (_needsToPlay == _playedRounds) { 
           StopGame();
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

    public void VisitMapBuff()
    {
        throw new NotImplementedException();
    }

    public void VisitBallBuff(BallBuff ballBuff)
    {
        ballBuff.BallTarget = _ball;
        _ball.SetBuff(ballBuff);
    }

    public void VisitPlayerBuff(PlayerBuff playerBuff)
    {
        var player = GetBonusablePlayer();
        playerBuff.PlayerTarget = player;
        player.SetBuff(playerBuff);
    }

    public void VisitPlayerAbility(Iability ability) 
    {
        var player = GetBonusablePlayer();
        player.SetAbility(ability);
    }

    private Player GetBonusablePlayer() 
    {
        if (_leftPlayerScore < _rightPlayerScore) return _leftPlayer;
        if (_leftPlayerScore > _rightPlayerScore)return _rightPlayer;
        if (_lastLostPlayer == PlayerSide.leftPlayer) return _leftPlayer;
        else return _rightPlayer;
    }

    private void OnEnable()
    {
        UpdatePlayerScore += UpdateScore;
        PauseGameAction += PauseGame;
        ResumeGameAction += ResumeGame;
        SetAbilityAction += SetAbility;
        RestartGameAction += RestartGame;
        ExitAction += ExitGame;
    }
    private void OnDisable()
    {
        UpdatePlayerScore -= UpdateScore;
        PauseGameAction -= PauseGame;
        ResumeGameAction -= ResumeGame;
        SetAbilityAction -= SetAbility;
        RestartGameAction -= RestartGame;
        ExitAction -= ExitGame;
    }
}