using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StarForce
{
    public class MonsterData : IMonster
    {
        public MonsterData(int entityId, int typeId) : base(entityId, typeId, CampType.Enemy)
        {
        }

        public override int MaxHP => 20;

        public int Defense => 5;

        public int Attack => 10;

        public override void Move(Vector3 direct)
        {
        }

        public override void PlayAnimation(string name)
        {
        }
    }
}

