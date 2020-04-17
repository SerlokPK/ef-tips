using Classes;
using Model;
using Model.Interfaces;
using System;
using System.Data.Entity;
using System.Linq;

namespace DataModel
{
    public class NinjaContext: DbContext
    {
        public DbSet<Ninja> Ninjas { get; set; }
        public DbSet<Clan> Clans { get; set; }
        public DbSet<NinjaEquipment> Equipment { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // We can ignore IsDirty property so it will not be migrated into tables
            modelBuilder.Types().Configure(c => c.Ignore("IsDirty"));
            base.OnModelCreating(modelBuilder);
        }

        // We can override default save changes with our custom desire
        public override int SaveChanges()
        {
            foreach(var history in this.ChangeTracker.Entries().Where(e => e.Entity is IModificationHistory &&
                                                                      e.State == EntityState.Added || 
                                                                      e.State == EntityState.Modified)
                                                                .Select(e => e.Entity as IModificationHistory))
            {
                history.DateModified = DateTime.Now;
                if(history.DateCreated == DateTime.MinValue)
                {
                    history.DateCreated = DateTime.Now;
                }
            }
            int result = base.SaveChanges();
            foreach (var history in this.ChangeTracker.Entries().Where(e => e.Entity is IModificationHistory)
                                                                .Select(e => e.Entity as IModificationHistory))
            {
                history.IsDirty = false;
            }
            return result;
        }
    }
}
