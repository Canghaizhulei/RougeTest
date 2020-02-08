using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extentions
{
    public static T GetOrAddCompontent<T>(this GameObject gameObject) where T : Component
    {
        T t = gameObject.GetComponent<T>();
        if(t == null)
        {
            t = gameObject.AddComponent<T>();
        }

        return t;
    }

    public static void PlayAnimation(this Animator animator,string animation)
    {
        animator.Play(animation, -1, 0);
    }
}
