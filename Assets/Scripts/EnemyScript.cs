using System.Collections;
using UnityEngine;
using static P1Script;

public class EnemyScript : MonoBehaviour
{
    public enum EnemyState
    {
        Idle = 0,
        Attacking = 1,
        TakingDmg = 2,
        Died = 3
    }

    public GameObject enemy;
    public SpriteRenderer enemySR;
    public float MaxHealth;
    public float CurrentHealth;
    public EnemyState eState;
    public P1Script target;
    public ArrowQTE ArrowScript;
    public TurnManager myTurnMgr;

    public bool endTurn;
    void Start()
    {
        enemySR = GetComponent<SpriteRenderer>();
        myTurnMgr = TurnManager.t_Mgr;

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
                break;
            case EnemyState.Attacking:
                break;
            case EnemyState.TakingDmg:
                break;
            case EnemyState.Died:
                break;

        }
    }
    public void SetEnemyState(EnemyState e)
    {
        if (eState == e) { return; }
        eState = e;
    }

    IEnumerator DoQTE(QTEtype type, int noDmg, int lowDmg, int normalDmg)
    {
        QTEresult result = QTEresult.Perfect;

        if (type == QTEtype.Arrows)
        {
            ArrowQTE aQTE = FindAnyObjectByType<ArrowQTE>();
            yield return StartCoroutine(aQTE.StartArrowQTE((r) => result = r));
        }

        if (result == QTEresult.Miss)
        {
            target.TakeDmg(normalDmg);
        }
        else if (result == QTEresult.Good)
        {
            target.TakeDmg(lowDmg);
        }
        else if (result == QTEresult.Perfect)
        {
            target.TakeDmg(noDmg);
        }
        SetEnemyState(EnemyState.Idle);
        endTurn = true;
    }

    public void randomATK()
    {
        int r = Random.Range(0, 3);
        switch (r)
        {
            case 0:
                Attack1();
                break;
            case 1:
                Attack2();
                break;
            case 2:
                Attack3();
                break;
        }
        return;
    }
    public void Attack1()
    {
        SetEnemyState(EnemyState.Attacking);
        ArrowScript.timeLimit = 3f;
        ArrowScript.numArrows = 5;
        StartCoroutine(DoQTE(QTEtype.Arrows, 0, 8, 15));
    }
    public void Attack2()
    {
        SetEnemyState(EnemyState.Attacking);
        ArrowScript.timeLimit = 4f;
        ArrowScript.numArrows = 7;
        StartCoroutine(DoQTE(QTEtype.Arrows, 0, 12, 20));
    }
    public void Attack3()
    {
        SetEnemyState(EnemyState.Attacking);
        ArrowScript.timeLimit = 5f;
        ArrowScript.numArrows = 10;
        StartCoroutine(DoQTE(QTEtype.Arrows, 0, 17, 25));
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
