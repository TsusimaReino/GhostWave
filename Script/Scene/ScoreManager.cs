using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// スコア処理
/// </summary>
public class ScoreManager : MonoBehaviour
{
    ////敵を倒した時のスコア
    [SerializeField]
    public int _scoreValue = 10;
    //スコア
    public static int _score;

    //スコア表示用のテキスト
    Text _text;

    void Awake()
    {
        //テキストコンポーネントのとの取得
        _text = GetComponent<Text>();
        //スコアの初期化
        _score = 0;
    }

    void Update()
    {
        //テキストコンポーネントのテキストに表示する文字列を設定
        _text.text = "Score: " + _score;
        EnemyUi();
    }
    private void EnemyUi()
    {

        //スコアを加算する
        ScoreManager._score += _scoreValue;
    }
}
