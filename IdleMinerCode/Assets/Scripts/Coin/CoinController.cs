using Komastar.IdleMiner.Interface;
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

        private void OnEnable()
        {
            Vein.VeinModel.OnQueryDone.AddListener(CreateCoins);
        }
        private void OnDisable()
        {

            Vein.VeinModel.OnQueryDone.RemoveListener(CreateCoins);
        }

        public void CreateCoins(Vector3 position, int count)
        {
            for (int i = 0; i < count; i++)
            {
                CoinView coin = coinPool.Rent();
                coin.CoinCtrl = this;
                coin.transform.parent = transform;
                coin.transform.position = position;
                coin.TurnOn();
                coin.Spring();
            }
        }

        public void CreateCoins(IQueryResponse response)
        {
            CreateCoins(response.Position, response.Amount);
        }

        public void Return(CoinView coin)
        {
            coinPool.Return(coin);
        }
    }
}