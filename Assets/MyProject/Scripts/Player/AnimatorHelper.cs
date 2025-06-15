using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AnimatorHelper
{
    public static float GetClipLengthByHash(Animator animator, int clipHash)
    {
        if (animator?.runtimeAnimatorController == null) return 0f;
        foreach (var clip in animator.runtimeAnimatorController.animationClips)
            if (Animator.StringToHash(clip.name) == clipHash)
                return clip.length;
        return 0f;
    }
}

