using System.Collections;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {
    static UIController _instance;
    public static UIController Instance => _instance;

    [SerializeField] GameManager _gameManager = default;
    [SerializeField] GameObject _player = default;
    [SerializeField] PlayerExperienceController _playerExperienceController = default;
    [SerializeField] TMP_Text _coinAmountText = default,
                              _crystalAmountText = default,
                              _currentPlayerLevelText = default,
                              _currentLevelObjectsText = default,
                              _pickUpText = default;
    [SerializeField] CollectableChecker _collectableChecker = default;

    float _currentSpeed, _highestSpeed, _currentDistance, _highestDistance;


    void Awake() {
        _instance = this;
        _gameManager.LevelEnded += OnLevelEnded;
        _collectableChecker.PickableEntered += OnCollectableEntered;
        _collectableChecker.PickableExit += OnCollectableExit;
        LevelGenerator.Instance.LevelGenerated += OnLevelGenerated;
        
    }
    

    void OnDestroy() {
        LevelGenerator.Instance.LevelGenerated -= OnLevelGenerated;
        _gameManager.LevelEnded -= OnLevelEnded;
        _collectableChecker.PickableEntered -= OnCollectableEntered;
        _collectableChecker.PickableExit -= OnCollectableExit;
    }

    
    void OnLevelEnded() {
        UpdateMenuTexts();
    }


    void OnLevelGenerated() {
        UpdateMenuTexts();
    }

    public void UpdateMenuTexts() {
        _coinAmountText.text = "Gold:" + SoftCurrencyController.Instance.CoinAmount();
        _crystalAmountText.text = _playerExperienceController.CurrentLevelExpirience() + "/" +
                                  GameConfigurations.Instance.CurrentGameConfigs.RequieredExperience;
        _currentPlayerLevelText.text = "Level:" + _playerExperienceController.CurrentPlayerLevel();
       _currentLevelObjectsText.text = "Progress:" + SoftCurrencyController.Instance.CurrentLevelObjectsCollected + "/" +
                                       LevelGenerator.Instance.CurrentLevelObjectsAmount;
    }



    public void ShowPickUpText() {
        _pickUpText.gameObject.SetActive(true);
    }

    void OnCollectableEntered() {
        ShowPickUpText();
    }

    void OnCollectableExit() {
        HidePickUpText();

    }

    public void HidePickUpText() {
        _pickUpText.gameObject.SetActive(false);
    }
}
