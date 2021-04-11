using Komastar.IdleMiner.Data;
using Komastar.IdleMiner.Manager;
using UnityEngine;
using UnityEngine.Events;

namespace Komastar.IdleMiner.Enemy
{
    public class EnemyModel
    {
        public UnityAction<int> OnTakeDamage;
        public UnityAction<int, int> OnChangeHp;   //  current, max
        public UnityAction OnDeath;

        public StatusDO Current;
        public StatusDO Max;

        public int Level;
        public EnemyDO EnemyData;

        public EnemyModel() { }

        public EnemyModel(EnemyDO data, int level)
        {
            Level = level;
            EnemyData = data;
            EnemyData.Exp *= Level;
            var status = DataManager.Get().GetStatus(EnemyData.StatusId);
            Max = new StatusDO
            {
                Atk = status.Base.Atk + status.Growth.Atk * (Level - 1),
                Hp = status.Base.Hp + status.Growth.Hp * (Level - 1),
                MoveSpeed = status.Base.MoveSpeed + status.Growth.MoveSpeed * (Level - 1)
            };
            Current = Max;
        }

        public void TakeDamage(int damage)
        {
            OnTakeDamage?.Invoke(damage);
            Current.Hp -= damage;
            Current.Hp = Mathf.Clamp(Current.Hp, 0, Max.Hp);
            OnChangeHp?.Invoke(Current.Hp, Max.Hp);
            if (0 == Current.Hp)
            {
                OnDeath?.Invoke();
            }
        }

        public int GetDamage()
        {
            return Current.Atk;
        }
    }
}