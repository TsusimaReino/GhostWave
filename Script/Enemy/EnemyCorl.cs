using UnityEngine;
using UnityEngine.AI;
/// <summary>
/// GĚîęNX
/// </summary>
public class EnemyCorl : MonoBehaviour
{
    //vC[ĚHP
    protected PlayerHealth _playerHealth = default;
    //GĚHP
    protected EnemyHealth _enemyHealth = default;
    protected Rigidbody _enemyRigidbody = default;
    protected Animator _enemyAnim = default;
    protected Transform _enemyTarget = default;
    protected GameObject _enemyGameTarget = default;
    protected NavMeshAgent _enemyNav = default;  //NavMesh Agent(G)

    //protected  Transform  _target;
    public void Start()
    {
        
    }
}
