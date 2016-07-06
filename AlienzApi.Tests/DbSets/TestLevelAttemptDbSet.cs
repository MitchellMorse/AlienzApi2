using System.Linq;
using AlienzApi.Models.GameModels;

namespace AlienzApi.Tests.DbSets
{
    class TestLevelAttemptDbSet : InitialDbSet<LevelAttempt>
    {
        public override LevelAttempt Find(params object[] keyValues)
        {
            return this.SingleOrDefault(level => level.Id == (int)keyValues.Single());
        }
    }
}
