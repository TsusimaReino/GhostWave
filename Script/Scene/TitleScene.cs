using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// シーン遷移
/// </summary>
public class TitleScene : MonoBehaviour
{
    void Update()
    {
        // もし入力したキーがSpaceキーならば、中の処理を実行する
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // SampleSceneに切り替える
            SceneManager.LoadScene("MainGame");
        }
    }
}