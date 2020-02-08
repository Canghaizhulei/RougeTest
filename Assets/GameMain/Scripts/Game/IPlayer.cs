using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StarForce
{
    public abstract class IPlayer : TargetableObject
    {
        public abstract void PlayAnimation(string name);
        public abstract void Move(Vector3 direct);
    }
}

