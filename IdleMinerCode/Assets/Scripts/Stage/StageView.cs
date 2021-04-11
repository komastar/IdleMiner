using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Komastar.IdleMiner.Stage
{
    public class StageView : MonoBehaviour
    {
        public UnityAction OnScrollReset;

        [SerializeField]
        private Transform lastBackgroundElem;
        [SerializeField]
        private Text stageLevelText;
        [SerializeField]
        private Text stageEnemyCountText;

        private float scrollLimit;

        private void Awake()
        {
            scrollLimit = -lastBackgroundElem.position.x;
        }

        public void Setup()
        {
            StagePresenter.OnChangeStageLevel += SetStageLevel;
            StagePresenter.OnChangeEnemyCount += SetEnemyCount;
        }

        public void ScrollBackground(float speed)
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
            if (transform.position.x <= scrollLimit)
            {
                transform.position = Vector3.zero;
                OnScrollReset?.Invoke();
            }
        }

        public void SetStageLevel(int level)
        {
            stageLevelText.text = $"Stage : {level + 1}";
        }

        public void SetEnemyCount(int count)
        {
            stageEnemyCountText.text = $"{count} / {Constant.Max.EnemyCount}";
        }
    }
}