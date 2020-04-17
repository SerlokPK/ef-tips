using Classes;
using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;

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
            RetrieveDataWithStoredProc();
            Console.WriteLine("Press key to close");
            Console.ReadKey();
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
            var id = 4;
            using (var context = new NinjaContext())
            {
                // Find first checks local memory for specified argument, if it's not
                // found, it will then call DB and execute itself as SingleOrDefault.
                // Difference between find and SOD is that find doesn't return
                // referenced data, in this case, he will ony return data from ninja table
                var ninja = context.Ninjas.Find(id);
                ninja = context.Ninjas.SingleOrDefault(x => x.Id == id);
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
