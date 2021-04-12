using Komastar.IdleMiner.Data;
using Komastar.Foundation;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Komastar.IdleMiner.Manager
{
    public class DataManager : Singleton<DataManager>
    {
        private static bool isInit = false;

        private Dictionary<int, StageDO> stageDb;
        private Dictionary<int, UnitStatusDO> statusDb;

        private Dictionary<int, GearDO> gearDb;

        private void Awake()
        {
            Init();
        }

        public void Init()
        {
            if (isInit)
            {
                return;
            }

            LoadDatabase(ref stageDb);
            LoadDatabase(ref statusDb);
            LoadDatabase(ref gearDb);
            isInit = true;
        }

        private void LoadDatabase<T>(ref Dictionary<int, T> db) where T : IDataObject
        {
            string path;
            var t = typeof(T);
            var attr = Attribute
                .GetCustomAttributes(t, typeof(DOPathAttribute))
                .FirstOrDefault(a => a is DOPathAttribute);
            if (null != attr)
            {
                path = ((DOPathAttribute)attr).Path;
            }
            else
            {
                Debug.LogWarning($"{t.Name} has not DOPathAttribute");
                path = t.Name;
            }

            db = new Dictionary<int, T>();
            var dataList = Resources.LoadAll<TextAsset>(path);
            for (int i = 0; i < dataList.Length; i++)
            {
                var data = JObject.Parse(dataList[i].text).ToObject<T>();
                db.Add(data.Id, data);
            }
        }

        public UnitStatusDO GetStatus(int id)
        {
            if (statusDb.ContainsKey(id))
            {
                return statusDb[id];
            }
            else
            {
                Debug.LogError("Can not find status");

                return default;
            }
        }

        public StageDO GetStage(int id)
        {
            if (stageDb.ContainsKey(id))
            {
                return stageDb[id];
            }
            else
            {
                Debug.LogError("Can not find stage");

                return default;
            }
        }

        public GearDO GetGear(int id)
        {
            if (gearDb.ContainsKey(id))
            {
                return gearDb[id];
            }
            else
            {
                Debug.LogError("Can not find gear");

                return default;
            }
        }

        public List<GearDO> GetAllGears()
        {
            return gearDb.Values.ToList();
        }

    }
}
