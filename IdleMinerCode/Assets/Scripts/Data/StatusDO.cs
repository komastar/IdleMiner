using System;

namespace Komastar.IdleMiner.Data
{
    [Serializable]
    public struct StatusDO
    {
        public int Atk;
        public int Hp;
        public float MoveSpeed;

        public override string ToString()
        {
            return $"Atk:{Atk} / Hp:{Hp} / MoveSpeed:{MoveSpeed}";
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
