using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework.Fsm;

namespace StarForce
{
    public class ReadyState : FsmState<IPlayer>
    {
        protected override void OnEnter(IFsm<IPlayer> procedureOwner)
        {
            procedureOwner.Owner.PlayAnimation("Attackstandy");
        }

        protected override void OnUpdate(IFsm<IPlayer> procedureOwner, float elapseSeconds, float realElapseSeconds)
        {

        }

        protected override void OnLeave(IFsm<IPlayer> procedureOwner, bool isShutdown)
        {

        }
    }
}

