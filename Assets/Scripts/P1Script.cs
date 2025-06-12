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
    public enum AttackTypes
    {
        Fire = 0,
        Water = 1,
        Grass = 2,
        Lightning = 3,
        Air = 4,
        Dark = 5,
        Light = 6
    }
    public GameObject player;
    public SpriteRenderer mySR;
    public int CurrentHealth;
    public int MaxHealth;
    public PlayerState myState;
    public EnemyScript enemy;

    public void Start()
    {
        mySR = GetComponent<SpriteRenderer>();
    }

    public void Update()
    {
        PlayerState s = PlayerState.Idle;

        SetState(s);

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
                mySR.color = Color.red;
                break;
            case PlayerState.Dying:
                mySR.color = Color.grey;
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

    public void Attack()
    {
        enemy.TakeDmg(10);
    }
    void TakeDmg(int dmg)
    {
        PlayerState s = PlayerState.TakingDmg;
        CurrentHealth -= dmg;
        SetState(s);
    }
}
