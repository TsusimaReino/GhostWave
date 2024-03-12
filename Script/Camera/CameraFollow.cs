using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
    public Transform _target; //カメラがついていくターゲットのポジション
    public float _smoothing = 5f; //カメラのついていくスピード

    Vector3 _offset; //カメラとターゲットの間の距離

    void Start()
    {
        //カメラとターゲットの間の距離を算出
        _offset = transform.position - _target.position;
    }

    //物体が動く毎に呼び出される
    void FixedUpdate()
    {
        //カメラの移動先
        Vector3 targetCamPos = _target.position + _offset;

        //カメラを移動する
        transform.position = Vector3.Lerp(transform.position, targetCamPos, _smoothing * Time.deltaTime);
    }
}