using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework.Fsm;

namespace StarForce
{
    public class IdleState : FsmState<IPlayer>
    {
        private IPlayer player;
        IFsm<IPlayer> fsm;

        protected override void OnEnter(IFsm<IPlayer> procedureOwner)
        {
            procedureOwner.Owner.PlayAnimation("Attackstandy");
            fsm = procedureOwner;
            player = procedureOwner.Owner;
            EasyJoystick.On_JoystickMove += OnJoystickMove;

        }

        protected override void OnUpdate(IFsm<IPlayer> procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            if(GameMgr.Instance.GetPlayerAttackTarget() != null)
            {
                ChangeState<AttactState>(procedureOwner);
            }
        }

        protected override void OnLeave(IFsm<IPlayer> procedureOwner, bool isShutdown)
        {
            EasyJoystick.On_JoystickMove -= OnJoystickMove;
        }

        void OnJoystickMove(MovingJoystick move)
        {
            ChangeState<RunState>(fsm);
        }
    }
}

