﻿using System.Collections;
using  System.IO;
using System.Collections.Generic;
using UnityEngine;

public class GameConfigurations : MonoBehaviour {
    static GameConfigurations _instance;
    public static GameConfigurations Instance => _instance;
    
    [SerializeField] string _gameConfigsPath = default;
    public struct GameConfigs {
        public int PlayerSpeed;
        public int RequieredExperience;
    }

    GameConfigs _currentGameConfigs;
    public GameConfigs CurrentGameConfigs => _currentGameConfigs;

    void Awake() {
        _instance = this;
        LoadConfigs();
    }

    void LoadConfigs() {
            string currentJson = File.ReadAllText(Application.dataPath + _gameConfigsPath + "/GameConfig.json");
            _currentGameConfigs = JsonUtility.FromJson<GameConfigs>(currentJson);
    }
}
