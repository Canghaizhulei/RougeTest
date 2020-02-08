using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework.Fsm;

namespace StarForce
{
    public class RunState : FsmState<IPlayer>
    {
        private IPlayer player;
        IFsm<IPlayer> fsm;

        protected override void OnEnter(IFsm<IPlayer> procedureOwner)
        {
            procedureOwner.Owner.PlayAnimation("Run");
            fsm = procedureOwner;
            player = procedureOwner.Owner;
            EasyJoystick.On_JoystickMove += OnJoystickMove;
            EasyJoystick.On_JoystickMoveEnd += OnJoystickEnd;
        }

        protected override void OnUpdate(IFsm<IPlayer> procedureOwner, float elapseSeconds, float realElapseSeconds)
        {

        }

        protected override void OnLeave(IFsm<IPlayer> procedureOwner, bool isShutdown)
        {
            EasyJoystick.On_JoystickMove -= OnJoystickMove;
            EasyJoystick.On_JoystickMoveEnd -= OnJoystickEnd;
        }

        void OnJoystickMove(MovingJoystick move)
        {
            player.Move(new Vector3(move.joystickAxis.x, 0, move.joystickAxis.y));
        }

        void OnJoystickEnd(MovingJoystick move)
        {
            ChangeState<IdleState>(fsm);
        }
    }
}

