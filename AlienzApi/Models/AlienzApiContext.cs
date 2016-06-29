using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using AlienzApi.Models.GameModels;
using AlienzApi.Models.Interfaces;

namespace AlienzApi.Models
{
    public class AlienzApiContext : DbContext, IAlienzApiContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public AlienzApiContext() : base("name=AlienzApiContext")
        {
            this.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
        }

        public System.Data.Entity.DbSet<AlienzApi.Models.ExampleModels.Author> Authors { get; set; }

        public System.Data.Entity.DbSet<AlienzApi.Models.ExampleModels.Book> Books { get; set; }

        public System.Data.Entity.DbSet<AlienzApi.Models.GameModels.Level> Levels { get; set; }

        public void MarkAsModified(Level item)
        {
            Entry(item).State = EntityState.Modified;
        }
    }
}
