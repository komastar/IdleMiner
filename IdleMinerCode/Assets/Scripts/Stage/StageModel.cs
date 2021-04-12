using Komastar.IdleMiner.Data;

namespace Komastar.IdleMiner.Stage
{
    public class StageModel
    {
        public int ScrollIndex;
        public StageDO StageData;

        public StageModel(StageDO stage)
        {
            ScrollIndex = 0;
            StageData = stage;
        }
    }
}
