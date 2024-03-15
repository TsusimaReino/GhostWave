using UnityEngine;
[RequireComponent(typeof(EnemyMovement),typeof(EnemyAttack))]

/// <summary>
/// 敵の最大HP、効果音
/// </summary>
public class EnemyHealth : EnemyCorl
{
    [SerializeField]
    [Header("敵のHPの初期値")]
    public int _enemyStartingHealth = 100;

    [SerializeField]
    [Header("敵の最新のHP")]
    public int _enemyCurrentHealth = default;

    [SerializeField]
    [Header("敵の消滅時間")]
    public float _enemySinkSpeed = 2.5f;

    [SerializeField]
    [Header("敵を倒したときのスコア")]
    public int _scoreValue = 10;

    [Header("音クリップ")]
    [SerializeField]
    public AudioClip _enemyDeathClip;

    //敵の効果音
    private AudioSource _enemyAudio;
    ////攻撃された時のエフェクト
    private ParticleSystem _hitParticles = default;
    private CapsuleCollider _capsuleCollider = default;
    //敵が倒されたかフラグ
    private bool _isDead = default;
    //敵の消滅フラグ
    private bool _isSinking = default;
    //const int DESTROYTIME = 2;
    /// <summary>
    /// コンポーネントの設定
    /// </summary>
    void Awake()
    {
        _enemyAnim = GetComponent<Animator>();
        _enemyAudio = GetComponent<AudioSource>();
        _hitParticles = GetComponentInChildren<ParticleSystem>();
        _capsuleCollider = GetComponent<CapsuleCollider>();

        //最新のHPを初期化
        _enemyCurrentHealth = _enemyStartingHealth;
    }

    /// <summary>
    /// 敵が落ちる
    /// </summary>
    private void Update()
    {
        //消滅フラグがオンの場合
        if (_isSinking)
        {
            //敵が落ちる
            transform.Translate(-Vector3.up * _enemySinkSpeed * Time.deltaTime);
        }
    }

    /// <summary>
    /// 攻撃を受けた時の処理
    /// </summary>
    /// <param name="amount"></param>
    /// <param name="hitPoint"></param>
    public void TakeDamage(int amount, Vector3 hitPoint)
    {
        //倒されたフラグがオンの場合
        if (_isDead)
        {
            return;
        }
        //攻撃を受けた時の音を鳴らす
        _enemyAudio.Play();
        //HPを減らす
        _enemyCurrentHealth -= amount;
        //攻撃を受けた時のエフェクトを表示する
        _hitParticles.transform.position = hitPoint;
        _hitParticles.Play();
        //敵のHPが0以下になった場合
        if (_enemyCurrentHealth <= 0)
        {
            //敵が倒される
            Death();
        }
    }

    /// <summary>
    /// 敵が倒された場合の処理
    /// </summary>
    private void Death()
    {
        //敵が倒されたかフラグをオンにする
        _isDead = true;

        _capsuleCollider.isTrigger = true;
        //倒された時のアニメーショントリガーをオンにする
        _enemyAnim.SetTrigger("Dead");
        //倒された時の効果音を設定する
        _enemyAudio.clip = _enemyDeathClip;
        //敵が倒された時の効果音を鳴らす
        _enemyAudio.Play();
    }


    /// <summary>
    /// 敵を消滅させる処理
    /// </summary>
    public void StartSinking()
    {
        //NavMesh Agentコンポーネントをオフにする
        GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
        //RigidbodyコンポーネントのKinematicをオンにする(重力の効果を受けない)
        GetComponent<Rigidbody>().isKinematic = true;
        //消滅フラグをオンにする
        _isSinking = true;
        ////スコアを加算する
        ScoreManager._score += _scoreValue;
        //敵のオブジェクトを破壊する
        //Destroy(gameObject, 2f);
        this.gameObject.SetActive(false);
    }
}
//class Starting : EnemyCorl
//{
//    //敵の消滅時間
//    [SerializeField]
//    public float _enemySinkSpeed = 2.5f;
//    private bool _isinking = default;

//    /// <summary>
//    /// 敵が落ちる
//    /// </summary>
//    private void Update()
//    {
//        //消滅フラグがオンの場合
//        if (_isinking)
//        {
//            //敵が落ちる
//            transform.Translate(-Vector3.up * _enemySinkSpeed * Time.deltaTime);
//        }
//    }

//    //NavMesh Agentコンポーネントをオフにする
//    GetComponent<UnityEngine.AI.NavMeshAgent>().enable = false;
//        //RigidbodyコンポーネントのKinematicをオンにする(重力の効果を受けない)
//        GetComponent<Rigidbody>().isKinematic = true;
//        //消滅フラグをオンにする
//        _isinking = true;
//        //スコアを加算する
//        ScoreManager._score += _scoreValue;
//        //敵のオブジェクトを破壊する
//        Destroy(gameObject, 2f);
//        this.gameObject.SetActive(false);
//}

public class EnemiesDamage : MonoBehaviour, IDamageable
{
    [SerializeField]
    [Header("敵のHPの初期値")]
    public int _enemyStartingHealth = 100;

    [SerializeField]
    [Header("敵の最新のHP")]
    public int _enemyCurrentHealth = default;

    [Header("音クリップ")]
    [SerializeField]
    public AudioClip _enemyDeathClip;

    //攻撃された時のエフェクト
    private ParticleSystem _hitParticles = default;
    private CapsuleCollider _capsuleCollider = default;
    //敵が倒されたかフラグ
    //private bool _isDead = default;

    //敵の効果音
    private AudioSource _enemyAudio;
    private Animator _enemyAnim = default;

    void Awake()
    {
        //最新のHPを初期化
        _enemyCurrentHealth = _enemyStartingHealth;
        _hitParticles = GetComponentInChildren<ParticleSystem>();
        _capsuleCollider = GetComponent<CapsuleCollider>();
        _enemyAnim = GetComponent<Animator>();
        _enemyAudio = GetComponent<AudioSource>();
    }
    public void Damege(int valure, Vector3 hitPoint)　//敵に加えるダメージ,effect
    {

        //NotImplementedException 未実装のメソッド
        //throw new System.NotImplementedException();
        _enemyCurrentHealth -= valure;
        //攻撃を受けた時のエフェクトを表示する
        _hitParticles.transform.position = hitPoint;
        _hitParticles.Play();
    }

    public void Death()　//Lifeが０になったら実装
    {
        //throw new System.NotImplementedException();
        //敵のHPが0以下になった場合
        if (_enemyCurrentHealth <= 0)
        {
            //敵が倒される
            EnemyDeath();
        }
    }
    private void EnemyDeath()
    {
        //敵が倒されたかフラグをオンにする
        //_isDead = true;
        _capsuleCollider.isTrigger = true;
        //倒された時のアニメーショントリガーをオンにする
        _enemyAnim.SetTrigger("Dead");
        //倒された時の効果音を設定する
        _enemyAudio.clip = _enemyDeathClip;
        //敵が倒された時の効果音を鳴らす
        _enemyAudio.Play();
    }

    public void Damege(int valure)
    {
        throw new System.NotImplementedException();
    }
}
