using System;
using System.Data.Entity;
using System.Threading.Tasks;
using AlienzApi.Models.GameModels;

namespace AlienzApi.Models.Interfaces
{
    public interface IAlienzApiContext : IDisposable
    {
        DbSet<Level> Levels { get; }
        int SaveChanges();
        Task<int> SaveChangesAsync();
        void MarkAsModified(Level level);
    }
}
