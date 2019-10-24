using System.ComponentModel.DataAnnotations;

namespace FSO.SDD.DbModel
{
    /// <summary>
    /// история Спринта
    /// </summary>
    public class JiraSprintHistory
    {
        /// <summary>
        /// Ид 
        /// </summary>
        [Key]
        public int Id { get; set; }


        /// <summary>
        /// проектная область
        /// </summary>
        public int SptintId { get; set; }


        /// <summary>
        /// Суммарная оценка ёмкости задач, котрые включены в спринт
        /// </summary>

        public int TasksEstimationValue { get; set; }

        public JiraSprintSate State { get; set; }

        public int StateId { get; set; }
    }
}