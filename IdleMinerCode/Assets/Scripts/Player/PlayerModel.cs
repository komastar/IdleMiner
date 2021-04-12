using Komastar.IdleMiner.Data;
using Komastar.IdleMiner.Interface;
using Komastar.IdleMiner.Manager;
using Komastar.IdleMiner.Vein;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Profiling;

namespace Komastar.IdleMiner.Player
{
    [Serializable]
    public class PlayerModel
    {
        [JsonIgnore]
        public UnityAction<int> OnLevelUp;
        [JsonIgnore]
        public UnityAction<int, int> OnChangeExp;
        [JsonIgnore]
        public UnityAction<int> OnTakeDamage;
        [JsonIgnore]
        public UnityAction<int, int> OnChangeHp;   //  current, max
        [JsonIgnore]
        public UnityAction<int> OnChangeAtk;

        public int Level;
        public int Exp;
        public int MaxExp;

        [SerializeField]
        public StatusDO Current;
        [SerializeField]
        public StatusDO Max;

        public int StageLevel;
        public int WeaponId;

        [SerializeField]
        private int maxHp;
        [JsonIgnore]
        public int MaxHp
        {
            get => Max.Hp + maxHp;
            set => maxHp = value;
        }

        [SerializeField]
        private int maxAtk;
        [JsonIgnore]
        public int MaxAtk
        {
            get => Max.Atk + maxAtk;
            set => maxAtk = value;
        }

        [JsonIgnore]
        public float QuerySpeed { get => Current.AtkSpeed; }

        [JsonIgnore]
        public Dictionary<EGearType, StatusDO> Mod;

        [SerializeField]
        private UnitStatusDO playerData;

        public static PlayerModel Initialize()
        {
            if (File.Exists(Constant.PlayerPath.Save))
            {
                string playerSaveDataJsonString = File.ReadAllText(Constant.PlayerPath.Save);

                return JObject.Parse(playerSaveDataJsonString).ToObject<PlayerModel>();
            }
            else
            {
                return new PlayerModel(Constant.Init.Level, Constant.Init.Exp, Constant.Init.MaxExp);
            }
        }

        public PlayerModel() { }

        public PlayerModel(int level, int exp, int maxExp)
        {
            Level = level;
            Exp = exp;
            MaxExp = maxExp;

            Current = new StatusDO();
            Max = new StatusDO();
        }

        public void Setup()
        {
            VeinModel.OnQueryDone.AddListener(OnQueryDone);

            Mod = new Dictionary<EGearType, StatusDO>();

            playerData = DataManager.Get().GetStatus(0);

            UpdateMax();
            if (0 == Current.Hp)
            {
                UpdateCurrent();
            }

            OnLevelUp += (level) =>
            {
                OnChangeHp?.Invoke(Current.Hp, MaxHp);
                OnChangeAtk?.Invoke(MaxAtk);
            };
            OnLevelUp?.Invoke(Level);
            OnChangeExp?.Invoke(Exp, MaxExp);
            OnChangeHp?.Invoke(Current.Hp, MaxHp);
        }

        public void OnChangeStageLevel(int stageLevel)
        {
            StageLevel = stageLevel;
        }

        public void Save()
        {
            string saveJsonString = JObject.FromObject(this).ToString(Formatting.Indented);
            string savePath = Constant.PlayerPath.Save;
            File.WriteAllText(
                savePath
                , saveJsonString);
        }

        public void UpdateCurrent()
        {
            Current = Max;
        }

        public void UpdateMax()
        {
            Max.Atk = playerData.Base.Atk + playerData.Growth.Atk * (Level - 1);
            Max.Hp = playerData.Base.Hp + playerData.Growth.Hp * (Level - 1);
            Max.MoveSpeed = playerData.Base.MoveSpeed + playerData.Growth.MoveSpeed * (Level - 1);
        }

        public void EarnExp(int exp)
        {
            Exp += exp;
            LevelUp();
            OnChangeExp?.Invoke(Exp, MaxExp);
        }

        public void LevelUp()
        {
            int prevLevel = Level;
            while (MaxExp <= Exp)
            {
                Exp -= MaxExp;
                MaxExp = (int)(MaxExp * Constant.ExpStep);
                Level++;
            }

            if (prevLevel != Level)
            {
                UpdateMax();
                UpdateCurrent();
                OnLevelUp?.Invoke(Level);
            }
        }

        public void OnQueryDone(IQueryResponse response)
        {
            Profiler.BeginSample("OnQueryDone");
            Debug.Log($"{response.Coin.CoinType} : {response.Amount}");
            Profiler.EndSample();
        }
    }
}
