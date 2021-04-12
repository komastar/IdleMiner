using Komastar.IdleMiner.Data;
using Komastar.IdleMiner.Interface;
using Komastar.IdleMiner.Manager;
using Komastar.IdleMiner.Stage;
using Komastar.IdleMiner.UI;
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
        private UIEquipPresenter equipPresenter;

        private float nextAttackTime;

        public IQueryable TargetVein;
        public IQueryRequest QueryRequest;

        private void OnDestroy()
        {
            model.Save();
        }

        private void Update()
        {
            if (!ReferenceEquals(null, TargetVein))
            {
                if (nextAttackTime <= Time.time)
                {
                    nextAttackTime = Time.time + model.QuerySpeed;
                    view.Query();
                }
            }
        }

        public void Setup()
        {
            model = PlayerModel.Initialize();
            model.OnLevelUp += view.InfoView.OnLevelUp;
            model.OnChangeExp += view.InfoView.OnEarnExp;
            model.OnChangeAtk += view.InfoView.OnChangeAtk;
            model.Setup();

            StagePresenter.OnChangeStageLevel += model.OnChangeStageLevel;

            view.OnTargetEnter += (t) =>
            {
                TargetVein = t;
            };
            view.OnTargetExit += (t) =>
            {
                if (t == TargetVein)
                {
                    TargetVein = null;
                }
            };
            view.OnQueryAction += Query;

            QueryRequest = new QueryRequest() { Power = 1 };
        }

        public int GetStageLevel()
        {
            return model.StageLevel;
        }

        public float GetMoveSpeed()
        {
            if (!ReferenceEquals(null, TargetVein))
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

        private void Query()
        {
            TargetVein.Query(QueryRequest);
        }
    }
}