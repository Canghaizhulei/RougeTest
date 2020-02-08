using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StarForce
{
    public abstract class IMonster : TargetableObjectData
    {
        public IMonster(int entityId, int typeId, CampType camp) : base(entityId, typeId, camp)
        {
        }

        public abstract void PlayAnimation(string name);
        public abstract void Move(Vector3 direct);
    }
}
