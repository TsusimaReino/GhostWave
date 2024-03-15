using UnityEngine;

public class EnemyManager : EnemyCorl
{
    //“G‚ÌƒIƒuƒWƒFƒNƒg
    public GameObject _enemy = default;
    //“G‚Ì¶¬ŠÔŠu
    public float _spawnTime = 3f;
    //“G‚ğ¶¬‚·‚éêŠ
    [SerializeField]
    public Transform[] _spawnPoints;
    [SerializeField]
    private GameObject _player = default;

    /// <summary>
    /// “G‚ğ¶¬‚·‚éŠÔŠu‚ÅA“G‚ğ¶¬‚·‚éˆ—‚ğŒÄ‚Ô
    /// </summary>
    new void Start()
    {
        _playerHealth = _player.GetComponent<PlayerHealth>();
        //_currentHealth = GetComponent<CurrentHealth>();
        //InvokeRepeating(ˆê’èŠÔŒã‚ÉŒJ‚è•Ô‚µÀs)
        InvokeRepeating("Spawn", _spawnTime, _spawnTime);
    }

    /// <summary>
    /// “G‚ğ¶¬‚·‚éˆ—
    /// </summary>
    void Spawn()
    {
        //ƒvƒŒƒCƒ„[‚ÌHP‚ª0ˆÈ‰º‚Ìê‡
        if (_playerHealth._playerCurrentHealth <= 0f)
        {
            return;
        }

        //“G‚ğ¶¬‚·‚éêŠ‚ğƒ‰ƒ“ƒ_ƒ€‚ÉŒˆ‚ß‚é
        int spawnPointIndex = Random.Range(0, _spawnPoints.Length);

        //“G‚ğ¶¬‚·‚é
        Instantiate(_enemy, _spawnPoints[spawnPointIndex].position, 
                            _spawnPoints[spawnPointIndex].rotation);
    }
}