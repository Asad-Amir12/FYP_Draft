using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitCollider : MonoBehaviour
{
    [SerializeField] EnemyStats enemyStats;
    private void Awake()
    {
        if (enemyStats == null)
        {

            // Look up the hierarchy to find the EnemyStats on the root enemy object
            enemyStats = GetComponentInParent<EnemyStats>();
        }

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerWeapon"))
        {
            EventBus.TriggerOnEnemyAttacked();
            Debug.Log("Enemy hit by player weapon");
            enemyStats.ModifyHealth(-DamageDealer.Instance.PlayerAtkDmg);
        }
    }
}
