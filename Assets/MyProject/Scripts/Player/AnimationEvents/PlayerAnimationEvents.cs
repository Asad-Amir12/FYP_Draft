using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{
    [SerializeField] private Collider weaponCollider;

    public void EnableWeaponCollider()
    {
        if (weaponCollider != null)
        {
            weaponCollider.enabled = true;
        }
        else
        {
            Debug.LogWarning("Weapon collider not assigned in PlayerAnimationEvents.");
        }
    }

    public void DisableWeaponCollider()
    {
        if (weaponCollider != null)
        {
            weaponCollider.enabled = false;
        }
        else
        {
            Debug.LogWarning("Weapon collider not assigned in PlayerAnimationEvents.");
        }
    }
}
