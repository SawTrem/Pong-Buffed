using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] Button _resumeButton;
    [SerializeField] Button _exitButton;
    [SerializeField] Button _restartButton;

    [SerializeField] TextMeshProUGUI _rightPLayerScore;
    [SerializeField] TextMeshProUGUI _leftPLayerScore;
    [SerializeField] TextMeshProUGUI _roundsCount;
    [SerializeField] TextMeshProUGUI _context;

    [SerializeField] GameManager _gameManager;

    public Action<int, int, int> UpdateGameDataAction;
    public Action ResetGameDataAction;
    public Action ShowPauseAction;
    private void UpdateGameData(int roundCount, int leftPlayerScore, int rightPlayerScore)
    {
        _rightPLayerScore.text = rightPlayerScore.ToString();
        _leftPLayerScore.text = leftPlayerScore.ToString();
        _roundsCount.text = roundCount.ToString();
    }
    private void ResetGameData()
    {
        _rightPLayerScore.text = 0.ToString();
        _leftPLayerScore.text = 0.ToString();
        _roundsCount.text = 0.ToString();
    }

    public void Resume()
    {
        _context.text = "";
        _context.gameObject.SetActive(false);
        _resumeButton.gameObject.SetActive(false);
        _exitButton.gameObject.SetActive(false);
        _gameManager.ResumeGameAction?.Invoke();
    }
    public void Exit()
    {

    }
    public void Restart()
    {
        
    }
    private void ShowPauseMenu() 
    {
        _context.text = "Pause";
        _context.gameObject.SetActive(true);
        _resumeButton.gameObject.SetActive(true);
        _exitButton.gameObject.SetActive(true);
    }
    private void OnEnable()
    {
        UpdateGameDataAction += UpdateGameData;
        ResetGameDataAction += ResetGameData;
        ShowPauseAction += ShowPauseMenu;
    }
    private void OnDisable() 
    {
        UpdateGameDataAction -= UpdateGameData;
        ResetGameDataAction -= ResetGameData;
        ShowPauseAction -= ShowPauseMenu;
    }
}