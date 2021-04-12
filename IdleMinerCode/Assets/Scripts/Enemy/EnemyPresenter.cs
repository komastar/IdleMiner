using Komastar.IdleMiner.Data;
using Komastar.IdleMiner.Interface;
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

        public void Setup(WaveDO wave)
        {
            if (ReferenceEquals(null, dataManager))
            {
                dataManager = DataManager.Get();
            }

            EnemyDO enemyData = dataManager.GetEnemy(wave.Id);
            name = enemyData.Name;

            model = new EnemyModel(enemyData, wave.Level);
            model.OnDeath += SetDeath;
            model.OnDeath += view.Death;
            model.OnChangeHp += view.SetHp;

            view.Setup();
            view.OnAttack += Attack;
            view.OnTriggerEnter += TriggerEnter;
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

        private void TriggerEnter(IInteractable target)
        {
        }

        public void Attack()
        {
            playerPresenter.TakeDamage(model.GetDamage());
        }
    }
}
