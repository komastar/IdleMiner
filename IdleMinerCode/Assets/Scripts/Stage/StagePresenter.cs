using DG.Tweening;
using Komastar.IdleMiner.Manager;
using Komastar.IdleMiner.Player;
using Komastar.IdleMiner.Vein;
using UnityEngine;
using UnityEngine.Events;

namespace Komastar.IdleMiner.Stage
{
    public class StagePresenter : MonoBehaviour
    {
        public static UnityAction<int> OnChangeStageLevel;
        public static UnityAction<int> OnChangeSpawnCount;

        private DataManager dataManager;

        [SerializeField]
        private StageView view;
        private StageModel model;

        [SerializeField]
        private PlayerPresenter playerPresenter;

        [SerializeField]
        private int stageLevel;
        [SerializeField]
        private int spawnCount;

        [SerializeField]
        private VeinSpawner veinSpawner;

        private void Awake()
        {
            DOTween.Init();
            VeinModel.Init();

            OnChangeSpawnCount = null;
            OnChangeStageLevel = null;

            spawnCount = 0;

            dataManager = DataManager.Get();
            dataManager.Init();

            view.OnScrollReset += OnScrollReset;
        }

        private void Start()
        {
            playerPresenter.Setup();
            OnChangeStageLevel?.Invoke(stageLevel);
            OnChangeSpawnCount?.Invoke(spawnCount);
            OnScrollReset();
        }

        private void Update()
        {
            view.ScrollBackground(playerPresenter.GetMoveSpeed());
        }

        public void OnScrollReset()
        {
            ++spawnCount;
            veinSpawner.Spawn();
            if (Constant.Max.SpawnCount < spawnCount)
            {
                spawnCount = 1;
                ++stageLevel;
                OnChangeStageLevel?.Invoke(stageLevel);
            }
            OnChangeSpawnCount?.Invoke(spawnCount);
        }
    }
}