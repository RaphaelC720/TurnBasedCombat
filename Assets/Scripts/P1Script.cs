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
    public GameObject atkUI;

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
        if (CurrentHealth == 0)
        {
            die();
        }
    }
    public void FixedUpdate()
    {
        switch (myState)
        {
            case PlayerState.Idle:
                break;
            case PlayerState.Attacking:
                break;
            case PlayerState.Blocking:
                break;
            case PlayerState.TakingDmg:
                break;
            case PlayerState.Dying:
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
        AQTEScript.timeLimit = 4f;
        AQTEScript.numArrows = 5;
        atkUI.SetActive(false);
        StartCoroutine(DoQTE(QTEtype.Arrows, 0, 5, 15));
    }
    public void MediumAttack()
    {
        SetState(PlayerState.Attacking);
        AQTEScript.timeLimit = 5f;
        AQTEScript.numArrows = 10;
        atkUI.SetActive(false);
        StartCoroutine(DoQTE(QTEtype.Arrows, 0, 8, 25));
    }
    public void HeavyAttack()
    {
        SetState(PlayerState.Attacking);
        AQTEScript.timeLimit = 6f;
        AQTEScript.numArrows = 15;
        atkUI.SetActive(false);
        StartCoroutine(DoQTE(QTEtype.Arrows, 0, 12, 40));
    }
    public void TakeDmg(int dmg)
    {
        PlayerState s = PlayerState.TakingDmg;
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
