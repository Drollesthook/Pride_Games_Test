using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

using UnityEngine;
using UnityEngine.InputSystem;

public class CollectableChecker : MonoBehaviour {

    public event Action PickableEntered, PickableExit;
    
    [SerializeField] float _overlapRange = default;
    [SerializeField] LayerMask _collectableMask = default;

    Collider[] _colliders;
    
    enum States{ WaitingForCollectable,
        CanPickUpCoin
    }

    States _currentState = States.WaitingForCollectable;

    void Update() {
        _colliders = Physics.OverlapSphere(transform.position, _overlapRange, _collectableMask);
        if (_colliders.Length == 0)
            SetState(States.WaitingForCollectable);
        else {
            SetState(States.CanPickUpCoin);
        }
        
    }

    void SetState(States newState) {
        switch (newState) {
            case States.WaitingForCollectable:
                if (_currentState != newState) {
                    _currentState = newState;
                    UIController.Instance.HidePickUpText();
                }
                break;
            case States.CanPickUpCoin:
                if (_currentState != newState) {
                    _currentState = newState;
                    UIController.Instance.ShowPickUpText();
                }
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
    }

    public void PickUp(InputAction.CallbackContext context) {
        if (_currentState == States.CanPickUpCoin && context.action.triggered) {
            var collectableObject = _colliders[0].GetComponent<ICollected>();
                 collectableObject?.Collect();
            // foreach (var collider in _colliders) {
            //     var collectableObject = collider.GetComponent<ICollected>();
            //     collectableObject?.Collect();
            // }
        }
    }
    
    void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _overlapRange);
    }

}
