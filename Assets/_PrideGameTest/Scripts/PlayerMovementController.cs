using UnityEngine;

public class PlayerMovementController : MonoBehaviour {
    PlayerController _playerController;
    Vector2 _moveDirection;
    int _playerSpeed;
    Vector3 _startPosition;
    Vector2 _minBound, _maxBound;
    const int PLAYER_Y_POSITION = 1;
    
    void Awake() {
        _playerController = new PlayerController();
        _playerController.Enable();
    }

    void Start() {
        _playerSpeed = GameConfigurations.Instance.CurrentGameConfigs.PlayerSpeed;
        _startPosition = transform.position;
        GameManager.Instance.LevelStarted += OnLevelStarted;
        LevelGenerator.Instance.LevelGenerated += OnLevelGenerated;
    }

    void OnDestroy() {
        GameManager.Instance.LevelStarted -= OnLevelStarted;
        LevelGenerator.Instance.LevelGenerated -= OnLevelGenerated;
    }

    void Update() {
        Move();
    }

    void OnLevelStarted() {
        transform.position = _startPosition;
    }

    void OnLevelGenerated() {
        _minBound = LevelGenerator.Instance.PlayerMovementClamp.min;
        _maxBound = LevelGenerator.Instance.PlayerMovementClamp.max;
    }

    void Move() {
        _moveDirection = _playerController.Player.Move.ReadValue<Vector2>();
        Vector3 newMoveDirection = new Vector3(_moveDirection.x, 0, _moveDirection.y) * _playerSpeed * Time.deltaTime;
        var newPosition = transform.position + newMoveDirection;
        var clampedPosition = new Vector3(Mathf.Clamp(newPosition.x,_minBound.x, _maxBound.x), PLAYER_Y_POSITION, Mathf.Clamp(newPosition.z, _minBound.y, _maxBound.y));
        transform.position = clampedPosition;
    }
}
