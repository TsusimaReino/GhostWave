using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// UI関連
/// </summary>
public class PlayerHealth : PlayerCorl
{   
    //スタート時点のHP
    public int _playerStartingHealth = 100;
    //最新のHP
    public int _playerCurrentHealth;
    public Slider _healthSlider;
    //攻撃された時の画像
    public Image _damageImage;
    //倒された時の効果音
    public AudioClip _deathClip;
    //攻撃された時の画像の表示速度
    public float _flashSpeed = 5f;
    //攻撃された時の画像の色
    public Color _flashColour = new Color(1f, 0f, 0f, 0.1f);

    //音楽
    private AudioSource _playerAudio;
    private PlayerMovement _playerMovement;
    private PlayerShoting _playerShoting;
    //プレイヤーが倒されているかのフラグ
    private bool _isDead;
    //プレイヤーがダメージを受けているかのフラグ
    private bool _damaged;

    /// <summary>
    /// 体力を初期化
    /// </summary>
    void Awake()
    {
        _playerAudio = GetComponent<AudioSource>();
        _playerMovement = GetComponent<PlayerMovement>();
        _playerShoting = GetComponentInChildren<PlayerShoting>();

        _playerCurrentHealth = _playerStartingHealth;
    }

    //プレイヤーが攻撃を受けた処理
    void Update()
    {
        //プレイヤーがダメージを受けた場合
        if (_damaged)
        {
            //攻撃された時の画像の色を設定する
            _damageImage.color = _flashColour;
        }
        //ダメージを受けていない場合
        else
        {
            //攻撃された時の画像の色をクリアする
            _damageImage.color = Color.Lerp(_damageImage.color, Color.clear, _flashSpeed * Time.deltaTime);
        }
        //ダメージを受けているかのフラグをオフにする
        _damaged = false;
    }

    //プレイヤーが攻撃された時の処理
    public void TakeDamage(int amount)
    {
        //ダメージを受けているかのフラグをオンにする
        _damaged = true;

        //プレイヤーのHPを減らす
        _playerCurrentHealth -= amount;

        //HPバーを減らす
        _healthSlider.value = _playerCurrentHealth;

        //攻撃を受けた時の効果音を鳴らす
        _playerAudio.Play();

        //HPが0以下になった場合、かつ既にたい押されていない場合、倒される
        if (_playerCurrentHealth <= 0 && !_isDead)
        {
            Death();
        }
    }

    //プレイヤーが倒された場合の処理
    void Death()
    {
        //プレイヤーが倒されているかのフラグをオンにする
        _isDead = true;

        //弾を撃つエフェクトをオフにする
        _playerShoting.DisableEffects();

        //倒された時のアニメーションのフラグをオンにする
        _playeranim.SetTrigger("Die");

        //倒された時の効果音を設定する
        _playerAudio.clip = _deathClip;
        //倒された時の効果音を鳴らす
        _playerAudio.Play();

        //プレイヤーを動けなくする
        _playerMovement.enabled = false;
        //弾を撃てなくする
        _playerShoting.enabled = false;
    }
    /// <summary>
    /// レベルリセット処理 未実装
    /// </summary>
    public void RestartLevel()
    {
        Scene.LoadScene(0);
    }
}