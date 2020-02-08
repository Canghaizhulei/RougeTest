using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework.Fsm;
using UnityGameFramework.Runtime;

namespace StarForce
{
    public class PlayerTom : IPlayer
    {
        public const float AttackInternal = 1f;
        [SerializeField]
        private PlayerData data;
        private CharacterController character;
        private float moveSpeed = 5;
        private float rotateSpeed = 30;
        private IFsm<IPlayer> fsm;
        private Animator ani;

        protected override void OnInit(object userData)
        {
            base.OnInit(userData);

            data = userData as PlayerData;
            if(data == null)
            {
                Log.Error(" player data is not valide");
                return;
            }

            character = gameObject.GetOrAddComponent<CharacterController>();
            ani = GetComponent<Animator>();
            fsm = GameEntry.Fsm.CreateFsm<IPlayer>(this, new FsmState<IPlayer>[] { new IdleState(), new RunState(), new AttactState() });
            fsm.Start<IdleState>();
        }

        protected override void OnShow(object userData)
        {
            base.OnShow(userData);

            transform.position = data.Position;
            transform.eulerAngles = Vector3.zero;
        }

        protected override void OnDead(Entity attacker)
        {
            base.OnDead(attacker);
            fsm.Change<DeathState>();
            GameMgr.Instance.OnPlayerDeath();
        }

        public override void PlayAnimation(string name)
        {
            ani.PlayAnimation(name);
        }

        public void StartGame()
        {
        }

        public override void Move(Vector3 dir)
        {
            character.SimpleMove(dir * moveSpeed);
            transform.LookAt(transform.position + dir);
        }

        public override ImpactData GetImpactData()
        {
            return new ImpactData(CampType.Player, data.HP, data.Attack, data.Defense);
        }
    }
}

