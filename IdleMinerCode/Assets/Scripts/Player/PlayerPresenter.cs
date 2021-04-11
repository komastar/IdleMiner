using Komastar.IdleMiner.Data;
using Komastar.IdleMiner.Enemy;
using Komastar.IdleMiner.Manager;
using Komastar.IdleMiner.Stage;
using Komastar.IdleMiner.UI;
using Komastar.IdleMiner.UI.Player;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Komastar.IdleMiner.Player
{
    [SelectionBase]
    public class PlayerPresenter : MonoBehaviour
    {
        [SerializeField]
        private PlayerView view;
        [SerializeField]
        private PlayerModel model;

        [SerializeField]
        private UIPlayerInfoView infoView;

        [SerializeField]
        private UIEquipPresenter equipPresenter;

        [SerializeField]
        private EnemyPresenter enemyPresenter;

        private bool isCombat;
        private float nextAttackTime;

        private void OnDestroy()
        {
            model.Save();
        }

        private void Update()
        {
            if (isCombat)
            {
                if (nextAttackTime <= Time.time)
                {
                    nextAttackTime = Time.time + 1f;
                    view.Attack();
                }
            }
        }

        public void Setup()
        {
            model = PlayerModel.Initialize();
            model.OnLevelUp += infoView.OnLevelUp;
            model.OnChangeExp += infoView.OnEarnExp;
            model.OnChangeHp += infoView.OnChangeHp;
            model.OnChangeAtk += infoView.OnChangeAtk;
            model.Setup();

            view.OnTrigger += OnTriggerView;
            view.OnAttack += Attack;

            enemyPresenter.OnDeath += OnDeathEnemy;

            StagePresenter.OnChangeStageLevel += model.OnChangeStageLevel;

            GearDO weapon = DataManager.Get().GetGear(model.WeaponId);
            equipPresenter.Equip(weapon);
        }

        private void OnDeathEnemy(EnemyDO enemyData)
        {
            model.EarnExp(enemyData.Exp);
        }

        private void OnTriggerView(bool isCollision)
        {
            isCombat = isCollision;
        }

        public int GetStageLevel()
        {
            return model.StageLevel;
        }

        public float GetMoveSpeed()
        {
            if (isCombat)
            {
                return 0f;
            }
            else
            {
                return model.Current.MoveSpeed;
            }
        }
        public int GetWeaponId()
        {
            return model.WeaponId;
        }

        public void EquipGear(GearDO gearData)
        {
            model.EquipGear(gearData);
        }

        private void Attack()
        {
            enemyPresenter.TakeDamage(model.MaxAtk);
        }

        public void TakeDamage(int damage)
        {
            model.TakeDamage(damage);
            view.TakeDamage(damage);

            if (0 == model.Current.Hp)
            {
                SceneManager.LoadScene("GameScene");
            }
        }
    }
}