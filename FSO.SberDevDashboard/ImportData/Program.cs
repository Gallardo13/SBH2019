using FSO.SDD.DataBaseEfStore;
using FSO.SDD.DbModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;

namespace ImportData
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new StoreContext(@"..\..\..\..\FSO.SDD.DataBase.db;"))
            {
                ImportUsers(db);

                db.SaveChanges();
            }

            /*using (var db = new StoreContext(@"..\..\..\..\..\FSO.SberDevDashboard\FSO.SDD.DataBase.db;"))
            {
                foreach (var user in db.Users)
                    Console.WriteLine(user.Name);
            }

            Console.ReadLine();*/
        }

        private static void ImportUsers(StoreContext db)
        {
            var users = File
                .ReadAllText(@"../../../../users.csv")
                .Split(Environment.NewLine)
                .Skip(1)
                .Where(e => !string.IsNullOrEmpty(e))
                .Select(e => e.Split(";"))
                .Select(e => new User { Id = int.Parse(e[0]), Login = e[1], Name = e[2], Email = e[3] });

            foreach (var user in users)
                db.Users.Add(user);
        }
    }
}
