using System;
using UniRx.Toolkit;
using UnityEngine;

namespace Komastar.IdleMiner.Coin
{
    public class CoinPool : ObjectPool<CoinView>
    {
        private readonly CoinView coinPrefab;

        public CoinPool(CoinView prefab)
        {
            coinPrefab = prefab;
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }

        protected override CoinView CreateInstance()
        {
            return UnityEngine.Object.Instantiate(coinPrefab);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        protected override void OnBeforeRent(CoinView instance)
        {
            //base.OnBeforeRent(instance);
        }

        protected override void OnBeforeReturn(CoinView instance)
        {
            base.OnBeforeReturn(instance);
        }

        protected override void OnClear(CoinView instance)
        {
            base.OnClear(instance);
        }
    }
}
