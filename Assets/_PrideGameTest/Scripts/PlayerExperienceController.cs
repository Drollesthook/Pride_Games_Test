using UnityEngine;

public class PlayerExperienceController : MonoBehaviour {

    int _amountOfRequiredCrystals;
    int _startLevel = 1;

    void Start() {
        _amountOfRequiredCrystals = GameConfigurations.Instance.CurrentGameConfigs.RequieredExperience;
    }

    public int CurrentPlayerLevel() {
        return Mathf.CeilToInt(SoftCurrencyController.Instance.CrystalAmount / _amountOfRequiredCrystals)+ _startLevel;
    }

    public int CurrentLevelExpirience() {
        return SoftCurrencyController.Instance.CrystalAmount % _amountOfRequiredCrystals;
    }
}
