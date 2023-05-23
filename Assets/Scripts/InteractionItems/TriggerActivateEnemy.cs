using System.Collections.Generic;
using UnityEngine;

public class TriggerActivateEnemy : MonoBehaviour
{
    [SerializeField] private List<GameObject> _gameObjects;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        foreach (GameObject item in _gameObjects)
        {
            item.SetActive(true);
        }
        Destroy(gameObject);
    }
}
