using GameFramework.Event;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;
namespace StarForce
{
    public enum GameState
    {
        Ready,
        Start,
        End
    }

    public class GameMgr : Singleton<GameMgr>
    {
        private const float minAttackDistance = 3;
        private int[] monsterCountPerTimes = new int[] { 3};
        private float createMonsterTime;
        private PlayerTom player;
        private List<SoulMonster> monsters;
        public GameState State { private set; get; }
        private List<Vector3> monsterPositions;
        private float timer;
        private int wave;
        private bool isWinner;

        public PlayerTom Player => player;

        public void OnInit()
        {
            monsterPositions = new List<Vector3> { new Vector3(2, 0, -6), new Vector3(24, 0, -6), new Vector3(28.5f, 0, 16.3f), new Vector3(7.3f, 0, 5.1f), new Vector3(-6, 0, 14.7f), new Vector3(18, 0, 14.7f) };
        }

        public void OnEnter()
        {
            GameEntry.Event.Subscribe(ShowEntitySuccessEventArgs.EventId, OnShowEntitySuccess);
            GameEntry.Event.Subscribe(ShowEntityFailureEventArgs.EventId, OnShowEntityFailure);

            GameEntry.Entity.ShowPlayer(new PlayerData(GameEntry.Entity.GenerateSerialId(), 10000) {
                Position = new Vector3(12,0,-6)
            });

            monsters = new List<SoulMonster>();
            ChangeState(GameState.Start);
            timer = 0;
            wave = 0;
            createMonsterTime = 2f;
        }

        public void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            if(State == GameState.Start)
            {
                timer += elapseSeconds;
                if(timer > createMonsterTime)
                {
                    createMonsterTime = 15f;
                    timer = 0;
                    if (wave< monsterCountPerTimes.Length)
                    {
                        CreateMonster(monsterCountPerTimes[wave]);
                        wave++;
                    }
                }
            }
        }

        public void OnLeave()
        {
            GameEntry.Event.Unsubscribe(ShowEntitySuccessEventArgs.EventId, OnShowEntitySuccess);
            GameEntry.Event.Unsubscribe(ShowEntityFailureEventArgs.EventId, OnShowEntityFailure);
        }

        public void ChangeState(GameState state)
        {
            State = state;
            switch (state)
            {
                case GameState.Ready:
                    break;
                case GameState.Start:
                    break;
                case GameState.End:
                    break;
                default:
                    Debug.LogError("not add this state: " + state);
                    break;
            }
        }

        protected virtual void OnShowEntitySuccess(object sender, GameEventArgs e)
        {
            ShowEntitySuccessEventArgs ne = (ShowEntitySuccessEventArgs)e;
            if (ne.EntityLogicType == typeof(PlayerTom))
            {
                player = (PlayerTom)ne.Entity.Logic;
                CameraFollow.Instance.Target = player.transform;
            }else if(ne.EntityLogicType == typeof(SoulMonster))
            {
                monsters.Add((SoulMonster)ne.Entity.Logic);
            }
        }

        protected virtual void OnShowEntityFailure(object sender, GameEventArgs e)
        {
            ShowEntityFailureEventArgs ne = (ShowEntityFailureEventArgs)e;
            Log.Warning("Show entity failure with error message '{0}'.", ne.ErrorMessage);
        }

        private void CreateMonster(int count)
        {
            List<Vector3> pos = GetRandomMonsterPosition(count);
            for(int i = 0;i < count; i++)
            {
                GameEntry.Entity.ShowMonster(new MonsterData(GameEntry.Entity.GenerateSerialId(), 10001) {
                    Position = pos[i]
                });
            }
        }

        List<Vector3> GetRandomMonsterPosition(int count)
        {
            if(count > monsterPositions.Count)
            {
                Log.Error("not enough position");
                return null;
            }

            for(int i = 0;i < 50;i++)
            {
                int rad = Random.Range(0, monsterPositions.Count);
                int rad2 = Random.Range(0, monsterPositions.Count);
                Vector3 temp = monsterPositions[rad];
                monsterPositions[rad] = monsterPositions[rad2];
                monsterPositions[rad2] = temp;
            }

            return monsterPositions.GetRange(0, count);
        }

        public void OnMonsterDeath(SoulMonster monster)
        {
            if (monsters.Contains(monster))
            {
                monsters.Remove(monster);
            }

            if(wave >= monsterCountPerTimes.Length && monsters.Count == 0)
            {
                isWinner = true;
                ChangeState(GameState.End);
            }
        }

        public void OnPlayerDeath()
        {
            isWinner = false;
            ChangeState(GameState.End);
        }

        public bool CanAttackPlayer(Entity attacker)
        {
            return AIUtility.GetDistance(player, attacker) < minAttackDistance;
        }

        public SoulMonster GetPlayerAttackTarget()
        {
            if(monsters.Count <= 0)
            {
                return null;
            }

            float minDis = float.MaxValue;
            int minIndex = 0;
            for(int i = 0;i < monsters.Count; i++)
            {
                float dis = AIUtility.GetDistance(player, monsters[i]);
                if(dis< minDis)
                {
                    minDis = dis;
                    minIndex = i;
                }
            }

            if(minDis < minAttackDistance)
            {
                return monsters[minIndex];
            }

            return null;
        }
    }
}