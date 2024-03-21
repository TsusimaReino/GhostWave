using UnityEngine;
/// <summary>
///　プレイヤーの基底クラス
/// </summary>
public class PlayerCorl:MonoBehaviour
{
    [SerializeField]
    protected Animator _playeranim;
    protected Rigidbody _playerRigidbody;
    protected CharacterController _playerCharacterController;
    protected Transform _playerTransform;
    protected Animator _playerAnimator;
    protected float _playerHorizontal;
    protected float _playerVertical;
    protected Vector3 _velocity;
    protected Vector3 _aim;
    //プレイヤーの回転
    protected Quaternion _playerRotation;

    private PlayerHealth _playerHealth;
    private PlayerShoting _playerShoting;

    [System.NonSerialized] //プレイヤーのHPを親クラスに
    public float _hP = default;
}

