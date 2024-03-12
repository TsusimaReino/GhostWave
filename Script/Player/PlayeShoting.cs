using UnityEngine;
/// <summary>
/// 弾のダメージ処理
/// </summary>
public class PlayerShoting : PlayerCorl
{
    [Header("弾のダメージ")]
    [SerializeField]
    public  int _damagePerShot = 20;
    [Header("撃つ間隔")]
    [SerializeField]
    public float _timeBetweenBullets = 0.15f;
    [Header ("飛距離")]
    [SerializeField]
    public float _range = 100f;
    //経過時間
    private float _timer = default;
    //rayは弾の攻撃範囲
    private Ray _shootRay = new Ray();
    //弾が当たった物体
    private RaycastHit _shootHit = default;
    //撃てるもののみ識別する
    private int _shootableMask = default;
    //弾を打った時のエフェクト
    private ParticleSystem _gunParticles = default;
    private LineRenderer _gunLine = default;
    private AudioSource _gunAudio = default;
    private Light _gunLight = default;
    //エフェクトが消える時間
    private float _effectsDisplayTime = 0.2f;

    void Awake()
    {
        //Shootable Layerを取得
        _shootableMask = LayerMask.GetMask("Shootable");
        //コンポーネントを取得
        _gunParticles = GetComponent<ParticleSystem>();
        _gunLine = GetComponent<LineRenderer>();
        _gunAudio = GetComponent<AudioSource>();
        _gunLight = GetComponent<Light>();
    }
    //経過時間を計測処理
    void Update()
    {
        //経過時間を計測
        _timer += Time.deltaTime;

        //弾を打つボタンを押した時、かつ経過時間が弾を打つ間隔よりも大きい場合
        if (Input.GetButton("Fire1") && _timer >= _timeBetweenBullets && Time.timeScale != 0)
        {
            //弾を打つ
            Shoot();
        }

        //経過時間がエフェクトの表示時間よりも大きくなった場合
        if (_timer >= _timeBetweenBullets * _effectsDisplayTime)
        {
            //エフェクトを非表示にする
            DisableEffects();
        }
    }

    /// <summary>
    /// 銃を撃つエフェクトをオフにする処理
    /// </summary>
    public void DisableEffects()
    {
        _gunLine.enabled = false;
        _gunLight.enabled = false;
    }

    /// <summary>
    /// 弾を撃つ処理
    /// </summary>
    void Shoot()
    {
        //経過時間を初期化
        _timer = 0f;

        //弾を撃つエフェクトをオンにする
        _gunAudio.Play();
        _gunLight.enabled = true;

        //弾を連写することを想定して、オフにしてからオンにする
        _gunParticles.Stop();
        _gunParticles.Play();

        //射線のスタート位置を設定する
        _gunLine.enabled = true;
        _gunLine.SetPosition(0, transform.position);

        //弾の攻撃範囲のスタート位置を設定する
        _shootRay.origin = transform.position;
        //弾の飛んでいく方向を設定する
        _shootRay.direction = transform.forward;

        //弾を飛ばす処理　(Rayを飛ばし、障害物に当たった場合
        if (Physics.Raycast(_shootRay, out _shootHit, _range, _shootableMask))
        {
            //弾が当たった障害物のEnemyHealthスクリプトコンポーネントを取得する
            EnemyHealth enemyHealth = _shootHit.collider.GetComponent<EnemyHealth>();

            //EnemyHealthスクリプトコンポーネントがnullではない場合(敵に弾が当たった場合)
            if (enemyHealth != null)
            {
                //敵にダメージを与える
                enemyHealth.TakeDamage(_damagePerShot, _shootHit.point);
            }
            //射線を障害物で当たった場所で止める
            _gunLine.SetPosition(1, _shootHit.point);
        }
        //障害物に当たらなかった場合
        else
        {
            //射線を弾の飛距離分表示する
            _gunLine.SetPosition(1, _shootRay.origin + (_shootRay.direction * _range));
        }
    }
}