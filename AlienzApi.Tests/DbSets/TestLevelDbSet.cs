using System.Linq;
using AlienzApi.Models.GameModels;

namespace AlienzApi.Tests.DbSets
{
    class TestLevelDbSet : InitialDbSet<Level>
    {
        public override Level Find(params object[] keyValues)
        {
            return this.SingleOrDefault(level => level.Id == (int) keyValues.Single());
        }
    }
}
