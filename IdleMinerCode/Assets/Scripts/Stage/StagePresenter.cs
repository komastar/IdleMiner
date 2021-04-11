using DG.Tweening;
using Komastar.IdleMiner.Data;
using Komastar.IdleMiner.Enemy;
using Komastar.IdleMiner.Manager;
using Komastar.IdleMiner.Player;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.Events;

namespace Komastar.IdleMiner.Stage
{
    public class StagePresenter : MonoBehaviour
    {
        public static UnityAction<int> OnChangeStageLevel;
        public static UnityAction<int> OnChangeEnemyCount;

        private DataManager dataManager;

        [SerializeField]
        private StageView view;
        private StageModel model;

        [SerializeField]
        private PlayerPresenter playerPresenter;

        [SerializeField]
        private EnemyPresenter enemyPresenter;

        [SerializeField]
        private int stageLevel;
        [SerializeField]
        private int enemySpawnCount;

        private void Awake()
        {
#if UNITY_EDITOR
            EditorBuildSettings.scenes = new EditorBuildSettingsScene[]
                {
                    new EditorBuildSettingsScene("Assets/Scenes/GameScene.unity", true)
                };
#endif
            DOTween.Init();

            OnChangeEnemyCount = null;
            OnChangeStageLevel = null;
            enemySpawnCount = 0;

            dataManager = DataManager.Get();
            dataManager.Init();

            var stage = dataManager.GetStage(1);

            model = new StageModel(stage);

            view.Setup();
            view.OnScrollReset += OnScrollReset;
        }

        private void Start()
        {
            playerPresenter.Setup();
            stageLevel = playerPresenter.GetStageLevel();
            OnChangeStageLevel?.Invoke(stageLevel);
            OnChangeEnemyCount?.Invoke(enemySpawnCount);
            OnScrollReset();
        }

        private void Update()
        {
            view.ScrollBackground(playerPresenter.GetMoveSpeed());
        }

        public void OnScrollReset()
        {
            WaveDO wave = model.GetEnemy();
            wave.Level += stageLevel;
            enemyPresenter.Setup(wave);
            ++enemySpawnCount;
            if (Constant.Max.EnemyCount < enemySpawnCount)
            {
                enemySpawnCount = 1;
                ++stageLevel;
                OnChangeStageLevel?.Invoke(stageLevel);
            }
            OnChangeEnemyCount?.Invoke(enemySpawnCount);
        }
    }
}