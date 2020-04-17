using Model;
using System.Data.Entity;
using System.Linq;

namespace DataModel
{
    public class DataHelpers
    {
        public static void NewDbWithSeed()
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<NinjaContext>());
            using(var context = new NinjaContext())
            {
                if (context.Ninjas.Any())
                {
                    return;
                }
                context.Clans.Add(new Clan { ClanName = "Naruto clan" });
                context.Clans.Add(new Clan { ClanName = "Goku clan" });
                context.Clans.Add(new Clan { ClanName = "Sasuke clan" });
            }
        }
    }
}
