using Unity.VisualScripting;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public enum EnemyState
    {
        Idle = 0,
        Attacking = 1,
        TakingDmg = 2,
        Died = 3
    }

    public enum EnemyType
    {
        Fire = 0,
        Water = 1,
        Grass = 2,
        Lightning = 3,
        Air = 4,
        Dark = 5,
        Light = 6
    }

    public GameObject enemy;
    public SpriteRenderer enemySR;
    public int MaxHealth;
    public int CurrentHealth;
    public EnemyState eState;
    public P1Script target;
    void Start()
    {
        enemySR = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        EnemyState e = EnemyState.Idle;
        if (CurrentHealth <= 0)
            die();
        
        SetEnemyState(e);
    }
    
    public void FixedUpdate()
    {
        switch (eState)
        {
            case EnemyState.Idle:
                enemySR.color = Color.white;
                break;
            case EnemyState.Attacking:
                enemySR.color = Color.white;
                break;
            case EnemyState.TakingDmg:
                enemySR.color = Color.red;
                break;
            case EnemyState.Died:
                enemySR.color = Color.grey;
                break;

        }
    }
    public void SetEnemyState(EnemyState e)
    {
        if (eState == e) { return; }
        eState = e;

        if (eState == EnemyState.Idle)
        {
            //myAnim.Play("Idle");
        }
        else if (eState == EnemyState.Attacking)
        {
            //myAnim.Play("Attacking");
        }

        else if (eState == EnemyState.TakingDmg)
        {
            //myAnim.Play("Hurt");
        }
        else if (eState == EnemyState.Died)
        {
            //myAnim.Play("Dying");
        }
    }
    public void TakeDmg(int dmg)
    {
        EnemyState s = EnemyState.TakingDmg;
        CurrentHealth -= dmg;
        SetEnemyState(s);
    }
    public void die()
    {
        EnemyState e = EnemyState.Died;
        Destroy(enemy);
        SetEnemyState(e);
    }
}
