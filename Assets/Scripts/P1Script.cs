using System.Collections;
using UnityEngine;

public class P1Script : MonoBehaviour
{
    public enum PlayerState
    {
        Idle = 0,
        Attacking = 1,
        Blocking = 2,
        TakingDmg = 3,
        Dying = 4
    }
    public enum QTEtype
    {
        Arrows = 0,
    }
    public enum QTEresult
    {
        Miss = 0,
        Good = 1,
        Perfect = 2
    }

    public GameObject player;
    public SpriteRenderer mySR;
    public int CurrentHealth;
    public float MaxHealth;
    public PlayerState myState;
    public QTEtype myQTEtype;
    public ArrowQTE AQTEScript;
    public EnemyScript enemy;
    public bool endTurn;

    public TurnManager myTurnMgr;

    public void Start()
    {
        mySR = GetComponent<SpriteRenderer>();
        myTurnMgr = TurnManager.t_Mgr;

        PlayerState s = PlayerState.Idle;

        SetState(s);
    }

    public void Update()
    {

    }
    public void FixedUpdate()
    {
        switch (myState)
        {
            case PlayerState.Idle:
                mySR.color = Color.white;
                break;
            case PlayerState.Attacking:
                mySR.color = Color.white;
                break;
            case PlayerState.Blocking:
                mySR.color = Color.white;
                break;
            case PlayerState.TakingDmg:
                //mySR.color = Color.red;
                break;
            case PlayerState.Dying:
                mySR.color = Color.grey;
                break;

        }
        switch (myQTEtype)
        {
            case QTEtype.Arrows:
                break;
        }

    }
    public void SetState(PlayerState s)
    {
        if (myState == s) { return; }
        myState = s;

        if (myState == PlayerState.Idle)
        {
            //myAnim.Play("Idle");
        }
        else if (myState == PlayerState.Attacking)
        {
            //myAnim.Play("Attacking");
        }
        else if (myState == PlayerState.Blocking)
        {
            //myAnim.Play("Block");
        }
        else if (myState == PlayerState.TakingDmg)
        {
            //myAnim.Play("Hurt");
        }
        else if (myState == PlayerState.Dying)
        {
            //myAnim.Play("Dying");
        }
    }

    IEnumerator DoQTE(QTEtype type, int missDmg, int goodDmg, int perfectDmg)
    {
        QTEresult result = QTEresult.Perfect;

        if (type == QTEtype.Arrows)
        {
            ArrowQTE aQTE = FindAnyObjectByType<ArrowQTE>();
            yield return StartCoroutine(aQTE.StartArrowQTE((r) => result = r));
        }

        if (result == QTEresult.Miss)
        {
            enemy.TakeDmg(missDmg);
        }
        else if (result == QTEresult.Good)
        {
            enemy.TakeDmg(goodDmg);
        }
        else if (result == QTEresult.Perfect)
        {
            enemy.TakeDmg(perfectDmg);
        }

        SetState(PlayerState.Idle);
        endTurn = true;
    }

    public void LightAttack()
    {
        SetState(PlayerState.Attacking);
        AQTEScript.timeLimit = 3f;
        AQTEScript.numArrows = 5;
        StartCoroutine(DoQTE(QTEtype.Arrows, 0, 3, 10));
    }
    public void MediumAttack()
    {
        SetState(PlayerState.Attacking);
        AQTEScript.timeLimit = 5f;
        AQTEScript.numArrows = 10;
        StartCoroutine(DoQTE(QTEtype.Arrows, 0, 5, 20));
    }
    public void HeavyAttack()
    {
        SetState(PlayerState.Attacking);
        AQTEScript.timeLimit = 5f;
        AQTEScript.numArrows = 15;
        StartCoroutine(DoQTE(QTEtype.Arrows, 0, 10, 40));
    }
    public void TakeDmg(int dmg)
    {
        PlayerState s = PlayerState.TakingDmg;
        mySR.color = Color.red;
        CurrentHealth -= dmg;
        SetState(s);
    }
    public void die()
    {
        PlayerState s = PlayerState.Dying;
        Destroy(player);
        SetState(s);
    }
}
