namespace DataModel.Migrations
{
    using System.Data.Entity.Migrations;
    
    // This file was created using 'enable-migrations'
    internal sealed class Configuration : DbMigrationsConfiguration<DataModel.NinjaContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DataModel.NinjaContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
