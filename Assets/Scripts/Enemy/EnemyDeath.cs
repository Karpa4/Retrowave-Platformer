using System.Collections;
using UnityEngine;
using Zenject;

public class EnemyDeath : BaseDeath, IScore
{
    [SerializeField] private int score;
    [SerializeField] private float timeToDestroy;
    [SerializeField] private Collider2D col;
    [SerializeField] private BaseEnemy baseEnemy;

    private PlayerScore playerScore;

    public int Score { get => score; set => score = value; }

    [Inject]
    public void Construct(PlayerScore playerScore)
    {
        this.playerScore = playerScore;
    }

    protected override void IsDead()
    {
        baseEnemy.EnemyIsDead();
        playerScore.AddScore(score);
        col.enabled = false;
        StartCoroutine(DelayDestroy());
        base.IsDead();
    }

    private IEnumerator DelayDestroy()
    {
        yield return new WaitForSeconds(timeToDestroy);
        Destroy(gameObject);
    }
}
