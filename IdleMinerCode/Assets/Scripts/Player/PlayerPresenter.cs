using Komastar.IdleMiner.Data;
using Komastar.IdleMiner.Interface;
using System;
using UnityEngine;

namespace Komastar.IdleMiner.Player
{
    [SelectionBase]
    public class PlayerPresenter : MonoBehaviour
    {
        [SerializeField] private PlayerView view;
        [SerializeField] private PlayerModel model;

        private float nextQueryTime;

        public IQueryable TargetVein;
        public IQueryRequest QueryRequest;

        private void OnDestroy()
        {
            PlayerModel.Save(model);
        }

        private void Update()
        {
            if (!ReferenceEquals(null, TargetVein))
            {
                if (nextQueryTime <= Time.time)
                {
                    nextQueryTime = Time.time + model.QuerySpeed;
                    view.Query();
                }
            }
        }

        public void Setup()
        {
            model = PlayerModel.Load();
            model.Setup();

            view.OnTargetEnter += (target) => { TargetVein = target; };
            view.OnTargetExit += (target) => { TargetVein = (target == TargetVein) ? null : TargetVein; };
            view.OnTriggerQuery += Query;
            view.Setup();

            QueryRequest = new QueryRequest() { Power = 1 };
        }

        public float GetMoveSpeed()
        {
            return (!ReferenceEquals(null, TargetVein)) ? 0f : model.MoveSpeed;
        }

        private void Query()
        {
            TargetVein.Query(QueryRequest);
        }
    }
}