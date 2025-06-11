using UnityEngine;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
public class AnimationEvents : MonoBehaviour
{
    [SerializedDictionary("ClipName", "Collider")]
    public SerializedDictionary<string, List<Collider>> Hitboxes;

    public void EnableBallCollider()
    {
        if (Hitboxes.TryGetValue("Ball", out List<Collider> ballCollider))
        {
            foreach (var collider in ballCollider)
            {
                collider.enabled = true;
            }
        }

        else
        {
            Debug.LogWarning("Ball collider not found in Hitboxes dictionary.");
        }
    }
    public void DisableBallCollider()
    {
        if (Hitboxes.TryGetValue("Ball", out List<Collider> ballCollider))
        {
            foreach (var collider in ballCollider)
            {
                collider.enabled = false;
            }
        }
        else
        {
            Debug.LogWarning("Ball collider not found in Hitboxes dictionary.");
        }
    }

    public void EnableHandCollider()
    {
        if (Hitboxes.TryGetValue("Hand", out List<Collider> handCollider))
        {
            foreach (var collider in handCollider)
            {
                collider.enabled = true;
            }
        }
        else
        {
            Debug.LogWarning("Hand collider not found in Hitboxes dictionary.");
        }
    }
    public void DisableHandCollider()
    {
        if (Hitboxes.TryGetValue("Hand", out List<Collider> handCollider))
        {
            foreach (var collider in handCollider)
            {
                collider.enabled = false;
            }
        }
        else
        {
            Debug.LogWarning("Hand collider not found in Hitboxes dictionary.");
        }
    }

    public void DisableAllAttackColliders()
    {
        foreach (var colliderList in Hitboxes.Values)
        {
            foreach (var collider in colliderList)
            {
                collider.enabled = false;
            }
        }
    }

}
