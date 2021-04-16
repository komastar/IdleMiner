using UnityEngine;
using UnityEngine.Events;

namespace Komastar.IdleMiner.Stage
{
    public class StageView : MonoBehaviour
    {
        public UnityAction OnScrollReset;

        [SerializeField]
        private Transform lastBackgroundElem;

        private Vector3 initPosition;
        private float scrollLimit;

        private void Awake()
        {
            initPosition = transform.position;
            scrollLimit = -lastBackgroundElem.position.x;
        }

        public void ScrollBackground(float speed)
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
            if (transform.position.x <= scrollLimit)
            {
                transform.position = initPosition;
                OnScrollReset?.Invoke();
            }
        }
    }
}