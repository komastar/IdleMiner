using Komastar.IdleMiner.Coin;
using System.Collections.Generic;
using UnityEngine;

namespace Komastar.IdleMiner.Vein
{
    public class VeinSpawner : MonoBehaviour
    {
        [SerializeField]
        private VeinPresenter veinPrefab;

        [SerializeField]
        private CoinController coinCtrl;

        [SerializeField]
        private Transform[] veinSpawnPoints;

        private List<VeinPresenter> veins;

        public void Spawn()
        {
            if (null == veins)
            {
                veins = new List<VeinPresenter>();
                for (int i = 0; i < veinSpawnPoints.Length; i++)
                {
                    var vein = Instantiate(veinPrefab, veinSpawnPoints[i]);
                    vein.CoinCtrl = coinCtrl;
                    vein.Init();
                    vein.Ready();
                    veins.Add(vein);
                }
            }
            else
            {
                Respawn();
            }
        }

        public void Respawn()
        {
            for (int i = 0; i < veins.Count; i++)
            {
                veins[i].Ready();
            }
        }
    }
}
