using System.Collections.Generic;

namespace Komastar.IdleMiner.Data
{
    [DOPath("Stage")]
    public struct StageDO : IDataObject
    {
        public int Id { get; set; }
        public List<WaveDO> EnemyList;
    }

    public struct WaveDO
    {
        public int Id;
        public int Level;
    }
}
