using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StarForce
{
    public class PlayerData : TargetableObjectData
    {
        public PlayerData(int entityId, int typeId) : base(entityId, typeId,CampType.Player)
        {
        }

        public override int MaxHP => 100;

        public int Defense => 10;

        public int Attack => 20;
    }
}

