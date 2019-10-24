using System;
using System.ComponentModel.DataAnnotations;

namespace FSO.SDD.DbModel
{
    /// <summary>
    /// Релиз
    /// </summary>
    public class JiraRelease
    {
        /// <summary>
        /// Ид 
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// КЭ
        /// </summary>
        public string ConfigurationItem { get; set; }

        public string Name { get; set; }

        public DateTime StartDateTime { get; set; }

        /// <summary>
        /// Запланированная дата установки в ПРОМ
        /// </summary>
        public DateTime PlannedFinishDateTime { get; set; }

        public DateTime ActualFinishDateTime { get; set; }

        /// <summary>
        /// Запланированная дата начала ИФТ
        /// </summary>
        public DateTime PlannedIFTDateTime { get; set; }

        public DateTime ActualIFTDateTime { get; set; }

        /// <summary>
        /// Запланированная дата начала ПСИ
        /// </summary>
        public DateTime PlannedATDateTime { get; set; }

        public DateTime ActualATDateTime { get; set; }

        public JiraReleaseState State { get; set; }

        public int StateId { get; set; }

        /// <summary>
        /// Суммарная оценка ёмкости задач, котрые включены в релиз
        /// </summary>

        public int TasksEstimationValue { get; set; }

        /// <summary>
        /// Остаточная оценка ёмкости задач, котрые включены в релиз
        /// </summary>

        public int TasksRemainderValue { get; set; }
    }
}