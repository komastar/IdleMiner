using System.Collections.Generic;
using UnityEngine;

namespace Komastar.IdleMiner.Coin
{
    public class CoinController : MonoBehaviour
    {
        [SerializeField]
        private CoinView coinPrefab;

        private CoinPool coinPool;

        private void Awake()
        {
            coinPool = new CoinPool(coinPrefab);
            coinPool.PreloadAsync(10, 1);
        }

        public void CreateCoins(Vector3 position, int count)
        {
            for (int i = 0; i < count; i++)
            {
                CoinView coin = coinPool.Rent();
                coin.CoinCtrl = this;
                coin.transform.position = position;
                coin.TurnOn();
                coin.Spring();
            }
        }

        public void Return(CoinView coin)
        {
            coinPool.Return(coin);
        }
    }
}