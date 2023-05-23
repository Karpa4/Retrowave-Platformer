using System;
using System.Collections;
using UnityEngine;

public class PlayerFinder : MonoBehaviour
{
    [SerializeField] private float attackRange;
    [SerializeField] private float checkDelay;
    [SerializeField] private LayerMask playerMask;
    [SerializeField] private LayerMask checkForObstacleMask;

    public event Action<Transform> ChangeTarget;

    private bool playerIsFind;
    private Collider2D[] resultCol;
    private const float DistanceToCheckRotation = 1;

    private void Start()
    {
        playerIsFind = false;
        resultCol = new Collider2D[1];
        StartCoroutine(FindPlayer());
    }

    private IEnumerator FindPlayer()
    {
        int result = 0;
        while (true)
        {
            yield return new WaitForSeconds(checkDelay);
            result = Physics2D.OverlapCircleNonAlloc(transform.position, attackRange, resultCol, playerMask);
            if (result == 0 && playerIsFind)
            {
                ChangeTarget?.Invoke(null);
                playerIsFind = false;
            }
            else if (result == 1)
            {
                CheckForObstacle(resultCol[0].transform);
            }
        }
    }

    private void CheckForObstacle(Transform player)
    {
        Vector2 direction = player.position - transform.position;
        RaycastHit2D rayInfo = Physics2D.Raycast(transform.position, direction, attackRange, checkForObstacleMask);
        if (rayInfo.collider)
        {
            if (rayInfo.collider.CompareTag(GlobalConstants.PlayerTag))
            {
                if (!playerIsFind)
                {
                    ChangeTarget?.Invoke(player);
                    playerIsFind = true;
                }
                CheckRotation(player);
                return;
            }
        }

        if (playerIsFind)
        {
            ChangeTarget?.Invoke(null);
            playerIsFind = false;
        }
    }

    public void CheckRotation(Transform target)
    {
        Quaternion currentRotation = transform.rotation;
        if (target.position.x > transform.position.x && currentRotation.y == 0)
        {
            transform.rotation = new Quaternion(0, 1, 0, 0);
        }
        else if (target.position.x < transform.position.x && currentRotation.y == 1)
        {
            transform.rotation = new Quaternion(0, 0, 0, 1);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
