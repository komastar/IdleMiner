using Cysharp.Threading.Tasks;
using DG.Tweening;
using Komastar.IdleMiner.Player;
using Komastar.IdleMiner.UI;
using UnityEngine;

namespace Komastar.IdleMiner.Enemy
{
    public class EnemyView : PlayerView
    {
        private Camera cam;

        public Transform HpBarPosition;

        public RectTransform UIHpBarTransform;
        public UIHpBarView uiHpBarView;

        private void Awake()
        {
            cam = Camera.main;
            damageTextParent = UIDamageTextPresenter.Get();
        }

        private void LateUpdate()
        {
            UIHpBarTransform.position = cam.WorldToScreenPoint(HpBarPosition.position);
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
            ViewTransform.DOComplete();
        }

        public void Setup()
        {
            OnTrigger = null;
            OnAttack = null;
            gameObject.SetActive(true);
            UIHpBarTransform.gameObject.SetActive(true);
            ViewTransform.localPosition = Vector3.zero;
            UIHpBarTransform.position = cam.WorldToScreenPoint(HpBarPosition.position);
        }

        public void Death()
        {
            gameObject.SetActive(false);
            UIHpBarTransform.gameObject.SetActive(false);
        }

        public async UniTask AttackAsync()
        {
            await ViewTransform.DOLocalMove(Vector3.left * .25f, .05f);

            OnAttack?.Invoke();

            await ViewTransform.DOLocalMove(Vector3.zero, .05f);
        }

        public void SetHp(int hp, int maxHp)
        {
            uiHpBarView.SetValue(hp, maxHp);
        }

        public override void Attack()
        {
            AttackAsync().Forget();
        }
    }
}