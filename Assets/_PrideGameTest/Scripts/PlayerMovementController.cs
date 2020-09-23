using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementController : MonoBehaviour {
    PlayerController _playerController;
    Vector2 _moveDirection;
    CharacterController _characterController;
    int _playerSpeed;
    Vector3 _startPosition;
    void Awake() {
        _playerSpeed = GameConfigurations.Instance.CurrentGameConfigs.PlayerSpeed;
        _playerController = new PlayerController();
        _playerController.Enable();
        _characterController = GetComponent<CharacterController>();
    }

    void Start() {
        _startPosition = transform.position;
        GameManager.Instance.LevelStarted += OnLevelStarted;
    }

    void OnDestroy() {
        GameManager.Instance.LevelStarted -= OnLevelStarted;
        
    }

    void Update() {
        Move();
    }

    void OnLevelStarted() {
        print(_startPosition);
        transform.position = _startPosition;
    }

    void Move() {
        _moveDirection = _playerController.Player.Move.ReadValue<Vector2>();
        Vector3 newMoveDirection = new Vector3(_moveDirection.x, 0, _moveDirection.y) * _playerSpeed * Time.deltaTime;
        //_characterController.Move(newMoveDirection);
        var newPosition = transform.position + newMoveDirection;
        transform.position = newPosition;
    }
}
