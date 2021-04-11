using System.Collections.Generic;
using UnityEngine;

namespace Komastar.IdleMiner.Coin
{
    public class CoinController : MonoBehaviour
    {
        [SerializeField]
        private CoinView coinPrefab;

        private Queue<CoinView> coinPool;

        private void Awake()
        {
            coinPool = new Queue<CoinView>();
            for (int i = 0; i < 5; i++)
            {
                var coin = CreateCoin();
                coinPool.Enqueue(coin);
            }
        }

        public void CreateCoins(Vector3 position, int count)
        {
            for (int i = 0; i < count; i++)
            {
                CoinView coin;
                if (0 < coinPool.Count)
                {
                    coin = coinPool.Dequeue();
                }
                else
                {
                    coin = CreateCoin();
                }

                coin.transform.position = position;
                coin.TurnOn();
                coin.Spring();
            }
        }

        public void Return(CoinView coin)
        {
            coinPool.Enqueue(coin);
        }

        private CoinView CreateCoin()
        {
            var coin = Instantiate(coinPrefab, transform);
            coin.CoinCtrl = this;
            coin.TurnOff();

            return coin;
        }
    }
}