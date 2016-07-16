using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlienzApi.Models.GameModels;

namespace AlienzApi.Tests.DbSets
{
    public class TestAwardReasonDbSet : InitialDbSet<AwardReason>
    {
        public override AwardReason Find(params object[] keyValues)
        {
            return this.SingleOrDefault(a => a.Id == (int)keyValues.Single());
        }
    }
}
