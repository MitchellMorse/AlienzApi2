using System.Linq;
using AlienzApi.Models.GameModels;

namespace AlienzApi.Tests.DbSets
{
    class TestPlayerDbSet : InitialDbSet<Player>
    {
        public override Player Find(params object[] keyValues)
        {
            return this.SingleOrDefault(level => level.Id == (int)keyValues.Single());
        }
    }
}
