using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float enemyHealth=100f;
    EnemyAi EnemyAi;
    public bool isenemyDead;

    public Collider[] enemyCol;
    private void Start()
    {
        EnemyAi = GetComponent<EnemyAi>();
    }
    public void DeductHealth(float deductHealth)
    {
        if (!isenemyDead)
        {
            enemyHealth -= deductHealth;
            if (enemyHealth<=0)
            {
                EnemyDead();
            }
        }
    }
    void EnemyDead()
    {
        isenemyDead = true;
        EnemyAi.EnemyDeathAnim();
        EnemyAi.agent.speed = 0f;
        foreach (var col in enemyCol)
        {
            col.enabled = false;
        }
        enemyHealth = 0f;
        UIManager.Instance.killCounter++;
        UIManager.Instance.UpdatekillCounterUI();
        Destroy(gameObject, 3f);
    }

}
