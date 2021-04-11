using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Komastar.IdleMiner.UI
{
    public class UIDamageTextView : MonoBehaviour
    {
        [SerializeField]
        private Text damageText;

        [SerializeField]
        private float moveSpeed;

        [SerializeField]
        private float lifeTime;

        private UIDamageTextPresenter damageTextParent;
        private WaitForSeconds lifeTimer;

        private void Awake()
        {
            lifeTimer = new WaitForSeconds(lifeTime);
            damageTextParent = UIDamageTextPresenter.Get();
        }

        public void Update()
        {
            transform.position += Vector3.up * moveSpeed * Time.deltaTime;
        }

        private IEnumerator LifeTimer()
        {
            yield return lifeTimer;

            damageTextParent.Return(this);
        }

        public void SetDamage(int damage)
        {
            damageText.text = $"{damage}";
        }

        public void TurnOn()
        {
            gameObject.SetActive(true);
            StartCoroutine(LifeTimer());
        }

        public void TurnOff()
        {
            gameObject.SetActive(false);
        }
    }
}