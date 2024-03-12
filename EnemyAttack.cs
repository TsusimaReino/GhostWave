using UnityEngine;

public class EnemyAttack : EnemyCorl
{ 　
    //攻撃する時間間隔
    public float _timeBetweenAttacks = 0.5f;
    //攻撃のダメージ量
    public int _attackDamage = 10;
    private GameObject _player;
    //攻撃する範囲にプレイヤーが存在するかのフラグ
    private bool _playerInRange;
    //経過時間
    private float _timer;

    void Awake()
    {
        //プレイヤーを取得
        _player = GameObject.FindGameObjectWithTag("Player");
        //プレイヤーのHPを取得
        _playerHealth = _player.GetComponent<PlayerHealth>();
        //敵のHPを取得
        _enemyHealth = GetComponent<EnemyHealth>();
        //アニメータコンポーネントを取得
        _enemyAnim = GetComponent<Animator>();
    }

    /// <summary>
    /// 物体と衝突した時の処理
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        //衝突した物体がプレイヤーの場合
        if (other.gameObject == _player)
        {
            //攻撃する範囲にプレイヤーが存在するかのフラグをオンにする
            _playerInRange = true;
        }
    }

    /// <summary>
    /// 衝突した物体が離れた時の処理
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit(Collider other)
    {
        //衝突した物体がプレイヤーの場合
        if (other.gameObject == _player)
        {
            //攻撃する範囲にプレイヤーが存在するかのフラグをオフにする
            _playerInRange = false;
        }
    }

    /// <summary>
    /// 攻撃を受けたプレイヤー処理
    /// </summary>
    private void Update()
    {
        //経過時間を加算する
        _timer += Time.deltaTime;

        //攻撃の時間間隔よりも経過時間が長い、かつプレイヤーが攻撃範囲に存在する場合、かつ敵のHPが0以上の場合
        if (_timer >= _timeBetweenAttacks && _playerInRange && _enemyHealth._enemyCurrentHealth > 0)
        {
            //プレイヤーを攻撃する
            Attack();
        }

        //プレイヤーのHPが0以下になった場合
        if (_playerHealth._playerCurrentHealth <= 0)
        {
            //プレイヤーが倒された時のアニメーションフラグをオンにする
            _enemyAnim.SetTrigger("PlayerDead");
        }
    }

    /// <summary>
    /// 敵が攻撃する処理
    /// </summary>
    void Attack()
    {
        //経過時間を0にする
        _timer = 0f;

        //プレイヤーのHPが0よりも大きい場合
        if (_playerHealth._playerCurrentHealth > 0)
        {
            //プレイヤーにダメージを与える
            _playerHealth.TakeDamage(_attackDamage);
        }
    }
}
