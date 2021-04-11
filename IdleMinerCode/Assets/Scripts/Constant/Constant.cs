using UnityEngine;

namespace Komastar.IdleMiner
{
    public static class Constant
    {
        public static class Init
        {
            public static int Level => 1;
            public static int Exp => 0;
            public static int MaxExp => 10;
        }

        public static class Max
        {
            public static int Level => 60;
            public static int EnemyCount => 10;
        }

        public static float ExpStep => 1.5f;

        public static class Data
        {
            public static string Status => "Status";
            public static string Stage => "Stage";
            public static string Enemy => "Enemy";
            public static string Equip => "Equip";
        }

        public static class PlayerPath
        {
            public static string Save => $"{Application.persistentDataPath}/PlayerSaveData.json";
        }
    }

    public enum EGearType
    {
        Weapon,
        Armor,
        Count
    }
}
