using UnityEngine;
using UnityEngine.AI;
/// <summary>
/// “G‚ÌŠî’êƒNƒ‰ƒX
/// </summary>
public class EnemyCorl : MonoBehaviour
{
    //ƒvƒŒƒCƒ„[‚ÌHP
    protected PlayerHealth _playerHealth = default;
    //“G‚ÌHP
    protected EnemyHealth _enemyHealth = default;
    protected Rigidbody _enemyRigidbody = default;
    protected Animator _enemyAnim = default;
    protected Transform _enemyTarget = default;
    protected GameObject _enemyGameTarget = default;
    protected NavMeshAgent _enemyNav = default;  //NavMesh Agent(“G)

    //protected  Transform  target;
    public void Start()
    {
        
    }
}
