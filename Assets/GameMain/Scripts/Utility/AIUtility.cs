//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2020 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace StarForce
{
    /// <summary>
    /// AI 工具类。
    /// </summary>
    public static class AIUtility
    {
        /// <summary>
        /// 获取实体间的距离。
        /// </summary>
        /// <returns>实体间的距离。</returns>
        public static float GetDistance(Entity fromEntity, Entity toEntity)
        {
            Transform fromTransform = fromEntity.CachedTransform;
            Transform toTransform = toEntity.CachedTransform;
            return (toTransform.position - fromTransform.position).magnitude;
        }

        private static int CalcDamageHP(int attack, int defense)
        {
            if (attack <= 0)
            {
                return 0;
            }

            if (defense < 0)
            {
                defense = 0;
            }

            return attack * attack / (attack + defense);
        }

        public static void OnDamage(TargetableObject entity, Entity attacker)
        {
            if (entity == null || attacker == null)
            {
                return;
            }

            TargetableObject target = attacker as TargetableObject;
            if (target != null)
            {
                ImpactData entityImpactData = entity.GetImpactData();
                ImpactData targetImpactData = target.GetImpactData();

                int entityDamageHP = CalcDamageHP(targetImpactData.Attack, entityImpactData.Defense);
                int targetDamageHP = CalcDamageHP(entityImpactData.Attack, targetImpactData.Defense);

                entity.ApplyDamage(target, entityDamageHP);
            }
        }
    }
}
