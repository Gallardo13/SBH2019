using FSO.SDD.DataBaseEfStore;
using FSO.SDD.DbModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ImportData
{
    public static class ParseExtension
    {
        public static DateTime ParseToDate(this string str) =>
            DateTime.ParseExact(str, "d-MMM", System.Globalization.CultureInfo.GetCultureInfo("EN-en"));

        public static int ParseToInt(this string str) => int.Parse(str);
    }

    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new StoreContext(@"..\..\..\..\FSO.SDD.DataBase.db;"))
            {
                ImportUsers(db);
                ImportEpics(db);
                ImportSprintStatus(db);
                ImportSprint(db);
                ImportReleaseStatus(db);
                ImportRelease(db);
                ImportJiraTaskState(db);
                ImportJiraTask(db);
                ImportJiraEpicTask(db);
                ImportJiraSprintTask(db);
                ImportJiraReleaseTask(db);
                db.SaveChanges();
            }
        }

        private static IEnumerable<string[]> ReadFile(string fileName) => File
                .ReadAllText(fileName)
                .Split(Environment.NewLine)
                .Skip(1)
                .Where(e => !string.IsNullOrEmpty(e))
                .Select(e => e.Split(","));

        private static void ImportUsers(StoreContext db)
        {
            var users = ReadFile(@"../../../../../Пользователи.csv")
                .Select(e => new User { Id = e[0].ParseToInt(), Login = e[1], Name = e[2], Email = e[3] });

            foreach (var user in users)
                db.Users.Add(user);
        }

        private static void ImportEpics(StoreContext db)
        {
            var els = ReadFile(@"../../../../../Эпики.csv")
                .Select(e => new JiraEpic {
                    Id = e[0].ParseToInt(),
                    ProjectKey = e[1],
                    Name = e[2],
                    TasksEstimationValue = e[6].ParseToInt()
                });

            foreach (var el in els)
                db.JiraEpics.Add(el);
        }

        private static void ImportSprintStatus(StoreContext db)
        {
            var els = ReadFile(@"../../../../../СпринтСтатус.csv")
                .Select(e => new JiraSprintSate
                {
                    Id = e[0].ParseToInt(),
                    Name = e[1]
                });

            foreach (var el in els)
                db.JiraSprintSatess.Add(el);
        }

        private static void ImportSprint(StoreContext db)
        {
            var els = ReadFile(@"../../../../../Спринты.csv")
                .Select(e => new JiraSprint
                {
                    Id = e[0].ParseToInt(),
                    StartDateTime = e[1].ParseToDate(),
                    EndDateTime = e[2].ParseToDate(),
                    ProjectKey = e[3],
                    TeamName = e[4],
                    SprintEstimatedValue = e[5].ParseToInt(),
                    TasksEstimationValue = e[6].ParseToInt(),
                    StateId = e[7].ParseToInt()
                });

            foreach (var el in els)
                db.JiraSprints.Add(el);
        }

        private static void ImportReleaseStatus(StoreContext db)
        {
            var els = ReadFile(@"../../../../../РелизСостояние.csv")
                .Select(e => new JiraReleaseState
                {
                    Id = e[0].ParseToInt(),
                    Name = e[1]
                });

            foreach (var el in els)
                db.JiraReleaseStates.Add(el);
        }

        private static void ImportRelease(StoreContext db)
        {
            var els = ReadFile(@"../../../../../Релизы.csv")
                .Select(e => new JiraRelease
                {
                    Id = e[0].ParseToInt(),
                    ConfigurationItem = e[1],
                    Name = e[2],
                    StateId = 1
                });

            foreach (var el in els)
                db.JiraReleases.Add(el);
        }

        private static void ImportJiraTaskState(StoreContext db)
        {
            var taskStates = ReadFile(@"../../../../../JiraTaskState.csv")
                .Select(e => new JiraTaskState { Id = e[0].ParseToInt(), Name = e[1] });

            foreach (var taskState in taskStates)
                db.JiraTaskStates.Add(taskState);
        }

        private static void ImportJiraTask(StoreContext db)
        {
            var fileName = new[] { @"../../../../../Задачи1.csv", @"../../../../../Задачи2.csv" };
            var obj = new object();

            Parallel.ForEach(fileName, (fn) =>
            {
                var jiraTasks = ReadFile(fn)
                    .Select(e => new JiraTask
                    {
                        Id = e[0].ParseToInt(),
                        AuthorId = e[2].ParseToInt(),
                        ConfigurationItem = e[11],
                        CreatedDateTime = e[4].ParseToDate(),
                        DefectSeverity = e[14].ParseToInt(),
                        Description = e[3],
                        Estimation = e[8].ParseToInt(),
                        Key = e[0],
                        OriginalEstimation = e[7].ParseToInt(),
                        OwnerId = e[10].ParseToInt(),
                        ProjectKey = e[1],
                        Remainder = e[9].ParseToInt(),
                        StateId = e[6].ParseToInt(),
                        UpdatedDateTime = e[5].ParseToDate()
                    });

                foreach (var jiraTask in jiraTasks)
                    lock(obj)
                        db.JiraTasks.Add(jiraTask);
            });
        }

        private static void ImportJiraEpicTask(StoreContext db)
        {
            var fileName = new[] { @"../../../../../Задачи1.csv", @"../../../../../Задачи2.csv" };
            var obj = new object();

            Parallel.ForEach(fileName, (fn) =>
            {
                var els = ReadFile(fn)
                    .OrderBy(e => e[18])
                    .Select(e => new JiraEpicTask
                    {
                        EpicId = e[18].ParseToInt(),
                        TaskId = e[0].ParseToInt()
                    });

                foreach (var el in els)
                    lock (obj)
                        db.JiraEpicTasks.Add(el);
            });
        }

        private static void ImportJiraSprintTask(StoreContext db)
        {
            var fileName = new[] { @"../../../../../Задачи1.csv", @"../../../../../Задачи2.csv" };
            var obj = new object();

            Parallel.ForEach(fileName, (fn) =>
            {
                var els = ReadFile(fn)
                    .OrderBy(e => e[15])
                    .Select(e => new JiraSprintTask
                    {
                        SprintId = e[15].ParseToInt(),
                        TaskId = e[0].ParseToInt()
                    });

                foreach (var el in els)
                    lock (obj)
                        db.JiraSprintTasks.Add(el);
            });
        }

        private static void ImportJiraReleaseTask(StoreContext db)
        {
            var fileName = new[] { @"../../../../../Задачи1.csv", @"../../../../../Задачи2.csv" };
            var obj = new object();

            Parallel.ForEach(fileName, (fn) =>
            {
                var els = ReadFile(fn)
                    .OrderBy(e => e[13])
                    .Select(e => new JiraReleaseTask
                    {
                        ReleaseId = e[13].ParseToInt(),
                        TaskId = e[0].ParseToInt()
                    });

                foreach (var el in els)
                    lock (obj)
                        db.JiraReleaseTasks.Add(el);
            });
        }

    }
}
