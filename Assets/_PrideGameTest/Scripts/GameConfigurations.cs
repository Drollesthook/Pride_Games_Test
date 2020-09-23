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

    Object _gameConfigObject;
    static GameConfigurations _instance;
    GameConfigs _currentGameConfigs;

    void Awake() {
        _instance = this;
        LoadConfigs();
    }

    void LoadConfigs() {
        _gameConfigObject = Resources.Load(_gameConfigsPath, typeof(TextAsset));
        var currentJson = _gameConfigObject as TextAsset;
        if (currentJson != null)
            _currentGameConfigs = JsonUtility.FromJson<GameConfigs>(currentJson.text);
    }
}
