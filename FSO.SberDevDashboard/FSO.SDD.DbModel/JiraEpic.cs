using System;
using System.ComponentModel.DataAnnotations;

namespace FSO.SDD.DbModel
{
    /// <summary>
    /// Эпик
    /// </summary>
    public class JiraEpic
    {
        /// <summary>
        /// Ид 
        /// </summary>
        [Key]
        public int Id { get; set; }


        /// <summary>
        /// проектная область
        /// </summary>
        public string ProjectKey { get; set; }


        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Реальная дата вывода в пром (когда встал в пром релиз с последней задачей)
        /// </summary>
        public DateTime FinishDateTime { get; set; }

        /// <summary>
        /// Ожидаемая дата вывода в пром (то, что мы пообещали бизнесу)
        /// </summary>
        public DateTime ExpectationDateTime { get; set; }

        /// <summary>
        /// Крайня дата вывода в пром (позже совсем нельзя)
        /// </summary>
        public DateTime DeadlineDateTime { get; set; }


        /// <summary>
        /// Суммарная оценка ёмкости задач, котрые включены в эпик
        /// </summary>

        public int TasksEstimationValue { get; set; }

    }
}