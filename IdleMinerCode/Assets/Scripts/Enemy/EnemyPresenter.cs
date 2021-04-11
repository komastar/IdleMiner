using Komastar.IdleMiner.Data;
using Komastar.IdleMiner.Manager;
using Komastar.IdleMiner.Player;
using UnityEngine;
using UnityEngine.Events;

namespace Komastar.IdleMiner.Enemy
{
    [SelectionBase]
    public class EnemyPresenter : MonoBehaviour
    {
        public UnityAction<EnemyDO> OnDeath;

        private DataManager dataManager;

        [SerializeField]
        private EnemyView view;
        private EnemyModel model;

        [SerializeField]
        private PlayerPresenter playerPresenter;

        private bool isCombat;
        private float nextAttackTime;

        private void Awake()
        {
            dataManager = DataManager.Get();
        }

        private void Update()
        {
            if (isCombat)
            {
                if (nextAttackTime <= Time.time)
                {
                    nextAttackTime = Time.time + 1.5f;
                    view.Attack();
                }
            }
        }

        public void Setup(WaveDO wave)
        {
            EnemyDO enemyData = dataManager.GetEnemy(wave.Id);
            name = enemyData.Name;

            model = new EnemyModel(enemyData, wave.Level);
            model.OnDeath += SetDeath;
            model.OnDeath += view.Death;
            model.OnChangeHp += view.SetHp;

            view.Setup();
            view.OnAttack += Attack;
            view.OnTrigger += OnTriggerView;
            view.SetHp(model.Current.Hp, model.Max.Hp);
        }

        private void SetDeath()
        {
            OnDeath?.Invoke(model.EnemyData);
        }

        public void TakeDamage(int damage)
        {
            model.TakeDamage(damage);
            view.TakeDamage(damage);
        }

        private void OnTriggerView(bool isCollision)
        {
            isCombat = isCollision;
        }

        public void Attack()
        {
            playerPresenter.TakeDamage(model.GetDamage());
        }
    }
}
