using System;
using System.ComponentModel.DataAnnotations;

namespace FSO.SDD.DbModel
{
    /// <summary>
    /// Спринт
    /// </summary>
    public class JiraSprint
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
        /// Название команды
        /// </summary>
        public string TeamName { get; set; }

        /// <summary>
        /// Дата начала спринта
        /// </summary>
        public DateTime StartDateTime { get; set; }

        /// <summary>
        /// Дата окончания спринта
        /// </summary>
        public DateTime EndDateTime { get; set; }

        /// <summary>
        /// Ожидаемая ёмкость спринта в сторипоинтах
        /// </summary>
        public int SprintEstimatedValue { get; set; }

        /// <summary>
        /// Суммарная оценка ёмкости задач, котрые включены в спринт
        /// </summary>

        public int TasksEstimationValue { get; set; }

        public JiraSprintSate State { get; set; }

        public int StateId { get; set; }
    }
}
