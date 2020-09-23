using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance => _instance;
    public event Action LevelStarted, LevelEnded;
    
    static GameManager _instance;
    
    void Awake() {
        _instance = this;
    }

    void Start() {
        StartLevel();
    }

    public void StartLevel() {
        LevelStarted?.Invoke();
    }

    public void EndLevel() {
        LevelEnded?.Invoke();
        StartLevel();
    }
    
}
