using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using AlienzApi.Models.GameModels;
using AlienzApi.Models.Interfaces;

namespace AlienzApi.Tests.DbSets
{
    public class TestAlienzApiContext : IAlienzApiContext
    {
        public DbSet<Level> Levels { get;set; }

        public void Dispose()
        {
            
        }

        public void MarkAsModified(Level level)
        {
            
        }

        public int SaveChanges()
        {
            return 0;
        }

        public Task<int> SaveChangesAsync()
        {
            return new Task<int>(() => 0);
        }

        public TestAlienzApiContext()
        {
            this.Levels = new TestLevelDbSet();
        }
    }
}
