using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

using UnityEditor.Experimental.RestService;

using UnityEngine;

using Random = UnityEngine.Random;

public class LevelGenerator : MonoBehaviour {
    static LevelGenerator _instance;
    public static LevelGenerator Instance => _instance;
    public int CurrentLevelObjectsAmount => _currentLevelObjectsAmount;
    public event Action LevelGenerated;
    public (Vector2 min, Vector2 max) PlayerMovementClamp => CalculateClampVector();
    
    [SerializeField] float _platformSize = default;
    [SerializeField] string _levelConfigsPath = default;
    
    GameLevelConfigs _currentLevelConfig;
    List<Platform> _currentLevelPlatforms = new List<Platform>();
    
    int _currentLevelObjectsAmount;
    Vector2 _maxBound, _minBound;
    
    class GameLevelConfigs {
        public int FieldSize = default;
        public int MaximumAmountOfObjects = default;
    }

    GameLevelConfigs[] _gameLevelConfigs;
    void Awake() {
        GameManager.Instance.LevelStarted += OnLevelStarted;
        _instance = this;
        LoadConfigs();
    }


    void OnDestroy() {
        GameManager.Instance.LevelStarted -= OnLevelStarted;
    }

    void LoadConfigs() {
        _gameLevelConfigs = new GameLevelConfigs[Directory.GetFiles(Application.dataPath + _levelConfigsPath, "*.json").Length];
        int i = 0;
        foreach (string file in Directory.GetFiles(Application.dataPath + _levelConfigsPath, "*.json")) {
            string currentJson = File.ReadAllText(file);
            _gameLevelConfigs[i] = JsonUtility.FromJson<GameLevelConfigs>(currentJson);
            i++;
        }
    }

    void OnLevelStarted() {
        GenerateLevel();
    }
    void GenerateLevel() {
        Reset();
       _currentLevelConfig = GetRandomLevelConfig();
       PlacePlatforms(_currentLevelConfig.FieldSize);
       SpawnInteractableObjects();
       LevelGenerated?.Invoke();
    }

    (Vector2 min, Vector2 max) CalculateClampVector() {
        _minBound.x = -_platformSize / 2;
        _minBound.y = -_platformSize / 2;
        _maxBound.x = _platformSize * _currentLevelConfig.FieldSize - _platformSize / 2;
        _maxBound.y = _platformSize * _currentLevelConfig.FieldSize - _platformSize / 2;
        return (_minBound,  _maxBound);
    }

    void PlacePlatforms(int fieldSize) {
        for (int i = 0; i < fieldSize; i++)
        {
            for (int j = 0; j < fieldSize; j++) {
                _currentLevelPlatforms.Add(Pooler.Instance.SpawnPlatform(new Vector3(i * _platformSize,0,j * _platformSize)));
            }
        }
    }

    GameLevelConfigs GetRandomLevelConfig() {
        int randomConfigNumber = Random.Range(0, _gameLevelConfigs.Length);
        return _gameLevelConfigs[randomConfigNumber];
    }

    void Reset() {
        _currentLevelPlatforms.Clear();
        Pooler.Instance.DespawnPlatforms();
    }

    void SpawnInteractableObjects() {
        List<Platform> listOfCurrentPlatforms = new List<Platform>();
        List<Platform> listToSpawn = new List<Platform>();
        listOfCurrentPlatforms.AddRange(_currentLevelPlatforms);
        _currentLevelObjectsAmount = Random.Range(2, _currentLevelConfig.MaximumAmountOfObjects);
        for (int i = 0; i < _currentLevelObjectsAmount; i++) {
            int randomIndex = Random.Range(0, listOfCurrentPlatforms.Count);
            var platform = listOfCurrentPlatforms[randomIndex];
            listOfCurrentPlatforms.RemoveAt(randomIndex);
            listToSpawn.Add(platform);
        }

        listToSpawn[0].SpawnCoin();
        listToSpawn.RemoveAt(0);
        listToSpawn[0].SpawnCrystal();
        listToSpawn.RemoveAt(0);
        foreach (Platform platform in listToSpawn) {
            if (Random.value < 0.5) platform.SpawnCoin();
            else platform.SpawnCrystal();
        }
    }
}
