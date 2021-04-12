using DG.Tweening;
using Komastar.IdleMiner.Interface;
using Komastar.IdleMiner.UI;
using UnityEngine;
using UnityEngine.Events;

namespace Komastar.IdleMiner.Player
{
    public class PlayerView : MonoBehaviour
    {
        public UnityAction<IInteractResult> OnAfterInteract;
        public UnityAction<IInteractable> OnTriggerEnter;
        public UnityAction OnTriggerExit;
        public UnityAction OnAttack;

        protected UIDamageTextPresenter damageTextParent;

        public Transform ViewTransform;
        public Transform HeadTransform;

        [SerializeField]
        private CharacterView characterView;

        public IInteractable Target;

        private void Awake()
        {
            damageTextParent = UIDamageTextPresenter.Get();
            characterView.OnAttack += OnAttackCallback;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            var interactable = collision.gameObject.GetComponent<IInteractable>();
            if (!ReferenceEquals(null, interactable))
            {
                Target = interactable;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            var interactable = collision.gameObject.GetComponent<IInteractable>();
            if (!ReferenceEquals(null, interactable)
                && Target == interactable)
            {
                Target = null;
            }
        }

        private void OnDestroy()
        {
            OnAttack = null;
            ViewTransform.DOComplete();
        }

        public virtual void Interact()
        {
            characterView.PlayAttack();
        }

        public void TakeDamage(int damage)
        {
            var damageText = damageTextParent.Rent(HeadTransform.position);
            damageText.SetDamage(damage);
            damageText.TurnOn();
        }

        private void OnAttackCallback()
        {
            var interactResult = Target.Interact();
            OnAfterInteract?.Invoke(interactResult);
        }
    }
}