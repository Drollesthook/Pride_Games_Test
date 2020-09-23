using  System.IO;
using UnityEngine;

public class GameConfigurations : MonoBehaviour {
    public static GameConfigurations Instance => _instance;
    public GameConfigs CurrentGameConfigs => _currentGameConfigs;
    
    public class GameConfigs {
        public int PlayerSpeed = default;
        public int RequieredExperience = default;
    }

    [SerializeField] string _gameConfigsPath = default;
    
    static GameConfigurations _instance;
    GameConfigs _currentGameConfigs;

    void Awake() {
        _instance = this;
        LoadConfigs();
    }

    void LoadConfigs() {
        string currentJson = File.ReadAllText(Application.dataPath + _gameConfigsPath + "/GameConfig.json");
        _currentGameConfigs = JsonUtility.FromJson<GameConfigs>(currentJson);
    }
}
