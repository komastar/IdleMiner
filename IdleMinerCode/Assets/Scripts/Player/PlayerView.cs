using Cysharp.Threading.Tasks;
using DG.Tweening;
using Komastar.IdleMiner.UI;
using UnityEngine;
using UnityEngine.Events;

namespace Komastar.IdleMiner.Player
{
    public class PlayerView : MonoBehaviour
    {
        public UnityAction<bool> OnTrigger;
        public UnityAction OnAttack;

        protected UIDamageTextPresenter damageTextParent;

        public Transform ViewTransform;
        public Transform HeadTransform;

        [SerializeField]
        private CharacterView characterView;

        private void Awake()
        {
            damageTextParent = UIDamageTextPresenter.Get();
            characterView.OnAttack += OnAttackCallback;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            OnTrigger?.Invoke(true);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            OnTrigger?.Invoke(false);
        }

        private void OnDestroy()
        {
            OnAttack = null;
            ViewTransform.DOComplete();
        }

        public virtual void Attack()
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
            OnAttack?.Invoke();
        }
    }
}