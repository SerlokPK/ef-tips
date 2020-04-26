using Classes;
using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
// this namespace allows us to use Include
using System.Data.Entity;
using Model;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Not sure, but I think it prevents DB from redoing migrations 
            // Database.SetInitializer(new NullDatabaseInitializer<NinjaContext>());
            // InsertNinja();
            // InsertMultipleNinjas();
            // SimpleNinjaQueries();
            // QueryAndUpdateNinja();
            // QueryAndUpdateNinjaDisconnectedApp();
            // RetrieveDataWithFind();
            // RetrieveDataWithStoredProc();
            // DeleteNinja();
            // AddNinjaWithEquipment();
            GetDataWithoutTracking();
            Console.WriteLine("Press key to close");
            Console.ReadKey();
        }

        private static void GetDataWithoutTracking()
        {
            using (var context = new NinjaContext())
            {
                // As no tracking is storing data into memory, so if I wanted to update
                // ninja, I couldn't do it directly as 'ninja.Name = ...'
                var ninja = context.Ninjas.AsNoTracking().FirstOrDefaultAsync();
            }
        }

        private static void AddNinjaWithEquipment()
        {
            var ninja = new Ninja
            {
                Name = "Naruto",
                ClanId = 1,
                DateOfBirth = new DateTime(1996,3,5),
                ServedInOniwaban = false
            };
            var muscles = new NinjaEquipment
            {
                Name = "Muscles",
                Type = Classes.Enums.EquipmentType.Tool
            };
            var spunk = new NinjaEquipment
            {
                Name = "Spunk",
                Type = Classes.Enums.EquipmentType.Weapon
            };
            using (var context = new NinjaContext())
            {
                context.Ninjas.Add(ninja);
                ninja.EquipmentOwned.Add(muscles);
                ninja.EquipmentOwned.Add(spunk);
                context.SaveChanges();
            }
        }

        private static void DeleteNinja()
        {
            using (var context = new NinjaContext())
            {
                // we can again use State property to set it to 'Deleted'
                var ninja = context.Ninjas.FirstOrDefault();
                context.Ninjas.Remove(ninja);
                context.SaveChanges();
            }
        }

        private static void RetrieveDataWithStoredProc()
        {
            using (var context = new NinjaContext())
            {
                // EF doesn't have direct call for procedures, so u can use
                // SqlQuery to execute some
                var ninjas = context.Ninjas.SqlQuery("exec SomeProcedure");
            }
        }

        private static void RetrieveDataWithFind()
        {
            var id = 6;
            using (var context = new NinjaContext())
            {
                // Find first checks local memory for specified argument, if it's not
                // found, it will then call DB and execute itself as SingleOrDefault.
                // Difference between find and SOD is that Find can't use Include, u can't return
                // referenced data, in this case, he will ony return data from ninja table
                // U don't have to specify key to find method, EF has that logic, so in some cases
                // this is performance hit
                var ninja1 = context.Ninjas.Find(id);

                // This is eager loading, we load everything at start
                var ninja2 = context.Ninjas.Include(x => x.EquipmentOwned).SingleOrDefault(x => x.Id == id);
                Console.WriteLine($"Equipment: {ninja2.EquipmentOwned.FirstOrDefault().Name}");

                // if we don't want to load referenced list data for all entities at once, we can later load it
                // This is explicit loading, we load what we want
                var ninja3 = context.Ninjas.SingleOrDefault(x => x.Id == id);
                context.Entry(ninja3).Collection(x => x.EquipmentOwned).Load();

                // I can enable lazy loading by defining VIRTUAL inside Ninja model, on EquipmentOwned property
                // By doing that, I am enabling loading additional data when I need it
                // I didn't specify VIRTUAL so this wont work now
                Console.WriteLine("Equipment count" + ninja1.EquipmentOwned.Count);
            }
        }

        private static void QueryAndUpdateNinjaDisconnectedApp()
        {
            Ninja ninja;
            using (var context = new NinjaContext())
            {
                ninja = context.Ninjas.FirstOrDefault();
            }

            ninja.ServedInOniwaban = !ninja.ServedInOniwaban;

            using (var context = new NinjaContext())
            {
                // We can also change ninja state by setting his State to modified and then
                // all of his properties will be updated, even if data didn't change 
                // context.Ninjas.Attach(ninja);
                // context.Entry(ninja).State = System.Data.Entity.EntityState.Modified;

                context.Ninjas.Add(ninja);
                // this is new transaction, so it doesn't know previous state of ninja, so I either need to
                // get that ninja again, or to say 'Add' and it will use it as 'Insert', because
                // it already exists
                context.SaveChanges();
            }
        }

        private static void QueryAndUpdateNinja()
        {
            using (var context = new NinjaContext())
            {
                var ninja = context.Ninjas.FirstOrDefault();
                ninja.ServedInOniwaban = !ninja.ServedInOniwaban;
                context.SaveChanges();
            }
        }

        private static void SimpleNinjaQueries()
        {
            using (var context = new NinjaContext())
            {
                var ninjas = context.Ninjas.ToList();
                // Never iterate directly over DB context, bcuz transaction stays open
                // as long as u iterate
                //foreach(var ninja in context.Ninjas)
                //{
                //    // ...
                //}
            }
        }

        private static void InsertMultipleNinjas()
        {
            var ninja1 = new Ninja
            {
                Name = "Sakuro",
                ServedInOniwaban = false,
                DateOfBirth = new DateTime(1989, 1, 1),
                ClanId = 1
            };

            var ninja2 = new Ninja
            {
                Name = "Kimetsu",
                ServedInOniwaban = false,
                DateOfBirth = new DateTime(1989, 1, 1),
                ClanId = 1
            };
            using (var context = new NinjaContext())
            {
                context.Ninjas.AddRange(new List<Ninja> { ninja1, ninja2 });
                context.SaveChanges();
            }
        }

        private static void InsertNinja()
        {
            var ninja = new Ninja
            {
                Name = "Hamato",
                ServedInOniwaban = false,
                DateOfBirth = new DateTime(1989, 1, 1),
                ClanId = 1
            };
            using (var context = new NinjaContext())
            {
                // U can use this line to log DB actions
                // context.Database.Log = Console.WriteLine;
                context.Ninjas.Add(ninja);
                context.SaveChanges();
            }
        }
    }
}
