using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//仮manager
public class GameManager : MonoBehaviour
{
    public static GameManager _instance;

    public int _totalEnemies;
    public int _enemiesRemaining;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        _enemiesRemaining = _totalEnemies;
    }

    public void EnemyDestroyed()
    {
        _enemiesRemaining--;

        if (_enemiesRemaining <= 0)
        {
            Debug.Log("All enemies destroyed! You win!");
            // クリア処理をここに追加する（例えばゲームを停止する、クリア画面を表示するなど）
        }
    }
}

