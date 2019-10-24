using FSO.SDD.DataBaseEfStore;
using FSO.SDD.DbModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
                ImportJiraTaskState(db);
                ImportJiraTask(db);

                db.SaveChanges();
            }
        }

        private static IEnumerable<string[]> ReadFile(string fileName) => File
                .ReadAllText(fileName)
                .Split(Environment.NewLine)
                .Skip(1)
                .Where(e => !string.IsNullOrEmpty(e))
                .Select(e => e.Split(";"));

        private static void ImportUsers(StoreContext db)
        {
            var users = ReadFile(@"../../../../../user.csv")
                .Select(e => new User { Id = int.Parse(e[0]), Login = e[1], Name = e[2], Email = e[3] });

            foreach (var user in users)
                db.Users.Add(user);
        }

        private static void ImportJiraTaskState(StoreContext db)
        {
            var taskStates = ReadFile(@"../../../../../JiraTaskState.csv")
                .Select(e => new JiraTaskState { Id = int.Parse(e[0]), Name = e[1] });

            foreach (var taskState in taskStates)
                db.JiraTaskStates.Add(taskState);
        }

        private static void ImportJiraTask(StoreContext db)
        {
            var jiraTasks = ReadFile(@"../../../../../JiraTask.csv")
                .Select(e => new JiraTask
                {
                    Id = int.Parse(e[0]),
                    AuthorId = int.Parse(e[2]),
                    ConfigurationItem = e[11],
                    CreatedDateTime = DateTime.ParseExact(e[4], "dd.MMM", System.Globalization.CultureInfo.GetCultureInfo("Ru-ru")),
                    DefectSeverity = int.Parse(e[14]),
                    Description = e[3],
                    Estimation = int.Parse(e[8]),
                    Key = e[0],
                    OriginalEstimation = int.Parse(e[7]),
                    //OwnerId = int.Parse(e[10]),
                    ProjectKey = e[1],
                    Remainder = int.Parse(e[9]),
                    StateId = int.Parse(e[6]),
                    UpdatedDateTime = DateTime.Parse(e[5])
                });

            foreach (var jiraTask in jiraTasks)
                db.JiraTasks.Add(jiraTask);
        }

    }
}
