using UnityEngine;

public class EnemyHealth : EnemyCorl
{
    //敵のHPの初期値
    [SerializeField]
    public int _enemyStartingHealth = 100;
    //敵の最新のHP
    [SerializeField]
    public int _enemyCurrentHealth = default;
    //敵の消滅時間
    [SerializeField]
    public float _sinkSpeed = 2.5f; 
    //敵を倒した時のスコア
    [SerializeField]
    public int _scoreValue = 10;
    //音クリップ
    public AudioClip _enemyDeathClip;
    //敵の効果音
    private AudioSource _enemyAudio;
    //攻撃された時のエフェクト
    private ParticleSystem 　_hitParticles;
    private CapsuleCollider _capsuleCollider;
    //敵が倒されたかフラグ
    private bool _isDead = default;
    //敵の消滅フラグ
    private bool _isSinking = default;


    //const int DestroyTime = 2;
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
            transform.Translate(-Vector3.up * _sinkSpeed * Time.deltaTime);
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
        //スコアを加算する
        ScoreManager._score += _scoreValue;
        //敵のオブジェクトを破壊する
        //Destroy(gameObject, 2f);
        this.gameObject.SetActive(false);
    }
}
