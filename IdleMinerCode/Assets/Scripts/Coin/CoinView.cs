using UnityEngine;

namespace Komastar.IdleMiner.Coin
{
    public class CoinView : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer coinRenderer;

        [SerializeField]
        private Rigidbody2D coinBody;

        private float lifeTime;

        public CoinController CoinCtrl;

        private void OnDisable()
        {
            transform.position = Vector3.zero;
        }

        private void Update()
        {
            if (lifeTime < Time.time)
            {
                lifeTime = 0f;
                TurnOff();
            }
        }

        public void TurnOn()
        {
            lifeTime = Time.time + 2f;
            gameObject.SetActive(true);
        }

        public void TurnOff()
        {
            coinBody.velocity = Vector2.zero;
            CoinCtrl.Return(this);
        }

        public void Spring()
        {
            Vector2 dir = new Vector2(Random.Range(-1f, 1f), Random.Range(0f, 3f));
            dir.Normalize();
            dir *= 15f;
            coinBody.AddForce(dir);
        }
    }
}