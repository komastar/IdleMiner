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

        private float scrollLimit;

        private void Awake()
        {
            scrollLimit = -lastBackgroundElem.position.x;
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
    }
}