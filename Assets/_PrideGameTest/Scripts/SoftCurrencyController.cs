using System;

using UnityEngine;

public class SoftCurrencyController : MonoBehaviour {
    static SoftCurrencyController _instance;
    public static SoftCurrencyController Instance => _instance;

    public int CurrentLevelObjectsCollected => _currentLevelObjectsCollected;

    public event Action CurrencyChanged;
    [SerializeField] GameManager _gameManager = default;

    const string COIN_AMOUNT = "coin_amount";
    const string CRYSTAL_AMOUNT = "crystal_amount";
    int _currentCoins;
    int _currentCrystals;
    int _currentLevelObjectsCollected;

    void Awake() {
        _instance = this;
    }

    void Start()
    {
        LoadCurrency();
        _gameManager.LevelEnded += SaveCurrencyAmount;
        _gameManager.LevelStarted += OnLevelStarted;
    }

    void OnDestroy() {
        _gameManager.LevelEnded -= SaveCurrencyAmount;
        _gameManager.LevelStarted -= OnLevelStarted;
    }

    void LoadCurrency() {
        _currentCrystals = PlayerPrefs.GetInt(CRYSTAL_AMOUNT, 0);
        _currentCoins = PlayerPrefs.GetInt(COIN_AMOUNT, 0);
        
    }

    void OnLevelStarted() {
        _currentLevelObjectsCollected = 0;
    }

    public void AddCoin() {
        _currentCoins++;
        AddOneObject();
    }

    public void AddCrystal() {
        _currentCrystals++;
        AddOneObject();
    }

    void AddOneObject() {
        _currentLevelObjectsCollected++;
        CurrencyChanged?.Invoke();
    }


    public int CoinAmount() {
        return _currentCoins;
    }

    public int CrystalAmount() {
        return _currentCrystals;
    }



    void SaveCurrencyAmount() {
        PlayerPrefs.SetInt(COIN_AMOUNT, _currentCoins);
        PlayerPrefs.SetInt(CRYSTAL_AMOUNT, _currentCrystals);
    }
}
