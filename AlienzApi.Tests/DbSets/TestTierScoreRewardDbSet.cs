using System.Linq;
using AlienzApi.Models.GameModels;

namespace AlienzApi.Tests.DbSets
{
    public class TestTierScoreRewardDbSet : InitialDbSet<TierScoreReward>
    {
        public override TierScoreReward Find(params object[] keyValues)
        {
            return this.SingleOrDefault(a => a.Id == (int)keyValues.Single());
        }
    }
}
