using System;
using System.Collections.Generic;
using System.Linq;
using FSO.SDD.DataBaseEfStore;
using FSO.SDD.DbModel;
using FSO.SDD.DbModel.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace FSO.SDD.MetricsCalculator
{
    public class MetricsLibrary
    {

        private static IEnumerable<JiraTaskHistory> GetJiraTaskHistorys(StoreContext db)
        {
            return db.JiraTaskHistorys.Include(h => h.Task);
        }

        private static IEnumerable<JiraTask> GetJiraTasks(StoreContext db)
        {
            return db.JiraTasks.Include(t=>t.Owner);
        }


        /// <summary>
        /// вычисление значений по метрике сгорания спринта
        /// </summary>
        public static void CalculateBurnDown()
        {
            // для каждого дня от создания первой задачи и до сегодня
            // для каждой задачи, у которой день в диапазоне от создания до последнего обновления
            // взять из истории актуальные на день оценки ёмкости
            // найти связанные спринт/релиз/эпик
            // для них вычислить общую ёмкость и длительность, оптимальное значение остатка ёмкости на день, реалььное значение остатка по всем задачам
            // вывести в индикатор посчитанные значения (серии), день (измерение), релиз/эпик/спринт/таска/юзер (ключи)

            var db = new StoreContext();

            var tasks = db.JiraTasks.OrderBy(t=>t.CreatedDateTime).ToList();

            var firstTask = tasks.FirstOrDefault();

            if(firstTask == null)
                return;

            var startDay = firstTask.CreatedDateTime.Date.AddDays(1).AddSeconds(-1); //последняя секунда дня

            var lastDay = DateTime.Now.Date.AddDays(1).AddSeconds(-1); //последняя секунда текущего дня

            var totalDays = (lastDay - startDay).Days;

            if(totalDays < 1)
                return;

            for (int i = 1; i <= totalDays; i++)
            {

                var currentDate = startDay.AddDays(i);


                var tasksHistory = GetJiraTaskHistorys(db)
                    .Where(h => h.Task.CreatedDateTime < currentDate 
                                && h.Task.UpdatedDateTime > currentDate 
                                && (h.StateId == (int) JiraTaskStates.Open || h.StateId == (int) JiraTaskStates.InWork))
                    .ToList();

                var currentTasksIds = tasksHistory.Select(h => h.TaskId).ToList();

                //TODO: and so on.... 
            }

        }
    }
}
