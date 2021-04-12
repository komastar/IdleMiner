using Komastar.IdleMiner.Data;
using Komastar.IdleMiner.Interface;
using Komastar.IdleMiner.UI;
using Komastar.IdleMiner.UI.Player;
using UnityEngine;
using UnityEngine.Events;

namespace Komastar.IdleMiner.Player
{
    public class PlayerView : MonoBehaviour
    {
        public UnityAction OnQueryAction;
        public UnityAction<IQueryable> OnTargetEnter;
        public UnityAction<IQueryable> OnTargetExit;

        protected UIDamageTextPresenter damageTextParent;

        public Transform ViewTransform;
        public Transform HeadTransform;

        [SerializeField]
        private CharacterView characterView;

        public UIPlayerInfoView InfoView;

        private void Awake()
        {
            damageTextParent = UIDamageTextPresenter.Get();
            characterView.OnTriggerAnimEvent += QueryAction;
        }

        private void OnDestroy()
        {
            OnQueryAction = null;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            var queryTarget = collision.gameObject.GetComponent<IQueryable>();
            if (!ReferenceEquals(null, queryTarget))
            {
                OnTargetEnter?.Invoke(queryTarget);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            var queryTarget = collision.gameObject.GetComponent<IQueryable>();
            if (!ReferenceEquals(null, queryTarget))
            {
                OnTargetExit?.Invoke(queryTarget);
            }
        }

        public virtual void Query()
        {
            characterView.PlayQuery();
        }

        public void TakeDamage(int damage)
        {
            var damageText = damageTextParent.Rent(HeadTransform.position);
            damageText.SetDamage(damage);
            damageText.TurnOn();
        }

        private void QueryAction()
        {
            OnQueryAction?.Invoke();
        }
    }
}