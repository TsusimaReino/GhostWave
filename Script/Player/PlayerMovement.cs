using UnityEngine;
/// <summary>
/// 移動キー　視点マウス
/// </summary>
public class PlayerMovement : PlayerCorl
{
    //プレイヤの移動速度
    private float _playerSpeed = 6f;
    //プレイヤーの移動方向
    private Vector3 _playerMovement = default;
    private int _floorMask = default;
    //カメラからのray
    private readonly float _camRayLength = 100f;

    void Awake()
    {
        //Floorのrayermaskを作成
        _floorMask = LayerMask.GetMask("Floor");
        _playerRigidbody = GetComponent<Rigidbody>();
        _playerAnimator = GetComponent<Animator>();
    }

    /// <summary>
    /// オブジェクトが動くたびに呼ばれる
    /// </summary>
    void FixedUpdate()
    {
        //インプットから左右上下の移動量を-1もしくは1で受け取る
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        //プレイヤーを動かすMove()を呼び出し
        Move(horizontal, vertical);

        //プレイヤーの方向を動かすTurning()を呼び出し
        Turning();

        //プレイヤーのアニメーションを設定するAnimating()を呼び出し
        Animating(horizontal, vertical);
    }

    /// <summary>
    /// プレイヤーを動かす処理
    /// </summary>
    /// <param name="horizontal"></param>
    /// <param name="vertical"></param>
    private void Move(float horizontal, float vertical)
    {
        //移動量を設定
        _playerMovement.Set(horizontal, 0f, vertical);

        //移動するベクトルを1にし、移動する距離を設定する
        _playerMovement = _playerMovement.normalized * _playerSpeed * Time.deltaTime;

        //プレイヤーのポジションを動かす
        _playerRigidbody.MovePosition(transform.position + _playerMovement);
    }

    /// <summary>
    /// プレイヤー方向を動かす処理
    /// </summary>
    private void Turning()
    {
        //カメラから、マウスで指している方向のrayを取得する
        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        //rayが衝突した情報を取得する
        RaycastHit floorHit;

        //rayを飛ばして、床に衝突した場合の処理
        if (Physics.Raycast(cameraRay, out floorHit, _camRayLength, _floorMask))
        {
            //マウスで指している場所と、プレイヤーの場所の差分を取得
            Vector3 playerToMouse = floorHit.point - transform.position;

            //プレイヤはy座標には動かさない
            playerToMouse.y = 0f;

            //プレイヤーの場所から、マウスで指している場所への角度を取得
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);

            //プレイヤーの角度(プレイヤーの向き)を、新しく設定
            _playerRigidbody.MoveRotation(newRotation);
        }
    }

    /// <summary>
    /// アニメーションの処理
    /// </summary>
    /// <param name="horizontal"></param>
    /// <param name="vertical"></param>
    private void Animating(float horizontal, float vertical)
    {
        //プレイヤーの移動量が0以外の場合、walkingをtrueにする
        bool walking = horizontal != 0f || vertical != 0f;

        //アニメーションのパラメーターOsWalkingをwalkingの値で設定する
        _playerAnimator.SetBool("IsWalking", walking);
    }
}