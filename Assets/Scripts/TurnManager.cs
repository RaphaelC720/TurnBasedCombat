using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public static TurnManager t_Mgr;
    public enum TurnOrder
    {
        nullTurn = 0,
        PlayerTurn = 1,
        EnemyTurn = 2,
    }
    public P1Script player;
    public EnemyScript enemy;
    public CanvasManager canvasManager;
    public GameObject AttackUI;

    public TurnOrder CurrentTurn;

    public void Start()
    {
        t_Mgr = this;
        CurrentTurn = TurnOrder.PlayerTurn;
        AttackUI.SetActive(true);

    }
    public void Update()
    {
        TurnOrder t = CurrentTurn;

        switch (CurrentTurn)
        {
            case TurnOrder.nullTurn:
                AttackUI.SetActive(false);
                break;
            case TurnOrder.PlayerTurn:
                break;
            case TurnOrder.EnemyTurn:
                AttackUI.SetActive(false);
                break;
        }

        if (CurrentTurn == TurnOrder.PlayerTurn && player.endTurn)
        {
            player.endTurn = false;
            SetTurnState(TurnOrder.EnemyTurn); 
        }
        if (CurrentTurn == TurnOrder.EnemyTurn && enemy.endTurn)
        {
            enemy.endTurn = false;
            AttackUI.SetActive(true);

            SetTurnState(TurnOrder.PlayerTurn);
        }
    }

    public void SetTurnState(TurnOrder t)
    {
        if (CurrentTurn == t) { return; }
        CurrentTurn = t;
        Debug.Log("Switched to: " + CurrentTurn);

        if (t == TurnOrder.nullTurn)
        {
            player.SetState(P1Script.PlayerState.Idle);
            CurrentTurn = t;
        }

        else if (t == TurnOrder.PlayerTurn)
        {
            player.SetState(P1Script.PlayerState.Idle);
        }

        else if (t == TurnOrder.EnemyTurn)
        {
            Debug.Log("attacked");
            enemy.randomATK();
        }
        //conditional checks that change the state and set variable go here
    }
}
