﻿using UnityEngine;

public class CameraFollowController : MonoBehaviour {
   [SerializeField] Transform _objectToFollow;
   [SerializeField] Vector3 _offset;
   [SerializeField] float _followSpeed, _lookSpeed;

   void LateUpdate() {
      LookAtTarget();
      MoveToTarget();
   }

   void LookAtTarget() {
      Vector3 _lookDirection = _objectToFollow.position - transform.position;
      Quaternion _rot = Quaternion.LookRotation(_lookDirection, Vector3.up);
      transform.rotation = Quaternion.Lerp(transform.rotation, _rot, _lookSpeed * Time.deltaTime);
   }

   void MoveToTarget() {
      Vector3 _targetPos = _objectToFollow.position + _offset;


      transform.position = Vector3.Lerp(transform.position, _targetPos, _followSpeed * Time.deltaTime);
   }
}
