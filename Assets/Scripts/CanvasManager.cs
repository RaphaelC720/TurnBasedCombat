using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    public Image Playerfillbar;
    public P1Script Player;
    public Image Enemyfillbar;
    public EnemyScript Enemy;
    public ArrowQTE QTE;
    public Image TimerFillbar;
    public TextMeshProUGUI playertext;
    public TextMeshProUGUI enemytext;

    void Update()
    {
        float HealthPercent = Mathf.Clamp01(Player.CurrentHealth / Player.MaxHealth);
        Playerfillbar.fillAmount = HealthPercent;

        float EHealthPercent = Mathf.Clamp01(Enemy.CurrentHealth / Enemy.MaxHealth);
        Enemyfillbar.fillAmount = EHealthPercent;

        float TimerPercent = Mathf.Clamp01(QTE.timeLeft / QTE.timeLimit);
        TimerFillbar.fillAmount = TimerPercent;

        //playertext.text = " " + Player.DmgTaken + " ";
        //enemytext.text = " " + Enemy.EnemyDmgTaken + " ";
    }
}
