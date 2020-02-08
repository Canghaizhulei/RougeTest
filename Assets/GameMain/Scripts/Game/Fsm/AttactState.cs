using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework.Fsm;

namespace StarForce
{
    public class AttactState : FsmState<IPlayer>
    {
        private float attackTimer;
        IFsm<IPlayer> procedureOwner;

        bool startAttack;
        float attackingTimer;
        SoulMonster attackingMonster;

        protected override void OnEnter(IFsm<IPlayer> procedureOwner)
        {
            attackTimer = 10000;
            this.procedureOwner = procedureOwner;
            startAttack = false;
            attackingTimer = 0;
            EasyJoystick.On_JoystickMove += OnJoystickMove;
        }

        protected override void OnUpdate(IFsm<IPlayer> procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            attackTimer += elapseSeconds;
            if(attackTimer > PlayerTom.AttackInternal)
            {
                attackTimer = 0;
                TryAttack();
            }

            if (startAttack)
            {
                attackingTimer += elapseSeconds;
                if(attackingTimer >= 0.8f)
                {
                    AttackOver();
                    attackingTimer = 0;
                }
            }
        }

        protected override void OnLeave(IFsm<IPlayer> procedureOwner, bool isShutdown)
        {
            EasyJoystick.On_JoystickMove -= OnJoystickMove;
            this.procedureOwner = null;
            attackingMonster = null;
        }

        void TryAttack()
        {
            SoulMonster monster = GameMgr.Instance.GetPlayerAttackTarget();
            if(monster == null)
            {
                ChangeState<IdleState>(procedureOwner);
            }else
            {
                StartAttack(monster);
            }
        }

        void StartAttack(SoulMonster monster)
        {
            startAttack = true;
            attackingTimer = 0;
            procedureOwner.Owner.transform.LookAt(monster.transform);
            procedureOwner.Owner.PlayAnimation("Attack00");
            attackingMonster = monster;
        }

        void AttackOver()
        {
            startAttack = false;
            AIUtility.OnDamage(attackingMonster, procedureOwner.Owner);
            attackingMonster = null;
        }

        void OnJoystickMove(MovingJoystick move)
        {
            ChangeState<RunState>(procedureOwner);
        }
    }
}
