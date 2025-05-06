using System;
using UnityEngine;


public class SwordHitDetector : MonoBehaviour
{
    // Layer mask to filter only enemies
    [Tooltip("Which layers count as enemies?")]
    public LayerMask enemyLayerMask;

    // Fired when we hit an enemy collider
    public event Action<Collider> OnSwordHit;

    [SerializeField] private Collider swordCollider;

    private void Awake()
    {

        swordCollider.isTrigger = true; // ensure it's a trigger
    }

    private void OnTriggerEnter(Collider other)
    {
        // Only notify if the other object is in the enemy layer mask
        if ((enemyLayerMask.value & (1 << other.gameObject.layer)) != 0)
        {
            OnSwordHit?.Invoke(other);
        }
    }


    public void EnableDetector() => swordCollider.enabled = true;
    public void DisableDetector() => swordCollider.enabled = false;
}
