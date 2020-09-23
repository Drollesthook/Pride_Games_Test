using UnityEngine;

public class CompletitionChecker : MonoBehaviour {

    void Start() {
        SoftCurrencyController.Instance.CurrencyChanged += OnCurrencyChanged;
    }

    void OnDestroy() {
        
        SoftCurrencyController.Instance.CurrencyChanged -= OnCurrencyChanged;
    }

    void OnCurrencyChanged() {
        if (LevelGenerator.Instance.CurrentLevelObjectsAmount == SoftCurrencyController.Instance.CurrentLevelObjectsCollected)
            GameManager.Instance.EndLevel();
    }
}
