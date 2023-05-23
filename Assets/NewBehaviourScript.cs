using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public int score;
    public int count;
    public int enemyCount;

    void Start()
    {
        foreach (CollectScore go in Resources.FindObjectsOfTypeAll(typeof(CollectScore)) as CollectScore[])
        {
            if (!EditorUtility.IsPersistent(go.transform.root.gameObject) && !(go.hideFlags == HideFlags.NotEditable || go.hideFlags == HideFlags.HideAndDontSave))
            {
                count++;
                score += go.Score;
            }
        }

        foreach (EnemyDeath go in Resources.FindObjectsOfTypeAll(typeof(EnemyDeath)) as EnemyDeath[])
        {
            if (!EditorUtility.IsPersistent(go.transform.root.gameObject) && !(go.hideFlags == HideFlags.NotEditable || go.hideFlags == HideFlags.HideAndDontSave))
            {
                enemyCount++;
                score += go.Score;
            }
        }
    }

    
}
