using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework.Fsm;

namespace StarForce
{
    public class DeathState : FsmState<IPlayer>
    {
        protected override void OnEnter(IFsm<IPlayer> procedureOwner)
        {
            procedureOwner.Owner.PlayAnimation("Death");

        }

        protected override void OnUpdate(IFsm<IPlayer> procedureOwner, float elapseSeconds, float realElapseSeconds)
        {

        }

        protected override void OnLeave(IFsm<IPlayer> procedureOwner, bool isShutdown)
        {

        }
    }
}

