using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
/// <summary>
/// インターフェースでダメージ継承
/// </summary>
public interface IDamageable
{
    public void Damege(int valure);//ダメージ処理
    public void Death();//死んだ処理
}

public class PlayerDamage : MonoBehaviour //インターフェースコンポーネント取得使用
{
    private void Update()
    {
        GetComponent<IDamageable>();
    }
}
