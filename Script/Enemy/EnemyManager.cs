using UnityEngine;

public class EnemyManager : EnemyCorl
{
    //敵のオブジェクト
    public GameObject _enemy = default;
    //敵の生成間隔
    public float _spawnTime = 3f;
    //敵を生成する場所
    [SerializeField]
    public Transform[] _spawnPoints;
    [SerializeField]
    private GameObject _player = default;

    /// <summary>
    /// 敵を生成する間隔で、敵を生成する処理を呼ぶ
    /// </summary>
    new void Start()
    {
        _playerHealth = _player.GetComponent<PlayerHealth>();
        //_currentHealth = GetComponent<CurrentHealth>();
        //InvokeRepeating(一定時間後に繰り返し実行)
        InvokeRepeating("Spawn", _spawnTime, _spawnTime);
    }

    /// <summary>
    /// 敵を生成する処理
    /// </summary>
    void Spawn()
    {
        //プレイヤーのHPが0以下の場合
        if (_playerHealth._playerCurrentHealth <= 0f)
        {
            return;
        }

        //敵を生成する場所をランダムに決める(複数スポーン地点がある場合のみ)
        int spawnPointIndex = Random.Range(0, _spawnPoints.Length);

        //敵を生成する
        Instantiate(_enemy, _spawnPoints[spawnPointIndex].position, 
                            _spawnPoints[spawnPointIndex].rotation);
    }
}