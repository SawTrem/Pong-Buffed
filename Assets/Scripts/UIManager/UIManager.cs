using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] Button _resumeButton;
    [SerializeField] Button _exitButton;
    [SerializeField] Button _restartButton;

    [SerializeField] Button _abilityCard1;
    [SerializeField] Button _abilityCard2;
    [SerializeField] Button _abilityCard3;

    [SerializeField] TextMeshProUGUI _rightPLayerScore;
    [SerializeField] TextMeshProUGUI _leftPLayerScore;
    [SerializeField] TextMeshProUGUI _roundsCount;
    [SerializeField] TextMeshProUGUI _context;

    [SerializeField] GameManager _gameManager;
    [SerializeField] AbilityCardGenerator _abilityCardGenerator;

    public Action<int, int, int> UpdateGameDataAction;
    public Action ResetGameDataAction;
    public Action ShowPauseAction;
    public Action ShowAbilityMenuAction;
    public Action ShowEndGameAction;

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

    private void ShowPauseMenu() 
    {
        _context.text = "Pause";
        _context.gameObject.SetActive(true);
        _resumeButton.gameObject.SetActive(true);
        _exitButton.gameObject.SetActive(true);
    }

    private void ShowAbilityMenu() 
    {
        SetVisualForCards(_abilityCard1 ,_abilityCardGenerator.GetVisual(0));
        SetVisualForCards(_abilityCard2, _abilityCardGenerator.GetVisual(1));
        SetVisualForCards(_abilityCard3, _abilityCardGenerator.GetVisual(2));
    }

    private void ShowEndMenu() 
    {
        _context.text = "game ended";
        _context.gameObject.SetActive(true);
        _restartButton.gameObject.SetActive(true);
        _exitButton.gameObject.SetActive(true);
    }

    private void SetVisualForCards(Button abilityCard,AbilityCardSettings abilityCardSettings) 
    {
        abilityCard.gameObject.SetActive(true);
        abilityCard.image = abilityCardSettings.abilityImage;
        abilityCard.GetComponentInChildren<TextMeshProUGUI>().text = abilityCardSettings.name;
    }

    public void SendAbilityToGameManager(int index) 
    {
        _abilityCard1.gameObject.SetActive(false);
        _abilityCard2.gameObject.SetActive(false);
        _abilityCard3.gameObject.SetActive(false);
        _gameManager.SetAbilityAction.Invoke(_abilityCardGenerator.GetBonus(index));
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
        _gameManager.ExitAction?.Invoke();
    }
    public void Restart()
    {
        _context.gameObject.SetActive(false);
        _restartButton.gameObject.SetActive(false);
        _exitButton.gameObject.SetActive(false);
        _gameManager.RestartGameAction?.Invoke();
    }

    private void OnEnable()
    {
        UpdateGameDataAction += UpdateGameData;
        ResetGameDataAction += ResetGameData;
        ShowPauseAction += ShowPauseMenu;
        ShowAbilityMenuAction += ShowAbilityMenu;
        ShowEndGameAction += ShowEndMenu;
    }
    private void OnDisable() 
    {
        UpdateGameDataAction -= UpdateGameData;
        ResetGameDataAction -= ResetGameData;
        ShowPauseAction -= ShowPauseMenu;
        ShowAbilityMenuAction -= ShowAbilityMenu;
        ShowEndGameAction -= ShowEndMenu;
    }
}