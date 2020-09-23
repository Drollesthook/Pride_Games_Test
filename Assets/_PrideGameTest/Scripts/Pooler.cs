using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pooler : MonoBehaviour {
    static Pooler _instance;
    public static Pooler Instance => _instance;
    
    [SerializeField] Platform _platform = default;
    [SerializeField] int _amountPlatformsToPrespawn = default;
    
    List<Platform> _freePlatforms = new List<Platform>();
    List<Platform> _busyPlatforms = new List<Platform>();

    void Awake() {
        _instance = this;
        PrespawnPlatforms();
    }

    void PrespawnPlatforms() {
        for (int i = 0; i < _amountPlatformsToPrespawn; i++) {
            CreateNewPlatform();
        }
    }

    void CreateNewPlatform() {
        var newPlatform = Instantiate(_platform, transform);
        newPlatform.gameObject.SetActive(false);
        _freePlatforms.Add(newPlatform);
    }

    public Platform SpawnPlatform(Vector3 spawnPosition) {
        while (true) {
            if (_freePlatforms.Count > 0) {
                var platformToSpawn = _freePlatforms[0];
                platformToSpawn.gameObject.SetActive(true);
                _busyPlatforms.Add(platformToSpawn);
                _freePlatforms.Remove(platformToSpawn);
                platformToSpawn.transform.position = spawnPosition;
                return platformToSpawn;
            }
            CreateNewPlatform();
        }
    }

    public void DespawnPlatforms() {
        if (_busyPlatforms.Count <= 0) return;
        foreach (Platform platform in _busyPlatforms) {
            platform.gameObject.SetActive(false);
            _freePlatforms.Add(platform);
        }
        _busyPlatforms.Clear();
    }
}
