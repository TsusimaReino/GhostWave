using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 基底クラス
/// </summary>

public class Unitychanunity : MonoBehaviour
{
    protected CharacterController _characterController;
    protected Rigidbody _rigidbody;
    protected Transform _transform;
    protected Animator _animator;
    protected float _horizontal;
    protected float _vertical;
    protected Vector3 _velocity;
    protected Vector3 _aim;
    //プレイヤーの回転
    protected Quaternion _playerRotation;

    private void Update()
    {
        _horizontal = Input.GetAxis("Horizontal");
        _vertical = Input.GetAxis("Vertical");
    }
}