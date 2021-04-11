using Komastar.IdleMiner.Data;
using System.Collections.Generic;

namespace Komastar.IdleMiner.Stage
{
    public class StageModel
    {
        public int ScrollIndex;
        public StageDO StageData;

        private Queue<WaveDO> enemyQueue;

        public StageModel(StageDO stage)
        {
            ScrollIndex = 0;
            StageData = stage;
            enemyQueue = new Queue<WaveDO>(StageData.EnemyList);
        }

        public WaveDO GetEnemy()
        {
            WaveDO enemy = enemyQueue.Dequeue();
            enemyQueue.Enqueue(enemy);

            return enemy;
        }
    }
}
