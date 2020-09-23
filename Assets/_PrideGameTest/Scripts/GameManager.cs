using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static GameManager _instance;
    public static GameManager Instance => _instance;
        public event Action LevelStarted, LevelEnded, GamePaused, GameContinued;
        // Start is called before the first frame update
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
