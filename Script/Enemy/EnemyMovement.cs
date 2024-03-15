using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : EnemyCorl
{
    private Transform _player;

    void Awake()
    {
        //プレイヤータグのゲームオブジェクトを探し、場所を取得する
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        //プレイヤーのHPを取得する
        _playerHealth = _player.GetComponent<PlayerHealth>();
        //敵のHPを取得
        _enemyHealth = GetComponent<EnemyHealth>();
        //NavMeshAgentコンポーネントを取得する
        _enemyNav = GetComponent<NavMeshAgent>();
    }

    /// <summary>
    /// 敵をプレイヤーに向かって動かす
    /// </summary>
    private void Update()
    {
        //敵とプレイヤーが生きている場合
        if (_enemyHealth._enemyCurrentHealth > 0 && _playerHealth._playerCurrentHealth > 0)
        {
            //NavMesh Agent(敵)をプレイヤーに向かって動かす
            _enemyNav.SetDestination(_player.position);
        }
        //敵もしくは、プレイヤーが倒されている場合
        else
        {
            _enemyNav.enabled = false;
        }
    }
}
