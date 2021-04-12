using System;

namespace Komastar.IdleMiner.Data
{
    [Serializable]
    public struct StatusDO
    {
        public int Atk;
        public int Hp;
        public float MoveSpeed;
        public float AtkSpeed;

        public override string ToString()
        {
            return $"Atk:{Atk} / Hp:{Hp}\n" +
                $"MvSpd:{MoveSpeed} / AtkSpd : {AtkSpeed}";
        }
    }

    [DOPath("Status")]
    [Serializable]
    public struct UnitStatusDO : IDataObject
    {
        public int Id { get; set; }
        public StatusDO Base;
        public StatusDO Growth;
    }
}
