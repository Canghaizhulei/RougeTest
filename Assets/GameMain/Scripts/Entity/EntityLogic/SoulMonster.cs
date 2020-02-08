using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;
using UnityEngine.AI;

namespace StarForce
{
    public class SoulMonster : TargetableObject
    {
        [SerializeField]
        private MonsterData data;
        private NavMeshAgent agent;

        protected override void OnInit(object userData)
        {
            base.OnInit(userData);

            data = userData as MonsterData;
            if (data == null)
            {
                Log.Error(" Monster data is not valide");
                return;
            }

            agent = gameObject.GetComponent<NavMeshAgent>();
        }

        protected override void OnShow(object userData)
        {
            base.OnShow(userData);
            agent.SetDestination(GameMgr.Instance.Player.transform.position); ;
        }

        protected override void OnDead(Entity attacker)
        {
            base.OnDead(attacker);
            GameMgr.Instance.OnMonsterDeath(this);
        }

        public override ImpactData GetImpactData()
        {
            return new ImpactData(CampType.Enemy, data.HP, data.Attack, data.Defense);
        }
    }
}

