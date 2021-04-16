using Komastar.IdleMiner.Data;
using Komastar.IdleMiner.Interface;
using Komastar.IdleMiner.Vein;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;

namespace Komastar.IdleMiner.Player
{
    [Serializable]
    public class PlayerModel
    {
        public float MoveSpeed;
        public float QuerySpeed;
        public Dictionary<ECoinType, int> Wallet;

        public static PlayerModel Load()
        {
            if (File.Exists(Constant.PlayerPath.Save))
            {
                string playerSaveDataJsonString = File.ReadAllText(Constant.PlayerPath.Save);

                return JObject.Parse(playerSaveDataJsonString).ToObject<PlayerModel>();
            }
            else
            {
                return new PlayerModel();
            }
        }

        public static void Save(PlayerModel data)
        {
            string saveJsonString = JObject.FromObject(data).ToString(Formatting.Indented);
            string savePath = Constant.PlayerPath.Save;
            File.WriteAllText(savePath, saveJsonString);
        }

        public PlayerModel()
        {
            Wallet = new Dictionary<ECoinType, int>();
            MoveSpeed = 2.5f;
            QuerySpeed = 1f;
        }

        public void Setup()
        {
            VeinModel.OnQueryDone.AddListener(OnQueryDone);
        }

        public void OnQueryDone(IQueryResponse response)
        {
            if (Wallet.ContainsKey(response.Coin.CoinType))
            {
                Wallet[response.Coin.CoinType] += response.Amount;
            }
            else
            {
                Wallet.Add(response.Coin.CoinType, response.Amount);
            }
        }
    }
}
