using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : PlayerCorl //派生クラス
{
    [Header("走る速度")]
    public float _speed = default;
    [Header("回転速度")]
    public float _rotateSpeed = default;
    //[Header("移動速度")]
    //public float _moveSpeed = default;
    private void Start()
    {
        _playerRigidbody = GetComponent<Rigidbody>();
        _playerTransform = GetComponent<Transform>();
       // _animator = GetComponent<Animator>();
        //プレイヤーの回転場所を取得
        _playerRotation = _playerTransform.rotation;
    }

    void FixedUpdate()
    {
        Quaternion horizontalRotation =
            Quaternion.AngleAxis(Camera.main.transform.eulerAngles.y, Vector3.up);

        _velocity = horizontalRotation *
            new Vector3(_playerHorizontal, _playerRigidbody.velocity.y, _playerVertical).normalized;

        _aim = horizontalRotation *
            new Vector3(_playerHorizontal, 0, _playerVertical).normalized;

        if (_aim.magnitude > 0.5f)
        {
            _playerRotation = Quaternion.LookRotation(_aim, Vector3.up);
        }

        _playerTransform.rotation =
            Quaternion.RotateTowards(_playerTransform.rotation, _playerRotation, 600 * Time.deltaTime);

        if (_velocity.magnitude > 0.1f)
        {
            //_animator.SetBool("running", true);
        } else
        {
            //_animator.SetBool("running", false);
        }
        _playerRigidbody.velocity = _velocity * _speed;
    }
}