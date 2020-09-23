using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class LevelGenerator : MonoBehaviour {
    public static LevelGenerator Instance => _instance;
    public int CurrentLevelObjectsAmount => _currentLevelObjectsAmount;
    public event Action LevelGenerated;
    public (Vector2 min, Vector2 max) PlayerMovementClamp => CalculateClampVector();
    
    [SerializeField] float _platformSize = default;
    [SerializeField] string _levelConfigsPath = default;
    
    static LevelGenerator _instance;
    GameLevelConfigs _currentLevelConfig;
    List<Platform> _currentLevelPlatforms = new List<Platform>();
    int _currentLevelObjectsAmount;
    Vector2 _maxBound, _minBound;
    GameLevelConfigs[] _gameLevelConfigs;
    Object[] _gameLevelConfigObjects;
    
    class GameLevelConfigs {
        public int FieldSize = default;
        public int MaximumAmountOfObjects = default;
    }

    void Awake() {
        GameManager.Instance.LevelStarted += OnLevelStarted;
        _instance = this;
        LoadConfigs();
    }


    void OnDestroy() {
        GameManager.Instance.LevelStarted -= OnLevelStarted;
    }

    void LoadConfigs() {
        _gameLevelConfigObjects = Resources.LoadAll(_levelConfigsPath, typeof(TextAsset));
        _gameLevelConfigs = new GameLevelConfigs[_gameLevelConfigObjects.Length];
        for (var i = 0; i < _gameLevelConfigObjects.Length; i++) {
            Object file = _gameLevelConfigObjects[i];
            var currentJson = file as TextAsset;
            if (currentJson != null)
                _gameLevelConfigs[i] = JsonUtility.FromJson<GameLevelConfigs>(currentJson.text);
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
