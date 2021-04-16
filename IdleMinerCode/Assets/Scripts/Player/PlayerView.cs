using Komastar.IdleMiner.Interface;
using UnityEngine;
using UnityEngine.Events;
using UniRx;
using UniRx.Triggers;

namespace Komastar.IdleMiner.Player
{
    public class PlayerView : MonoBehaviour
    {
        public UnityAction OnTriggerQuery;
        public UnityAction<IQueryable> OnTargetEnter;
        public UnityAction<IQueryable> OnTargetExit;

        [SerializeField] private CharacterView characterView;

        private void Awake()
        {
            this.OnTriggerEnter2DAsObservable()
                .Where(c => false == ReferenceEquals(null, c.GetComponent<IQueryable>()))
                .Select(c => c.GetComponent<IQueryable>())
                .Subscribe((x) => OnTargetEnter?.Invoke(x));

            this.OnTriggerExit2DAsObservable()
                .Where(c => false == ReferenceEquals(null, c.GetComponent<IQueryable>()))
                .Select(c => c.GetComponent<IQueryable>())
                .Subscribe(x => OnTargetExit?.Invoke(x));
        }

        public void Setup()
        {
            characterView.OnTriggerAnimEvent += OnTriggerQuery;
        }

        public void Query()
        {
            characterView.PlayQuery();
        }
    }
}